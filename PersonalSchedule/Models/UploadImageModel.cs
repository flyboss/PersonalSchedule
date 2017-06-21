using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace PersonalSchedule.Models
{
    public class UploadImageModel
    {
        public string headFileName { get; set; }

        public int x { get; set; }


        public int y { get; set; }


        public int width { get; set; }


        public int height { get; set; }
    }
}