using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using XD.Models;


namespace AplicatieConcediuAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdaugareAngajatNouController : Controller
    {
        private readonly ILogger<Angajat> _logger;
        private readonly GameOfThronesContext _gameOfThronesContext;

        public AdaugareAngajatNouController(ILogger<Angajat> logger, GameOfThronesContext gameOfThronesContext)
        {
            _logger = logger;
            _gameOfThronesContext = gameOfThronesContext;
           

        }
         [HttpPost("PostAdaugareAngajatNou")]
        public   ActionResult PostAdaugareAngajatNou ( Angajat angajat)

        {
            
            _gameOfThronesContext.Angajats.Add(angajat);
            _gameOfThronesContext.SaveChanges();

            return Ok();
        }

    }
}
