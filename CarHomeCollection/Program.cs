using CarHomeDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarHomeCollection
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
            
            /*
             * 已完成功能
            FormLogin f1 = new FormLogin();
            f1.ShowDialog();

            //进入主窗体
            if (!StaticInfo.CheckUserNameIsNull())
            {
                FormMain fmain = new FormMain();
                fmain.Text += "   欢迎" + StaticInfo.CarUserName + "登陆";
                fmain.ShowDialog();
            }*/
        }
    }
}
