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
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private void txtReg_userName_Leave(object sender, EventArgs e)
        {
            //当控件不再是活动空间时，检查用户名是否重复
            labRgs_username.Text = "";
            //
            string strName = txtReg_userName.Text.Trim();
            if (string.IsNullOrEmpty(strName))
            {
                TextBoxInputError("请输入用户名", labRgs_username, pic_username);
                return;
            }
            
            //
            int result = CarHomeMethod.CheckUserName(strName);
            if (result == 0)
            {
                TextBoxInputOk(pic_username);
            }
            else
            {
                TextBoxInputError("当前用户名已被注册", labRgs_username, pic_username);
            }
        }

        private void txtReg_userName_TextChanged(object sender, EventArgs e)
        {
            //检查用户名是否超过指定长度
            if (txtReg_userName.Text.Trim().Length > 15)
            {
                MessageBox.Show("用户名最大长度为15个字符");
                //处理超长数据
                //1.截断，直接截断数据
                txtReg_userName.Text = txtReg_userName.Text.Substring(0, 15);
                //2.清空 
                //texReg_userName.Text = "";
            }
        }

        private void txtReg_passwolrd_Leave(object sender, EventArgs e)
        {
            labRgs_password.Text = "";
            string strPass = txtReg_passwolrd.Text.Trim();
            if (string.IsNullOrEmpty(strPass))
            {
                TextBoxInputError("请输入密码", labRgs_password, pic_password);
                return;
            }

            //检查密码长度，大于6小于11
            if (strPass.Length < 6 || strPass.Length > 11)
            {
                //MessageBox.Show("输入的密码长度应在6到11位之间");
                TextBoxInputError("输入的密码长度应在6到11位之间", labRgs_password, pic_password);
                return;
            }

            //
            TextBoxInputOk(pic_password);
        }

        private void txtReg_passwolrd2_Leave(object sender, EventArgs e)
        {
            labRgs_password2.Text = "";
            string password2 = txtReg_passwolrd2.Text.Trim();
            //
            if (string.IsNullOrEmpty(password2))
            {
                TextBoxInputError("请输入密码", labRgs_password2, pic_password2);
                return;
            }

            //检查密码长度，大于6小于11
            if (password2.Length < 6 || password2.Length > 11)
            {
                TextBoxInputError("输入的密码长度应在6到11位之间", labRgs_password2, pic_password2);
                return;
            }

            //
            if (txtReg_passwolrd.Text.Trim() != password2)
            {
                TextBoxInputError("两次输入的密码不一致", labRgs_password2, pic_password2);
                TextBoxInputError("两次输入的密码不一致", labRgs_password, pic_password);
                return;
            }
            else
            {
                labRgs_password.Text = "";
                TextBoxInputOk(pic_password);
            }
            //
            TextBoxInputOk(pic_password2);
        }

        private void btnReg_Register_Click(object sender, EventArgs e)
        {
            //输入非空判断
            string uname = txtReg_userName.Text.Trim(); //Trim 只能移除首尾两端的空格
            //string uname = txtReg_userName.Text.Replace(" ", ""); //Replace 替换所有空格
            string upass = txtReg_passwolrd.Text.Trim();
            string upass2 = txtReg_passwolrd2.Text.Trim();
            string uQQ = txtReg_QQ.Text.Trim();
            if (string.IsNullOrEmpty(uname) || string.IsNullOrEmpty(upass) || string.IsNullOrEmpty(upass2))
            {
                MessageBox.Show("带*号为必填项");
                return;
            }

            //两次输入密码是否一致
            if (!upass.Equals(upass2))
            {
                MessageBox.Show("两次输入的密码不一致");
                return;
            }

            #region 不安全的写法，容易找出sql注入
            //字符串拼接的方式 向数据库插入数据
            //string sql = string.Format("INSERT INTO CH_Users (CH_UserName, CH_Passworld, CH_QQ) VALUES({0}, {1}, {2});", uname, upass, uQQ);
            //int res= sqlHelper.ExecuteNonQuery(sql, null);
            //if (res > 0)
            //    MessageBox.Show("恭喜【" + uname + "】注册成功");
            //else
            //    MessageBox.Show("很遗憾【" + uname + "】注册失败");
            #endregion

            #region 旧代码  Ctrl K + S
            //安全写法
            //string sql = "INSERT INTO Users (T_UserName, T_Password, T_QQNum) VALUES(@T_UserName, @T_Password, @T_QQNum);";
            ////
            //SQLiteParameter[] parms = new SQLiteParameter[] {
            //    new SQLiteParameter( "@T_UserName", uname),
            //    new SQLiteParameter( "@T_Password", upass),
            //    new SQLiteParameter( "@T_QQNum", uQQ),
            //};
            //int res = sqlHelper.ExecuteNonQuery(sql, parms);

            //if (res > 0)
            //{
            //    MessageBox.Show("恭喜【" + uname + "】注册成功");
            //    carUserName = uname;
            //    carUserQQ = uQQ;
            //    this.Close();//关闭窗口
            //}
            //else
            //    MessageBox.Show("很遗憾【" + uname + "】注册失败"); 
            #endregion

            int res = CarHomeMethod.Register(uname, upass, uQQ);

            if (res > 0)
            {
                MessageBox.Show("恭喜【" + uname + "】注册成功");
                StaticInfo.CarUserName = uname;
                StaticInfo.CarUserQQ = uQQ;
                this.Close();//关闭窗口
            }
            else
                MessageBox.Show("很遗憾【" + uname + "】注册失败");
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {

        }


        void TextBoxInputError(string errorCon, Label label, PictureBox picBox)
        {
            label.Text = errorCon;
            label.ForeColor = Color.Red;
            picBox.Image = CarHomeCollection.Properties.Resources._2;
        }
        void TextBoxInputOk(PictureBox picBox)
        {
            picBox.Image = CarHomeCollection.Properties.Resources._1;
        }
    }
}
