﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using XD.Models;
using System.Linq;

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
                _gameOfThronesContext.Angajats.Add(angajat);
                _gameOfThronesContext.SaveChanges();
                return "Inregistrare efectuata!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        //formular autentificare endpoint
        //[HttpPost("GetAngajatAutentificare")]
        //public ActionResult<Angajat> GetAngajatAutentificare(Angajat a)
        //{
        //    if (a.Email == null && a.Parola == null)
        //    {
        //        throw new  Exception("Email sau parola invalide") ;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            Angajat c = _gameOfThronesContext.Angajats.Select(x => x).FirstOrDefault();
        //            if (c == null)
        //                throw new Exception("Nu avem angajat");

        //            string jsonString = JsonSerializer.Serialize<Angajat>(c);
        //        }

        //        catch (Exception ex)
        //        {
        //            throw new Exception("Nu avem angajat");
        //        }
        //    }
        //    throw new Exception("Nu avem angajat");
        //}

        [HttpPost("AngajatAutentificare")]
        public ActionResult<Angajat> AngajatAutentificare(Angajat a)
        {

            if (a.Email == null && a.Parola == null)
            {
                throw new Exception("Email si parola invalide");
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
            return Ok();
        }


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
    }
}