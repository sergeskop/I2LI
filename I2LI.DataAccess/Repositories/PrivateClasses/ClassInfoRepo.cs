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

        public override IQueryable<ClassInfo> AsQueriable()
        {
            //Eager load all properties
            return PrivateClassesDBContext.ClassInfoes.Include(c => c.ClassCategory).Include(c => c.Orders);
        }

        public override IEnumerable<ClassInfo> GetAll()
        {
            return GetAllClasses();
        }

        public override IEnumerable<ClassInfo> FindMany(Expression<Func<ClassInfo, bool>> predicate)
        {
            return AsQueriable().Where(predicate).ToList();
        }

        #endregion Overrides

        #region IClassInfoRepo implementation

        public List<ClassInfo> GetAllClasses()
        {
            return AsQueriable().ToList();
        }

        public ClassInfo GetClassById(int id)
        {
            return AsQueriable().FirstOrDefault(c => c.Id == id);
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
