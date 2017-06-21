namespace ModelLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("personal_schedule.event")]
    public partial class _event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public _event()
        {
            action = new HashSet<action>();
            link = new HashSet<link>();
            mark = new HashSet<mark>();
            tag = new HashSet<tag>();
        }

        [Key]
        [StringLength(50)]
        public string e_id { get; set; }

        [Required]
        [StringLength(50)]
        public string u_id { get; set; }

        [Required]
        [StringLength(100)]
        public string e_title { get; set; }

        [Required]
        [StringLength(1000)]
        public string e_content { get; set; }

        [Required]
        [StringLength(3)]
        public string e_auth { get; set; }

        [Required]
        [StringLength(50)]
        public string e_type { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime e_time { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<action> action { get; set; }

        public virtual user user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<link> link { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mark> mark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tag> tag { get; set; }
    }
}
