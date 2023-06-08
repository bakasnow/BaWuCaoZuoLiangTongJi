using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaWuCaoZuoLiangTongJi
{
    public partial class SheZhiForm : Form
    {
        public SheZhiForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗口 创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SheZhiForm_Load(object sender, EventArgs e)
        {
            Text = "设置";

            SheZhiBiaoJieGou sheZhiBiao = SQLite.DB.Queryable<SheZhiBiaoJieGou>().First(sz => sz.SheZhiID == 1);

            textBox_ZuiGaoBingFaShu.Text = Convert.ToString(sheZhiBiao.ZuiGaoBingFaShu);
            textBox_HuoQuJianGe.Text = Convert.ToString(sheZhiBiao.HuoQuJianGe);
            comboBox_JieGuoPaiXu.SelectedIndex = sheZhiBiao.JieGuoPaiXu;
        }

        /// <summary>
        /// 工具栏按钮 保存 被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton_BaoCun_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_ZuiGaoBingFaShu.Text, out int zuiGaoBingFaShu) == false)
            {
                MessageBox.Show(text: "请填写最高并发数。", caption: "笨蛋雪：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                return;
            }

            if (zuiGaoBingFaShu <= 0)
            {
                MessageBox.Show(text: "最高并发数不得低于1。", caption: "笨蛋雪：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                textBox_ZuiGaoBingFaShu.Text = "1";
                return;
            }

            if (int.TryParse(textBox_HuoQuJianGe.Text, out int huoQuJianGe) == false)
            {
                MessageBox.Show(text: "请填写获取间隔。", caption: "笨蛋雪：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                return;
            }

            if (huoQuJianGe <= 0)
            {
                MessageBox.Show(text: "获取间隔不得低于1秒。", caption: "笨蛋雪：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                textBox_HuoQuJianGe.Text = "1";
                return;
            }

            int jieGuoPaiXu = comboBox_JieGuoPaiXu.SelectedIndex;
            if (jieGuoPaiXu < 0)
            {
                MessageBox.Show(text: "请选择至少一种结果排序方式。", caption: "笨蛋雪：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                comboBox_JieGuoPaiXu.SelectedIndex = 0;
                return;
            }

            int jieGuo = SQLite.DB.Updateable(new SheZhiBiaoJieGou
            {
                SheZhiID = 1,
                ZuiGaoBingFaShu = zuiGaoBingFaShu,
                HuoQuJianGe = huoQuJianGe,
                JieGuoPaiXu = jieGuoPaiXu
            }).ExecuteCommand();

            if (jieGuo <= 0)
            {
                MessageBox.Show(text: "设置保存失败，请联系作者。", caption: "笨蛋雪：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                return;
            }

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
    }
}
