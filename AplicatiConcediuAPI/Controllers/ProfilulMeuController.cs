using Microsoft.AspNetCore.Mvc;

using XD.Models;

namespace AplicatieConcediuAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfilulMeuController : Controller
    {

        private readonly ILogger<ProfilulMeuController> _logger;
        private readonly GameOfThronesContext _gameOfThronesContext;

        public ProfilulMeuController(ILogger<ProfilulMeuController> logger, GameOfThronesContext gameOfThronesContext)
        {
            _logger = logger;
            _gameOfThronesContext = gameOfThronesContext;
        }
        [HttpGet("GetProfilulMeu/{AngajatEmail}")]
        public Angajat GetProfilulMeu(string AngajatEmail)
        {
            return (Angajat)_gameOfThronesContext.Angajats.Where(x => x.Email == AngajatEmail).FirstOrDefault();

        }
    }
}
