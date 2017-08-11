namespace I2LI.DataAccess.Entities.PrivateClasses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        public int PaymentTypeId { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public int AccountInfoId { get; set; }

        [StringLength(20)]
        public string Adjustment { get; set; }

        [Column(TypeName = "money")]
        public decimal? AdjustmentAmount { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(20)]
        public string CreatedBy { get; set; }

        public DateTime LastChanged { get; set; }

        [StringLength(20)]
        public string LastChangedBy { get; set; }

        [StringLength(200)]
        public string Comments { get; set; }

        public virtual AccountInfo AccountInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        public virtual PaymentType PaymentType { get; set; }
    }
}
