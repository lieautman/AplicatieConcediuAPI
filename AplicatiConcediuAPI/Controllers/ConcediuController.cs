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


        //USE: Pagina de vizualizare de concedii (Pagina_ConcediileMele)
        //formular vizualizare concedii preluare concedii
        [HttpPost("PostPreluareConcedii")]
        public ActionResult<List<Concediu>> PostPreluareConcedii(Angajat a)//angajat doar cu email
        {
            List<Concediu> b = _gameOfThronesContext.Concedius.Include(x => x.Angajat).Select(x => new Concediu() { Id=x.Id, AngajatId=x.AngajatId, TipConcediuId = x.TipConcediuId, InlocuitorId = x.InlocuitorId , Inlocuitor = x.Inlocuitor, StareConcediuId = x.StareConcediuId , StareConcediu = x.StareConcediu, TipConcediu = x.TipConcediu, DataInceput = x.DataInceput, DataSfarsit = x.DataSfarsit, Comentarii=x.Comentarii, Angajat=x.Angajat}).Where(x => x.Angajat.Email == a.Email).ToList();
            if (b.Count != 0) { return Ok(b); }
            return NoContent();
        }


        //USE: Pagina de aprobare concedii (Aprobare_Concediu)
        //formular de preluare concedii pentru aprobare
        [HttpGet("GetConcediiSpreAprobare")]
        public List<Concediu> GetConcediiSpreAprobare()
        {
            List<Concediu> concediuSpreAprobare = _gameOfThronesContext.Concedius.Include(tc => tc.TipConcediu)
                .Include(angajat => angajat.Angajat)
                .Include(inlocuitor => inlocuitor.Inlocuitor).Where(con => con.StareConcediuId == 3)
                .Select(concediu => new Concediu
                {
                    Id = concediu.Id,
                    TipConcediu = new TipConcediu { Nume = concediu.TipConcediu.Nume },
                    DataInceput = concediu.DataInceput,
                    DataSfarsit = concediu.DataSfarsit,
                    Inlocuitor = new Angajat { Nume = concediu.Inlocuitor.Nume },
                    Comentarii = concediu.Comentarii,
                    Angajat = new Angajat { Nume = concediu.Angajat.Nume }
                }).ToList();


            return concediuSpreAprobare;
        }
        //formular de preluare concedii pentru aprobare (preluare concediu dupa id pentru a-i modifica starea)
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
        //formular de preluare concedii pentru aprobare (update pe stare a unui concediu dat)
        [HttpPost("UpdateStareConcediu")]
        public ActionResult UpdateStareConcediu([FromBody] Concediu concediu)
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
