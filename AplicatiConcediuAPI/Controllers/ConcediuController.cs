using Microsoft.AspNetCore.Mvc;
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

        //formular vizualizare concedii
        [HttpGet("GetAngajatAutentificare")]
        public List<Concediu> GetAngajatAutentificare(string email)
        {
            List<Concediu> a = _gameOfThronesContext.Concedius.Select(x => x).ToList();

            return a;
        }


        //formular creare concediu
    }
}
