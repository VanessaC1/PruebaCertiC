using System; 
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Web;
using System.Web.Http;
using System.Web.WebPages;
using DAL;
using Microsoft.AspNetCore.Mvc;

namespace PruebaCertiC.Controllers
{ 
    public class ToWords
    {
        int dNum;
        string NumberToDollarsResult;
    }
    public class LlamadoController : ApiController
    {

        // GET: api/Llamado = Recuperar Datos
        /* public IEnumerable<Lllamado> Get()
         {
             using (PruebaCertiEntities db = new PruebaCertiEntities())
             {
                 return db.Lllamado.ToList();
             }
         }*/

        // GET: api/Llamado/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Llamado = Insertar

        public string Post (Models.ModelRequestWords objRequest)

        {
            string objresponse = string.Empty;
            WserviceWord.NumberConversionSoapTypeClient ws = new WserviceWord.NumberConversionSoapTypeClient(); 
            if (objRequest.metodo == "NumberToWords")
            {
                objresponse = ws.NumberToWords(objRequest.valor);
            }

            if (objRequest.metodo == "NumberToDollars")
            {
                objresponse = ws.NumberToDollars(objRequest.valor);
            }

            if (string.IsNullOrEmpty(objresponse))
            { 
                    objresponse = "Los datos ingresados son incorrectos"; 
            }
             
            HttpResponseMessage msg = null;
            try
            {
                using (PruebaCertiEntities db = new PruebaCertiEntities())
                {
                    db.Bitacora_CertiC.Add(new Bitacora_CertiC() { fecha = DateTime.Now, metodo = objRequest.metodo });
                    db.SaveChanges();
                    msg = Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {

                msg = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
          

            return objresponse;
        }

        // PUT: api/Llamado/5 = Actualizar
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Llamado/5 = Eliminar
        public void Delete(int id)
        {
        }
    }
}
