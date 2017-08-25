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
        //Accounts
        List<AccountInfo> GetAllAccounts();
        AccountInfo GetAccountByStudent(StudentInfo student);
        void AddAccount(AccountInfo account);
        //Students
        List<StudentInfo> GetAllStudents();
        StudentInfo GetStudentById(int id);
        void AddStudent(StudentInfo student);
        //Parents
        List<ParentInfo> GetAllParents();
        List<ParentInfo> GetParentsByStudent(StudentInfo student);
        void AddParent(ParentInfo parent);
    }
}

