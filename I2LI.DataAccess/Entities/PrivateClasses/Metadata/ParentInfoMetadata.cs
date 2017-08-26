using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2LI.DataAccess.Entities.PrivateClasses
{
    [MetadataType(typeof(ParentInfoMetadata))]
    public partial class ParentInfo
    {
    }

    public class ParentInfoMetadata
    {
        [StringLength(20, MinimumLength = 0)]
        public string LastName { get; set; }
    }
}
