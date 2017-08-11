namespace I2LI.DataAccess.Entities.PrivateClasses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParentInfo")]
    public partial class ParentInfo
    {
        public int Id { get; set; }

        public int AccountInfoId { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(25)]
        public string Email { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }

        public virtual AccountInfo AccountInfo { get; set; }
    }
}
