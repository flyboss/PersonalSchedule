namespace ModelLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("personal_schedule.link")]
    public partial class link
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string e_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string l_title { get; set; }

        [Required]
        [StringLength(200)]
        public string l_src { get; set; }

        [Required]
        [StringLength(200)]
        public string l_content { get; set; }

        [Required]
        [StringLength(3)]
        public string l_type { get; set; }

        public virtual _event _event { get; set; }
    }
}
