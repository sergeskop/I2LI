using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using I2LI.DataAccess.Entities.PrivateClasses;

namespace I2LI.DataAccess.Repositories.PrivateClasses
{
    public interface IClassInfoRepo : 
        IGenericRepository<I2LI.DataAccess.Entities.PrivateClasses.ClassInfo>
    {
        List<ClassInfo> GetAllClasses();
        ClassInfo GetClassById(int id);
    }
}
