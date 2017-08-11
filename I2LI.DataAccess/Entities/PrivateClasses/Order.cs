namespace I2LI.DataAccess.Entities.PrivateClasses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string BookingNumber { get; set; }

        public int ClassInfoId { get; set; }

        public int? PaymentId { get; set; }

        public int StudentInfoId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? VoidDate { get; set; }

        [StringLength(200)]
        public string Comments { get; set; }

        public virtual ClassInfo ClassInfo { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual StudentInfo StudentInfo { get; set; }
    }
}
