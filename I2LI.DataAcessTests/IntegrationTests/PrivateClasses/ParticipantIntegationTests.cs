using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class ParticipantIntegationTests
    {
        [TestMethod, Ignore]
        public void ParticipantIntegationTests_AddAccount()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                List<AccountInfo> validAccounts = new List<AccountInfo>() {
                    new AccountInfo() { LastName = "Skop", Email = "sergeskop@yahoo.com", DateApplied = DateTime.Now },
                    new AccountInfo() { LastName = "Skop", Email = "skopserge@gmail.com", DateApplied = DateTime.Now }
                };

                ParticipantRepo repo = new ParticipantRepo(dbContext);
                try
                {
                    foreach (var a in validAccounts)
                    {
                        repo.AddAccount(a);
                    }
                    dbContext.SaveChanges();
                }
                catch (DbEntityValidationException exc)
                {
                    string[] validationErrorMessages = exc.EntityValidationErrors.Select(x => x.ValidationErrors.ToList()[0].ErrorMessage).ToArray();
                    Assert.Fail(String.Join("; ", validationErrorMessages));
                }
                catch (Exception exc)
                {
                    Assert.Fail(exc.ToString());
                }

                var res = repo.GetAllAccounts().SingleOrDefault(a => a.Email == validAccounts[0].Email);
                Assert.IsNotNull(res);
            }
        }

        [TestMethod]
        public void ParticipantIntegationTests_GetAllAccounts()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                ParticipantRepo repo = new ParticipantRepo(dbContext);

                var res = repo.GetAllAccounts();
                Assert.IsTrue(res != null && res.Count > 0); 
            }
        }

        [TestMethod, Ignore]
        public void ParticipantIntegationTests_GetAccountByStudent()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                ParticipantRepo repo = new ParticipantRepo(dbContext);
                StudentInfo student = new StudentInfo() { LastName = "Skop", FirstName = "Abby", AccountInfoId = 1, Gender = "f" };
                var res = repo.GetAllAccounts();
                Assert.IsTrue(res != null && res.Count > 0);
            }
        }

        [TestMethod, Ignore]
        public void ParticipantIntegationTests_AddParent()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                List<ParentInfo> validParents = new List<ParentInfo>() {
                    new ParentInfo() { LastName = "Skop", FirstName = "Serge", AccountInfoId = 1 },
                    new ParentInfo() { LastName = "Skop", FirstName = "Lily", AccountInfoId = 1 },
                };

                ParticipantRepo repo = new ParticipantRepo(dbContext);
                try
                {
                    foreach (var p in validParents)
                    {
                        repo.AddParent(p);
                    }
                    dbContext.SaveChanges();
                }
                catch (DbEntityValidationException exc)
                {
                    string[] validationErrorMessages = exc.EntityValidationErrors.Select(x => x.ValidationErrors.ToList()[0].ErrorMessage).ToArray();
                    Assert.Fail(String.Join("; ", validationErrorMessages));
                }
                catch (Exception exc)
                {
                    Assert.Fail(exc.ToString());
                }

                var res = repo.GetAllParents().SingleOrDefault(p => p.LastName == "Skop" && p.FirstName == "Serge");
                Assert.IsNotNull(res);
            }
        }

        [TestMethod, Ignore]
        public void ParticipantIntegationTests_AddStudent()
        {
            using (PrivateClassesDBContext dbContext = new PrivateClassesDBContext())
            {
                List<StudentInfo> validStudents = new List<StudentInfo>() {
                    new StudentInfo() { LastName = "Skop", FirstName = "Abby", AccountInfoId = 1, Gender = "f", DateOfBirth = new DateTime(2010, 10, 12)},
                    new StudentInfo() { LastName = "Skop", FirstName = "Max", AccountInfoId = 1, Gender = "m", DateOfBirth = new DateTime(2006, 7, 1) },
                };

                ParticipantRepo repo = new ParticipantRepo(dbContext);
                try
                {
                    foreach (var s in validStudents)
                    {
                        repo.AddStudent(s);
                    }
                    dbContext.SaveChanges();
                }
                catch (DbEntityValidationException exc)
                {
                    string[] validationErrorMessages = exc.EntityValidationErrors.Select(x => x.ValidationErrors.ToList()[0].ErrorMessage).ToArray();
                    Assert.Fail(String.Join("; ", validationErrorMessages));
                }
                catch (Exception exc)
                {
                    Assert.Fail(exc.ToString());
                }

                var res = repo.GetAllStudents().SingleOrDefault(s => s.LastName == "Skop" && s.FirstName == "Abby");
                Assert.IsNotNull(res);
            }
        }
    }
}
