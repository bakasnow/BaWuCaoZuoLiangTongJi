using System;

namespace BaWuCaoZuoLiangTongJi.CanShu
{
    /// <summary>
    /// 查询参数
    /// </summary>
    public class ChaXunCanShu : SheZhiBiaoJieGou
    {
        /// <summary>
        /// Cookie
        /// </summary>
        public string Cookie;

        /// <summary>
        /// 用户名
        /// </summary>
        public string YongHuMing;

        /// <summary>
        /// 贴吧名
        /// </summary>
        public string TiebaName;

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime KaiShiRiQi;

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime JieShuRiQi;

        /// <summary>
        /// 是否吧主
        /// </summary>
        public bool IsBaZhu;

        /// <summary>
        /// 是否小吧主
        /// </summary>
        public bool IsXiaoBaZhu;

        /// <summary>
        /// 是否语音小编
        /// </summary>
        public bool IsYuYinXiaoBian;

        /// <summary>
        /// 是否其他职务
        /// </summary>
        public bool IsQiTaZhiWu;

        /// <summary>
        /// 是否全部操作
        /// </summary>
        public bool IsQuanBuCaoZuo;

        /// <summary>
        /// 是否删帖
        /// </summary>
        public bool IsShanTie;

        /// <summary>
        /// 是否屏蔽
        /// </summary>
        public bool IsPingBi;
    }
}
