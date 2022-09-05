using System;
using System.Collections.Generic;

namespace XD.Models
{
    public partial class Echipa
    {
        public Echipa()
        {
            Angajats = new HashSet<Angajat>();
        }

        public int Id { get; set; }
        public string Nume { get; set; } = null!;
        public string Descriere { get; set; } = null!;
        public byte[]? Poza { get; set; }

        public virtual ICollection<Angajat> Angajats { get; set; }
    }
}
