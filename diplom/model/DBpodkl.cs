using diplom.ta_ble;
using System.Collections.Generic;
using System.Data.Entity;
using System.Collections.Generic;

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
    }
}
