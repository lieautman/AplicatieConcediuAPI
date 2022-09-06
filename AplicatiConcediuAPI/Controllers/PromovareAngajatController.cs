using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text;
using XD.Models;

namespace AplicatieConcediuAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromovareAngajatController : ControllerBase
    {
        private readonly ILogger<Angajat> _logger;
        private readonly GameOfThronesContext _gameOfThronesContext;
        public PromovareAngajatController(ILogger<Angajat> logger, GameOfThronesContext gameOfThronesContext)
        {
            _logger = logger;
            _gameOfThronesContext = gameOfThronesContext;
        }

        [HttpGet("PromovareAngajat")]
        public string PromovareAngajat()
        {
           List<Angajat> a= _gameOfThronesContext.Angajats.Select(x => new Angajat() { Id=x.Id,Nume=x.Nume,Prenume=x.Prenume,Email=x.Email,DataNasterii=x.DataNasterii,Cnp=x.Cnp,Numartelefon=x.Numartelefon,IdEchipa=x.IdEchipa, ManagerId=x.ManagerId}).
                Where(x=>x.ManagerId != null).ToList();
            string jsonString = JsonSerializer.Serialize<List<Angajat>>(a);
            return jsonString;

        }

    }
}
