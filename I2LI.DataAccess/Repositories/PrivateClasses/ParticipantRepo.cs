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
    public class ParticipantRepo :
        GenericRepository<I2LI.DataAccess.Entities.PrivateClasses.StudentInfo>, IParticipantRepo
    {
        public PrivateClassesDBContext PrivateClassesDBContext { get { return _dbContext as PrivateClassesDBContext; } }

        public ParticipantRepo(PrivateClassesDBContext dbContext) :
            base(dbContext)
        {
        }

        #region Overrides

        public override IQueryable<StudentInfo> AsQueryable()
        {
            //Eager load all properties, including navigation properties
            if (IncludeNavigationProperties)
            {
                return PrivateClassesDBContext.StudentInfoes.Include(s => s.AccountInfo).Include(s => s.Orders);
            }
            return base.AsQueryable();
        }

        public override IEnumerable<StudentInfo> GetAll()
        {
            return GetAllStudents();
        }

        public override IEnumerable<StudentInfo> FindMany(Expression<Func<StudentInfo, bool>> predicate)
        {
            return AsQueryable().Where(predicate).ToList();
        }

        #endregion Overrides

        #region IParticipantRepo implementation

        public List<StudentInfo> GetAllStudents()
        {
            return AsQueryable().ToList();
        }

        public StudentInfo GetStudentById(int id)
        {
            return AsQueryable().FirstOrDefault(c => c.Id == id);
        }

        public void AddAccount(AccountInfo account)
        {
            PrivateClassesDBContext.AccountInfoes.Add(account);
        }

        public void AddParent(ParentInfo parent)
        {
            PrivateClassesDBContext.ParentInfoes.Add(parent);
        }

        public void AddStudent(StudentInfo student)
        {
            PrivateClassesDBContext.StudentInfoes.Add(student);
        }

        public List<AccountInfo> GetAllAccounts()
        {
            return PrivateClassesDBContext.AccountInfoes.ToList();
        }

        public AccountInfo GetAccountByStudent(StudentInfo student)
        {
            //TODO: validate single
            return PrivateClassesDBContext.AccountInfoes.FirstOrDefault(a => a.Id == student.AccountInfoId);
        }

        public List<ParentInfo> GetAllParents()
        {
            return PrivateClassesDBContext.ParentInfoes.ToList();
        }

        public List<ParentInfo> GetParentsByStudent(StudentInfo student)
        {
            return PrivateClassesDBContext.ParentInfoes.Where(p => p.AccountInfoId == student.AccountInfoId).ToList();
        }

        #endregion IParticipantRepo implementation


    }
}
