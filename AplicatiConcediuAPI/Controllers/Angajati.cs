using Microsoft.AspNetCore.Mvc;
using XD.Controllers;
using XD.Models;

namespace AplicatieConcediuAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AngajatController : ControllerBase
    {
        private readonly ILogger<Angajat> _logger;
        private readonly GameOfThronesContext _gameOfThronesContext;

        public AngajatController(ILogger<Angajat> logger, GameOfThronesContext gameOfThronesContext)
        {
            _logger = logger;
            _gameOfThronesContext = gameOfThronesContext;
        }


        [HttpGet("GetAngajati/all")]
        public List<Angajat> GetAngajati()
        {
            List<Angajat> a = new List<Angajat>();
            //_gameOfThronesContext.Concedius.Include(x => x.TipConcediu).Select(x => new Concediu() { Id = x.Id, Comentarii = x.Comentarii, TipConcediu = x.TipConcediu }).Where(x => x.TipConcediu.Id == 2).ToList();


            return a;
        }

    }
}
