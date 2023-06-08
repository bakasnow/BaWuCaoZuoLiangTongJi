using BaWuCaoZuoLiangTongJi.CanShu;
using BaWuCaoZuoLiangTongJi.LeiXing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiebaApi.TiebaAppApi;
using TiebaApi.TiebaBaWuApi;
using TiebaApi.TiebaCanShu;
using TiebaApi.TiebaJieGou;
using TiebaApi.TiebaLeiXing;
using TiebaApi.TiebaWebApi;

namespace BaWuCaoZuoLiangTongJi
{
    public static class ChaXunXianChengGuanLi
    {
        /// <summary>
        /// 线程是否停止
        /// </summary>
        public static bool IsTingZhi;

        /// <summary>
        /// 列表
        /// </summary>
        public static ListView listView1;

        /// <summary>
        /// 工具栏
        /// </summary>
        public static ToolStrip toolStrip1;

        /// <summary>
        /// 工具栏 停止
        /// </summary>
        public static ToolStripButton toolStripButton_TingZhi;

        /// <summary>
        /// 查询线程
        /// </summary>
        private static Thread ChaXunXianCheng = null;

        /// <summary>
        /// 启动线程
        /// </summary>
        /// <param name="canShu"></param>
        public static void KaiShi(ChaXunCanShu canShu)
        {
            IsTingZhi = false;
            ChaXunXianCheng = new Thread(() => HouTaiRenWu(canShu))
            {
                IsBackground = true
            };
            ChaXunXianCheng.Start();
        }

        /// <summary>
        /// 停止线程
        /// </summary>
        public static void TingZhi()
        {
            IsTingZhi = true;
        }

