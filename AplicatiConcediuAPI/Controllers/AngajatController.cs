using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using XD.Models;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace AplicatieConcediuAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AngajatController : ControllerBase
    {
        private readonly ILogger<Angajat> _logger;
        private readonly GameOfThronesContext _gameOfThronesContext;

        public AngajatController(ILogger<Angajat> logger, GameOfThronesContext gameOfThronesContext)
        {
            _logger = logger;
            _gameOfThronesContext = gameOfThronesContext;
        }

        [HttpPost("UpdateAprobareAngajare")]
        public ActionResult UpdateAprobareAngajare([FromBody] Angajat angajat)
        {
            var a = _gameOfThronesContext.Angajats.Where(a => a.Id == angajat.Id).FirstOrDefault();
            if (a != null)
            {
                a.DataAngajarii = angajat.DataAngajarii;
                //a.NumarZileConceiduRamase = angajat.NumarZileConceiduRamase;
                a.Salariu = angajat.Salariu;
                a.ManagerId = angajat.ManagerId;
                a.EsteAngajatCuActeInRegula = angajat.EsteAngajatCuActeInRegula;

                _gameOfThronesContext.SaveChanges();
            }

            return Ok();

        }



        //functie de verificare parametrii angajat
        private void validareAngajatInregistrare(Angajat a, out bool isError)
        {
            isError = false;
            string nume = a.Nume;
            string prenume = a.Prenume;
            DateTime data_nastere = a.DataNasterii;
            string nr_telefon = a.Numartelefon;
            string cnp = a.Cnp;
            string SerieNrBuletin = a.SeriaNumarBuletin;
            string parola = a.Parola;
            string email = a.Email;

            //validari
            //completare campuri
            if (!isError)
            {
                if (nume == "")
                {
                    isError = true;
                }
                if (prenume == "")
                {
                    isError = true;
                }
                if (nr_telefon == "")
                {
                    isError = true;
                }
                if (cnp == "")
                {
                    isError = true;
                }
                if (SerieNrBuletin == "")
                {
                    isError = true;
                }
                if (parola == "")
                {
                    isError = true;
                }
                if (email == "")
                {
                    isError = true;
                }
            }//empty sring
            if (!isError)
            {
                if (nume == null)
                {
                    isError = true;
                }
                if (prenume == null)
                {
                    isError = true;
                }
                if (data_nastere == null)
                {
                    isError = true;
                }
                if (nr_telefon == null)
                {
                    isError = true;
                }
                if (cnp == null)
                {
                    isError = true;
                }
                if (SerieNrBuletin == null)
                {
                    isError = true;
                }
                if (parola == null)
                {
                    isError = true;
                }
                if (email == null)
                {
                    isError = true;
                }
            }//null

            //TODO: lungimile pot fi preluatedin baza de date
            //verificare pe nr de caractere minime
            if (!isError)
            {
                if (nume.Length < 2)
                {

                    isError = true;
                }
                if (prenume.Length < 2)
                {

                    isError = true;
                }
                //dataNastere nu are
                if (nr_telefon.Length < 10)
                {
                    isError = true;
                }
                if (cnp.Length < 13)
                {
                    isError = true;
                }
                if (SerieNrBuletin.Length < 6)
                {
                    isError = true;
                }
                if (parola.Length < 3)
                {

                    isError = true;
                }
                if (email.Length < 3)
                {
                    isError = true;
                }
            }
            //verificare pe nr de caractere maxime
            if (!isError)
            {
                if (nume.Length > 150)
                {

                    isError = true;
                }
                if (prenume.Length > 150)
                {

                    isError = true;
                }
                //dataNastere nu are
                if (nr_telefon.Length > 20)
                {
                    isError = true;
                }
                if (cnp.Length > 20)
                {
                    isError = true;
                }
                if (SerieNrBuletin.Length > 8)
                {
                    isError = true;
                }
                if (parola.Length > 100)
                {

                    isError = true;
                }
                if (email.Length > 100)
                {
                    isError = true;
                }
            }

            //verificare validitate date campuri
            if (!isError)
            {
                //cnp si data nasterii corespund
                {
                    //cnp si data nasterii corespund
                    {
                        string cnpDataNastere = cnp.Substring(1, 6);
                        string dataNastereFormatataString = data_nastere.ToString();
                        int index = dataNastereFormatataString.IndexOf('/', dataNastereFormatataString.IndexOf('/') + 1);
                        string luna = dataNastereFormatataString.Substring(0, dataNastereFormatataString.IndexOf("/"));
                        string zi = dataNastereFormatataString.Substring(dataNastereFormatataString.IndexOf("/") + 1, index - dataNastereFormatataString.IndexOf("/") - 1);
                        string an = dataNastereFormatataString.Substring(index + 1 + 2, dataNastereFormatataString.IndexOf(" ") - index - 3);

                        if (zi.Length == 1)
                        {
                            zi = "0" + zi;
                        }
                        if (luna.Length == 1)
                        {
                            luna = "0" + luna;
                        }

                        if (cnpDataNastere != an + luna + zi)
                        {
                            isError = true;
                        }
                    }
                }
                const string reTelefon = "^[0-9]*$";
                if (!Regex.Match(nr_telefon, reTelefon, RegexOptions.IgnoreCase).Success)
                {
                    isError = true;
                }
                const string reCnp = "^[0-9]*$";
                if (!Regex.Match(cnp, reCnp, RegexOptions.IgnoreCase).Success)
                {
                    isError = true;
                }
                //validare email
                const string reEmail = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
                if (!Regex.Match(email, reEmail, RegexOptions.IgnoreCase).Success)
                {
                    isError = true;
                }
                //validare parola
                const string reParola = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
                if (!Regex.Match(parola, reParola, RegexOptions.IgnoreCase).Success)
                {
                    isError = true;
                }
                //data nastere in viitor
                if (data_nastere > DateTime.Now)
                {
                    isError = true;
                }

            }
        }
        private void validareAngajatProfil(Angajat a, out bool isError)
        {
            isError = false;


            DateTime? data_angajare = a.DataAngajarii;
            decimal? salariu = a.Salariu;

            //validari
            //verificare validitate date campuri
            if (!isError)
            {
                const string reSalariu = "^[\\d./-]+$";
                if (!Regex.Match(salariu.ToString(), reSalariu, RegexOptions.IgnoreCase).Success)
                {
                    isError = true;
                }
                //data nastere in viitor
                if (data_angajare > DateTime.Now)
                {
                    isError = true;
                }

            }
        }


        //USE: Pagini start (Pagina_Inregistrare + Pagina_Autentificare)
        //formular inregistrare endpoint
        [HttpPost("PostAngajatInregistrare")]
        public string PostAngajatInregistrare([FromBody] Angajat a)
        {
            try
            {
                Angajat angajat = new Angajat();
                angajat.Nume = a.Nume;
                angajat.Prenume = a.Prenume;
                angajat.DataNasterii = a.DataNasterii;
                angajat.Email = a.Email;
                angajat.Numartelefon = a.Numartelefon;
                angajat.Cnp = a.Cnp;
                angajat.SeriaNumarBuletin = a.SeriaNumarBuletin;
                angajat.Parola = a.Parola;
                //aditionale
                angajat.EsteAdmin = false;
                //angajat.NumarZileConceiduRamase = 21;
                angajat.ManagerId = 1;
                angajat.EsteAngajatCuActeInRegula = false;
                angajat.Salariu = 0;

                bool isError = false;
                validareAngajatInregistrare(a, out isError);

                if (!isError)
                {
                    //verificare email existent
                    try
                    {
                        Angajat angjat2 = _gameOfThronesContext.Angajats.Select(x => x).Where(x => x.Email == a.Email).First();
                        isError = true;
                        return "Email folosit deja!";
                    }
                    catch
                    {
                        //return "nimic!";
                    }
                }

                if (!isError)
                {
                    _gameOfThronesContext.Angajats.Add(angajat);
                    _gameOfThronesContext.SaveChanges();
                    return "Inregistrare efectuata!";
                }
                else
                {
                    return "Eroare de validare!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //formular autentificare endpoint
        [HttpPost("AngajatAutentificare")]
        public ActionResult<Angajat> AngajatAutentificare([FromBody] Angajat a)
        {

            if (a.Email == null && a.Parola == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    Angajat c = _gameOfThronesContext.Angajats.Select(x => x).Where(x => x.Email == a.Email && x.Parola == a.Parola).FirstOrDefault();
                    if (c == null)
                        return NotFound();
                    return Ok(c);

                }
                catch (Exception ex)
                {
                    return NotFound(ex);
                }
            }
        }


        //USE: Pagina profil (Pagina_Profil_Angajat)
        //formular afisare profil angajat preluare poza
        [HttpPost("PostPreluarePoza")]
        public ActionResult<Angajat> PostPreluarePoza(Angajat a)
        {
            string email = a.Email;
            Angajat ang = new Angajat();
            ang = _gameOfThronesContext.Angajats.Select(x => x).Where(x => x.Email == email).FirstOrDefault();
            if (ang != null) { return Ok(ang); }
            return NoContent();
        }
        //formular afisare profil angajat incarcare poza
        [HttpPost("PostIncarcarePoza")]
        public ActionResult<Angajat> PostIncarcarePoza(Angajat a)
        {
            try
            {
                if (a.Poza!=null)
                {
                    string email = a.Email;
                    Angajat ang = new Angajat();
                    ang = _gameOfThronesContext.Angajats.Select(x => x).Where(x => x.Email == email).FirstOrDefault();
                    ang.Poza = a.Poza;
                    _gameOfThronesContext.SaveChanges();
                    if (ang != null) { return Ok(ang); }
                    return NoContent();
                }
                else
                {
                    return NoContent();
                }
            }
            catch
            {
                return NoContent();
            }
        }
        //formular afisare profil pe edit, acceptare modificari
        [HttpPost("PostEditareDateAngajat")]
        public ActionResult<Angajat> PostEditareDateAngajat(Angajat a)
        {
            try
            {
                bool isError = false;
                validareAngajatInregistrare(a, out isError);
                if(isError)
                {
                    return NoContent();
                }
                validareAngajatProfil(a, out isError);
                if (!isError)
                {
                    Angajat ang = _gameOfThronesContext.Angajats.Select(x => x).Where(x => x.Id == a.Id).FirstOrDefault();
                    ang.Nume = a.Nume;
                    ang.Prenume = a.Prenume;
                    ang.DataAngajarii = a.DataAngajarii;
                    ang.Email = a.Email;
                    ang.Numartelefon = a.Numartelefon;
                    ang.DataNasterii = a.DataNasterii;
                    ang.Cnp = a.Cnp;
                    ang.SeriaNumarBuletin = a.SeriaNumarBuletin;
                    ang.Salariu = a.Salariu;
                    _gameOfThronesContext.SaveChanges();
                    return Ok(ang);
                }
                return NoContent();
            }
            catch
            {
                return NoContent();
            }
        }
        //formular afisare profil preluare date angajat
        [HttpGet("GetDateAngajat/{AngajatEmail}")]
        public Angajat GetDateAngajat(string AngajatEmail)
        {
            return _gameOfThronesContext.Angajats.Where(x => x.Email == AngajatEmail).FirstOrDefault();
        }
        [HttpGet("GetManageri")]
        public List<Angajat> GetManageri()
        {
            return _gameOfThronesContext.Angajats.Where(x => x.ManagerId == null).ToList();

        }
        [HttpGet("GetAngajatiPerEchipa")]
        public List<Angajat> GetAngajatiPerEchipa( string numeEchipa)
        {
            List<Angajat> lista = _gameOfThronesContext.Angajats.Include(e => e.IdEchipaNavigation)
                .Where(e => e.IdEchipaNavigation.Nume == numeEchipa).Select(angajat => new Angajat
                {
                    Id = angajat.Id,
                    Nume = angajat.Nume,
                    Prenume = angajat.Prenume,
                    Email = angajat.Email,
                    IdEchipaNavigation = new Echipa { Nume = angajat.IdEchipaNavigation.Nume }

                }).ToList();
            return lista;

        }
        [HttpGet("GetEchipe")]
        public List<Echipa> GetEchipe()
        {
            return _gameOfThronesContext.Echipas.Select(x => x).ToList();

        }
        [HttpDelete("StergereAngajat/{email}")]
        public ActionResult<Angajat> StergereAngajat(string email)
        {
            var ang = _gameOfThronesContext.Angajats.Where(x => x.Email == email).FirstOrDefault();

            if (ang != null)
            {

                _gameOfThronesContext.Angajats.Remove(ang);
                _gameOfThronesContext.SaveChanges();

            }

            return Ok();

        }




        //USE: Pagina vizualizare concedii (Pagina_ConcediileMele)
        //formular concedii vizualizare endpoint(preluare numar zile concediu ramase pentru angajat)
        [HttpPost("PostPreluareNumarZileConcediuRamase")]
        public ActionResult<Angajat> PostPreluareNumarZileConcediuRamase(Angajat a)
        {
            string email = a.Email;
            Angajat ang = new Angajat();
            ang = _gameOfThronesContext.Angajats.Select(x => x).Where(x => x.Email == email).FirstOrDefault();
            if (ang != null) { return Ok(ang); }
            return NoContent();
        }


        //USE: Pagina promovare angajat (Promovare_Angajat)
        //formular promovare angajat, afisare nume si prenume in label
        [HttpGet("NumePrenumeAngajat")]
        public ActionResult<Angajat> NumePrenumeAngajat(string email)
        {
            Angajat a = _gameOfThronesContext.Angajats.Where(x => x.Email == email).Select(x => new Angajat() { Nume = x.Nume, Prenume = x.Prenume }).FirstOrDefault();
            return Ok(a);
        }


        //USE: Pagina toti angajatii (TotiAngajatii)
        //formular toti angajatii preluare date angajati
        [HttpPost("PostPreluareDateDespreTotiAngajatiiDinEchipa/{index1}/{index2}")]
        public ActionResult<List<Angajat>> PostPreluareDateDespreTotiAngajatiiDinEchipa([FromBody] Angajat a, int index1, int index2)
        {
            List<Angajat> conc = new List<Angajat>();
            if (a.IdEchipa != null)
                conc = _gameOfThronesContext.Angajats.Include(x => x.Manager).Include(x => x.IdEchipaNavigation).Select(x => new Angajat() { Id = x.Id, Nume = x.Nume, Prenume = x.Prenume, Email = x.Email, Parola = x.Parola, DataAngajarii = x.DataAngajarii, DataNasterii = x.DataNasterii, Cnp = x.Cnp, SeriaNumarBuletin = x.SeriaNumarBuletin, Numartelefon = x.Numartelefon, EsteAdmin = x.EsteAdmin, ManagerId = x.ManagerId, Manager = x.Manager, Salariu = x.Salariu, EsteAngajatCuActeInRegula = x.EsteAngajatCuActeInRegula, IdEchipa = x.IdEchipa, IdEchipaNavigation = new Echipa() { Descriere = x.IdEchipaNavigation.Descriere, Nume = x.IdEchipaNavigation.Nume } }).Where(x => x.IdEchipa == a.IdEchipa && x.Nume.Contains(a.Nume) && x.Prenume.Contains(a.Prenume) && x.Email.Contains(a.Email) && x.Manager.Nume.Contains(a.Manager.Nume) && x.IdEchipaNavigation.Nume.Contains(a.IdEchipaNavigation.Nume)).ToList();
            else
                conc = _gameOfThronesContext.Angajats.Include(x => x.Manager).Include(x => x.IdEchipaNavigation).Select(x => new Angajat() { Id = x.Id, Nume = x.Nume, Prenume = x.Prenume, Email = x.Email, Parola = x.Parola, DataAngajarii = x.DataAngajarii, DataNasterii = x.DataNasterii, Cnp = x.Cnp, SeriaNumarBuletin = x.SeriaNumarBuletin, Numartelefon = x.Numartelefon, EsteAdmin = x.EsteAdmin, ManagerId = x.ManagerId, Manager = x.Manager, Salariu = x.Salariu, EsteAngajatCuActeInRegula = x.EsteAngajatCuActeInRegula, IdEchipa = x.IdEchipa, IdEchipaNavigation = new Echipa() { Descriere = x.IdEchipaNavigation.Descriere, Nume = x.IdEchipaNavigation.Nume } }).Where(x => x.Nume.Contains(a.Nume) && x.Prenume.Contains(a.Prenume) && x.Email.Contains(a.Email) && x.Manager.Nume.Contains(a.Manager.Nume) && x.IdEchipaNavigation.Nume.Contains(a.IdEchipaNavigation.Nume)).ToList();
            if (conc != null)
            {
                if (index2 > conc.Count)
                    index2 = conc.Count;
                return Ok(conc.GetRange(index1, index2 - index1));
            }
            return NoContent();
        }


        //preluare numar pagini
        [HttpPost("PostPreluareNumarDePaginiDinEchipa/{nrElemPePag}")]
        public ActionResult<int> PostPreluareNumarDePaginiDinEchipa([FromBody] Angajat a, int nrElemPePag)
        {
            List<Angajat> conc = new List<Angajat>();
            if (a.IdEchipa != null)
                conc = _gameOfThronesContext.Angajats.Include(x => x.Manager).Include(x => x.IdEchipaNavigation).Select(x => new Angajat() { Id = x.Id, Nume = x.Nume, Prenume = x.Prenume, Email = x.Email, Parola = x.Parola, DataAngajarii = x.DataAngajarii, DataNasterii = x.DataNasterii, Cnp = x.Cnp, SeriaNumarBuletin = x.SeriaNumarBuletin, Numartelefon = x.Numartelefon, EsteAdmin = x.EsteAdmin, ManagerId = x.ManagerId, Manager = x.Manager, Salariu = x.Salariu, EsteAngajatCuActeInRegula = x.EsteAngajatCuActeInRegula, IdEchipa = x.IdEchipa, IdEchipaNavigation = new Echipa() { Descriere = x.IdEchipaNavigation.Descriere, Nume = x.IdEchipaNavigation.Nume } }).Where(x => x.IdEchipa == a.IdEchipa && x.Nume.Contains(a.Nume) && x.Prenume.Contains(a.Prenume) && x.Email.Contains(a.Email) && x.Manager.Nume.Contains(a.Manager.Nume) && x.IdEchipaNavigation.Nume.Contains(a.IdEchipaNavigation.Nume)).ToList();
            else
                conc = _gameOfThronesContext.Angajats.Include(x => x.Manager).Include(x => x.IdEchipaNavigation).Select(x => new Angajat() { Id = x.Id, Nume = x.Nume, Prenume = x.Prenume, Email = x.Email, Parola = x.Parola, DataAngajarii = x.DataAngajarii, DataNasterii = x.DataNasterii, Cnp = x.Cnp, SeriaNumarBuletin = x.SeriaNumarBuletin, Numartelefon = x.Numartelefon, EsteAdmin = x.EsteAdmin, ManagerId = x.ManagerId, Manager = x.Manager, Salariu = x.Salariu, EsteAngajatCuActeInRegula = x.EsteAngajatCuActeInRegula, IdEchipa = x.IdEchipa, IdEchipaNavigation = new Echipa() { Descriere = x.IdEchipaNavigation.Descriere, Nume = x.IdEchipaNavigation.Nume } }).Where(x => x.Nume.Contains(a.Nume) && x.Prenume.Contains(a.Prenume) && x.Email.Contains(a.Email) && x.Manager.Nume.Contains(a.Manager.Nume) && x.IdEchipaNavigation.Nume.Contains(a.IdEchipaNavigation.Nume)).ToList();

            if (conc != null)
            {
                int nrPag = conc.Count / nrElemPePag;
                if (conc.Count % nrElemPePag > 0)
                    nrPag++;
                return Ok(nrPag);
            }
            return NoContent();
        }

        //USE: Pagina promovare angajajti (promovare)
        //formular promovare angajati, formare echipa, preluare ang dupa email
        [HttpPost("PreluareAngajatDupaEmail")]
        public ActionResult<Angajat> PreluareAngajatDupaEmail(Angajat a)
        {
            Angajat angj = new Angajat();
            angj = _gameOfThronesContext.Angajats.Select(x => x).Where(x => x.Email == a.Email).FirstOrDefault();
            if (angj != null) { return Ok(angj); }
            return NoContent();
        }
        [HttpGet("GetPreluareNrPaginiAngajatiDePromovat/{nrElemPePag}")]
        public ActionResult<int> GetPreluareNrPaginiAngajatiDePromovat(int nrElemPePag)
        {

            List<Angajat> ang = _gameOfThronesContext.Angajats.Include(x => x.IdEchipaNavigation).Select(x => new Angajat()
            {
                Id = x.Id,
                Nume = x.Nume,
                Prenume = x.Prenume,
                Email = x.Email,
                Parola = x.Parola,
                DataAngajarii = x.DataAngajarii,
                DataNasterii = x.DataNasterii,
                Cnp = x.Cnp,
                SeriaNumarBuletin = x.SeriaNumarBuletin,
                Numartelefon = x.Numartelefon,
                EsteAdmin = x.EsteAdmin,
                ManagerId = x.ManagerId,
                Salariu = x.Salariu,
                EsteAngajatCuActeInRegula = x.EsteAngajatCuActeInRegula,
                IdEchipa = x.IdEchipa
            }).
  ToList();
            string jsonString = JsonSerializer.Serialize<List<Angajat>>(ang);

            if (ang != null)
            {
                int nrPag = ang.Count / nrElemPePag;
                if (ang.Count % nrElemPePag > 0)
                    nrPag++;
                return Ok(nrPag);
            }
            return NoContent();

        }
        [HttpGet("GetPreluareDateDespreTotiAngajatiiPentruPromovare/{index1}/{index2}")]
        public ActionResult<List<Angajat>> GetPreluareDateDespreTotiAngajatiiPentruPromovare( int index1, int index2)
        {
            List<Angajat> ang = _gameOfThronesContext.Angajats.Include(x => x.IdEchipaNavigation).Select(x => new Angajat()
            {
                Id = x.Id,
                Nume = x.Nume,
                Prenume = x.Prenume,
                Email = x.Email,
                Parola = x.Parola,
                DataAngajarii = x.DataAngajarii,
                DataNasterii = x.DataNasterii,
                Cnp = x.Cnp,
                SeriaNumarBuletin = x.SeriaNumarBuletin,
                Numartelefon = x.Numartelefon,
                EsteAdmin = x.EsteAdmin,
                ManagerId = x.ManagerId,
                Salariu = x.Salariu,
                EsteAngajatCuActeInRegula = x.EsteAngajatCuActeInRegula,
                IdEchipa = x.IdEchipa
            }).
  ToList();
            if (ang != null)
            {
                if (index2 > ang.Count)
                    index2 = ang.Count;
                return Ok(ang.GetRange(index1, index2 - index1));
            }
            return NoContent();
        }

        [HttpGet("GetAllAngajati")]
        public List<Angajat> GetAllAngajati()
        {
            List<Angajat> ang = _gameOfThronesContext.Angajats.Include(echipa=>echipa.IdEchipaNavigation).Select(x => new Angajat()
            {
                Id = x.Id,
                Nume = x.Nume,
                Prenume = x.Prenume,
                Email = x.Email,
                IdEchipaNavigation = new Echipa { Nume=x.IdEchipaNavigation.Nume} 
            }). ToList();
            return ang;
        }


        [HttpGet("GetAllAngajatiDeFormatEchipa/{id}")]
        public List<Angajat> GetAllAngajatiDeFormatEchipa(int id)
        {
            List<Angajat> ang = _gameOfThronesContext.Angajats.Include(echipa => echipa.IdEchipaNavigation).Where(x=>x.Id!=id).Select(x => new Angajat()
            {
                Id = x.Id,
                Nume = x.Nume,
                Prenume = x.Prenume,
                Email = x.Email,
                IdEchipaNavigation = new Echipa { Nume = x.IdEchipaNavigation.Nume }
            }).ToList();
            return ang;
        }


        [HttpGet("GetAngajatiById")]
        public ActionResult<Angajat> GetAngajatiById(int id)
        {
           Angajat ang = _gameOfThronesContext.Angajats.Where(x=>x.Id==id)
                .Include(echipa => echipa.IdEchipaNavigation)
                .Select(x => new Angajat()
                {Id=x.Id, Nume = x.Nume, Prenume = x.Prenume, Email = x.Email,Poza=x.Poza, IdEchipaNavigation = new Echipa { Nume = x.IdEchipaNavigation.Nume } }).FirstOrDefault();
 
            return Ok(ang);
        }

        //public ActionResult<Angajat> NumePrenumeAngajat(string email)
        //{
        //    Angajat a = _gameOfThronesContext.Angajats.Where(x => x.Email == email).Select(x => new Angajat() { Nume = x.Nume, Prenume = x.Prenume }).FirstOrDefault();
        //    return Ok(a);
        //}
    }
}