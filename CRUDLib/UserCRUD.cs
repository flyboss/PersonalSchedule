using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLib;

namespace CRUDLib
{
    public class UserCRUD
    {
        static public int AddUser(Model1 db, string u_id, string u_name, string u_pwd, string u_auth, string u_photo)
        {
            try
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                user user = new user();
                user.u_id = u_id;
                user.u_name = u_name;
                user.u_pwd = u_pwd;
                user.u_auth = u_auth;
                user.u_photo = u_photo;
                db.user.Add(user);
                db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                return -1;
            }
            return 1;
        }
    }
}
