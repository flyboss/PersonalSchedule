namespace ModelLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("personal_schedule.user")]
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            _event = new HashSet<_event>();
            follow = new HashSet<follow>();
            follow1 = new HashSet<follow>();
            mark = new HashSet<mark>();
        }

        [Key]
        [StringLength(50)]
        public string u_id { get; set; }

        [Required]
        [StringLength(50)]
        public string u_name { get; set; }

        [Required]
        [StringLength(100)]
        public string u_pwd { get; set; }

        [Required]
        [StringLength(3)]
        public string u_auth { get; set; }

        [Required]
        [StringLength(100)]
        public string u_photo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<_event> _event { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<follow> follow { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<follow> follow1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mark> mark { get; set; }
    }
}
