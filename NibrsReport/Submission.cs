using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NibrsModels.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NibrsModels.Extensions;

namespace NibrsModels.NibrsReport
{
    /// <summary>
    ///     In the NibrsReport namespace, all XML elements that must be serialized must be given public access in order for
    ///     NibrsSerializer to print them accordingly. This also gives full freedom for NibrsReportBuilder to build reports
    ///     however it sees fit.
    /// </summary>
    [BsonIgnoreExtraElements]
    [XmlRoot("Submission", Namespace = Namespaces.cjisNibrs)]
    public class Submission : INibrsSerializable
    {
        [BsonIgnore]
        [XmlIgnore]
        [JsonIgnore]
        private static readonly NibrsSerializer.NibrsSerializer Serializer =
            new NibrsSerializer.NibrsSerializer(typeof(Submission));

        [XmlElement("MessageMetadata", Namespace = Namespaces.cjis)]
        public MessageMetadata MessageMetadata = new MessageMetadata();

        [BsonIgnore] [XmlIgnore] [JsonIgnore] public List<Report> RejectedReports = new List<Report>();


        [XmlElement("Report")] public List<Report> Reports = new List<Report>();

        [XmlAttribute("schemaLocation", Namespace = Namespaces.xsi)]
        //[JsonIgnore]
        public string XsiSchemaLocation = Constants.Misc.schemaLocation;

        public Submission()
        {
            //Id = ObjectId.GenerateNewId();
        }


        public Submission(string runnumber, string environment, string submissionDateInMMDDYYYY)
        {

            Runnumber = runnumber;
            Environment = environment;
            SubmissionDate = submissionDateInMMDDYYYY.IsNullBlankOrEmpty() ?   (DateTime?)null : Convert.ToDateTime(DateTime.ParseExact(submissionDateInMMDDYYYY, "mmddyyyy", DateTimeFormatInfo.InvariantInfo).ToString(@"mm\/dd\/yyyy"));
        }

        public Submission(params Report[] reports)
        {
            Id = ObjectId.GenerateNewId();

            foreach (var r in reports) Reports.Add(r);
        }

        [XmlIgnore]
        [JsonConverter(typeof(ObjectIdConverter))]
        // Removed Bson Ignore to save the value in the MonogDB. While deserilizing using JsonDeserilzer the Json value from Json string 
        // will  replace the NewId, if created by the getter method.
        public ObjectId Id
        {
            get
            {

                _id = _id == ObjectId.Empty ? ObjectId.GenerateNewId() : _id;

                return _id;
            }

            set
            {
                _id = value;
            }
        }


        [XmlIgnore]
        [BsonIgnore]
        [JsonIgnore]
        private ObjectId _id;


        [BsonElement]
        [XmlIgnore] public string Runnumber { get; set; }


        [BsonElement]
        [XmlIgnore] public DateTime? SubmissionDate { get; set; }

        [BsonElement]
        [XmlIgnore] public string Environment { get; set; }

        [BsonElement]
        [XmlIgnore]
        public bool IsNibrsReportable
        {
            get
            {

                if (Environment == "C")
                {
                    return true;
                }

                return false;

            }
        }

        [BsonIgnore]
        [XmlIgnore]
        [JsonIgnore]
        public string Xml
        {
            get { return Serializer.Serialize(this); }

        }



        #region HelperProperties 

        /// Note: Below Helper Properties don't intend or useful for Ucr Reports.


        [XmlIgnore]
        [BsonElement]
        public string ReportingCategory => Reports[0].Header.NibrsReportCategoryCode;


        [XmlIgnore]
        [BsonElement]
        public string Ori => Reports[0].Header.ReportingAgency.OrgAugmentation.OrgOriId.Id;



        [XmlIgnore]
        [BsonElement]
        public string IncidentNumber => Reports[0]?.Incident?.ActivityId?.Id;

        #endregion


        public static Submission Deserialize(string filepath)
        {
            // Retrieve the XML file
            var fileInfo = new FileInfo(filepath);
            var xmlFile = new FileStream(filepath, FileMode.Open);
            var xmlReader = XmlReader.Create(xmlFile);

            // Deserialize the XML file
            Submission sub;
            try
            {
                //When deserializing, associations and persons do not have the full context of their complex elements.
                //Deserialization of XML nodes does not cross check the other XML nodes to give the original full context of the data involved;
                //It will create objects for only what is present within the node being deserialized.

                //For example, if you deseriablize an OffenseVictimAssociation you only have the context of the IDs of the associated offense and victim.
                //Further, you would not have the full context of the victim either because the victim is composed of a person, so you need to use the victim's ID
                //and retrieve the person data for that victim.

                sub = (Submission)Serializer.Deserialize(xmlReader);
                foreach (var report in sub.Reports) report.RebuildCrossReferencedRelationships();
            }
            catch (Exception e)
            {
                throw new Exception("There was an error deserializing a submission: " + fileInfo.Name, e);
            }

            xmlFile.Flush();

            xmlFile.Dispose();


            return sub;
        }

        [JsonIgnore]
        [BsonIgnore]
        [XmlIgnore]
        public string JsonString
        {
            get
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                };

                return JsonConvert.SerializeObject(this);
            }
        }

        public static Submission DeserializeJson(string filepath)
        {
            var jsonFile = new FileStream(filepath, FileMode.Open);
            var streamReader = new StreamReader(jsonFile, new UTF8Encoding());
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                };
                string json = streamReader.ReadToEnd();
                var submission = JsonConvert.DeserializeObject<Submission>(json);

                return submission;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                streamReader.Dispose();
                jsonFile.Close();
            }

        }


    }
}