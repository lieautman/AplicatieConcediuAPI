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
        [HttpGet("GetAngajatAutentificare")]
        public List<Concediu> GetAngajatAutentificare(string email)
        {
            List<Concediu> a = _gameOfThronesContext.Concedius.Include(x => x.Angajat).Select(x => x).Where(X=> X.Angajat.Email == email).ToList();
            return a;
        }


        //formular creare concediu
    }
}
