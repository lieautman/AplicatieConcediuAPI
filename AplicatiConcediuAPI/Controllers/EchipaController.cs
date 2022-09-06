using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using XD.Models;

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
    }
}
