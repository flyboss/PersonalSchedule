namespace ModelLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("personal_schedule.mark")]
    public partial class mark
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string u_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string e_id { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime m_time { get; set; }

        public virtual _event _event { get; set; }

        public virtual user user { get; set; }
    }
}
