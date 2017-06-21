namespace ModelLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("personal_schedule.action")]
    public partial class action
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string e_id { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "timestamp")]
        public DateTime a_starttime { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime a_endtime { get; set; }

        [Required]
        [StringLength(100)]
        public string a_title { get; set; }

        [Required]
        [StringLength(500)]
        public string a_content { get; set; }

        public virtual _event _event { get; set; }
    }
}
