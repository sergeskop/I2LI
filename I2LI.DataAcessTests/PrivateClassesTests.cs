using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Data.Entity;
using I2LI.DataAccess.Entities.PrivateClasses;

namespace I2LI.DataAcessTests
{
    [TestClass]
    public class PrivateClassesTests
    {
        [TestMethod]
        public void GetAllClasses_Test()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                var res = dbContext.ClassInfoes.Include(c => c.ClassCategory).ToList();
                Assert.IsTrue(res != null && res.Count > 0 && res[0].ClassCategory != null);
            }
        }

        [TestMethod]
        public void GetClasseById_Test()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                //var res = entities.ClassInfoes.Include(c => c.ClassCategory).FirstOrDefault(c => c.Id == 3);
                var res = dbContext.ClassInfoes.FirstOrDefault(c => c.Id == 3);
                dbContext.Entry(res).Reference(x => x.ClassCategory).Load();
                Assert.IsTrue(res != null && res.ClassCategory != null);
            }
        }
    }
}
