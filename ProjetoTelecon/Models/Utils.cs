using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjetoTelecon.Models
{
    public partial class Utils
    {
        public string Hash(string raw)
        {
            using (var algorith = SHA512.Create())
            {
                var hashedBytes = algorith.ComputeHash(Encoding.UTF8.GetBytes(raw));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public IDictionary<string, string> Upload(IFormFileCollection files, string name, string pasta = null)
        {
            try
            {
                IDictionary<string, string> savedFile = new Dictionary<string, string>();

                foreach (var file in files)
                {
                    var folderName = Path.Combine("wwwroot", "upload" + (pasta is null ? "" : "/" + pasta));
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                    var fileName = System.IO.Path.GetFileName(file.FileName);

                    string renameFile = name + "_" + Convert.ToString(Guid.NewGuid()) + "." + fileName.Split('.').Last();

                    if (fileName != "")
                        savedFile[file.Name] = renameFile;

                    var fullPath = Path.Combine(pathToSave, renameFile);
                    var dbPath = Path.Combine(folderName, fileName);

                    if (System.IO.File.Exists(renameFile))
                    {
                        System.IO.File.Delete(renameFile);
                    }

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return savedFile;
            }
            catch (Exception error)
            {
                throw;
            }
        }

        public bool RemoveFiles(string fileName, string pasta)
        {
            var folderName = Path.Combine("wwwroot", "upload" + (pasta is null ? "" : "/" + pasta));
            string newFullPath = Path.Combine(folderName, fileName);

            bool result = false;

            if (File.Exists(newFullPath))
            {
                File.Delete(newFullPath);

                result = true;
            }

            return result;
        }


        public bool RemoveFiles(string fileName)
        {
            var folderName = Path.Combine("wwwroot", "upload");
            string newFullPath = Path.Combine(folderName, fileName);

            bool result = false;

            if (File.Exists(newFullPath))
            {
                File.Delete(newFullPath);

                result = true;
            }

            return result;
        }
        //public string Slug(string text)
        //{
        //    text = text.ToLowerInvariant();

        //    text = Regex.Replace(text, @"\s", "-", RegexOptions.Compiled);
        //    text = Regex.Replace(text, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);
        //    text = text.Trim('-', '_');
        //    text = Regex.Replace(text, @"([-_]){2,}", "$1", RegexOptions.Compiled);

        //    return text;
        //}

        public string Slug(string txt)
        {
            byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            string str = Encoding.ASCII.GetString(bytes);

            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public void sendMail(string assunto, string mensagem, string email, string destinatario)
        {
            SmtpClient client = new SmtpClient();

            client.Host = "email-ssl.com.br";
            client.EnableSsl = true;
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("naoresponder@nacao.com", "ZAQ!2wsx");

            MailMessage mail = new MailMessage();
            mail.Sender = new MailAddress("naoresponder@nacao.com");
            mail.From = new MailAddress(email);

            if (!String.IsNullOrEmpty(destinatario))
            {
                mail.To.Add(new MailAddress(destinatario));
                //mail.To.Add(new MailAddress(email));
            }

            mail.Subject = assunto;
            mail.Body = mensagem;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            mail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            mail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            try
            {
                client.Send(mail);
            }
            catch (System.Exception erro)
            {
            }
            finally
            {
                mail = null;
            }
        }
    }
}
