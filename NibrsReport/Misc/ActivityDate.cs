using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using NibrsModels.Constants;
using System;
using System.Xml.Serialization;

namespace NibrsModels.NibrsReport.Misc
{
    /// <summary>
    ///     This class contains mechanisms that make its dateTime and date properties mutually exclusive
    ///     so that only one can get serialized at a time
    /// </summary>
    [XmlRoot("ActivityDate", Namespace = Namespaces.niemCore)]
    public class ActivityDate
    {
        private string _date;

        private string _dateTime;

        private DateTime? _realDateTime;

        public ActivityDate()
        {
        }

        public ActivityDate(string date)
        {
            Date = date;
        }

        public ActivityDate(string date, string time)
        {
            DateTime = date + "T" + time;
        }



        [XmlElement("DateTime", Namespace = Namespaces.niemCore)]
        public string DateTime
        {
            get { return _dateTime; }
            set
            {
                if (_date != null)
                    _date = null;
                _dateTime = value;



                if (_realDateTime == null)
                {
                    _realDateTime = System.DateTime.TryParse(value, out var dateTimeTemp) ? System.DateTime.SpecifyKind(dateTimeTemp,DateTimeKind.Local) : (DateTime?)null;
                    if (_realDateTime != null)
                    {
                        TimeZoneInfo tzf = TimeZoneInfo.Local;
                         
                        {
                            // if time is invalid due to daylight saving. adjust the time
                            _realDateTime = _realDateTime.Value.AddHours(1);
                        }
                    }
                }
            }
        }


        [XmlIgnore]
        [JsonIgnore] // stored in Mongo, so we can ignore JSON  as it will be computed  from either DateTime or Date when deserialized  from MongoDb.
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)] // get the value always in local time zone , tho it is stored in UTC format in MongoDb.
        public DateTime? RealDateTime
        {
            get { return _realDateTime; }
            set
            {
                _realDateTime = value;

                if (!string.IsNullOrWhiteSpace(DateTime)) DateTime = value?.ToString("yyyy-MM-ddTHH:mm:ss"); // store in 24 hour format

                if (!string.IsNullOrWhiteSpace(Date)) Date = value?.ToString("yyyy-MM-dd");
            }
        }



        [XmlElement("Date", Namespace = Namespaces.niemCore)]
        public string Date
        {
            get
            {
                if (_date != null)
                    return _date;
                return DateTime == null ? _dateTime?.Substring(0, _dateTime.IndexOf("T")) : null;
            }
            set
            {
                if (_dateTime != null)
                    _dateTime = null;
                _date = value;

                if (_realDateTime == null)
                {
                    _realDateTime = System.DateTime.TryParse(value, out var dateTimeTemp) ? System.DateTime.SpecifyKind(dateTimeTemp,DateTimeKind.Local) : (DateTime?)null;
                   
                }

            }
        }

        [BsonIgnore]
        [JsonIgnore]
        public string Time
        {
            get { return _dateTime.Substring(_dateTime.IndexOf("T") + 1); }
        }
    }
}