namespace I2LI.DataAccess.Entities.PrivateClasses
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PrivateClassesDBContext : DbContext
    {
        public PrivateClassesDBContext()
            : base("name=PrivateClassesDBContext")
        {
        }

        public virtual DbSet<AccountInfo> AccountInfoes { get; set; }
        public virtual DbSet<ClassCategory> ClassCategories { get; set; }
        public virtual DbSet<ClassInfo> ClassInfoes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ParentInfo> ParentInfoes { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<StudentInfo> StudentInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountInfo>()
                .Property(e => e.LastName)
                .IsFixedLength();

            modelBuilder.Entity<AccountInfo>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<AccountInfo>()
                .Property(e => e.Email)
                .IsFixedLength();

            modelBuilder.Entity<AccountInfo>()
                .HasMany(e => e.ParentInfoes)
                .WithRequired(e => e.AccountInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AccountInfo>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.AccountInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AccountInfo>()
                .HasMany(e => e.StudentInfoes)
                .WithRequired(e => e.AccountInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassCategory>()
                .Property(e => e.CategoryName)
                .IsFixedLength();

            modelBuilder.Entity<ClassCategory>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<ClassCategory>()
                .HasMany(e => e.ClassInfoes)
                .WithRequired(e => e.ClassCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassInfo>()
                .Property(e => e.ClassName)
                .IsFixedLength();

            modelBuilder.Entity<ClassInfo>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<ClassInfo>()
                .Property(e => e.Teacher)
                .IsFixedLength();

            modelBuilder.Entity<ClassInfo>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<ClassInfo>()
                .Property(e => e.ProductCode)
                .IsFixedLength();

            modelBuilder.Entity<ClassInfo>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<ClassInfo>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.ClassInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.BookingNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<ParentInfo>()
                .Property(e => e.LastName)
                .IsFixedLength();

            modelBuilder.Entity<ParentInfo>()
                .Property(e => e.FirstName)
                .IsFixedLength();

            modelBuilder.Entity<ParentInfo>()
                .Property(e => e.Address1)
                .IsUnicode(false);

            modelBuilder.Entity<ParentInfo>()
                .Property(e => e.Address2)
                .IsUnicode(false);

            modelBuilder.Entity<ParentInfo>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<ParentInfo>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Payment>()
                .Property(e => e.Adjustment)
                .IsFixedLength();

            modelBuilder.Entity<Payment>()
                .Property(e => e.AdjustmentAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Payment>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<Payment>()
                .Property(e => e.LastChangedBy)
                .IsFixedLength();

            modelBuilder.Entity<Payment>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentType>()
                .Property(e => e.PaymentName)
                .IsFixedLength();

            modelBuilder.Entity<PaymentType>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.PaymentType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudentInfo>()
                .Property(e => e.LastName)
                .IsFixedLength();

            modelBuilder.Entity<StudentInfo>()
                .Property(e => e.FirstName)
                .IsFixedLength();

            modelBuilder.Entity<StudentInfo>()
                .Property(e => e.NickName)
                .IsFixedLength();

            modelBuilder.Entity<StudentInfo>()
                .Property(e => e.Gender)
                .IsFixedLength();

            modelBuilder.Entity<StudentInfo>()
                .Property(e => e.MedicalAwareness)
                .IsUnicode(false);

            modelBuilder.Entity<StudentInfo>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.StudentInfo)
                .WillCascadeOnDelete(false);
        }
    }
}
