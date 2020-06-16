using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHomeDLL
{
    /// <summary>
    /// 静态类，保存所有固定信息
    /// </summary>
    public static class StaticInfo
    {
        public static string CarUserName { get; set; }
        public static string CarUserQQ { get; set; }

        //检查CarUserName是否为null
        public static bool CheckUserNameIsNull()
        {
            return string.IsNullOrEmpty(CarUserName);
        }
    }
}
