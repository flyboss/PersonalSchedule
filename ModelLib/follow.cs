namespace ModelLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("personal_schedule.follow")]
    public partial class follow
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string u_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string f_u_id { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime f_time { get; set; }

        public virtual user user { get; set; }

        public virtual user user1 { get; set; }
    }
}
