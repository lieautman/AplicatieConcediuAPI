using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

using XD.Models;

namespace AplicatieConcediuAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ConcediiController : Controller
    {
        private readonly ILogger<ConcediiController> _logger;
        private readonly GameOfThronesContext _gameOfThronesContext;

        public ConcediiController(ILogger<ConcediiController> logger, GameOfThronesContext gameOfThronesContext)
        {
            _logger = logger;
            _gameOfThronesContext = gameOfThronesContext;
        }

        [HttpGet( "GetConcediiSpreAprobare")]
        public List<Concediu> GetConcediiSpreAprobare()
        {
             List<Concediu> concediuSpreAprobare =  _gameOfThronesContext.Concedius.Include(tc => tc.TipConcediu)
                 .Include(angajat => angajat.Angajat)
                 .Include(inlocuitor => inlocuitor.Inlocuitor).Where(con => con.StareConcediuId == 3)
                 .Select(concediu => new Concediu { Id = concediu.Id, TipConcediu = new TipConcediu { Nume = concediu.TipConcediu.Nume },
                     DataInceput = concediu.DataInceput, DataSfarsit = concediu.DataSfarsit, Inlocuitor =  new Angajat { Nume = concediu.Inlocuitor.Nume },
                     Comentarii = concediu.Comentarii, Angajat =  new Angajat { Nume = concediu.Angajat.Nume}}).ToList(); 

         
            return concediuSpreAprobare;
        }

        [HttpGet("GetConcediuById/{id}")]
        public Concediu GetConcediuById(int id)
        {
            Concediu concediu = _gameOfThronesContext.Concedius.Where(con => con.Id == id).FirstOrDefault();

            if (concediu != null)
            {
                return concediu;
            }
            else
            {
                return new Concediu();
            }
            
        }

       

        [HttpPost( "UpdateStareConcediu")]
        public ActionResult UpdateStareConcediu([FromBody]Concediu concediu)
        {
            var co = _gameOfThronesContext.Concedius.Where(c => c.Id == concediu.Id).FirstOrDefault();
            if (co != null)
            {
                co.StareConcediuId = concediu.StareConcediuId;
                _gameOfThronesContext.SaveChanges();
            }
           



            return Ok();

        }
    }
}
