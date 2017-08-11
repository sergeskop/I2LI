using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using I2LI.DataAccess;
using I2LI.DataAccess.Entities.PrivateClasses;

namespace I2LI.WebApi.Controllers
{
    public class PrivateClassesController : ApiController
    {
        public IEnumerable<ClassInfo> GetAllClasses()
        {
            using (PrivateClassesDBContext entities = new PrivateClassesDBContext())
            {
                //var res = entities.ClassInfoes.Include("ClassCategory").ToList();
                var res = entities.ClassInfoes.Include(c => c.ClassCategory).ToList();
                return res;
            }
        }

        //public IEnumerable<ClassCategory> GetAllCategories()
        //{
        //    using (I2LIPrivateClassesDBContext entities = new I2LIPrivateClassesDBContext())
        //    {
        //        //res = entities.ClassInfoes.Include("Orders").Include("ClassCategory").ToList();
        //        var categories = entities.ClassCategories.ToList();
        //        return categories;
        //        //var res = entities.ClassInfoes.ToList();
        //        //return res;
        //    }
        //}

        public ClassInfo GetClassById(int id)
        {
            using (PrivateClassesDBContext entities = new PrivateClassesDBContext())
            {
                return entities.ClassInfoes.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}
