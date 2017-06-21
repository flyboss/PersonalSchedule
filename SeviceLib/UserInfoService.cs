using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLib;
using CRUDLib;
using System.Data.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SeviceLib
{
    public class UserInfoService
    {
         static public string ChangePwd(Model1 db, string login_id, string origin_pwd, string new_pwd)
        {
            try
            {
                user user = db.user.Find(login_id);
                if (user == null)
                {
                    return "网络异常，请重新登录";
                }
                if (user.u_pwd != origin_pwd)
                {
                    return "密码错误";
                }
                else
                {
                    user.u_pwd = new_pwd;
                    db.Entry<user>(user).State = EntityState.Modified;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    db.Configuration.ValidateOnSaveEnabled = true;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                return "网络异常，请重新登录";
            }
            return "修改成功！";
        }
        static public string FetchFollowed(Model1 db, string login_id)
        {
            try
            {

                var followed_i = from f in db.follow
                                 where f.u_id == login_id
                                 select new
                                 {
                                     u_id = f.f_u_id
                                 };
                List<string> followed_list = new List<string>();
                foreach(var f in followed_i)
                {
                    followed_list.Add(f.u_id);
                }
                
                JArray result = new JArray();
                foreach (var f in followed_list)
                {
                    JObject jo = new JObject();
                    user user = db.user.Find(f);
                    jo.Add("u_id", user.u_id);
                    jo.Add("u_name", user.u_name);
                    var event_i = from e in db._event
                                  where e.u_id == user.u_id
                                  orderby e.e_time descending
                                  select new
                                  {
                                      e_id = e.e_id,
                                      e_title = e.e_title,
                                      e_time = e.e_time
                                  };
                    if (event_i.Count() > 0)
                    {
                        foreach(var e in event_i)
                        {
                            jo.Add("e_id", e.e_id);
                            jo.Add("e_title", e.e_title);
                            DateTime dt = e.e_time;
                            jo.Add("e_time", dt.ToString("yyyy-MM-dd HH:mm"));
                            break;
                        }
                    }
                    else
                    {
                        jo.Add("e_id", "");
                        jo.Add("e_title", "");
                        jo.Add("e_time", "");
                    }
                    result.Add(jo);
                }
                return result.ToString();
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                return "[]";
            }
        }
    }
}
