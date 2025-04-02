using System.Collections.Generic;

namespace diplom.ta_ble
{
    public class Jurnal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Fakultet { get; set; }
        public string VidGr { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Fakultet> Fakultets { get; set; }
        public virtual ICollection<Vid> Vids { get; set; }
    }
}
