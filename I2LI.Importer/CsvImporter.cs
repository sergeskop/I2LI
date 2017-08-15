using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using I2LI.DataAccess.Entities.PrivateClasses;
using I2LI.DataAccess.Repositories.PrivateClasses;
using System.Data.Entity.Validation;

namespace I2LI.Importer
{
    public class CsvImporter
    {
        public static void ImportFile(string fileName, bool archive = false)
        {
            LoadFile(fileName);
            ArchivesFile(fileName);
        }

        private static void LoadFile(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                var csv = new CsvReader(sr);
                while (csv.Read())
                {
                    ClassInfo classInfo = new ClassInfo();
                    classInfo.FromCsv(csv);

                    using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
                    {
                        ClassInfoRepo repo = new ClassInfoRepo(dbContext);

                        repo.Add(classInfo);

                        try
                        {
                            //Save to DB
                            dbContext.SaveChanges();

                        }
                        catch (DbEntityValidationException)
                        {
                            throw;
                        }
                    }
                }
            }
        }

        private static void ArchivesFile(string fileName)
        {
        }
    }
}
