using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHomeDLL
{
    public static class CarHomeMethod
    {
        static SqliteHelper sqliteHelper;

        static CarHomeMethod()
        {
            string dbPath = AppDomain.CurrentDomain.BaseDirectory + "DB\\CarHomeCollectionDB.db";
            sqliteHelper = new SqliteHelper(dbPath, "");
        }

        public static void Login(string uname, string upass)
        {
            string sql = "SELECT T_UserName,T_QQNum FROM Users WHERE T_UserName=@UserName and T_Password = @Password;";
            SQLiteParameter[] parms = new SQLiteParameter[] {
                new SQLiteParameter("@UserName",uname),
                new SQLiteParameter("@Password",upass)
            };
            SQLiteDataReader dr = sqliteHelper.ExecuteReader(sql, parms);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    StaticInfo.CarUserName = dr["T_UserName"].ToString();
                    StaticInfo.CarUserQQ = dr["T_QQNum"].ToString();
                }
            }
        }

        public static int Register(string uname, string upass, string uQQ)
        {
            string sql = "INSERT INTO Users (T_UserName, T_Password, T_QQNum) VALUES(@T_UserName, @T_Password, @T_QQNum);";
            //
            SQLiteParameter[] parms = new SQLiteParameter[] {
                new SQLiteParameter( "@T_UserName", uname),
                new SQLiteParameter( "@T_Password", upass),
                new SQLiteParameter( "@T_QQNum", uQQ),
            };
            int res = sqliteHelper.ExecuteNonQuery(sql, parms);
            return res;
        }

        public static int CheckUserName(string strName)
        {
            string sql = "SELECT COUNT(*) FROM Users WHERE T_UserName = @UserName;";
            //
            SQLiteParameter parms = new SQLiteParameter("@UserName", strName);
            int result = Convert.ToInt32(sqliteHelper.ExecuteScalar(sql, parms));
            return result;
        }
    }
}
