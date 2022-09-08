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
        public ActionResult PostAdaugareAngajatNou(Angajat a)

        {   try
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
                angajat.EsteAdmin = false;
                angajat.NumarZileConceiduRamase = 21;
                angajat.ManagerId = 0;
                angajat.EsteAngajatCuActeInRegula = false;
                angajat.Salariu = 0;
                angajat.IdEchipa = 0;
                angajat.DataAngajarii = a.DataAngajarii;



                string nume = a.Nume;
                string prenume = a.Prenume;
                DateTime data_nastere = a.DataNasterii;
                string email = a.Email;
                string nr_telefon = a.Numartelefon;
                string cnp = a.Cnp;
                string SerieNrBuletin = a.SeriaNumarBuletin;
                string parola = a.Parola;
                
                int numarzileconcediu = (int)a.NumarZileConceiduRamase;
                int managerid = (int)a.ManagerId;
                bool esteangajatcuacteinregula = (bool)a.EsteAngajatCuActeInRegula;
                float salariu = (float)a.Salariu;
                int idechipa = (int)a.IdEchipa;
                DateTime data_angajarii = (DateTime)a.DataAngajarii;

                bool isError = false;


                //verificare daca sunt campuri goale
                if (!isError)
                {
                    if (nume == "")
                    {
                        isError = true;
                    }
                    if (prenume == "")
                    {
                        isError = true;
                    }
                    if (nr_telefon == "")
                    {
                        isError = true;
                    }
                    if (cnp == "")
                    {
                        isError = true;
                    }
                    if (SerieNrBuletin == "")
                    {
                        isError = true;
                    }
                    if (parola == "")
                    {
                        isError = true;
                    }
                    if (email == "")
                    {
                        isError = true;
                    }
                  
                    /*if (numarzileconcediu == 0)
                    {
                        isError = true;
                    }*/
                    if (managerid == 0)
                    {
                        isError = true;
                    }
                  
                    if (salariu == 0)
                    {
                        isError = true;
                    }
                    if (idechipa == 0)
                    {
                        isError = true;
                    }

                }
                // verificare empty string
                /*if (!isError)
                {
                    if (nume == null)
                    {
                        isError = true;
                    }
                    if (prenume == null)
                    {
                        isError = true;
                    }
                    if (data_nastere == null)
                    {
                        isError = true;
                    }
                    if (nr_telefon == null)
                    {
                        isError = true;
                    }
                    if (cnp == null)
                    {
                        isError = true;
                    }
                    if (SerieNrBuletin == null)
                    {
                        isError = true;
                    }
                    if (parola == null)
                    {
                        isError = true;
                    }
                    if (email == null)
                    {
                        isError = true;
                    }
                    if (esteadmin == null)
                    {
                        isError = true;
                    }
                    if (numarzileconcediu == null)
                    {
                        isError = true;
                    }
                    if (managerid == null)
                    {
                        isError = true;
                    }
                    if (esteangajatcuacteinregula == null)
                    {
                        isError = true;
                    }
                    if (salariu == null)
                    {
                        isError = true;
                    }
                    if (idechipa == null)
                    {
                        isError = true;
                    }
                } */

                //verificarea lugimii minime a  caracterelor
                if (!isError)
                {
                    if (nume.Length < 2)
                    {

                        isError = true;
                    }
                    if (prenume.Length < 2)
                    {

                        isError = true;
                    }
                    if (nr_telefon.Length < 10)
                    {
                        isError = true;
                    }
                    if (cnp.Length < 13)
                    {
                        isError = true;
                    }
                    if (SerieNrBuletin.Length < 6)
                    {
                        isError = true;
                    }
                    if (parola.Length < 8)
                    {

                        isError = true;
                    }
                    if (email.Length < 4)
                    {
                        isError = true;
                    }
                }
                // verificare daca depasesc lungimile caract
                if (!isError)
                {
                    if (nume.Length > 150)
                    {

                        isError = true;
                    }
                    if (prenume.Length > 100)
                    {

                        isError = true;
                    }
                    if (nr_telefon.Length > 20)
                    {
                        isError = true;
                    }
                    if (cnp.Length > 13)
                    {
                        isError = true;
                    }
                    if (SerieNrBuletin.Length > 6)
                    {
                        isError = true;
                    }
                    if (parola.Length > 100)
                    {

                        isError = true;
                    }
                    if (email.Length > 100)
                    {
                        isError = true;
                    }
                }
                //verificare validitate date campuri
                if (!isError)
                {
                    const string reTelefon = "^[0-9]*$";
                    if (!Regex.Match(nr_telefon, reTelefon, RegexOptions.IgnoreCase).Success)
                    {
                        isError = true;
                    }
                    const string reCnp = "^[0-9]*$";
                    if (!Regex.Match(cnp, reCnp, RegexOptions.IgnoreCase).Success)
                    {
                        isError = true;
                    }
                    //validare email
                    const string reEmail = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
                    if (!Regex.Match(email, reEmail, RegexOptions.IgnoreCase).Success)
                    {
                        isError = true;
                    }
                    // validare parola
                    const string reParola = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
                    if (!Regex.Match(parola, reParola, RegexOptions.IgnoreCase).Success)
                    {
                        isError = true;
                    }

                }
                //daca data nasterii este in viitor
                if (data_nastere > DateTime.Now)
                {
                    isError = true;
                }

                //daca data angajarii este inainte de data nasterii
                if (data_nastere > data_angajarii)
                {
                    isError = true;
                }

            }
            catch (Exception ex)
            {

            }

            _gameOfThronesContext.Angajats.Add(a);
            _gameOfThronesContext.SaveChanges();

            return Ok();
        }
    }
}
