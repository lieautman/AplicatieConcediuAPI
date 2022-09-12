using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using XD.Models;
using System.Linq;

namespace AplicatieConcediuAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EchipaController : Controller
    {

        private readonly ILogger<Angajat> _logger;
        private readonly GameOfThronesContext _gameOfThronesContext;

        public EchipaController(ILogger<Angajat> logger, GameOfThronesContext gameOfThronesContext)
        {
            _logger = logger;
            _gameOfThronesContext = gameOfThronesContext;
        }

        //formular vizualizare echipe endpoint (pozele acestora)
        [HttpGet("GetVizualizareEchipePoze")]
        public ActionResult<List<byte[]>> GetVizualizareEchipePoze()
        {
            List<byte[]> listaDePoze = _gameOfThronesContext.Echipas.Select(x => x.Poza).ToList();
            if (listaDePoze != null) { return Ok(listaDePoze); }
            return NoContent();
        }

        [HttpGet("GetNume")]
        public List<Echipa> GetNume()
        {
            List<Echipa> e = _gameOfThronesContext.Echipas.Select(x => new Echipa() { Id = x.Id, Nume = x.Nume }).ToList();
            return e;
        }

        [HttpGet("PozaEchipa")]
        public byte[] PozaEchipa([FromQuery] int id)
        {
            Echipa e = _gameOfThronesContext.Echipas.Where(e => e.Id == id).FirstOrDefault();
            if (e != null)
            {
                return e.Poza;
            }
            return null;

        }
        //update poza echipa in functie de id echipa
        [HttpPost("UpdatePozaEchipa")]
        public ActionResult<Echipa> UpdatePozaEchipa(Echipa echipa)
        {
            int id = echipa.Id;
            Echipa e = new Echipa();
            e = _gameOfThronesContext.Echipas.Select(x => x).Where(x => x.Id == id).FirstOrDefault();

            if (e != null)
            {
                e.Poza = echipa.Poza;
                _gameOfThronesContext.SaveChanges(); 
                return Ok();
            }
            return NoContent();
        }
    }
}
