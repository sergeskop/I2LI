using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace I2LI.DataAccess.Entities.PrivateClasses.Importable
{
    interface ICsvImportable
    {
        void FromCsv(CsvReader csv);
    }
}