using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace FE_GEM
{
    public class Entidades
    {
        public Entidades() { }
    }
    #region Respuesta
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class EnvelopeRespuesta
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public BodyRespuesta body { get; set; }
        }
        [XmlRoot(ElementName = "Body", Namespace = "")]
        public class BodyRespuesta
        {
            [XmlElement(ElementName = "nuevaSolicitudBatchSHA2Response", Namespace = "http://service.ceroPapel.dgsei.edomex.gob.mx/")]
            public nuevaSolicitudBatchSHA2Respuesta nuevaSolicitudBatchSHA2Respuesta { get; set; }

        [XmlElement(ElementName = "obtenerEvidenciaXmlSHA2Response", Namespace = "http://service.ceroPapel.dgsei.edomex.gob.mx/")]
        public obtenerEvidenciaXmlSHA2Response obtenerEvidenciaXmlSHA2Response { get; set; }



    }

    [XmlRoot(ElementName = "obtenerEvidenciaXmlSHA2Response", Namespace = "http://service.ceroPapel.dgsei.edomex.gob.mx/")]
    public class obtenerEvidenciaXmlSHA2Response
    {
        [XmlElement(ElementName = "return", Namespace = "")]
        public string Return { get; set; }
    }


    [XmlRoot(ElementName = "Return", Namespace = "http://service.ceroPapel.dgsei.edomex.gob.mx/")]
    public class Return
    {
        [XmlElement(ElementName = "contenido", Namespace = "")]
        public string contenido { get; set; }
        [XmlElement(ElementName = "nombre", Namespace = "")]
        public string nombre { get; set; }
    }






    [XmlRoot(ElementName = "nuevaSolicitudBatchSHA2Response", Namespace = "http://service.ceroPapel.dgsei.edomex.gob.mx/")]
        public class nuevaSolicitudBatchSHA2Respuesta
        {
            [XmlElement(ElementName = "return", Namespace = "")]
            public string Return { get; set; }
        }




    #endregion

    #region EntidadSolicitud
    [Serializable]
    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class EnvelopeEntidad
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public BodyEntidad body { get; set; }
    }



    [Serializable]
    [XmlRoot(ElementName = "Body", Namespace = "")]
    public class BodyEntidad
    {
        [XmlElement(ElementName = "nuevaSolicitudBatchSHA2", Namespace = "http://service.ceroPapel.dgsei.edomex.gob.mx/")]
        public NuevaSolicitud NuevaSolicitud { get; set; }
        [XmlElement(ElementName = "obtenerEvidenciaXmlSHA2", Namespace = "http://service.ceroPapel.dgsei.edomex.gob.mx/")]
        public obtenerEvidenciaXmlSHA2 obtenerEvidenciaXmlSHA2 { get; set; }
    }



    [Serializable]
    [XmlRoot(ElementName = "nuevaSolicitudBatchSHA2", Namespace = "http://service.ceroPapel.dgsei.edomex.gob.mx/")]
    public class NuevaSolicitud
    {
        [XmlElement(ElementName = "solicitud", Namespace = "")]
        public Solicitud Solicitud { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "obtenerEvidenciaXmlSHA2", Namespace = "http://service.ceroPapel.dgsei.edomex.gob.mx/")]

   
    public class obtenerEvidenciaXmlSHA2
    {
        [XmlElement(ElementName = "hashSolicitud", Namespace = "")]
        public string hashSolicitud { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "Solicitud", Namespace = "")]
    public class Solicitud
    {
        [XmlElement(ElementName = "asunto", Namespace = "", IsNullable = true)]
        public string Asunto { get; set; }
        [XmlElement(ElementName = "documento", Namespace = "")]
        public Documento Documento { get; set; }
        [XmlElement(ElementName = "fechaSolicitud", Namespace = "")]
        public DateTime FechaSolicitud { get; set; }
        [XmlElement(ElementName = "fechaVencimiento", Namespace = "")]
        public DateTime FechaVencimiento { get; set; }
        [XmlElement(ElementName = "firmantes", Namespace = "", IsNullable = true)]
        public string Firmantes { get; set; }
        [XmlElement(ElementName = "hash", Namespace = "", IsNullable = true)]
        public string Hash { get; set; }
        [XmlArray("listaFirmantes")]  
        [XmlArrayItem("item")]
        public string[] ListaFirmantes { get; set; }
        [XmlElement(ElementName = "listaOrdenada", Namespace = "")]
        public bool ListaOrdenada { get; set; }
        [XmlElement(ElementName = "sistemaSolicitante", Namespace = "", IsNullable = true)]
        public string SistemaSolicitante { get; set; }
        [XmlElement(ElementName = "solicitante", Namespace = "", IsNullable = true)]
        public string Solicitante { get; set; }
        [XmlElement(ElementName = "tipoDocumento", Namespace = "")]
        public int TipoDocumento { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "documento", Namespace = "")]
    public class Documento
    {
        [XmlElement(ElementName = "contenido", Namespace = "", IsNullable = true, DataType = "base64Binary")]
        public byte[] Contenido { get; set; }
        [XmlElement(ElementName = "hash", Namespace = "", IsNullable = true)]
        public string Hash { get; set; }
        [XmlElement(ElementName = "nombre", Namespace = "", IsNullable = true)]
        public string Nombre { get; set; }
    }

    #endregion


    #region firmaElectronicaXML

    [Serializable]
    [XmlRoot(ElementName = "signinginfo")]
    public class signinginfo
    {
        [XmlElement(ElementName = "document")]
        public string document { get; set; }

        [XmlElement(ElementName = "ca")]
        public ca ca { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "ca")]
    public class ca
    {
        [XmlElement(ElementName = "signatory")]
        public signatory signatory { get; set; }

    }

    [Serializable]
    [XmlRoot(ElementName = "signatory")]

    public class signatory
    {
        [XmlElement(ElementName = "signature")]
        public string signature { get; set; }
        //public string ocsp { get; set; }
        //public string tsp { get; set; }

    }




    #endregion


}