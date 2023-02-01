using Commons.Xml.Relaxng;
using XSD_DarioDrazenovicIIS.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace XSD_DarioDrazenovicIIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EsportsTeamController : ControllerBase
    {
        [HttpPost("validate-xsd")]
        public bool ValidateXsd(XmlElement esportsTeamArray)
        {
            try
            {
                bool fail = false;
                // It creates an XML document object from the esportsTeamArray input.
                XmlDocument documentXML = esportsTeamArray.OwnerDocument;
                documentXML.AppendChild(esportsTeamArray);

                // It sets up an XML schema set, adds the XSD schema file to it, and creates an XmlReader from the XML document.
                XmlSchemaSet schemaSet = new XmlSchemaSet();
                schemaSet.Add("http://schemas.datacontract.org/2004/07/XSD-DarioDrazenovicIIS.Model", Path.GetFullPath("esportsTeam_schema.xsd"));
                XmlReader xmlReader = new XmlNodeReader(documentXML);
                XDocument document = XDocument.Load(xmlReader);

                document.Validate(schemaSet, (o, e) =>
                {
                    Console.WriteLine("{0}", e.Message);
                    fail = true;
                });

                if (!fail)
                {
                    // The method serializes the XDocument into an object of type EsportsTeamArray using the DataContractSerializer class.
                    DataContractSerializer deserialization = new DataContractSerializer(typeof(EsportsTeamArray));
                    MemoryStream streamXml = new MemoryStream();
                    document.Save(streamXml);
                    // Sets the position of the stream to the beginning of the stream,
                    // so that the ReadObject method of the DataContractSerializer can read the entire serialized data stored in the MemoryStream.
                    streamXml.Position = 0;

                    EsportsTeamArray esportsT = (EsportsTeamArray)deserialization.ReadObject(streamXml);

                    foreach (var item in esportsT.EsportsTeamList)
                    {
                        Startup.EsportsTeamArray.EsportsTeamList.Add(item);
                    }


                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        [HttpPost("validate-rng")]
        public bool ValidateWithRng(XmlElement esportTeamArray)
        {
            XmlDocument documentXML = esportTeamArray.OwnerDocument;
            documentXML.AppendChild(esportTeamArray);
            var errors = false;
            XmlReader xmlReader = new XmlNodeReader(documentXML);
            XmlReader rng = XmlReader.Create(Path.GetFullPath("esportsTeam_rng.rng"));
            using (var reader = new RelaxngValidatingReader(xmlReader, rng))
            {
                reader.InvalidNodeFound += (source, message) =>
                {
                    Console.WriteLine("Error: " + message);
                    errors = true;
                    return true;
                };
                XDocument doc = XDocument.Load(reader);
            }

            if (errors)
            {
                return false;
            }
            else
            {
                DataContractSerializer deserijalizacija = new DataContractSerializer(typeof(EsportsTeamArray));
                MemoryStream streamXml = new MemoryStream();
                documentXML.Save(streamXml);
                streamXml.Position = 0;
                EsportsTeamArray sa = (EsportsTeamArray)deserijalizacija.ReadObject(streamXml);

                foreach (var item in sa.EsportsTeamList)
                {
                    Startup.EsportsTeamArray.EsportsTeamList.Add(item);
                }
                return true;
            }

        }



        /*private List<EsportsTeamArray> ListOfTeams;

        public EsportsTeamController(List<EsportsTeamArray> listOfTeams)
        {
            ListOfTeams = listOfTeams;
        }

        public List<EsportsTeamArray> Get()
        {
            return ListOfTeams;
        }


        [HttpPost]
        public void Post(EsportsTeamArray newTeam)
        {
            if (newTeam != null)
            {
                ListOfTeams.Add(newTeam);
                HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }*/
    }
}