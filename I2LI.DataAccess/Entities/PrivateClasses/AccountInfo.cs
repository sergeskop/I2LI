namespace I2LI.DataAccess.Entities.PrivateClasses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountInfo")]
    public partial class AccountInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccountInfo()
        {
            ParentInfoes = new HashSet<ParentInfo>();
            Payments = new HashSet<Payment>();
            StudentInfoes = new HashSet<StudentInfo>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        public DateTime DateApplied { get; set; }

        [StringLength(250)]
        public string Comments { get; set; }

        [StringLength(20)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParentInfo> ParentInfoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentInfo> StudentInfoes { get; set; }
    }
}
