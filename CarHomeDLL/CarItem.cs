using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHomeDLL
{
    /// <summary>
    /// 车辆数据实体类
    /// </summary>
    public class CarItem
    {
        private string urlI;
        /// <summary>
        /// url 地址
        /// </summary>
        public string UrlI
        {
            get { return urlI; }
            set { urlI = "https://car.autohome.com.cn/price/series-" + value + ".html"; }
        }

        /// <summary>
        /// 车辆名称
        /// </summary>
        private string _DisplayName;

        public string DisplayName
        {
            get { return "    " + N + (List != null && List.Count > 0 ? " ( " + List.Count + " )" : " "); }
            set
            {
                _DisplayName = value;

            }
        }

        /// <summary>
        /// 车辆名称
        /// </summary>
        public string N { get; set; }

        /// <summary>
        /// 所属字母区域
        /// </summary>
        public string L { get; set; }

        /// <summary>
        /// 子项
        /// </summary>
        public List<CarItem> List { get; set; }
    }
}
