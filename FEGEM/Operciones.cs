using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using RestSharp;
using RestSharp.Authenticators;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Collections;
using System.Xml;

namespace FE_GEM
{
    public class Operciones
    {
        public Operciones() { }

       
        public firmaElec proxy(string usuario, string contraseña, string cuts, string llave, string info)
        {
            try
            {
                string firma = string.Empty;
                string hash = nuevaSolicitud(usuario, contraseña, cuts, llave, info);
                if (hash == "")
                {
                    throw new Exception("No se pudo obtener el hash");
                }
                else
                {
                    firma = obtenerFirma(usuario, contraseña, hash);
                    firmaElec obj = new firmaElec();
                    obj.hash = hash;
                    obj.firma = firma;
                    return obj;
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string nuevaSolicitud(string usuario, string contraseña,  string cuts, string llave, string info)
        {
            try
            {
                string dato = string.Empty;
                string respuesta = string.Empty;
                Documento doc = new Documento();
                byte[] bytes = Encoding.ASCII.GetBytes(info);
                doc.Contenido = bytes;
                doc.Nombre = info;
                Solicitud sol = new Solicitud();
                sol.Asunto = "Solicitud de firma electronica para Reconocimientos";
                sol.Documento = doc;
                sol.FechaSolicitud = DateTime.Now;
                sol.FechaVencimiento = DateTime.Now;
                sol.Firmantes = cuts;
                sol.ListaFirmantes = new string[] { cuts, llave };
                sol.ListaOrdenada = true;
                sol.TipoDocumento = 0;
                NuevaSolicitud nvSol = new NuevaSolicitud() { Solicitud = sol };
                BodyEntidad entidad = new BodyEntidad() { NuevaSolicitud = nvSol };
                EnvelopeEntidad env = new EnvelopeEntidad() { body = entidad };
                string xmlData = "";
                XmlSerializer seriali = new XmlSerializer(typeof(EnvelopeEntidad));
                var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                var settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;
                using (var stream = new StringWriter())
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    seriali.Serialize(writer, env, emptyNamespaces);
                    xmlData = stream.ToString();
                }
                //var client = new RestClient("http://qasistemas2.edomex.gob.mx/ceroPapel/webService");
                var client = new RestClient("http://qasistemas2.edomex.gob.mx/ceroPapel/webService");
                client.Authenticator = new HttpBasicAuthenticator(usuario, contraseña);

                var request = new RestRequest();
                request.AddHeader("content-type", "text/xml");
                request.AddParameter("text/xml", xmlData, ParameterType.RequestBody);
                IRestResponse response = client.Post(request);
                if (response.IsSuccessful)
                {
                    dato = response.Content;
                    using (StringReader sr = new StringReader(dato))
                    {
                        byte[] b = Encoding.ASCII.GetBytes(dato);
                        Stream stream = new MemoryStream(b);
                        XmlSerializer serializer = new XmlSerializer(typeof(EnvelopeRespuesta));
                        EnvelopeRespuesta envelope = (EnvelopeRespuesta)serializer.Deserialize(stream);
                        if (!string.IsNullOrEmpty(envelope.body.nuevaSolicitudBatchSHA2Respuesta.Return))
                        {
                            respuesta = envelope.body.nuevaSolicitudBatchSHA2Respuesta.Return;
                        }
                    }
                }

                else
                {
                    throw new Exception(response.Content);
                }
                return respuesta;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }
        }
        public string obtenerFirma(string usuario, string contraseña, string hash)
        {
            try
            {
                string dato = string.Empty;
                string respuesta = string.Empty;
                obtenerEvidenciaXmlSHA2 ontenerEvidenciaSha2 = new obtenerEvidenciaXmlSHA2() { hashSolicitud = hash };
                BodyEntidad entidad = new BodyEntidad() { obtenerEvidenciaXmlSHA2 = ontenerEvidenciaSha2 };
                EnvelopeEntidad env = new EnvelopeEntidad() { body = entidad };
                string xmlData = "";
                XmlSerializer seriali = new XmlSerializer(typeof(EnvelopeEntidad));
                var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                var settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;
                using (var stream = new StringWriter())
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    seriali.Serialize(writer, env, emptyNamespaces);
                    xmlData = stream.ToString();
                }
                var client = new RestClient("http://qasistemas2.edomex.gob.mx/ceroPapel/webService");
                client.Authenticator = new HttpBasicAuthenticator(usuario, contraseña);
                var request = new RestRequest();
                request.AddHeader("content-type", "text/xml");
                request.AddParameter("text/xml", xmlData, ParameterType.RequestBody);
                IRestResponse response = client.Post(request);
                if (response.IsSuccessful)
                {
                    dato = response.Content;
                        using (StringReader sr = new StringReader(dato))
                        {
                            byte[] b = Encoding.ASCII.GetBytes(dato);
                            Stream stream = new MemoryStream(b);
                            XmlSerializer serializer = new XmlSerializer(typeof(EnvelopeRespuesta));
                            EnvelopeRespuesta envelope = (EnvelopeRespuesta)serializer.Deserialize(stream);
                            if (!string.IsNullOrEmpty(envelope.body.obtenerEvidenciaXmlSHA2Response.Return))
                            {
                                string datoFirma = string.Empty;
                                byte[] myBase64ret2 = Convert.FromBase64String(envelope.body.obtenerEvidenciaXmlSHA2Response.Return);
                                string myStr = System.Text.Encoding.UTF8.GetString(myBase64ret2);
                                datoFirma=myStr;
                               
                                    using (StringReader srFirma = new StringReader(datoFirma))
                                    {
                                        byte[] b2 = Encoding.ASCII.GetBytes(datoFirma);
                                        Stream streamXmlFirma = new MemoryStream(b2);
                                        XmlSerializer xmlDesealizer = new XmlSerializer(typeof(signinginfo));
                                        signinginfo v_signinginfo = (signinginfo)xmlDesealizer.Deserialize(streamXmlFirma);
                                //return v_signinginfo.ca.signatory.signature;
                                return datoFirma;
                                    }
                            }
                            else
                            {
                                throw new Exception("La peticion obtenerEvidenciaXmlSHA2Response esta vacia.");
                            }
                        }
                }
                else
                {
                    throw new Exception("La solicitud a obtenerEvidenciaXmlSHA2 no se realizó con exito.");
                }
                  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
         
        }
    
    }
    
    public class firmaElec
    {
        public string hash { get; set; }
        public string firma { get; set; }

    }
}