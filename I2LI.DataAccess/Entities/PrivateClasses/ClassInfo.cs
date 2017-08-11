namespace I2LI.DataAccess.Entities.PrivateClasses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClassInfo")]
    public partial class ClassInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassInfo()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        public int ClassCategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string ClassName { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [StringLength(15)]
        public string Teacher { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(30)]
        public string ProductCode { get; set; }

        [StringLength(200)]
        public string Comments { get; set; }

        public virtual ClassCategory ClassCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
