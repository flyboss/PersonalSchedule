using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLib;
using CRUDLib;

namespace SeviceLib
{
    public class PersonalScheduleService
    {
        public static string Register(Model1 db, string register_id, string register_pwd, string register_name = null, string register_auth = "usr")
        {
            if (register_name == null)
            {
                register_name = register_id;
            }
            int message = UserCRUD.AddUser(db, register_id, register_name, register_pwd, register_auth, "");
            if (message > 0)
            {
                return "注册成功";
            }
            else
            {
                return "该用户已被注册";
            }
        }
        public static string Login(Model1 db, string login_id, string login_pwd, out user user)
        {
            user = db.user.Find(login_id);
            if (user == null)
            {
                return "用户不存在";
            }
            else if (user.u_pwd == login_pwd)
            {
                return "登陆成功";
            }
            else
            {
                return "密码错误";
            }
        }
        public static string GetCalActionsByUId(Model1 db, string login_id)
        {
            var cal_output_list_own = ActionCRUD.GetCalActionsOwnByUId(db, login_id);
            var cal_output_list_mark = ActionCRUD.GetCalActionsMarkByUId(db, login_id);
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (var ca in cal_output_list_own)
            {
                sb.Append(ca.ToJson());
                sb.Append(",");
            }
            foreach (var ca in cal_output_list_mark)
            {
                sb.Append(ca.ToJson());
                sb.Append(",");
            }
            if (sb.Length > 1) sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }
    }
}
