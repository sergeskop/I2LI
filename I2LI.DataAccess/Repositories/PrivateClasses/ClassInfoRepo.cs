using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using I2LI.DataAccess.Entities.PrivateClasses;

namespace I2LI.DataAccess.Repositories.PrivateClasses
{
    public class ClassInfoRepo :
        GenericRepository<I2LI.DataAccess.Entities.PrivateClasses.ClassInfo>, IClassInfoRepo
    {
        protected PrivateClassesDBContext PrivateClassesDBContext { get { return _dbContext as PrivateClassesDBContext; } }

        public ClassInfoRepo(PrivateClassesDBContext dbContext) : 
            base(dbContext)
        {
        }

        #region Overrides

        public override IQueryable<ClassInfo> AsQueryable()
        {
            //Eager load all properties, including navigation properties
            if (IncludeNavigationProperties)
            {
                return PrivateClassesDBContext.ClassInfoes.Include(c => c.ClassCategory).Include(c => c.Orders);
            }
            //return PrivateClassesDBContext.ClassInfoes;
            return base.AsQueryable();
        }

        //public override IEnumerable<ClassInfo> GetAll()
        //{
        //    return GetAllClasses();
        //}

        public override IEnumerable<ClassInfo> FindMany(Expression<Func<ClassInfo, bool>> predicate)
        {
            return AsQueryable().Where(predicate).ToList();
        }

        #endregion Overrides

        #region IClassInfoRepo implementation

        public List<ClassInfo> GetAllClasses()
        {
                return AsQueryable().ToList();
        }

        public ClassInfo GetClassById(int id)
        {
            return AsQueryable().FirstOrDefault(c => c.Id == id);
        }

        public List<ClassCategory> GetAllClassCategories()
        {
            return PrivateClassesDBContext.ClassCategories.ToList();
        }

        public ClassCategory GetClassCategoryByKeyword(string keyword)
        {
            return PrivateClassesDBContext.ClassCategories.FirstOrDefault(c => c.CategoryName.ToLower().Contains(keyword)); ;
        }

        #endregion IClassInfoRepo implementation


    }
}
