using System;
using System.Collections.Generic;

namespace XD.Models
{
    public partial class Concediu
    {
        public int Id { get; set; }
        public int? TipConcediuId { get; set; }
        public DateTime DataInceput { get; set; }
        public DateTime DataSfarsit { get; set; }
        public int? InlocuitorId { get; set; }
        public string? Comentarii { get; set; }
        public int? StareConcediuId { get; set; }
        public int? AngajatId { get; set; }

        public virtual Angajat? Angajat { get; set; }
        public virtual Angajat? Inlocuitor { get; set; }
        public virtual StareConcediu? StareConcediu { get; set; }
        public virtual TipConcediu? TipConcediu { get; set; }

        public Concediu() { }

        public Concediu(int id, int? tipConcediuId, DateTime dataInceput, DateTime dataSfarsit, int? inlocuitorId, string? comentarii, int? stareConcediuId, int? angajatId, Angajat? angajat, Angajat? inlocuitor, StareConcediu? stareConcediu, TipConcediu? tipConcediu)
        {
            Id = id;
            TipConcediuId = tipConcediuId;
            DataInceput = dataInceput;
            DataSfarsit = dataSfarsit;
            InlocuitorId = inlocuitorId;
            Comentarii = comentarii;
            StareConcediuId = stareConcediuId;
            AngajatId = angajatId;
            Angajat = angajat;
            Inlocuitor = inlocuitor;
            StareConcediu = stareConcediu;
            TipConcediu = tipConcediu;
        }
    }
}