        /// <summary>
        /// 后台任务
        /// </summary>
        /// <param name="canShu"></param>
        private static void HouTaiRenWu(ChaXunCanShu canShu)
        {
            TiebaBaWuTuanDui tiebaBaWuTuanDui = new TiebaBaWuTuanDui(canShu.TiebaName);
            List<TiebaBaWuTuanDuiJieGou> tiebaBaWuTuanDuiLieBiao = tiebaBaWuTuanDui.Get();

            if (IsTingZhi) goto XianChengJieShuBiaoJi;

            if (canShu.IsBaZhu == false)
            {
                tiebaBaWuTuanDuiLieBiao.RemoveAll(bw => bw.ZhiWu == TiebaBaWuLeiXing.BaZhu);
            }

            if (canShu.IsXiaoBaZhu == false)
            {
                tiebaBaWuTuanDuiLieBiao.RemoveAll(bw => bw.ZhiWu == TiebaBaWuLeiXing.XiaoBaZhu);
            }

            if (canShu.IsYuYinXiaoBian == false)
            {
                tiebaBaWuTuanDuiLieBiao.RemoveAll(bw => bw.ZhiWu == TiebaBaWuLeiXing.YuYinXiaoBian);
            }

            if (canShu.IsQiTaZhiWu == false)
            {
                tiebaBaWuTuanDuiLieBiao.RemoveAll(bw => (int)bw.ZhiWu < 8);
            }

            if (IsTingZhi) goto XianChengJieShuBiaoJi;

            //将吧务列表写到控件中
            listView1.BeginUpdate();
            foreach (var baWuTuanDui in tiebaBaWuTuanDuiLieBiao)
            {
                if (IsTingZhi) break;

                ListViewItem listViewItem = new ListViewItem
                {
                    Text = canShu.TiebaName
                };
                listViewItem.SubItems.Add(baWuTuanDui.YongHuMing);
                listViewItem.SubItems.Add(TiebaLeiXingZhuanHuan.TiebaBaWuLeiXingZhuanWenBen(baWuTuanDui.ZhiWu));
                listViewItem.SubItems.Add("等待中..");
                listView1.Items.Add(listViewItem);
            }
            listView1.EndUpdate();

            if (IsTingZhi) goto XianChengJieShuBiaoJi;


            //多线程
            object lockObj = new object();
            int dangQianSuoYin = 0;
            Thread[] xianChengLieBiao = new Thread[canShu.ZuiGaoBingFaShu];
            for (int xianChengSuoYin = 0; xianChengSuoYin < canShu.ZuiGaoBingFaShu; xianChengSuoYin++)
            {
                //任务停止，跳出循环
                if (IsTingZhi) break;

                xianChengLieBiao[xianChengSuoYin] = new Thread(() =>
                {
                    while (true)
                    {
                        int suoYin;
                        lock (lockObj)
                        {
                            //如果当前索引 大于等于 任务总数
                            if (dangQianSuoYin >= tiebaBaWuTuanDuiLieBiao.Count)
                            {
                                suoYin = -1;
                            }
                            else
                            {
                                suoYin = dangQianSuoYin;
                                dangQianSuoYin++;
                            }
                        }

                        //没有任务了，退出
                        if (suoYin == -1) break;

                        //任务停止，跳出循环
                        if (IsTingZhi) break;

                        //取操作量
                        listView1.Items[suoYin].SubItems[3].Text = "获取中..";

                        string msg = string.Empty;
                        int caoZuoLiang = 0;
                        if (canShu.IsQuanBuCaoZuo)
                        {
                            caoZuoLiang = TiebaBaWu.GetCaoZuoLiang(new GetTieZiGuanLiRiZhiCanShu
                            {
                                Cookie = canShu.Cookie,
                                TiebaName = canShu.TiebaName,
                                YongHuMing = tiebaBaWuTuanDuiLieBiao[suoYin].YongHuMing,
                                KaiShiRiQi = canShu.KaiShiRiQi,
                                JieShuRiQi = canShu.JieShuRiQi,
                                CaoZuoLeiXing = TieZiGuanLiRiZhiCaoZuoLeiXing.QuanBuCaoZuo,
                                Pn = 1
                            }, out msg);

                            listView1.Items[suoYin].SubItems[3].Text = caoZuoLiang >= 0 ? Convert.ToString(caoZuoLiang) : msg;
                        }
                        else
                        {
                            if (canShu.IsShanTie)
                            {
                                caoZuoLiang += TiebaBaWu.GetCaoZuoLiang(new GetTieZiGuanLiRiZhiCanShu
                                {
                                    Cookie = canShu.Cookie,
                                    TiebaName = canShu.TiebaName,
                                    YongHuMing = tiebaBaWuTuanDuiLieBiao[suoYin].YongHuMing,
                                    KaiShiRiQi = canShu.KaiShiRiQi,
                                    JieShuRiQi = canShu.JieShuRiQi,
                                    CaoZuoLeiXing = TieZiGuanLiRiZhiCaoZuoLeiXing.ShanTie,
                                    Pn = 1
                                }, out msg);
                            }

                            if (canShu.IsPingBi)
                            {
                                caoZuoLiang += TiebaBaWu.GetCaoZuoLiang(new GetTieZiGuanLiRiZhiCanShu
                                {
                                    Cookie = canShu.Cookie,
                                    TiebaName = canShu.TiebaName,
                                    YongHuMing = tiebaBaWuTuanDuiLieBiao[suoYin].YongHuMing,
                                    KaiShiRiQi = canShu.KaiShiRiQi,
                                    JieShuRiQi = canShu.JieShuRiQi,
                                    CaoZuoLeiXing = TieZiGuanLiRiZhiCaoZuoLeiXing.PingBi,
                                    Pn = 1
                                }, out msg);
                            }

                            listView1.Items[suoYin].SubItems[3].Text = caoZuoLiang >= 0 ? Convert.ToString(caoZuoLiang) : msg;
                        }

                        //任务停止，跳出循环
                        if (IsTingZhi) break;
                        Thread.Sleep(canShu.HuoQuJianGe * 1000);
                    }
                });
                xianChengLieBiao[xianChengSuoYin].Start();
            }

            //等待线程全部结束
            for (int xianChengSuoYin = 0; xianChengSuoYin < canShu.ZuiGaoBingFaShu; xianChengSuoYin++)
            {
                xianChengLieBiao[xianChengSuoYin].Join();
            }

            //排序
            if (canShu.JieGuoPaiXu == (int)JieGuoPaiXuLeiXing.ZhiWuPaiXu)
            {

            }
            else if (canShu.JieGuoPaiXu == (int)JieGuoPaiXuLeiXing.CaoZuoLiangDuoDeQianZhi)
            {
                listView1.ListViewItemSorter = new ListViewItemComparer(3, SortOrder.Descending);
                listView1.Sort();
            }
            else if (canShu.JieGuoPaiXu == (int)JieGuoPaiXuLeiXing.CaoZuoLiangShaoDeQianZhi)
            {
                listView1.ListViewItemSorter = new ListViewItemComparer(3, SortOrder.Ascending);
                listView1.Sort();
            }

        XianChengJieShuBiaoJi:
            toolStripButton_TingZhi.Enabled = false;
            toolStrip1.Items.OfType<ToolStripButton>().Where(tsb => tsb.Text != "停止").ToList().ForEach(button => button.Enabled = true);
        }
    }
}
