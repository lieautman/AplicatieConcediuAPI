using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XD.Models;

namespace XD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly GameOfThronesContext _gameOfThronesContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, GameOfThronesContext gameOfThronesContext)
        {
            _logger = logger;
           _gameOfThronesContext = gameOfThronesContext;
        }


        [HttpGet("GetAngajati")]
        public List<Concediu> GetAngajati()
        {
            List<Concediu> a = new List<Concediu>();
                //_gameOfThronesContext.Concedius.Include(x => x.TipConcediu).Select(x => new Concediu() { Id = x.Id, Comentarii = x.Comentarii, TipConcediu = x.TipConcediu }).Where(x => x.TipConcediu.Id == 2).ToList();


            return a;
        }

        [HttpGet("AngajatiGetAll")]
        public List<Concediu> AngajatiGetAll()
        {
            List<Concediu> a = _gameOfThronesContext.Concedius.Include(x => x.TipConcediu).Include(x=>x.StareConcediu).Select(x => new Concediu() { Id = x.Id, Comentarii = x.Comentarii, TipConcediu = x.TipConcediu }).Where(x => x.TipConcediu.Id == 2).ToList();


            return a;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public List<Concediu> Get()
        //{
        //    List<Concediu> a = _gameOfThronesContext.Concedius.Include(x => x.TipConcediu).Select(x => new Concediu() { Id = x.Id, Comentarii = x.Comentarii, TipConcediu = x.TipConcediu }).Where(x=>x.TipConcediu.Id==2).ToList();


        //    return a;
        //}

        //[HttpPost(Name = "PostWeatherForecast")]
        //public List<Concediu> Post()
        //{
        //    List<Concediu> a = _gameOfThronesContext.Concedius.Include(x => x.TipConcediu).Select(x => new Concediu() { Id = x.Id, Comentarii = x.Comentarii, TipConcediu = x.TipConcediu }).ToList();

        //    List<Concediu> b = new List<Concediu>();
        //    for (long i = 0; i < a.LongCount(); i++)
        //    {
        //        Concediu conc = new Concediu(a[Int32.Parse(i.ToString())].Id, a[Int32.Parse(i.ToString())].TipConcediuId, a[Int32.Parse(i.ToString())].DataInceput, a[Int32.Parse(i.ToString())].DataSfarsit, a[Int32.Parse(i.ToString())].InlocuitorId, a[Int32.Parse(i.ToString())].Comentarii, a[Int32.Parse(i.ToString())].StareConcediuId, a[Int32.Parse(i.ToString())].AngajatId, a[Int32.Parse(i.ToString())].Angajat, a[Int32.Parse(i.ToString())].Inlocuitor, a[Int32.Parse(i.ToString())].StareConcediu, a[Int32.Parse(i.ToString())].TipConcediu);
        //        b.Add(conc);
        //    }
        //    return b;
        //}
        //[HttpPut(Name = "GetWeatherForecast")]
        //public List<Concediu> Put()
        //{
        //    return _gameOfThronesContext.Concedius.Include(x => x.TipConcediu).Select(x => new Concediu() { Id = x.Id, Comentarii = x.Comentarii, TipConcediu = x.TipConcediu }).ToList();
        //}
        //[HttpDelete(Name = "GetWeatherForecast")]
        //public List<Concediu> Delete()
        //{
        //    return _gameOfThronesContext.Concedius.Include(x => x.TipConcediu).Select(x => new Concediu() { Id = x.Id, Comentarii = x.Comentarii, TipConcediu = x.TipConcediu }).ToList();
        //}
    }
}