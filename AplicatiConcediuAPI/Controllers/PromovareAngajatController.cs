﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public List<Angajat> PromovareAngajat()
        {
           List<Angajat> a= _gameOfThronesContext.Angajats.Select(x => new Angajat() { Id=x.Id,Nume=x.Nume,Prenume=x.Prenume,Email=x.Email,DataNasterii=x.DataNasterii,Numartelefon=x.Numartelefon, ManagerId=x.ManagerId}).
                Where(x=>x.ManagerId != null).ToList();
            return a;

        }

    }
}
