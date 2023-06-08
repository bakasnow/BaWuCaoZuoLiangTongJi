using SqlSugar;
using System;
using System.IO;
using static TiebaApi.TiebaAppApi.TiebaYinJi;
using System.Windows.Forms;

namespace BaWuCaoZuoLiangTongJi
{
    public static class SQLite
    {
        public static SqlSugarClient DB = null;

        public static SqlSugarClient ZhangHaoDB = null;

        /// <summary>
        /// 创建SqlSugarClient 
        /// </summary>
        /// <returns></returns>
        public static void ChuangJianShuJuKu()
        {
            //创建数据库对象
            DB = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = $"DataSource={Path.Combine(Environment.CurrentDirectory, "BaWuCaoZuoLiangTongJi.sqlite")}",//连接符字串
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            });

            ZhangHaoDB = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = $"DataSource={Path.Combine(Environment.CurrentDirectory, "BaiduZhangHaoGuanLi.sqlite")}",//连接符字串
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            });
        }

        /// <summary>
        /// 创建表
        /// </summary>
        public static void ChuangJianBiao()
        {
            DB.CodeFirst.InitTables(typeof(SheZhiBiaoJieGou));
            if (DB.Queryable<SheZhiBiaoJieGou>().Count(zh => zh.SheZhiID == 1) <= 0)
            {
                int jieGuo = jieGuo = DB.Insertable(new SheZhiBiaoJieGou
                {
                    SheZhiID = 1,
                    ZuiGaoBingFaShu = 1,
                    HuoQuJianGe = 1,
                }).ExecuteCommand();

                if (jieGuo <= 0)
                {
                    MessageBox.Show(text: "设置初始化失败，请联系作者。", caption: "笨蛋雪：", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                }
            }

            ZhangHaoDB.CodeFirst.InitTables(typeof(BaiduZhangHaoBiaoJieGou));
            ZhangHaoDB.CodeFirst.InitTables(typeof(TiebaZhiWuBiaoJieGou));
        }
    }

    [SugarTable("SheZhiBiao")]
    public class SheZhiBiaoJieGou
    {
        [SugarColumn(IsPrimaryKey = true, ColumnDataType = "tinyint")]
        public int SheZhiID { get; set; }

        [SugarColumn(ColumnDataType = "tinyint", IsNullable = true)]
        public int ZuiGaoBingFaShu { get; set; }

        [SugarColumn(ColumnDataType = "tinyint", IsNullable = true)]
        public int HuoQuJianGe { get; set; }

        [SugarColumn(ColumnDataType = "tinyint", IsNullable = true)]
        public int JieGuoPaiXu { get; set; }
    }

    [SugarTable("BaiduZhangHaoBiao")]
    public class BaiduZhangHaoBiaoJieGou
    {
        [SugarColumn(IsPrimaryKey = true, ColumnDataType = "text")]
        public string TouXiangID { get; set; }

        [SugarColumn(ColumnDataType = "tinyint", IsNullable = true)]
        public bool IsQiYong { get; set; }

        [SugarColumn(ColumnDataType = "integer", IsNullable = true)]
        public long Uid { get; set; }

        [SugarColumn(ColumnDataType = "text", IsNullable = true)]
        public string YongHuMing { get; set; }

        [SugarColumn(ColumnDataType = "datetime", IsNullable = true)]
        public DateTime ZuiHouDiaoYongShiJian { get; set; }

        [SugarColumn(ColumnDataType = "text", IsNullable = true)]
        public string Cookie { get; set; }
    }

    [SugarTable("TiebaZhiWuBiao")]
    public class TiebaZhiWuBiaoJieGou
    {
        [SugarColumn(ColumnDataType = "integer", IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        [SugarColumn(ColumnDataType = "tinyint", IsNullable = true)]
        public bool IsQiYong { get; set; }

        [SugarColumn(ColumnDataType = "text")]
        public string TouXiangID { get; set; }

        [SugarColumn(ColumnDataType = "text", IsNullable = true)]
        public string YongHuMing { get; set; }

        [SugarColumn(ColumnDataType = "text")]
        public string TiebaName { get; set; }

        [SugarColumn(ColumnDataType = "text")]
        public string ZhiWu { get; set; }
    }
}
