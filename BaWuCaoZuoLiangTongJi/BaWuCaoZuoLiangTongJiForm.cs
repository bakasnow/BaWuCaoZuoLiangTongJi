using BakaSnowTool;
using CsharpHttpHelper.Enum;
using CsharpHttpHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using BaWuCaoZuoLiangTongJi.DaoChu;
using BaWuCaoZuoLiangTongJi.CanShu;

namespace BaWuCaoZuoLiangTongJi
{
    public partial class BaWuCaoZuoLiangTongJiForm : Form
    {
        public BaWuCaoZuoLiangTongJiForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 上次查询参数
        /// </summary>
        private static ChaXunCanShu ShangCiChaXunCanShu = new ChaXunCanShu();

        /// <summary>
        /// 窗口 创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaWuCaoZuoLiangTongJiForm_Load(object sender, EventArgs e)
        {
            //版本验证
            if (!BanBen.GetVersion())
            {
                Dispose();
            }

            Text = $"吧务操作量统计 v{BanBen.Version}";

            //创建数据库
            SQLite.ChuangJianShuJuKu();

            //创建表
            SQLite.ChuangJianBiao();

            //跨线程控件
            ChaXunXianChengGuanLi.listView1 = listView1;
            ChaXunXianChengGuanLi.toolStrip1 = toolStrip1;
            ChaXunXianChengGuanLi.toolStripButton_TingZhi = toolStripButton_TingZhi;
        }

        /// <summary>
        /// 工具栏按钮 账号 被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton_ZhangHao_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "百度账号管理.exe");
            if (File.Exists(path) == false)
            {
                MessageBox.Show(text: "百度账号管理器丢失，请联系作者。", caption: "笨蛋雪说：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                return;
            }

            using (Process process = new Process())
            {
                process.StartInfo.FileName = path;
                process.StartInfo.Arguments = "-zhanghao";
                process.Start();
            }
        }

        /// <summary>
        /// 工具栏按钮 吧务 被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton_BaWu_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "百度账号管理.exe");
            if (File.Exists(path) == false)
            {
                MessageBox.Show(text: "百度账号管理器丢失，请联系作者。", caption: "笨蛋雪说：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                return;
            }

            using (Process process = new Process())
            {
                process.StartInfo.FileName = path;
                process.StartInfo.Arguments = "-bawu";
                process.Start();
            }
        }

        /// <summary>
        /// 工具栏按钮 设置 被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton_SheZhi_Click(object sender, EventArgs e)
        {
            SheZhiForm sheZhiForm = new SheZhiForm();
            sheZhiForm.ShowDialog();
        }

        /// <summary>
        /// 工具栏按钮 开始 被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton_KaiShi_Click(object sender, EventArgs e)
        {
            //禁用所有控件
            toolStrip1.Items.OfType<ToolStripButton>().ToList().ForEach(button => button.Enabled = false);

            ChaXunForm chaXunForm = new ChaXunForm();
            chaXunForm.ShowDialog();
            if (chaXunForm.IsBaoCun == false)
            {
                //如果保存失败，恢复除了“停止”以外的所有控件
                toolStrip1.Items.OfType<ToolStripButton>().Where(tsb => tsb.Text != "停止").ToList().ForEach(button => button.Enabled = true);
                return;
            }

            if (listView1.Items.Count > 0)
            {
                if (MessageBox.Show(text: "开始后将清空之前的查询记录。\r\n\r\n是否继续？", caption: "笨蛋雪说：", buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Asterisk) == DialogResult.No)
                {
                    //用户取消操作，恢复除了“停止”以外的所有控件
                    toolStrip1.Items.OfType<ToolStripButton>().Where(tsb => tsb.Text != "停止").ToList().ForEach(button => button.Enabled = true);
                    return;
                }
            }

            listView1.Items.Clear();

            ShangCiChaXunCanShu = chaXunForm.CanShu;

            //启动线程
            ChaXunXianChengGuanLi.KaiShi(chaXunForm.CanShu);

            //恢复“停止”控件
            toolStripButton_TingZhi.Enabled = true;
        }

        /// <summary>
        /// 工具栏按钮 停止 被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton_TingZhi_Click(object sender, EventArgs e)
        {
            //禁用“停止”控件
            toolStripButton_TingZhi.Enabled = false;

            //停止线程
            ChaXunXianChengGuanLi.TingZhi();
        }

        /// <summary>
        /// 工具栏按钮 导出 被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton_DaoChu_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count <= 0)
            {
                MessageBox.Show(text: "没有可以导出的数据。", caption: "笨蛋雪说：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Asterisk);
                return;
            }

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "保存Excel文件";
                saveFileDialog.Filter = "Excel文件 (*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.FileName = $"{ShangCiChaXunCanShu.TiebaName}吧 操作量统计 {DateTime.Now:yyyyMMddHHmmss}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DaoChuDaoExcel daoChuDaoExcel = new DaoChuDaoExcel()
                    {
                        listView = listView1,
                        WenJianLuJing = saveFileDialog.FileName,
                        GongZuoBiaoMing = $"{ShangCiChaXunCanShu.KaiShiRiQi:yyyyMMdd}-{ShangCiChaXunCanShu.JieShuRiQi:yyyyMMdd}"
                    };

                    daoChuDaoExcel.DaoChu();

                    if (File.Exists(saveFileDialog.FileName))
                    {
                        if (MessageBox.Show(text: "Excel导出成功，是否立即打开文件？", caption: "笨蛋雪说：", buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            Process.Start(saveFileDialog.FileName);
                        }
                    }
                    else
                    {
                        MessageBox.Show(text: "Excel导出失败。", caption: "笨蛋雪说：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                    }
                }
            }

        }

        /// <summary>
        /// 工具栏按钮 关于 被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton_GuanYu_Click(object sender, EventArgs e)
        {
            string text = "" +
                $"名称：吧务操作量统计\r\n" +
                $"版本：{BanBen.Version}\r\n" +
                $"作者：雪\r\n\r\n" +
                $"声明：本软件永久免费\r\n\r\n" +
                $"是否加入贴吧管理器交流群？";

            if (MessageBox.Show(text: text, caption: "笨蛋雪说：", buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Information) == DialogResult.Yes)
            {
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = $"http://www.bakasnow.com/version.php?n={BanBen.Vname}",//URL     必需项
                    Method = "GET",//URL     可选项 默认为Get
                    Timeout = 100000,//连接超时时间     可选项默认为100000
                    ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                    Cookie = string.Empty,//字符串Cookie     可选项
                    UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0",//用户的浏览器类型，版本，操作系统     可选项有默认值
                    Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                    ContentType = "text/html",//返回类型    可选项有默认值
                    Referer = "http://www.bakasnow.com/",//来源URL     可选项
                    Allowautoredirect = false,//是否根据３０１跳转     可选项
                    AutoRedirectCookie = false,//是否自动处理Cookie     可选项
                    Postdata = string.Empty,//Post数据     可选项GET时不需要写
                    ResultType = ResultType.String,//返回数据类型，是Byte还是String
                };

                HttpResult result = http.GetHtml(item);
                string target = BST.JieQuWenBen(result.Html, "<link>", "</link>");
                if (!string.IsNullOrEmpty(target))
                {
                    Process.Start(target);
                }
            }
        }
    }
}
