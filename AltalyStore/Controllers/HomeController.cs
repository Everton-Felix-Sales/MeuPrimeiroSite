using AltalyStore.Libraries.Email;
using AltalyStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltalyStore.Controllers
{
    public class HomeController : Controller
    {
      
        private ContatoEmail _gerenciarEmail;
        public HomeController(ContatoEmail gerenciarEmail)
        {
            _gerenciarEmail = gerenciarEmail;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }
        public IActionResult Contato()
        {
            return View();
        }


        public IActionResult ContatoAcao()
        {
            try
            {
                Contato contato = new Contato();
                contato.Nome = HttpContext.Request.Form["nome"];
                contato.Email = HttpContext.Request.Form["email"];
                contato.Telefone = HttpContext.Request.Form["telefone"];
                contato.Texto = HttpContext.Request.Form["texto"];

                var listaMensagens = new List<ValidationResult>();
                var Contexto = new ValidationContext(contato);
                bool isValid = Validator.TryValidateObject(contato, Contexto, listaMensagens, true);



                if (isValid)
                {

                    ContatoEmail.EnviarContatoPorEmail(contato);

                    ViewData["MSG_S"] = "Mansagem de contato enviado com suceso!";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var texto in listaMensagens)
                    {
                        sb.Append(texto.ErrorMessage + "<br />");
                    }
                    ViewData["MSG_E"] = sb.ToString();
                    ViewData["CONTATO"] = contato;
                }
            }
            catch (Exception e)
            {
                ViewData["MSG_E"] = "Opps! Tivemos um erro Tente novamente mais tarde!";
                // TODO - implementar Log
            }



            return View("CONTATO");


        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
