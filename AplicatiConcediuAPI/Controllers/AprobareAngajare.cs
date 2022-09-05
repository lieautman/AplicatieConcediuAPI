using Microsoft.AspNetCore.Mvc;
using XD.Models;


namespace AplicatieConcediuAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    
    public class AprobareAngajareController : Controller
    {
        private readonly ILogger<Angajat> _logger;
        private readonly GameOfThronesContext _gameOfThronesContext;

        public AprobareAngajareController(ILogger<Angajat> logger, GameOfThronesContext gameOfThronesContext)
        {
            _logger = logger;
            _gameOfThronesContext = gameOfThronesContext;
        }
        [HttpGet("GetAprobareAngajare")]
        public List<Angajat> GetAprobareAngajare()
        {
            List<Angajat> a = new List<Angajat>();
           a = _gameOfThronesContext.Angajats.Where(x => x.EsteAngajatCuActeInRegula == false).Select(x => new Angajat() { Nume = x.Nume , Prenume = x.Prenume, Email = x.Email, Parola = x.Parola, DataNasterii = x.DataNasterii, Cnp = x.Cnp, SeriaNumarBuletin = x.SeriaNumarBuletin, Numartelefon = x.Numartelefon }).ToList();
            return a;
        }
    }
}
