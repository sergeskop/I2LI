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

namespace I2LI.DataAccess.Repositories.PrivateClasses.Tests
{
    [TestClass]
    public class ParticipantRepoTests
    {
        public static Mock<PrivateClassesDBContext> GetMockContext()
        {
            Mock<PrivateClassesDBContext> mockContext = new Mock<PrivateClassesDBContext>();

            //Mock Accounts
            List<AccountInfo> accounts = new List<AccountInfo>();
            accounts.Add( new AccountInfo() { Id = 1, LastName = "Skop", DateApplied = DateTime.Now });
            IQueryable<AccountInfo> queryableAccountList = accounts.AsQueryable();

            var mockAccountSet = new Mock<DbSet<AccountInfo>>();
            mockAccountSet.As<IQueryable<AccountInfo>>().Setup(m => m.Provider).Returns(queryableAccountList.Provider);
            mockAccountSet.As<IQueryable<AccountInfo>>().Setup(m => m.Expression).Returns(queryableAccountList.Expression);
            mockAccountSet.As<IQueryable<AccountInfo>>().Setup(m => m.ElementType).Returns(queryableAccountList.ElementType);
            mockAccountSet.As<IQueryable<AccountInfo>>().Setup(m => m.GetEnumerator()).Returns(queryableAccountList.GetEnumerator());

            mockAccountSet.Setup(x => x.Add(It.IsAny<AccountInfo>())).Callback<AccountInfo>((s) => accounts.Add(s));

            mockContext.Setup(m => m.AccountInfoes).Returns(() => mockAccountSet.Object);

            //Mock Students
            List<StudentInfo> studentInfoes = new List<StudentInfo>();
            studentInfoes.Add(new StudentInfo() { Id = 1, LastName = "Skop", FirstName = "Abby", AccountInfoId = 1, AccountInfo = accounts[0], DateOfBirth = new DateTime(2010,10,12), Gender = "f"});
            studentInfoes.Add(new StudentInfo() { Id = 2, LastName = "Skop", FirstName = "Max", AccountInfoId = 1, AccountInfo = accounts[0], DateOfBirth = new DateTime(2006,7, 1), Gender = "m" });

            // Convert the IEnumerable list to an IQueryable list
            IQueryable<StudentInfo> queryableStudentList = studentInfoes.AsQueryable();

            // Force DbSet to return the IQueryable members of our converted list object as its data source
            var mockStudentSet = new Mock<DbSet<StudentInfo>>();
            mockStudentSet.As<IQueryable<StudentInfo>>().Setup(m => m.Provider).Returns(queryableStudentList.Provider);
            mockStudentSet.As<IQueryable<StudentInfo>>().Setup(m => m.Expression).Returns(queryableStudentList.Expression);
            mockStudentSet.As<IQueryable<StudentInfo>>().Setup(m => m.ElementType).Returns(queryableStudentList.ElementType);
            mockStudentSet.As<IQueryable<StudentInfo>>().Setup(m => m.GetEnumerator()).Returns(queryableStudentList.GetEnumerator());

            mockStudentSet.Setup(x => x.Add(It.IsAny<StudentInfo>())).Callback<StudentInfo>((s) => studentInfoes.Add(s));

            mockContext.Setup(m => m.Set<StudentInfo>()).Returns(() => mockStudentSet.Object);

            mockContext.Setup(m => m.StudentInfoes).Returns(() => mockStudentSet.Object);

            //Mock Parents
            List<ParentInfo> ParentInfoes = new List<ParentInfo>();
            ParentInfoes.Add(new ParentInfo() { Id = 1, LastName = "Skop", FirstName = "Serge", AccountInfoId = 1, AccountInfo = accounts[0]});
            ParentInfoes.Add(new ParentInfo() { Id = 2, LastName = "Skop", FirstName = "Lily", AccountInfoId = 1, AccountInfo = accounts[0] });

            // Convert the IEnumerable list to an IQueryable list
            IQueryable<ParentInfo> queryableParentList = ParentInfoes.AsQueryable();

            // Force DbSet to return the IQueryable members of our converted list object as its data source
            var mockParentSet = new Mock<DbSet<ParentInfo>>();
            mockParentSet.As<IQueryable<ParentInfo>>().Setup(m => m.Provider).Returns(queryableParentList.Provider);
            mockParentSet.As<IQueryable<ParentInfo>>().Setup(m => m.Expression).Returns(queryableParentList.Expression);
            mockParentSet.As<IQueryable<ParentInfo>>().Setup(m => m.ElementType).Returns(queryableParentList.ElementType);
            mockParentSet.As<IQueryable<ParentInfo>>().Setup(m => m.GetEnumerator()).Returns(queryableParentList.GetEnumerator());

            mockParentSet.Setup(x => x.Add(It.IsAny<ParentInfo>())).Callback<ParentInfo>((s) => ParentInfoes.Add(s));

            mockContext.Setup(m => m.ParentInfoes).Returns(() => mockParentSet.Object);

            return mockContext;
        }

