using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaWuCaoZuoLiangTongJi.LeiXing
{
    /// <summary>
    /// 结果排序类型
    /// </summary>
    public enum JieGuoPaiXuLeiXing
    {
        /// <summary>
        /// 职务排序
        /// </summary>
        ZhiWuPaiXu = 0,

        /// <summary>
        /// 操作量多的前置
        /// </summary>
        CaoZuoLiangDuoDeQianZhi = 1,

        /// <summary>
        /// 操作量少的前置
        /// </summary>
        CaoZuoLiangShaoDeQianZhi = 2
    }
}
