using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib
{
    public class searchWeight
    {
        public string id { get; set; }
        public int count { get; set; }

        public searchWeight(string id,int count)
        {
            this.id = id;
            this.count = count;
        }
    }
}
