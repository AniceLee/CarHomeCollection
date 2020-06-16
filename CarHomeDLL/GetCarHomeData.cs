using HttpCodeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Drawing;

namespace CarHomeDLL
{
    /// <summary>
    /// 获取基本数据的类
    /// </summary>
    public static class GetCarHomeData
    {
        static HttpHelpers helper = new HttpHelpers();//请求执行对象

        static HttpItems items;//请求参数对象

        static HttpResults hr = new HttpResults();//请求结果对象

        static string StrCookie = "";//设置初始Cookie值

        /// <summary>

        /// 执行HttpCodeCreate

        /// </summary>

        public static string GetCarData()
        {

            string res = string.Empty;//请求结果,请求类型不是图片时有效

            string url = "http://car.autohome.com.cn/javascript/NewSpecCompare.js?20131010";//请求地址

            items = new HttpItems();//每次重新初始化请求对象

            items.URL = url;//设置请求地址

            //items.Referer = "http://car.autohome.com.cn/price/brand-35.html";//设置请求来源 
            items.Referer = "https://car.autohome.com.cn/price/series-3891.html";//设置请求来源

            items.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.120 Safari/537.36";//设置UserAgent

            items.Cookie = StrCookie;//设置字符串方式提交cookie

            items.Allowautoredirect = true;//设置自动跳转(True为允许跳转) 如需获取跳转后URL 请使用 hr.RedirectUrl

            items.ContentType = "application/x-www-form-urlencoded";//内容类型

            hr = helper.GetHtml(items, ref StrCookie);//提交请求

            res = hr.Html;//具体结果
            res = res.Split('=')[1];
            int x = res.LastIndexOf(";", res.Length); //lastindexof是从最后开始取.indexof才是从前面开始
            res = res.Substring(0, x);
            return res.Trim();//返回具体结果

        }

        public static string GetJsonValue(string JsonData)
        {
            string cmd = "$.[0].I";
            JToken jt;
            try
            {
                JObject o = JObject.Parse(JsonData);
                jt = o.SelectToken(cmd);
            }
            catch
            {
                JContainer json = (JContainer)JsonConvert.DeserializeObject(JsonData);
                jt = json.SelectToken(cmd);
            }

            if (jt != null)
            {
                return jt.ToString();
            }
            else
            {
                return "";
            }
        }



        /// <summary>
        /// Json到自定义对象
        /// </summary>
        /// <param name="JsonData">Json文本</param>
        /// <returns>自定义对象集合</returns>
        public static List<CarItem> GetJson2Item(string JsonData)
        {
            JContainer json = JsonConvert.DeserializeObject(JsonData) as JContainer; //反序列化json
            List<CarItem> list = GetJson2ItemPri(json);

            return list;
        }
        private static List<CarItem> GetJson2ItemPri(JContainer json)
        {
            List<CarItem> list = new List<CarItem>();
            //Newtonsoft.Json.Linq.JContainer json = (Newtonsoft.Json.Linq.JContainer)Newtonsoft.Json.JsonConvert.DeserializeObject(JsonData);
            for (int i = 0; i < json.Count; i++)
            {
                JObject jo = (JObject)json[i];
                JToken jk = jo["List"];
                CarItem ci = new CarItem()
                {
                    UrlI = jo["I"].ToString(),
                    N = jo["N"].ToString(),
                    L = jo["L"] == null ? "" : jo["L"].ToString()
                };
                ci.List = new List<CarItem>();
                int index = jk == null ? 0 : jk.Count();
                if (index > 0)
                {
                    ci.N += string.Format("({0})", index);
                    ci.List.AddRange(GetJson2ItemPri((JContainer)jk));//递归List中的数据
                }
                list.Add(ci);
                #region 不完善的结果
                //foreach (JObject item in jk)
                //{
                //    JToken subjk = item["List"];
                //    CarItem subci = new CarItem()
                //    {
                //        UrlI = item["I"].ToString(),
                //        N = item["N"].ToString(),
                //        L = item["L"] == null ? "" : item["L"].ToString()
                //    };
                //    subci.List = new List<CarItem>();
                //    foreach (JObject subitem in subjk)
                //    {
                //        JToken subjk1 = subitem["List"];
                //        CarItem subci1 = new CarItem()
                //        {
                //            UrlI = subitem["I"].ToString(),
                //            N = subitem["N"].ToString(),
                //            L = item["L"] == null ? "" : item["L"].ToString()
                //        };
                //        subci.List.Add(subci1);

                //    }
                //    ci.List.Add(subci); 
                //}
                //list.Add(ci); 
                #endregion
            }
            return list;
        }

        private static TreeNode GetJson2NodePri(JContainer json)
        {
            TreeNode tn = new TreeNode();
            for (int i = 0; i < json.Count; i++)
            {
                TreeNode td = new TreeNode();
                JObject jo = (JObject)json[i];
                JToken jk = jo["List"];
                CarItem ci = new CarItem()
                {
                    UrlI = jo["I"].ToString(),
                    N = jo["N"].ToString(),
                    L = jo["L"] == null ? "" : jo["L"].ToString()
                };
                ci.List = new List<CarItem>();

                int index = jk == null ? 0 : jk.Count();
                if (index > 0)
                {

                    ci.List.AddRange(GetJson2ItemPri((JContainer)jk));
                    td = GetJson2NodePri((JContainer)jk);
                }
                td.Name = ci.N;
                td.Text = ci.DisplayName;
                td.Tag = ci;
                if (!string.IsNullOrEmpty(ci.L))
                {
                    var treens = tn.Nodes.Find(ci.L, false);
                    if (treens.Length > 0)
                    {
                        //找到根
                        TreeNode tnd = treens[0];
                        Font subfont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                        td.NodeFont = subfont;
                        td.ForeColor = Color.Black;
                        tnd.Nodes.Add(td);
                    }
                    else
                    {
                        //没有找到就创建
                        TreeNode tnd = new TreeNode(ci.L);
                        tnd.Name = ci.L;
                        Font subfont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                        td.NodeFont = subfont;
                        td.ForeColor = Color.Black;
                        tnd.Nodes.Add(td);
                        tnd.Expand();
                        tn.Nodes.Add(tnd);
                    }
                }
                else
                {
                    Font subfont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                    td.NodeFont = subfont;
                    td.ForeColor = Color.BlueViolet;
                    tn.Nodes.Add(td);
                }

            }
            return tn;
        }
        /// <summary>
        /// Json到Node
        /// </summary>
        /// <param name="JsonData">Json文本</param>
        /// <returns>树节点</returns>
        public static TreeNode GetJson2Node(string JsonData)
        {
            JContainer json = (JContainer)JsonConvert.DeserializeObject(JsonData);
            TreeNode tn = GetJson2NodePri(json);
            return tn;
        }
        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <param name="JsonData">Json文本</param>
        /// <param name="tn">树节点</param>
        /// <returns>自定对象集合</returns>
        public static List<CarItem> GetJson2ALL(string JsonData, ref TreeNode tn)
        {

            List<CarItem> list = new List<CarItem>();
            JContainer json = (JContainer)JsonConvert.DeserializeObject(JsonData);

            return list;
        }

    }
}
