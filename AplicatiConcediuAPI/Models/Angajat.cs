using System;
using System.Collections.Generic;

namespace XD.Models
{
    public partial class Angajat
    {
        public Angajat()
        {
            ConcediuAngajats = new HashSet<Concediu>();
            ConcediuInlocuitors = new HashSet<Concediu>();
            InverseManager = new HashSet<Angajat>();
        }

        public int Id { get; set; }
        public string Nume { get; set; } = null!;
        public string Prenume { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Parola { get; set; }
        public DateTime? DataAngajarii { get; set; }
        public DateTime DataNasterii { get; set; }
        public string Cnp { get; set; } = null!;
        public string? SeriaNumarBuletin { get; set; }
        public string? Numartelefon { get; set; }
        public byte[]? Poza { get; set; }
        public bool? EsteAdmin { get; set; }
        public int? NumarZileConceiduRamase { get; set; }
        public int? ManagerId { get; set; }
        public decimal? Salariu { get; set; }
        public bool? EsteAngajatCuActeInRegula { get; set; }
        public int? IdEchipa { get; set; }

        public virtual Echipa? IdEchipaNavigation { get; set; }
        public virtual Angajat? Manager { get; set; }
        public virtual ICollection<Concediu> ConcediuAngajats { get; set; }
        public virtual ICollection<Concediu> ConcediuInlocuitors { get; set; }
        public virtual ICollection<Angajat> InverseManager { get; set; }

        public Angajat(int id, string nume, string prenume, string email, string? parola, DateTime? dataAngajarii, DateTime dataNasterii, string cnp, string? seriaNumarBuletin, string? numartelefon, byte[]? poza, bool? esteAdmin, int? numarZileConceiduRamase, int? managerId, decimal? salariu, bool? esteAngajatCuActeInRegula, int? idEchipa, Echipa? idEchipaNavigation, Angajat? manager, ICollection<Concediu> concediuAngajats, ICollection<Concediu> concediuInlocuitors, ICollection<Angajat> inverseManager)
        {
            Id = id;
            Nume = nume;
            Prenume = prenume;
            Email = email;
            Parola = parola;
            DataAngajarii = dataAngajarii;
            DataNasterii = dataNasterii;
            Cnp = cnp;
            SeriaNumarBuletin = seriaNumarBuletin;
            Numartelefon = numartelefon;
            Poza = poza;
            EsteAdmin = esteAdmin;
            NumarZileConceiduRamase = numarZileConceiduRamase;
            ManagerId = managerId;
            Salariu = salariu;
            EsteAngajatCuActeInRegula = esteAngajatCuActeInRegula;
            IdEchipa = idEchipa;
            IdEchipaNavigation = idEchipaNavigation;
            Manager = manager;
            ConcediuAngajats = concediuAngajats;
            ConcediuInlocuitors = concediuInlocuitors;
            InverseManager = inverseManager;
        }

        public Angajat(int id, string nume, string prenume, string email, string? parola, DateTime? dataAngajarii, DateTime dataNasterii, string cnp, string? seriaNumarBuletin, string? numartelefon, byte[]? poza, bool? esteAdmin, int? numarZileConceiduRamase, int? managerId, decimal? salariu, bool? esteAngajatCuActeInRegula, int? idEchipa)
        {
            Id = id;
            Nume = nume;
            Prenume = prenume;
            Email = email;
            Parola = parola;
            DataAngajarii = dataAngajarii;
            DataNasterii = dataNasterii;
            Cnp = cnp;
            SeriaNumarBuletin = seriaNumarBuletin;
            Numartelefon = numartelefon;
            Poza = poza;
            EsteAdmin = esteAdmin;
            NumarZileConceiduRamase = numarZileConceiduRamase;
            ManagerId = managerId;
            Salariu = salariu;
            EsteAngajatCuActeInRegula = esteAngajatCuActeInRegula;
            IdEchipa = idEchipa;
        }

    }
}