        [TestMethod]
        public void ParticipantTests_GetAllStudents_Mock()
        {
            ParticipantRepo repo = new ParticipantRepo(GetMockContext().Object) { IncludeNavigationProperties = false };

            var res = repo.GetAllStudents();
            Assert.IsTrue(res != null && res.Count > 0);
            Assert.AreEqual(res[0].AccountInfoId, 1, "Account Info invalid");
        }

        [TestMethod]
        public void ParticipantTests_GetStudentParents_Mock()
        {
            ParticipantRepo repo = new ParticipantRepo(GetMockContext().Object) { IncludeNavigationProperties = false };
            StudentInfo s = repo.GetStudentById(1);
            var res = repo.GetStudentParents(s);
            Assert.IsTrue(res != null && res.Count == 2);
        }

        [TestMethod]
        public void ParticipantTests_GetAccount_Mock()
        {
            ParticipantRepo repo = new ParticipantRepo(GetMockContext().Object) { IncludeNavigationProperties = false };
            Assert.IsTrue(repo.PrivateClassesDBContext.AccountInfoes.ToList().FirstOrDefault(x => x.LastName == "Skop") != null);
        }

        [TestMethod]
        public void ParticipantTests_AddAccount_Mock()
        {
            ParticipantRepo repo = new ParticipantRepo(GetMockContext().Object) { IncludeNavigationProperties = false };
            //Mock Account
            AccountInfo a = new AccountInfo() { Id = 2, LastName = "Baravik", DateApplied = DateTime.Now };
            int count1 = repo.PrivateClassesDBContext.AccountInfoes.Count();
            Assert.IsTrue(count1 == 1);

            repo.AddAccount(a);

            int count2 = repo.PrivateClassesDBContext.AccountInfoes.Count();
            Assert.IsTrue(count2 == 2);
        }

        [TestMethod]
        public void ParticipantTests_AddParent_Mock()
        {
            ParticipantRepo repo = new ParticipantRepo(GetMockContext().Object) { IncludeNavigationProperties = false };
            //Mock Parent
            ParentInfo p = new ParentInfo() { Id = 2, LastName = "Baravik", FirstName = "Mark", AccountInfoId = 2};
            int count1 = repo.PrivateClassesDBContext.ParentInfoes.Count();
            Assert.IsTrue(count1 == 2);

            repo.AddParent(p);

            int count2 = repo.PrivateClassesDBContext.ParentInfoes.Count();
            Assert.IsTrue(count2 == 3);
        }

        [TestMethod]
        public void ParticipantTests_AddStudent_Mock()
        {
            ParticipantRepo repo = new ParticipantRepo(GetMockContext().Object) { IncludeNavigationProperties = false };
            //Mock Student
            StudentInfo s = new StudentInfo() { Id = 3, LastName = "Baravik", FirstName = "Ron", AccountInfoId = 2 };
            int count1 = repo.PrivateClassesDBContext.StudentInfoes.Count();

            repo.AddStudent(s);

            int count2 = repo.PrivateClassesDBContext.StudentInfoes.Count();
            Assert.IsTrue(count2 == count1 + 1);
        }
    }
}
