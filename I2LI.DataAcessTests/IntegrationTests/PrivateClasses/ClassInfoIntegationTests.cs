﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using I2LI.DataAccess.Repositories.PrivateClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I2LI.DataAccess.Entities.PrivateClasses;
using I2LI.DataAccess.Repositories;
using Moq;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace I2LI.DataAcessTests.IntegrationTests.PrivateClasses
{
    [TestClass]
    public class ClassInfoIntegationTests
    {

        [TestMethod , Ignore]
        public void ClassInfoIntegationTests_GetAllClassCategories()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                ClassInfoRepo repo = new ClassInfoRepo(dbContext);

                var res = repo.GetAllClassCategories();
                Assert.IsTrue(res != null && res.Count > 0);
            }
        }

        [TestMethod , Ignore]
        public void ClassInfoIntegationTests_GetClassCategoryByKeyword()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                ClassInfoRepo repo = new ClassInfoRepo(dbContext);

                var res = repo.GetClassCategoryByKeyword("Party");
                Assert.IsTrue(res != null);
            }
        }

        [TestMethod , Ignore]
        public void ClassInfoIntegationTests_GetAllClasses()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                ClassInfoRepo repo = new ClassInfoRepo(dbContext);

                var res = repo.GetAllClasses();
                Assert.IsTrue(res != null && res.Count > 0 && res[0].ClassCategory != null);
            }
        }

        [TestMethod , Ignore]
        public void ClassInfoIntegationTests_GetAll()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                ClassInfoRepo repo = new ClassInfoRepo(dbContext);

                List<ClassInfo> res = repo.GetAll().ToList();
                Assert.IsTrue(res != null && res.Count > 0);
            }
        }

        [TestMethod , Ignore]
        public void ClassInfoIntegationTests_FindMany()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                GenericRepository<ClassInfo> repo = new GenericRepository<ClassInfo>(dbContext);

                //Calling generic method
                var res = repo.FindMany(x => x.ClassName.ToLower().Contains("Survival")).ToList();
                Assert.IsTrue(res != null && res.Count > 0);
            }
        }

        [TestMethod , Ignore]
        public void ClassInfoIntegationTests_FindOne()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                GenericRepository<ClassInfo> repo = new GenericRepository<ClassInfo>(dbContext);

                //Calling generic method
                var res = repo.FindOne(x => x.ClassName.ToLower().Contains("Survival") && x.DateStart > new DateTime(2017, 8, 1));
                Assert.IsTrue(res != null);
            }
        }

        [TestMethod , Ignore]
        public void ClassInfoIntegationTests_GetClassById()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                ClassInfoRepo repo = new ClassInfoRepo(dbContext);

                var res = repo.GetClassById(3);
                Assert.IsTrue(res != null && res.ClassCategory != null);
            }
        }

        [TestMethod , Ignore]
        public void ClassInfoIntegationTests_FindFullyPaidClasses()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                ClassInfoRepo repo = new ClassInfoRepo(dbContext);
                List<ClassInfo> res = repo.FindMany(c => c.Orders.Where(o => o.PaymentId.HasValue).Count() > 0).ToList();
                Assert.IsTrue(res != null && res.Count > 0);
            }
        }

        [TestMethod , Ignore]
        public void ClassInfoIntegationTests_FindUnPaidClasses()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                ClassInfoRepo repo = new ClassInfoRepo(dbContext);
                List<ClassInfo> res = repo.FindMany(c => c.Orders.Where(o => !o.PaymentId.HasValue).Count() > 0).ToList();
                Assert.IsTrue(res != null && res.Count > 0);
            }
        }

        [TestMethod, Ignore]
        public void ClassInfoIntegationTests_AddClass()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                ClassInfoRepo repo = new ClassInfoRepo(dbContext);
                repo.Add(new ClassInfo { ClassCategoryId = 1, ClassName = "TestClass", Location = "Trails", ProductCode = "C12345", DateStart = DateTime.Now, DateEnd = DateTime.Now });
                try
                {
                    dbContext.SaveChanges();
                }
                catch (DbEntityValidationException exc)
                {
                    Assert.Fail(exc.EntityValidationErrors.ToString());
                }
                catch (Exception exc)
                {
                    Assert.Fail(exc.ToString());
                }

                var res = repo.FindOne(c => c.ClassName == "TestClass");
                Assert.IsNotNull(res);
            }
        }
    }
}
