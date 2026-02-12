using diplom.ta_ble;
using System.Data.Entity;

namespace diplom
{
    public class DBpodkl : DbContext
    {
        public DBpodkl() : base("DBstr")
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Fakultet> Fakultets { get; set; }
        public DbSet<Vid> Vids { get; set; }
        public DbSet<Jurnal> Jurnals { get; set; }
        public DbSet<User> Users { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Jurnal>()
        //                .HasMany(q => q.Students)
        //                .WithMany(q => q.jur3);
        //    modelBuilder.Entity<Jurnal>()
        //                .HasMany(q => q.Fakultets)
        //                .WithMany(q => q.jur1);
        //    modelBuilder.Entity<Jurnal>()
        //                .HasMany(q => q.Vids)
        //                .WithMany(q => q.jur2);

        //}
    }
}
