using System.Xml.Serialization;
using NibrsModels.Constants;

namespace NibrsModels.NibrsReport.Substance
{
    [XmlRoot("SubstanceQuantityMeasure", Namespace = Namespaces.niemCore)]
    public class SubstanceQuantityMeasure
    {
        public SubstanceQuantityMeasure()
        {
        }

        public SubstanceQuantityMeasure(string decimalValue, string substanceUnitCode)
        {
            // Convert string to decimal value to match with NIBRS extraction spec 

            DecimalValue = decimalValue != null ?  decimal.TryParse(decimalValue, out decimal result) ? result.ToString() : null : null ;
                        
            SubstanceUnitCode = substanceUnitCode;
        }

        [XmlElement("MeasureDecimalValue", Namespace = Namespaces.niemCore)]
        public string DecimalValue { get; set; }

        [XmlElement("SubstanceUnitCode", Namespace = Namespaces.justice)]
        public string SubstanceUnitCode { get; set; }
    }
}