using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Erefinance.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Erefinance.Controllers
{


    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }




        public IActionResult Index(string userName, string userId, string brCode, string userType, string brName, string HardWare_BrId)
        {

            try
            {

                //throw new Exception();
                if (userName != null)
                {
                    //string sessValue = HttpContext.Session.GetString("userName");

                    //if (sessValue == null)
                    //{

                    //    return Redirect("http://192.168.0.127/sso/?msg=SessionOut");
                    //}
                    userName = Decrypt(userName);
                    userId = Decrypt(userId);
                    brCode = Decrypt(brCode);
                    userType = Decrypt(userType);
                    brName = Decrypt(brName);
                    HardWare_BrId = Decrypt(HardWare_BrId);

                    HttpContext.Session.SetString("userName", userName);
                    HttpContext.Session.SetString("userId", userId);
                    HttpContext.Session.SetString("brCode", brCode);
                    HttpContext.Session.SetString("userType", userType);
                    HttpContext.Session.SetString("brName", brName);
                    HttpContext.Session.SetString("HardWare_BrId", HardWare_BrId);

                }
                else
                { 
                    return Redirect("http://192.168.0.127/sso/?msg=notLoggedin"); 
                }
                return View();
            }
            catch (Exception ex)
            {
                return Redirect("http://192.168.0.127/sso/?msg=nullError"+ex);
            }
            
            //return View();
        }

        private string Decrypt(string cipherText)
        {
            try
            {
                string EncryptionKey = "MAKV2SPBNI657328B";
                cipherText = cipherText.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return cipherText;
            }
            catch (Exception ex)
            {

                Redirect("http://192.168.0.127/sso/?mes=decryptError"+ex);
                return "";

            }

        }

        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return Redirect("http://192.168.0.127/sso/");
        }
        /*
        public IActionResult Index()
        {
            return View();
        }
        */
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
