using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2LI.DataAccess.Entities.PrivateClasses
{
    [MetadataType(typeof(StudentInfoMetadata))]
    public partial class StudentInfo
    {
    }

    public class StudentInfoMetadata
    {
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/2000", "1/1/2050", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? DateOfBirth { get; set; }
    }
}
