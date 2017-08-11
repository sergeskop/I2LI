using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using I2LI.DataAccess.Entities.PrivateClasses.Importable;

namespace I2LI.DataAccess.Entities.PrivateClasses
{
    public partial class AccountInfo : IImportable
    {
        public void FromCsv(CsvReader csv)
        {
            this.LastName = csv.GetField<string>("Last name (customer)");
            this.Email = csv.GetField<string>("Email address (customer)");
        }
    }
}
