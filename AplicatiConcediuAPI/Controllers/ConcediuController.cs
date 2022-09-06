using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XD.Models;

namespace AplicatieConcediuAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConcediuController : ControllerBase
    {
        private readonly ILogger<Concediu> _logger;
        private readonly GameOfThronesContext _gameOfThronesContext;

        public ConcediuController(ILogger<Concediu> logger, GameOfThronesContext gameOfThronesContext)
        {
            _logger = logger;
            _gameOfThronesContext = gameOfThronesContext;
        }

        //formular vizualizare concedii preluare concedii
        [HttpPost("PostPreluareConcedii")]
        public ActionResult<List<Concediu>> PostPreluareConcedii(Angajat a)//angajat doar cu email
        {
            List<Concediu> b = _gameOfThronesContext.Concedius.Include(x => x.Angajat).Select(x => new Concediu() { Id=x.Id, AngajatId=x.AngajatId, TipConcediuId = x.TipConcediuId, InlocuitorId = x.InlocuitorId , Inlocuitor = x.Inlocuitor, StareConcediuId = x.StareConcediuId , StareConcediu = x.StareConcediu, TipConcediu = x.TipConcediu, DataInceput = x.DataInceput, DataSfarsit = x.DataSfarsit, Comentarii=x.Comentarii, Angajat=x.Angajat}).Where(x => x.Angajat.Email == a.Email).ToList();
            if (b.Count != 0) { return Ok(b); }
            return NoContent();
        }


        //formular creare concediu
    }
}
