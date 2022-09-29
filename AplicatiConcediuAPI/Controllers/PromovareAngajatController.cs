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
            List<Angajat> a = _gameOfThronesContext.Angajats.Include(x => x.IdEchipaNavigation).Select(x => new Angajat()
            { Id=x.Id,
              Nume=x.Nume,
              Prenume=x.Prenume,
              Email=x.Email,
              Parola=x.Parola,
              DataAngajarii=x.DataAngajarii,
              DataNasterii=x.DataNasterii,
              Cnp=x.Cnp,
              SeriaNumarBuletin=x.SeriaNumarBuletin,
              Numartelefon=x.Numartelefon,
              Poza=x.Poza,
              EsteAdmin=x.EsteAdmin,
              ManagerId=x.ManagerId,
              Salariu=x.Salariu,
              EsteAngajatCuActeInRegula=x.EsteAngajatCuActeInRegula,
              IdEchipa=x.IdEchipa}).
              ToList();
            string jsonString = JsonSerializer.Serialize<List<Angajat>>(a);
            return jsonString;

        }

        [HttpPost("UpdateManagerIdEchipaId")]
        public ActionResult<Angajat> UpdateManagerIdEchipaId([FromBody] List<Angajat> listaAngajati)
        {
            foreach (Angajat angajat in listaAngajati)
            {
                Angajat angBD = _gameOfThronesContext.Angajats.Select(x => x).Where(x => x.Email == angajat.Email).FirstOrDefault();
            
                angBD.Nume = angajat.Nume;
                angBD.Prenume = angajat.Prenume;
                angBD.Email = angajat.Email;
                angBD.Parola = angajat.Parola;
                angBD.DataAngajarii = angajat.DataAngajarii;
                angBD.DataNasterii = angajat.DataNasterii;
                angBD.Cnp = angajat.Cnp;
                angBD.SeriaNumarBuletin = angajat.SeriaNumarBuletin;
                angBD.Numartelefon = angajat.Numartelefon;
                angBD.Poza = angajat.Poza;
                angBD.EsteAdmin = angajat.EsteAdmin;
                //angBD.NumarZileConceiduRamase = angajat.NumarZileConceiduRamase;
                angBD.ManagerId = angajat.ManagerId;
                angBD.Salariu = angajat.Salariu;
                angBD.EsteAngajatCuActeInRegula = angajat.EsteAngajatCuActeInRegula;
                angBD.IdEchipa = angajat.IdEchipa;
                _gameOfThronesContext.SaveChanges();
              
            }
            return Ok();

        }
        [HttpPost("UpdateManagerIdEchipaIdReact")]
        public ActionResult<String> UpdateManagerIdEchipaIdReact([FromBody] List<Angajat> listaAngajati)
        {
            foreach (Angajat angajat in listaAngajati)
            {
                Angajat angBD = _gameOfThronesContext.Angajats.Select(x => x).Where(x => x.Email == angajat.Email).FirstOrDefault();

                angBD.ManagerId = angajat.ManagerId;
                angBD.IdEchipa = angajat.IdEchipa;
                _gameOfThronesContext.SaveChanges();

            }
            return Ok("Angajat promovat");

        }


    }
}
