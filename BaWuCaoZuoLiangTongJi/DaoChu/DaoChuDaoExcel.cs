using OfficeOpenXml;
using OfficeOpenXml.LoadFunctions.Params;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaWuCaoZuoLiangTongJi.DaoChu
{
    /// <summary>
    /// 导出到Excel
    /// </summary>
    public class DaoChuDaoExcel
    {
        /// <summary>
        /// 列表框
        /// </summary>
        public ListView listView;

        /// <summary>
        /// 文件路径
        /// </summary>
        public string WenJianLuJing;

        /// <summary>
        /// 工作表名
        /// </summary>
        public string GongZuoBiaoMing;

        /// <summary>
        /// 导出
        /// </summary>
        public void DaoChu()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(WenJianLuJing)))
            {
                //相同名称的工作表是否存在
                bool isCunZai = false;
                foreach (ExcelWorksheet Worksheet in package.Workbook.Worksheets)
                {
                    if (Worksheet.Name == GongZuoBiaoMing)
                    {
                        isCunZai = true;
                        break;
                    }
                }

                ExcelWorksheet worksheet;
                if (isCunZai)
                {
                    //选择工作表
                    worksheet = package.Workbook.Worksheets[GongZuoBiaoMing];
                    worksheet.ClearFormulas();
                }
                else
                {
                    //新建工作表
                    worksheet = package.Workbook.Worksheets.Add(GongZuoBiaoMing);
                }

                worksheet.Cells["A1"].LoadFromCollection(ListView2List(), c =>
                {
                    c.PrintHeaders = true;
                    c.HeaderParsingType = HeaderParsingTypes.CamelCaseToSpace;
                });
                package.Save();
            }
        }

        /// <summary>
        /// 列表框转列表
        /// </summary>
        /// <returns></returns>
        private List<JieGou> ListView2List()
        {
            List<JieGou> lieBiao = new List<JieGou>();

            for (int i = 0; i < listView.Items.Count; i++)
            {
                int.TryParse(listView.Items[i].SubItems[3].Text, out int caoZuoLiang);

                lieBiao.Add(new JieGou
                {
                    TiebaName = listView.Items[i].SubItems[0].Text,
                    YongHuMing = listView.Items[i].SubItems[1].Text,
                    ZhiWu = listView.Items[i].SubItems[2].Text,
                    CaoZuoLiang = caoZuoLiang
                });
            }

            return lieBiao;
        }
    }

    /// <summary>
    /// 结构
    /// </summary>
    public class JieGou
    {
        [DisplayName("贴吧名")]
        public string TiebaName { get; set; }

        [DisplayName("用户名")]
        public string YongHuMing { get; set; }

        [DisplayName("职务")]
        public string ZhiWu { get; set; }

        [DisplayName("操作量")]
        public int CaoZuoLiang { get; set; }
    }
}
