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
                    //Import Customer(Account and Parent)
                    int accountId = ImportCustomer(csv);

                    //ClassInfo
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

        private static int ImportCustomer(CsvReader csv)
        {
            int accointId = 0;
            
            //Import Parent
            ParentInfo p = new ParentInfo();
            p.FromCsv(csv);

            //
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {

                ParticipantRepo repo = new ParticipantRepo(dbContext);

                //If participant exist
                var existingParent = repo.GetAllParents().FirstOrDefault(x => x.LastName.ToLower() == p.LastName.ToLower() && x.Email == p.Email);
                if (existingParent != null)
                {
                    accointId = existingParent.AccountInfoId;
                    //TODO-1
                    //Update changes
                }
                else
                {
                    //repo.AddAccount(new AccountInfo { LastName = p.LastName, Email = p.Email, DateApplied = DateTime.Now});
                    p.AccountInfo = new AccountInfo { LastName = p.LastName, Email = p.Email, DateApplied = DateTime.Now };
                    repo.AddParent(p);
                }

                try
                {
                    //Save to DB
                    dbContext.SaveChanges();

                }
                catch (DbEntityValidationException exc)
                {
                    string[] validationErrorMessages = exc.EntityValidationErrors.Select(x => x.ValidationErrors.ToList()[0].ErrorMessage).ToArray();
                    throw new DbEntityValidationException(String.Join("; ", validationErrorMessages));
                }
            }

            return accointId;
        }

        private static void ArchivesFile(string fileName)
        {
        }
    }
}
