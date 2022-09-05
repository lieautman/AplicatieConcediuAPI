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

        [HttpPost("PostAngajatInregistrare")]
        public string PostAngajatInregistrare([FromBody] Angajat a)
        {
            try
            {
                Angajat angajat = new Angajat();
                angajat.Nume = a.Nume;
                angajat.Prenume = a.Prenume;
                angajat.DataNasterii = a.DataNasterii;
                angajat.Email = a.Email;
                angajat.Numartelefon = a.Numartelefon;
                angajat.Cnp = a.Cnp;
                angajat.SeriaNumarBuletin = a.SeriaNumarBuletin;
                angajat.Parola = a.Parola;
                _gameOfThronesContext.Angajats.Add(angajat);
                _gameOfThronesContext.SaveChanges();
                return "Inregistrare efectuata!";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpGet("PostAngajatInregistrare")]
        public string GetAngajatInregistrare()
        {
            return "Inregistrare efectuata!";
        }
    }
}
