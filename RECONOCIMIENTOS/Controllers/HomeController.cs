using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FE_GEM;

namespace RECONOCIMIENTOS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Firmar()
        {
            Operciones operciones = new Operciones();

            

            var hash= operciones.nuevaSolicitud("DesaUMB", "0000", "FOGJ931113HMCLRS03", "NA$4u2022", "C:\\CertaSha2\\prueba1.txt");
            var firma =operciones.obtenerFirma("DesaUMB", "0000", hash);
            //obtenerEvidenciaXmlSHA2 evidencia = new obtenerEvidenciaXmlSHA2();
            //var evi =evidencia.GetHashCode();
            return View();
        }
    }
}