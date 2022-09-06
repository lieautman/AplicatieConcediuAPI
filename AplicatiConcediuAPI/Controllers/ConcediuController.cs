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
        public List<Concediu> PostPreluareConcedii(Angajat a)//angajat doar cu email
        {
            List<Concediu> b = _gameOfThronesContext.Concedius.Include(x => x.Angajat).Select(x => x).Where(x=> x.Angajat.Email == a.Email).ToList();
            return b;
        }


        //formular creare concediu
    }
}
