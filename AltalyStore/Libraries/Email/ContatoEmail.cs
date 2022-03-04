using AltalyStore.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AltalyStore.Libraries.Email
{
    public class ContatoEmail
    {
       
        public static void EnviarContatoPorEmail(Contato contato)
        {
            
             //SMTP - Servidor que vai enviar a mensagm.
     
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(" abcde@gmail.com", "gbgbdfvbdf ");
            smtp.EnableSsl = true;
            

            string corpoMsg = string.Format("<h2>Contato - Make-Home</h2>" +
                "<b>Nome: </b> {0} <br />" +
                "<b>E-mail: </b> {1} <br />" +
                "<b>Texto: </b> {2} <br />" +
                "<br /> E-mail enviado automaticamente do site Make-Home.",
                contato.Nome,
                contato.Email,
                contato.Telefone,
                contato.Texto
                );


            /*
             * Mailmessage ---> Construir a mensagem 
             */
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(" abcde@gmail.com");
            mensagem.To.Add("abcde@gmail.com ");
            mensagem.Subject = "Contato - MakeHome - E-mail: " + contato.Email;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //Enviar Mensagem via SMTP
            smtp.Send(mensagem);
        }
    }
}
