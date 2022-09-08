﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using XD.Models;
using System.Linq;
using System.Text.RegularExpressions;

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
                a.NumarZileConceiduRamase = angajat.NumarZileConceiduRamase;
                a.Salariu = angajat.Salariu;
                a.Manager = angajat.Manager;
               
                _gameOfThronesContext.SaveChanges();
            }

            return Ok();

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
                angajat.NumarZileConceiduRamase = 21;
                angajat.ManagerId = 1;
                angajat.EsteAngajatCuActeInRegula = false;
                angajat.Salariu = 0;



                string nume = a.Nume;
                string prenume = a.Prenume;
                DateTime data_nastere = a.DataNasterii;
                string nr_telefon = a.Numartelefon;
                string cnp = a.Cnp;
                string SerieNrBuletin = a.SeriaNumarBuletin;
                string parola = a.Parola;
                string email = a.Email;
                bool isError = false;

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
                    //data nastere in viitor
                    if (data_nastere > DateTime.Now)
                    {
                        isError = true;
                    }
                    //verificare email existent
                    try
                    {
                        Angajat angjat2 = _gameOfThronesContext.Angajats.Select(x => x).Where(x => x.Email == email).First();
                        isError = true;
                        return "Email existent!";
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
                    return "Eroare de validare";
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
                    return NotFound();
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
            string email = a.Email;
            Angajat ang = new Angajat();
            ang = _gameOfThronesContext.Angajats.Select(x => x).Where(x => x.Email == email).FirstOrDefault();
            ang.Poza = a.Poza;
            _gameOfThronesContext.SaveChanges();
            if (ang != null) { return Ok(); }
            return NoContent();
        }


        //formular afisare profil preluare date angajat
        [HttpGet("GetDateAngajat/{AngajatEmail}")]
        public Angajat GetDateAngajat(string AngajatEmail)
        {
            return (Angajat)_gameOfThronesContext.Angajats.Where(x => x.Email == AngajatEmail).FirstOrDefault();

        }



        [HttpGet("GetManageri")]
        public List<Angajat> GetManageri()
        {
            return _gameOfThronesContext.Angajats.Where(x => x.ManagerId == null).ToList();

        }
        [HttpGet("GetEchipe")]
        public List<Echipa> GetEchipe()
        {
            return _gameOfThronesContext.Echipas.Select(x => x).ToList();

        }

        [HttpDelete("StergereAngajat")]
        public ActionResult<Angajat> StergereAngajat([FromBody]Angajat a)
        {
            var ang = _gameOfThronesContext.Angajats.Where(x => x.Id == a.Id).FirstOrDefault();

            if(ang!= null)
            {

                _gameOfThronesContext.Angajats.Remove(ang);
                _gameOfThronesContext.SaveChanges();

            }
            
            return Ok();

        }




        //USE: Pagina toti angajatii (TotiAngajatii)
        //formular toti angajatii preluare date angajati




        //USE: Pagina vizualizare concedii (Pagina_ConcediileMele)
        //formular concedii vizualizare endpoint(preluare numar zile concediu ramase pentru angajat)
        [HttpPost("PostPreluareNumarZileConcediuRamase")]
        public ActionResult<Angajat> PostPreluareNumarZileConcediuRamase(Angajat a)
        {
            string email = a.Email;
            Angajat ang = new Angajat();
            ang = _gameOfThronesContext.Angajats.Select(x => x).Where(x=> x.Email==email).FirstOrDefault();
            if (ang != null) { return Ok(ang); }
            return NoContent();
        }



        //USE: Pagina promovare angajat (Promovare_Angajat)
        [HttpGet("NumePrenumeAngajat")]
        public ActionResult<Angajat> NumePrenumeAngajat(string email)
        {
            Angajat a = _gameOfThronesContext.Angajats.Where(x=>x.Email==email).Select(x => new Angajat() { Nume = x.Nume, Prenume = x.Prenume }).FirstOrDefault();
            return Ok(a);
        }
    }
}