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

            AccountInfo a = new AccountInfo() { Id = 1, LastName = "Skop", DateApplied = DateTime.Now};
            List<StudentInfo> studentInfoes = new List<StudentInfo>();
            studentInfoes.Add(new StudentInfo() { Id = 1, LastName = "Skop", FirstName = "Abby", AccountInfoId = 1, AccountInfo = a, DateOfBirth = new DateTime(2010,10,12), Gender = "f"});
            studentInfoes.Add(new StudentInfo() { Id = 2, LastName = "Skop", FirstName = "Max", AccountInfoId = 1, AccountInfo = a, DateOfBirth = new DateTime(2006,7, 1), Gender = "m" });

            // Convert the IEnumerable list to an IQueryable list
            IQueryable<StudentInfo> queryableList = studentInfoes.AsQueryable();

            // Force DbSet to return the IQueryable members of our converted list object as its data source
            var mockSet = new Mock<DbSet<StudentInfo>>();
            mockSet.As<IQueryable<StudentInfo>>().Setup(m => m.Provider).Returns(queryableList.Provider);
            mockSet.As<IQueryable<StudentInfo>>().Setup(m => m.Expression).Returns(queryableList.Expression);
            mockSet.As<IQueryable<StudentInfo>>().Setup(m => m.ElementType).Returns(queryableList.ElementType);
            mockSet.As<IQueryable<StudentInfo>>().Setup(m => m.GetEnumerator()).Returns(queryableList.GetEnumerator());

            mockContext.Setup(m => m.StudentInfoes).Returns(() => mockSet.Object);

            return mockContext;
        }

        [TestMethod]
        public void ParticipantTests_GetAllStudents_DirectlyFromDBContext_Mock()
        {
            var mockContext = GetMockContext();

            var res = mockContext.Object.StudentInfoes.ToList();
            Assert.IsTrue(res != null && res.Count > 0);
            Assert.AreEqual(res[0].AccountInfoId, 1, "Account Info invalid");
        }

        [TestMethod]
        public void ParticipantTests_GetAllStudents_Mock()
        {
            ParticipantRepo repo = new ParticipantRepo(GetMockContext().Object) { IncludeNavigationProperties = false };

            var res = repo.GetAllStudents();
            Assert.IsTrue(res != null && res.Count > 0);
            Assert.AreEqual(res[0].AccountInfoId, 1, "Account Info invalid");
        }
    }
}
