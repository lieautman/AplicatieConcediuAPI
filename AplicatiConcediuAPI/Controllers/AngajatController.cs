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
        public string PostAngajatInregistrare(string nume, string prenume, string email, string parola, DateTime dataNasterii, string cnp, string serieSiNrCI, string numarTelefon)
        {
            //_gameOfThronesContext.Concedius.Include(x => x.TipConcediu).Select(x => new Concediu() { Id = x.Id, Comentarii = x.Comentarii, TipConcediu = x.TipConcediu }).Where(x => x.TipConcediu.Id == 2).ToList();
            Angajat angajat = new Angajat(nume, prenume, email, parola, dataNasterii, cnp, serieSiNrCI, numarTelefon);
            _gameOfThronesContext.Angajats.Add(angajat);
            return "Inregistrare efectuata!";
        }

        [HttpGet("GetUserPassAutentificare")]
        public ActionResult<Angajat> GetUserPassAutentificare( string email, string parola)
        {
            if (email == null && parola == null)
            {
                throw new Exception("Email si parola invalide");
            }
            else
            {
                Angajat a = _gameOfThronesContext.Angajats.Select(x => x).
                Where(x => x.Email == email && x.Parola == parola).FirstOrDefault();
                return a;
            }
            return Ok();

          
        }
    }
}
