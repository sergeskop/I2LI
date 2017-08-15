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
    public interface IParticipantRepo :
        IGenericRepository<I2LI.DataAccess.Entities.PrivateClasses.StudentInfo>
    {
        List<StudentInfo> GetAllStudents();
        StudentInfo GetStudentById(int id);
        List<ParentInfo> GetStudentParents(StudentInfo student);
        void AddAccount(AccountInfo account);
        void AddParent(ParentInfo parent);
        void AddStudent(StudentInfo student);
    }
}

