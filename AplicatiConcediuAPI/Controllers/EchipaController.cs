using Microsoft.AspNetCore.Mvc;
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



        //formular vizualizare echipe endpoint
        [HttpGet("GetVizualizareEchipe")]
        public List<Echipa> GetVizualizareEchipe()
        {
            return new List<Echipa>();
        }
    }
}
