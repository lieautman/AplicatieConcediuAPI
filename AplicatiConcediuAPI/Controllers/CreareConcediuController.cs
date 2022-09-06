using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("GetInlocuitor/{AngajatId}")]
        public List<Angajat> GetInlocuitori(int AngajatId)
        {

            Angajat angajatCurent = _gameOfThronesContext.Angajats.Where(x => x.Id == AngajatId).FirstOrDefault();
            if (angajatCurent == null)
                return null;
            List<Angajat> _inlocuitori = _gameOfThronesContext.Angajats.Select(a => new Angajat() {Prenume = a.Prenume ,Nume = a.Nume, Id = a.Id, IdEchipa = a.IdEchipa }).Where(a => a.IdEchipa == angajatCurent.IdEchipa && a.Id != angajatCurent.Id).ToList();
            return
                _inlocuitori;
        }
        [HttpGet("TipuriConcediu")]
        public List<TipConcediu> GetTipuriConcediu()
        {
            List<TipConcediu> _tipConcediu = _gameOfThronesContext.TipConcedius.Select(a => new TipConcediu() { Nume = a.Nume, Id = a.Id}).ToList();

            return
                   _tipConcediu;
        }

        [HttpPost("PostConcediu")]
        public ActionResult PostConcediu([FromBody] Concediu c)

        {
        
            _gameOfThronesContext.Concedius.Add(c);
            _gameOfThronesContext.SaveChanges();

            return Ok();

        }
    }
}
