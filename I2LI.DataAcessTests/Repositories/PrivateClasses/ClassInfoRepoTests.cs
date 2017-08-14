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
    public class ClassInfoRepoTests
    {

        public static Mock<PrivateClassesDBContext> GetMockContext()
        {
            Mock<PrivateClassesDBContext> mockContext = new Mock<PrivateClassesDBContext>();

            //--------------- Mock Categories ----------------------------------------------
            List<ClassCategory> categoryList = new List<ClassCategory>();
            categoryList.Add(new ClassCategory() { Id = 1, CategoryName = "Party" });
            categoryList.Add(new ClassCategory() { Id = 2, CategoryName = "Camp" });

            // Convert the IEnumerable list to an IQueryable list
            IQueryable<ClassCategory> queryableClassCategoryList = categoryList.AsQueryable();

            // Force DbSet to return the IQueryable members of our converted list object as its data source
            var mockClassCategorySet = new Mock<DbSet<ClassCategory>>();
            mockClassCategorySet.As<IQueryable<ClassCategory>>().Setup(m => m.Provider).Returns(queryableClassCategoryList.Provider);
            mockClassCategorySet.As<IQueryable<ClassCategory>>().Setup(m => m.Expression).Returns(queryableClassCategoryList.Expression);
            mockClassCategorySet.As<IQueryable<ClassCategory>>().Setup(m => m.ElementType).Returns(queryableClassCategoryList.ElementType);
            mockClassCategorySet.As<IQueryable<ClassCategory>>().Setup(m => m.GetEnumerator()).Returns(queryableClassCategoryList.GetEnumerator());
            mockContext.Setup(m => m.ClassCategories).Returns(() => mockClassCategorySet.Object);


            //--------------- Mock Orders ----------------------------------------------
            List<Order> orders = new List<Order>();
            orders.Add(new Order() { Id = 1, BookingNumber = "P1234", PaymentId = 1 });
            orders.Add(new Order() { Id = 1, BookingNumber = "P5678", PaymentId = 2 });

            //// Convert the IEnumerable list to an IQueryable list
            //IQueryable<Order> queryableOrderList = orders.AsQueryable();

            //// Force DbSet to return the IQueryable members of our converted list object as its data source
            //var mockOrderSet = new Mock<DbSet<Order>>();
            //mockOrderSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(queryableOrderList.Provider);
            //mockOrderSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(queryableOrderList.Expression);
            //mockOrderSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(queryableOrderList.ElementType);
            //mockOrderSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(queryableOrderList.GetEnumerator());
            //mockContext.Setup(m => m.ClassCategories).Returns(() => mockOrderSet.Object);

            //--------------- Mock Classes ----------------------------------------------
            List<ClassInfo> classInfoes = new List<ClassInfo>();
            classInfoes.Add(new ClassInfo()
            {
                Id = 1,
                ClassCategoryId = categoryList[0].Id,
                ClassCategory = categoryList[0],
                ClassName = "Birthday party",
                DateStart = new DateTime(2017, 7, 20),
                Location = "Trails"
            });
            classInfoes.Add(new ClassInfo()
            {
                Id = 2,
                ClassCategoryId = categoryList[1].Id,
                ClassCategory = categoryList[1],
                ClassName = "Summer Camp @ Aurora",
                DateStart = new DateTime(2017, 6, 1),
                Location = "Trails",
                Orders = orders
            });

            // Convert the IEnumerable list to an IQueryable list
            IQueryable<ClassInfo> queryableClassInfoList = classInfoes.AsQueryable();

            // Force DbSet to return the IQueryable members of our converted list object as its data source
            var mockClassInfoSet = new Mock<DbSet<ClassInfo>>();
            mockClassInfoSet.As<IQueryable<ClassInfo>>().Setup(m => m.Provider).Returns(queryableClassInfoList.Provider);
            mockClassInfoSet.As<IQueryable<ClassInfo>>().Setup(m => m.Expression).Returns(queryableClassInfoList.Expression);
            mockClassInfoSet.As<IQueryable<ClassInfo>>().Setup(m => m.ElementType).Returns(queryableClassInfoList.ElementType);
            mockClassInfoSet.As<IQueryable<ClassInfo>>().Setup(m => m.GetEnumerator()).Returns(queryableClassInfoList.GetEnumerator());

            mockContext.Setup(m => m.ClassCategories).Returns(() => mockClassCategorySet.Object);
            mockContext.Setup(m => m.ClassInfoes).Returns(() => mockClassInfoSet.Object);

            return mockContext;
        }

        [TestMethod]
        public void ClassInfoRepoTests_GetAllClassCategories_Mock()
        {
            ClassInfoRepo repo = new ClassInfoRepo(GetMockContext().Object) { IncludeNavigationProperties = false };

            var res = repo.GetAllClassCategories();
            Assert.IsTrue(res != null && res.Count == 2);
        }

        [TestMethod]
        public void ParticipantTests_GetAllClasses_Mock()
        {
            ClassInfoRepo repo = new ClassInfoRepo(GetMockContext().Object) { IncludeNavigationProperties = false };

            var res = repo.GetAllClasses();
            Assert.IsTrue(res != null && res.Count == 2);
        }

        [TestMethod]
        public void ParticipantTests_Find_Mock()
        {
            ClassInfoRepo repo = new ClassInfoRepo(GetMockContext().Object) { IncludeNavigationProperties = false };

            var res = repo.GetAllClasses().Where(x => x.ClassName.ToLower().Contains("Birthday") && x.ClassCategory.CategoryName.ToLower() == "party");
            Assert.IsTrue(res != null);
        }

        [TestMethod]
        public void ParticipantTests_GetClassById_Mock()
        {
            ClassInfoRepo repo = new ClassInfoRepo(GetMockContext().Object) { IncludeNavigationProperties = false };

            var res = repo.GetClassById(2);
            Assert.IsTrue(res != null);
        }

        [TestMethod]
        public void ParticipantTests_FindFullyPaidClasses_Mock()
        {
            ClassInfoRepo repo = new ClassInfoRepo(GetMockContext().Object) { IncludeNavigationProperties = false };
            List<ClassInfo> res = repo.FindMany(c => c.Orders.Where(o => o.PaymentId.HasValue).Count() > 0).ToList();
            Assert.IsTrue(res != null && res.Count > 0);
        }
    }
}