using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using XD.Models;

namespace AplicatieConcediuAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TotiAngajatiiController : Controller
    {

        private readonly ILogger<Angajat> _logger;
        private readonly GameOfThronesContext _gameOfThronesContext;

        public TotiAngajatiiController(ILogger<Angajat> logger, GameOfThronesContext gameOfThronesContext)
        {
            _logger = logger;
            _gameOfThronesContext = gameOfThronesContext;


        }
        [HttpGet("GetTotiAngajatii")]
        public List<Angajat> GetTotiAngajatii()
        {
            List<Angajat> a = new List<Angajat>();
            a = _gameOfThronesContext.Angajats.Select(x => new Angajat() { Nume = x.Nume, Prenume = x.Prenume,Email = x.Email,DataAngajarii = x.DataAngajarii}).ToList();
            return a;   

        }
    }
        

}
