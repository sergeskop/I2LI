using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using I2LI.DataAccess.Entities.PrivateClasses.Importable;
using I2LI.DataAccess.Repositories.PrivateClasses;

namespace I2LI.DataAccess.Entities.PrivateClasses
{
    public partial class ParentInfo : ICsvImportable
    {

        public void FromCsv(CsvReader csv)
        {
            this.FirstName = csv.GetField<string>("First name (customer)");
            this.LastName = csv.GetField<string>("Last name (customer)");
            this.Email = csv.GetField<string>("Email address (customer)");
            this.Phone = csv.GetField<string>("Phone (customer))")?.Replace('-', '\0');
        }
    }

}
