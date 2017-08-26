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
    public partial class ClassInfo : ICsvImportable
    {
        private static readonly string[] CategoryKeywordsLookup = new string[] 
        {
            "night out",
            "party",
            "camp",
            "after school",
            "preschool"
        };
         
        public void FromCsv(CsvReader csv)
        {
            this.ClassName = csv.GetField<string>("Select a Program");
            this.DateStart = csv.GetField<DateTime>("Start");
            this.DateEnd = csv.GetField<DateTime>("End");
            this.Location = csv.GetField<string>("Location");
            this.Teacher = csv.GetField<string>("Teacher");
            this.ProductCode = csv.GetField<string>("Product code");

            this.ClassCategoryId = GetClassCategory(ClassName).Id;
        }

        private ClassCategory GetClassCategory(string className)
        {
            //Get value from lookup
            string lookupValue = String.Empty;
            foreach (string s in CategoryKeywordsLookup)
            {
                if (className.ToLower().Contains(s))
                {
                    lookupValue = s;
                }
            }

            if (String.IsNullOrEmpty(lookupValue))
            {
                lookupValue = "default";
            }

            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                ClassInfoRepo repo = new ClassInfoRepo(dbContext);
                var res = repo.GetClassCategoryByKeyword(lookupValue);
                return res;
            }
        }
    }

}
