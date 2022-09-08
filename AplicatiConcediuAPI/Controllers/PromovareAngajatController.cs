using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text;
using XD.Models;

namespace AplicatieConcediuAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromovareAngajatController : ControllerBase
    {
        private readonly ILogger<Angajat> _logger;
        private readonly GameOfThronesContext _gameOfThronesContext;
        public PromovareAngajatController(ILogger<Angajat> logger, GameOfThronesContext gameOfThronesContext)
        {
            _logger = logger;
            _gameOfThronesContext = gameOfThronesContext;
        }

        [HttpGet("PromovareAngajat")]
        public string PromovareAngajat()
        {
            List<Angajat> a = _gameOfThronesContext.Angajats.Include(x => x.Echipa).Select(x => new Angajat() { Nume = x.Nume, Prenume = x.Prenume, Email = x.Email, DataNasterii = x.DataNasterii, Numartelefon = x.Numartelefon, Echipa = x.Echipa, IdEchipa = x.IdEchipa }).
              ToList();
            string jsonString = JsonSerializer.Serialize<List<Angajat>>(a);
            return jsonString;

        }

        [HttpPost("UpdateManagerIdEchipaId")]
        public ActionResult<Angajat> UpdateManagerIdEchipaId([FromBody]List<Angajat> listaAngajati)
        {
            Angajat an = _gameOfThronesContext.Angajats.Select(x=> x).Where(x => x.Email == a.Email).FirstOrDefault();
            if (an != null)
            {
                an.ManagerId = null;
                _gameOfThronesContext.SaveChanges();
            }
            return Ok();

        }


    }
}
