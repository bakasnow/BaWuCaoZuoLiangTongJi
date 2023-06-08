using BaWuCaoZuoLiangTongJi.CanShu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiebaApi.TiebaWebApi;

namespace BaWuCaoZuoLiangTongJi
{
    public partial class ChaXunForm : Form
    {
        public ChaXunForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 是否保存
        /// </summary>
        public bool IsBaoCun { get; private set; }

        /// <summary>
        /// 查询参数
        /// </summary>
        public ChaXunCanShu CanShu { get; private set; }

        /// <summary>
        /// 窗口 创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChaXunForm_Load(object sender, EventArgs e)
        {
            Text = "查询参数";

            DateTime nowDateTime = DateTime.Now;
            DateTime kaiShiRiQi = nowDateTime.AddDays(1 - nowDateTime.Day);
            DateTime jieShuRiQi = kaiShiRiQi.AddMonths(1).AddDays(-1);
            dateTimePicker_KaiShiRiQi.Value = kaiShiRiQi;
            dateTimePicker_JieShuRiQi.Value = jieShuRiQi;

            List<BaiduZhangHaoBiaoJieGou> baiduZhangHaoLieBiao = SQLite.ZhangHaoDB.Queryable<BaiduZhangHaoBiaoJieGou>()
                .Where(zh => zh.IsQiYong == true).ToList();
            foreach (var baiduZhangHao in baiduZhangHaoLieBiao)
            {
                comboBox_YongHuMing.Items.Add(baiduZhangHao.YongHuMing);
            }

            if (baiduZhangHaoLieBiao.Count > 0)
            {
                comboBox_YongHuMing.SelectedIndex = 0;
            }

            IsBaoCun = false;
        }

        /// <summary>
        /// 工具栏按钮 查询 被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton_ChaXun_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox_YongHuMing.Text))
            {
                MessageBox.Show(text: $"请选择贴吧账号，若没有贴吧账号请先退回主界面登录。", caption: "笨蛋雪说：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Asterisk);
                return;
            }

            if (string.IsNullOrEmpty(comboBox_TiebaName.Text))
            {
                MessageBox.Show(text: $"请选择{comboBox_YongHuMing.Text}任职的贴吧，若没有贴吧请先退回主界面添加。", caption: "笨蛋雪说：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Asterisk);
                return;
            }

            if (DateTime.Compare(dateTimePicker_KaiShiRiQi.Value, dateTimePicker_JieShuRiQi.Value) > 0)
            {
                MessageBox.Show(text: $"截止日期不能大于起始日期。", caption: "笨蛋雪说：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Asterisk);
                return;
            }

            if (checkBox_BaZhu.Checked == false && checkBox_XiaoBaZhu.Checked == false && checkBox_YuYinXiaoBian.Checked == false && checkBox_QiTaZhiWu.Checked == false)
            {
                MessageBox.Show(text: $"请至少选择一个职务类型。", caption: "笨蛋雪说：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Asterisk);
                return;
            }

            if (checkBox_QuanBuCaoZuo.Checked == false && checkBox_ShanTie.Checked == false && checkBox_PingBi.Checked == false)
            {
                MessageBox.Show(text: $"请至少选择一个操作类型。", caption: "笨蛋雪说：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Asterisk);
                return;
            }

            string cookie = SQLite.ZhangHaoDB.Queryable<BaiduZhangHaoBiaoJieGou>().First(zh => zh.YongHuMing == comboBox_YongHuMing.Text).Cookie;
            if (TiebaWeb.GetBaiduZhangHaoIsZaiXian(cookie) == false)
            {
                if (MessageBox.Show(text: $"百度账号[{comboBox_YongHuMing.Text}]可能掉线了。\r\n\r\n是否继续使用？", caption: "笨蛋雪说：", buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Asterisk) == DialogResult.No)
                {
                    //用户取消操作
                    return;
                }
            }

            SheZhiBiaoJieGou sheZhiBiao = SQLite.DB.Queryable<SheZhiBiaoJieGou>().First(sz => sz.SheZhiID == 1);

            CanShu = new ChaXunCanShu()
            {
                //设置参数
                ZuiGaoBingFaShu = sheZhiBiao.ZuiGaoBingFaShu,
                HuoQuJianGe = sheZhiBiao.HuoQuJianGe,
                JieGuoPaiXu = sheZhiBiao.JieGuoPaiXu,
                //查询参数
                Cookie = cookie,
                YongHuMing = comboBox_YongHuMing.Text,
                TiebaName = comboBox_TiebaName.Text,
                KaiShiRiQi = dateTimePicker_KaiShiRiQi.Value,
                JieShuRiQi = dateTimePicker_JieShuRiQi.Value,
                IsBaZhu = checkBox_BaZhu.Checked,
                IsXiaoBaZhu = checkBox_XiaoBaZhu.Checked,
                IsYuYinXiaoBian = checkBox_YuYinXiaoBian.Checked,
                IsQiTaZhiWu = checkBox_QiTaZhiWu.Checked,
                IsQuanBuCaoZuo = checkBox_QuanBuCaoZuo.Checked,
                IsShanTie = checkBox_ShanTie.Checked,
                IsPingBi = checkBox_PingBi.Checked
            };

            IsBaoCun = true;
            Close();
        }

        /// <summary>
        /// 工具栏按钮 退出 被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton_TuiChu_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 组合框 用户名 所选索引被更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_YongHuMing_SelectedIndexChanged(object sender, EventArgs e)
        {
            string yongHuMing = comboBox_YongHuMing.Text;
            if (string.IsNullOrEmpty(yongHuMing))
            {
                return;
            }

            List<TiebaZhiWuBiaoJieGou> tiebaZhiWuLieBiao = SQLite.ZhangHaoDB.Queryable<TiebaZhiWuBiaoJieGou>()
                .Where(zw => zw.YongHuMing == yongHuMing && zw.IsQiYong == true).ToList();
            foreach (var tiebaZhiWu in tiebaZhiWuLieBiao)
            {
                comboBox_TiebaName.Items.Add(tiebaZhiWu.TiebaName);
            }

            if (tiebaZhiWuLieBiao.Count > 0)
            {
                comboBox_TiebaName.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 组合框 全部操作 选择被更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_QuanBuCaoZuo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_QuanBuCaoZuo.Checked)
            {
                checkBox_ShanTie.Enabled = false;
                checkBox_PingBi.Enabled = false;
            }
            else
            {
                checkBox_ShanTie.Enabled = true;
                checkBox_PingBi.Enabled = true;
            }
        }

        /// <summary>
        /// 链接标签 上个月 被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkLabel_ShangGeYue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DateTime jinTian = DateTime.Today;
            DateTime kaiShiRiQi = new DateTime(jinTian.Year, jinTian.Month, 1).AddMonths(-1);
            DateTime jieShuRiQi = kaiShiRiQi.AddMonths(1).AddDays(-1);
            dateTimePicker_KaiShiRiQi.Value = kaiShiRiQi;
            dateTimePicker_JieShuRiQi.Value = jieShuRiQi;
        }

        /// <summary>
        /// 链接标签 本月 被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkLabel_BenYue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DateTime nowDateTime = DateTime.Now;
            DateTime kaiShiRiQi = nowDateTime.AddDays(1 - nowDateTime.Day);
            DateTime jieShuRiQi = kaiShiRiQi.AddMonths(1).AddDays(-1);
            dateTimePicker_KaiShiRiQi.Value = kaiShiRiQi;
            dateTimePicker_JieShuRiQi.Value = jieShuRiQi;
        }
    }
}
