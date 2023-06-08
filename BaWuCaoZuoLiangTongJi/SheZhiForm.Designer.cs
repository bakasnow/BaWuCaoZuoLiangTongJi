namespace BaWuCaoZuoLiangTongJi
{
    partial class SheZhiForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SheZhiForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_ZuiGaoBingFaShu = new System.Windows.Forms.TextBox();
            this.textBox_HuoQuJianGe = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_BaoCun = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_TuiChu = new System.Windows.Forms.ToolStripButton();
            this.comboBox_JieGuoPaiXu = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "最高并发数";
            // 
            // textBox_ZuiGaoBingFaShu
            // 
            this.textBox_ZuiGaoBingFaShu.Location = new System.Drawing.Point(91, 70);
            this.textBox_ZuiGaoBingFaShu.Name = "textBox_ZuiGaoBingFaShu";
            this.textBox_ZuiGaoBingFaShu.Size = new System.Drawing.Size(30, 21);
            this.textBox_ZuiGaoBingFaShu.TabIndex = 2;
            this.textBox_ZuiGaoBingFaShu.Text = "1";
            this.textBox_ZuiGaoBingFaShu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_HuoQuJianGe
            // 
            this.textBox_HuoQuJianGe.Location = new System.Drawing.Point(79, 100);
            this.textBox_HuoQuJianGe.Name = "textBox_HuoQuJianGe";
            this.textBox_HuoQuJianGe.Size = new System.Drawing.Size(30, 21);
            this.textBox_HuoQuJianGe.TabIndex = 4;
            this.textBox_HuoQuJianGe.Text = "1";
            this.textBox_HuoQuJianGe.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "获取间隔";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(115, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "秒";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_BaoCun,
            this.toolStripButton_TuiChu});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(214, 56);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_BaoCun
            // 
            this.toolStripButton_BaoCun.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_BaoCun.Image")));
            this.toolStripButton_BaoCun.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_BaoCun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_BaoCun.Name = "toolStripButton_BaoCun";
            this.toolStripButton_BaoCun.Size = new System.Drawing.Size(36, 53);
            this.toolStripButton_BaoCun.Text = "保存";
            this.toolStripButton_BaoCun.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripButton_BaoCun.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton_BaoCun.Click += new System.EventHandler(this.ToolStripButton_BaoCun_Click);
            // 
            // toolStripButton_TuiChu
            // 
            this.toolStripButton_TuiChu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_TuiChu.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_TuiChu.Image")));
            this.toolStripButton_TuiChu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_TuiChu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_TuiChu.Margin = new System.Windows.Forms.Padding(0, 1, 8, 2);
            this.toolStripButton_TuiChu.Name = "toolStripButton_TuiChu";
            this.toolStripButton_TuiChu.Size = new System.Drawing.Size(36, 53);
            this.toolStripButton_TuiChu.Text = "退出";
            this.toolStripButton_TuiChu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton_TuiChu.Click += new System.EventHandler(this.ToolStripButton_TuiChu_Click);
            // 
            // comboBox_JieGuoPaiXu
            // 
            this.comboBox_JieGuoPaiXu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_JieGuoPaiXu.FormattingEnabled = true;
            this.comboBox_JieGuoPaiXu.Items.AddRange(new object[] {
            "职务排序",
            "操作量多的前置",
            "操作量少的前置"});
            this.comboBox_JieGuoPaiXu.Location = new System.Drawing.Point(79, 130);
            this.comboBox_JieGuoPaiXu.Name = "comboBox_JieGuoPaiXu";
            this.comboBox_JieGuoPaiXu.Size = new System.Drawing.Size(110, 20);
            this.comboBox_JieGuoPaiXu.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "结果排序";
            // 
            // SheZhiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(214, 191);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox_JieGuoPaiXu);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_HuoQuJianGe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_ZuiGaoBingFaShu);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(230, 230);
            this.MinimumSize = new System.Drawing.Size(230, 230);
            this.Name = "SheZhiForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SheZhiForm";
            this.Load += new System.EventHandler(this.SheZhiForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_ZuiGaoBingFaShu;
        private System.Windows.Forms.TextBox textBox_HuoQuJianGe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_BaoCun;
        private System.Windows.Forms.ToolStripButton toolStripButton_TuiChu;
        private System.Windows.Forms.ComboBox comboBox_JieGuoPaiXu;
        private System.Windows.Forms.Label label4;
    }
}