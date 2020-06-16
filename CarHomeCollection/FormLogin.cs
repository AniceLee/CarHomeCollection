using CarHomeDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarHomeCollection
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string uname = txtUserName.Text.Trim(); //Trim 只能移除首尾两端的空格
            //string uname = txtReg_userName.Text.Replace(" ", ""); //Replace 替换所有空格
            string upass = txtPassworld.Text.Trim();
            if (string.IsNullOrEmpty(uname) || string.IsNullOrEmpty(upass))
            {
                MessageBox.Show("请输入用户名和密码");
                return;
            }
            //
            #region 旧代码
            //string sql = "SELECT T_UserName,T_QQNum FROM Users WHERE T_UserName=@UserName and T_Password = @Password;";
            //SQLiteParameter[] parms = new SQLiteParameter[] {
            //    new SQLiteParameter("@UserName",uname),
            //    new SQLiteParameter("@Password",upass)
            //};
            //SQLiteDataReader dr = StaticInfo.SqliteHelper.ExecuteReader(sql, parms);
            //if(dr.HasRows)
            //{
            //    while (dr.Read())
            //    {
            //        StaticInfo.CarUserName = dr["T_UserName"].ToString();
            //        StaticInfo.CarUserQQ = dr["T_QQNum"].ToString();
            //    }
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("没有数据，请注册");
            //    btnRegister_Click(sender, e);//打开注册窗口
            //} 
            #endregion

            CarHomeMethod.Login(uname, upass);
            if(StaticInfo.CheckUserNameIsNull())
            {
                MessageBox.Show("没有数据，请注册");
                btnRegister_Click(sender, e);//打开注册窗口
            }
            else
            {
                this.Close();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //显示注册窗体
            FormRegister formRegister = new FormRegister();
            formRegister.ShowDialog();

            //获取注册窗体结果
            //StaticInfo.CarUserName = formRegister.carUserName;
            //StaticInfo.CarUserQQ = formRegister.carUserQQ;
            if (StaticInfo.CheckUserNameIsNull())
            {
                MessageBox.Show("请先注册，再使用本软件");
                return;
            }

            //登陆窗口跳转至软件主窗口
            this.Close();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            //链接数据库
           
        }
    }
}
