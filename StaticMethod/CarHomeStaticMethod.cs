using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticMethod
{
    /// <summary>
    /// 公共方法库
    /// </summary>
    public static class CarHomeStaticMethod
    {
        //方法1，外部公共方法库
        public static bool CheckUserNameIsNull()
        {
            return string.IsNullOrEmpty(CarHomeDLL.StaticInfo.CarUserName);
        }
    }
}
