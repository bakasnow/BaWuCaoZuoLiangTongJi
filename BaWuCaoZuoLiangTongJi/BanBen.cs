using BakaSnowTool;
using CsharpHttpHelper;
using CsharpHttpHelper.Enum;
using System.Diagnostics;
using System.Windows.Forms;

namespace BaWuCaoZuoLiangTongJi
{
    public class BanBen
    {
        /// <summary>
        /// 软件名称
        /// </summary>
        public const string Vname = "bawucaozuoliangtongji";

        /// <summary>
        /// 版本号
        /// </summary>
        public static readonly string Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// 版本验证
        /// </summary>
        /// <returns></returns>
        public static bool GetVersion()
        {
            string html, v;
            while (true)
            {
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = "http://www.bakasnow.com/version.php?n=" + Vname,//URL     必需项
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
                html = result.Html;
                v = BST.JieQuWenBen(html, "<version>", "</version>");

                if (string.IsNullOrEmpty(v))
                {
                    if (MessageBox.Show(text: "版本获取失败，可能是网络异常，点击\"取消\"跳过验证", caption: "笨蛋雪说：", buttons: MessageBoxButtons.RetryCancel, icon: MessageBoxIcon.Error) == DialogResult.Cancel)
                    {
                        return true;
                    }
                }
                else
                {
                    //获取版本号成功
                    break;
                }
            }

            if (v != Version)
            {
                string msg =
                    "发现新版本，请至群共享下载最新版\n" +
                    "当前版本：" + Version + "\n" +
                    "最新版本：" + v + "\n\n" +
                    "是否立即加群？";

                if (MessageBox.Show(text: msg, caption: "笨蛋雪说：", buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    string target = BST.JieQuWenBen(html, "<link>", "</link>");
                    if (!string.IsNullOrEmpty(target))
                    {
                        Process.Start(target);
                    }

                    return false;
                }
            }

            return true;
        }
    }
}
