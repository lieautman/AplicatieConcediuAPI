using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using XD.Models;

namespace AplicatieConcediuAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreareConcediuController : Controller
    {
        private readonly ILogger<CreareConcediuController> _logger;
        private readonly GameOfThronesContext _gameOfThronesContext;
        public CreareConcediuController(ILogger<CreareConcediuController> logger, GameOfThronesContext gameOfThronesContext)
        {
            _logger = logger;
            _gameOfThronesContext = gameOfThronesContext;
        }
        // [HttpGet("GetInlocuitor/{AngajatId}")]
        //public List<Angajat> GetInlocuitori(int AngajatId)
        //{

        //    Angajat angajatCurent = _gameOfThronesContext.Angajats.Where(x => x.Id == AngajatId).FirstOrDefault();
        //    if (angajatCurent == null)
        //        return null;
        //    List<Angajat> angajats = new List<Angajat>();       
        //    List<Angajat> _inlocuitori = _gameOfThronesContext.Angajats.Select(a => new Angajat() { Prenume = a.Prenume, Nume = a.Nume, Id = a.Id, IdEchipa = a.IdEchipa,}).Where(a => a.IdEchipa == angajatCurent.IdEchipa && a.Id != angajatCurent.Id).ToList();
        //    return
        //        _inlocuitori;
        //}
        [HttpGet("GetInlocuitoriIndisponibili")]
        public List<Angajat> GetInlocuitoriIndisponibili(DateTime dataIncepere, DateTime dataIncetare, [FromQuery] int AngajatId)
        {
            Angajat angajatCurent = _gameOfThronesContext.Angajats.Where(x => x.Id == AngajatId).FirstOrDefault();
            if (angajatCurent == null)
                return null;
            //List<Concediu>conc =  _gameOfThronesContext.Concedius
            //    .Include(a => a.Angajat)
            //    .Where(c => c.Angajat.IdEchipa == angajatCurent.IdEchipa && c.Angajat.Id != angajatCurent.Id 
            //   && ((dataIncepere >= c.DataInceput && dataIncepere <= c.DataSfarsit) || (dataIncetare >= c.DataInceput && dataIncetare <= c.DataSfarsit))
            //  )
            //   .ToList();
            //List<Angajat> listAngCeNuPot = new List<Angajat>();
            //List<Angajat> listaInlocuitori = new List<Angajat>();
            //foreach (Concediu c in conc)
            //{
            //    listAngCeNuPot.Add(c.Angajat);
            //}
            //listAngCeNuPot = _gameOfThronesContext.Angajats
            //    .Select(a => new Angajat() { Prenume = a.Prenume, Nume = a.Nume, Id = a.Id, IdEchipa = a.IdEchipa, })
            //    .Where(a => a.IdEchipa == angajatCurent.IdEchipa && a.Id != angajatCurent.Id).ToList();  

            List<Angajat> asd = _gameOfThronesContext.Angajats
                .Include(x => x.ConcediuAngajats)
                .Where(x => x.IdEchipa == angajatCurent.IdEchipa && x.Id != AngajatId
                 && !x.ConcediuAngajats.Any(c => ((dataIncepere >= c.DataInceput && dataIncepere <= c.DataSfarsit) || (dataIncetare >= c.DataInceput && dataIncetare <= c.DataSfarsit)))
                 )
                .Select(a => new Angajat() { Prenume = a.Prenume, Nume = a.Nume, Id = a.Id, IdEchipa = a.IdEchipa })
                .ToList();
            return asd;
            // List<Angajat> _inlocuitori = _gameOfThronesContext.Angajats
            //     .Select(a => new Angajat() { Prenume = a.Prenume, Nume = a.Nume, Id = a.Id, IdEchipa = a.IdEchipa, })
            //     .Where(a => a.IdEchipa == angajatCurent.IdEchipa && a.Id != angajatCurent.Id)
            //     .ToList();
            //return
            //     _inlocuitori.Except(listAngCeNuPot).ToList();
        }


        [HttpGet("TipuriConcediu")]
        public List<TipConcediu> GetTipuriConcediu()
        {
            List<TipConcediu> _tipConcediu = _gameOfThronesContext.TipConcedius.Select(a => new TipConcediu() { Nume = a.Nume, Id = a.Id }).ToList();

            return
                   _tipConcediu;
        }
        [HttpGet("GetNrZileConcediu/{AngajatId}")]
        public Dictionary<int, int> GetNrZileConcediu(int AngajatId)
        {
            // c.DataSfarsit.Subtract(c.DataInceput).Days)
            // de scazut zilele maine
            Dictionary<int, int> DictionarZileConcediu = new Dictionary<int, int>();

            List<Concediu> ListaConcedii = _gameOfThronesContext.Concedius.Where(c => c.AngajatId == AngajatId && c.StareConcediuId == 1).ToList();
            List<TipConcediu> TipuriConcedii = _gameOfThronesContext.TipConcedius.Select(tc => new TipConcediu() { Id = tc.Id, NrZile = tc.NrZile }).ToList();
            foreach (Concediu c in ListaConcedii)
            {
                if (DictionarZileConcediu.ContainsKey(c.TipConcediuId.Value))
                {
                    DictionarZileConcediu[c.TipConcediuId.Value] = DictionarZileConcediu[c.TipConcediuId.Value] + c.DataSfarsit.Subtract(c.DataInceput).Days;
                }
                else
                    DictionarZileConcediu.Add(c.TipConcediuId.Value, c.DataSfarsit.Subtract(c.DataInceput).Days);
            }


            foreach (TipConcediu tc in TipuriConcedii)
            {
                if (DictionarZileConcediu.ContainsKey(tc.Id))
                {
                    DictionarZileConcediu[tc.Id] = tc.NrZile - DictionarZileConcediu[tc.Id];
                }
                else
                    DictionarZileConcediu[tc.Id] = tc.NrZile;
            }
            return DictionarZileConcediu;
        }

        //Angajat angajatCurent = _gameOfThronesContext.Angajats.Where(x => x.Email == AngajatEmail).FirstOrDefault();
        //if (angajatCurent == null)
        //    return null;
        //return _gameOfThronesContext.Angajats.Select(x => new Angajat() {Id = x.Id, NumarZileConceiduRamase = x.NumarZileConceiduRamase, Email = x.Email }).Where(x => x.Id == angajatCurent.Id).FirstOrDefault();



        [HttpPost("PostConcediu")]
        public ActionResult<Concediu> PostConcediu([FromBody] Concediu c)
        {
            try
            {
                _gameOfThronesContext.Concedius.Add(c);
                _gameOfThronesContext.SaveChanges();

                return Ok(c);

            }
            catch
            {
                return NoContent();
            }
        }
    }
}
