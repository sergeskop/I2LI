namespace I2LI.DataAccess.Entities.PrivateClasses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClassCategory")]
    public partial class ClassCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassCategory()
        {
            ClassInfoes = new HashSet<ClassInfo>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string CategoryName { get; set; }

        [StringLength(250)]
        public string Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassInfo> ClassInfoes { get; set; }
    }
}
