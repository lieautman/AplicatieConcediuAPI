using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace XD.Models
{
    public partial class TipConcediu
    {
        public TipConcediu()
        {
            Concedius = new HashSet<Concediu>();
        }
        
        public int Id { get; set; }
        public string Nume { get; set; } = null!;
        public string Cod { get; set; } = null!;
        public virtual ICollection<Concediu> Concedius { get; set; }
        public int NrZile { get; set; }
    }   
}
