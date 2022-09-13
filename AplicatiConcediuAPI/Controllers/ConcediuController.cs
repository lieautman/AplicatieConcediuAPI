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
        [HttpPost("PostPreluareConcedii/{index1}/{index2}")]
        public ActionResult<List<Concediu>> PostPreluareConcedii([FromBody]Angajat a,int index1, int index2)//angajat doar cu email
        {
            List<Concediu> b = _gameOfThronesContext.Concedius.Include(x => x.Angajat).Select(x => new Concediu() { Id=x.Id, AngajatId=x.AngajatId, TipConcediuId = x.TipConcediuId, InlocuitorId = x.InlocuitorId , Inlocuitor = x.Inlocuitor, StareConcediuId = x.StareConcediuId , StareConcediu = x.StareConcediu, TipConcediu = x.TipConcediu, DataInceput = x.DataInceput, DataSfarsit = x.DataSfarsit, Comentarii=x.Comentarii, Angajat=x.Angajat}).Where(x => x.Angajat.Email == a.Email).ToList();
            if (b.Count != 0) { return Ok(b); }
            return NoContent();
        }


        //USE: Pagina de aprobare concedii (Aprobare_Concediu)
        //formular de preluare concedii pentru aprobare
        [HttpGet("GetConcediiSpreAprobare/{index1}/{index2}")]
        public List<Concediu> GetConcediiSpreAprobare(int index1, int index2)
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
            if (concediuSpreAprobare != null)
            {
                if (index2 > concediuSpreAprobare.Count)
                    index2 = concediuSpreAprobare.Count;
                return concediuSpreAprobare.GetRange(index1, index2 - index1);
            }


            return concediuSpreAprobare;
        }
        [HttpGet("GetPreluareNumarDePaginiAprobareConcedii/{nrElemPePag}")]
        public ActionResult<int> GetPreluareNumarDePaginiAprobareConcedii( int nrElemPePag)
        {
            List<Concediu> conc = _gameOfThronesContext.Concedius.Include(tc => tc.TipConcediu)
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
            if (conc != null)
            {
                int nrPag = conc.Count / nrElemPePag;
                if (conc.Count % nrElemPePag > 0)
                    nrPag++;
                return Ok(nrPag);
            }
            return NoContent();
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
