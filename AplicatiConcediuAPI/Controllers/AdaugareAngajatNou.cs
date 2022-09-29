using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
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
        public string PostAdaugareAngajatNou(Angajat a)

        {   try
            {



              string nume = a.Nume;
                string prenume = a.Prenume;
                DateTime data_nastere = a.DataNasterii;
                string email = a.Email;
                string nr_telefon = a.Numartelefon;
                string cnp = a.Cnp;
                string SerieNrBuletin = a.SeriaNumarBuletin;
                string parola = a.Parola;
                
              //  int numarzileconcediu = (int)a.NumarZileConceiduRamase;
                int managerid = (int)a.ManagerId;
                //bool esteangajatcuacteinregula = (bool)a.EsteAngajatCuActeInRegula;
                float salariu = (float)a.Salariu;
                int idechipa = (int)a.IdEchipa;
                DateTime data_angajarii = (DateTime)a.DataAngajarii; 

                bool isError = false;


                //verificare daca sunt campuri goale
                if (!isError)
                {
                    if (a.Nume == "")
                    {
                        isError = true;
                    }
                    if (a.Prenume == "")
                    {
                        isError = true;
                    }
                    if (a.Numartelefon == "")
                    {
                        isError = true;
                    }
                    if (a.Cnp == "")
                    {
                        isError = true;
                    }
                    if (a.SeriaNumarBuletin == "")
                    {
                        isError = true;
                    }
                    if (a.Parola == "")
                    {
                        isError = true;
                    }
                    if (a.Email == "")
                    {
                        isError = true;
                    }
                  
                    /*if (numarzileconcediu == 0)
                    {
                        isError = true;
                    }*/
                    if (a.ManagerId == 0)
                    {
                        isError = true;
                    }
                  
                    if (a.Salariu == 0)
                    {
                        isError = true;
                    }
                    if (a.IdEchipa == 0)
                    {
                        isError = true;
                    }

                }
                
                
                //verificare validitate date campuri
                if (!isError)
                {  //validare email
                    const string reEmail = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
                    if (!Regex.Match(a.Email, reEmail, RegexOptions.IgnoreCase).Success)
                    {
                        isError = true;
                    }
                    
                }
                //daca data nasterii este in viitor
                if (a.DataNasterii > DateTime.Now)
                {
                    isError = true;
                }

                //daca data angajarii este inainte de data nasterii
                if (a.DataNasterii > a.DataAngajarii)
                {
                    isError = true;
                }
                if (!isError)
                {
                    _gameOfThronesContext.Angajats.Add(a);
                    _gameOfThronesContext.SaveChanges();

                    return "Adaugare efectuata";
                }
                else
                    return "Eroare  adaugare";

            }
            catch (Exception ex)
            {
                return "Eroare de adaugare";
            }
           
        }
    }
}
