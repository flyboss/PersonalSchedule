using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLib;

namespace CRUDLib
{
    public class MarkCRUD
    {
        public static int deleteMarksByEId(Model1 db, string e_id)
        {
            try
            {
                foreach (var mark in db.mark.Where(m => m.e_id == e_id))
                {
                    db.mark.Remove(mark);
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                return -1;
            }
            return 1;
        }
    }
}
