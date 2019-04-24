using NiboOFX.Models.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace NiboOFX.Models.DAO
{
    [XmlRoot("BANKTRANLIST", IsNullable = false)]
    public class OFXList
    {
        [XmlElement("STMTTRN")]
        public List<OFX> OfxList { get; set; }


        public class OFX
        {
            //[XmlElement("STMTTRN")]
            //public int Id { get; set; }

            [XmlElement(ElementName="TRNTYPE")]
            public string IdTrnTypeString
            {
                get
                {
                    return IdTrnType.ToString();
                }
                set
                {
                    IdTrnType = (int)Enum.Parse(typeof(TrnTypeEnum), value.ToString());
                }
            }

            [XmlElement(ElementName="DTPOSTED")]
            public string DtPostedString
            {
                get
                {
                    return DtPosted.ToString();
                }
                set
                {
                    DtPosted = DateTime.ParseExact(value, "yyyyMMddHHmmss", null);
                }
            }

            [XmlElement(ElementName="TRNAMT")]
            public string TrNamtString
            {
                get
                {
                    return TrNamt.ToString();
                }
                set
                {
                    TrNamt = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
                }
            }

            [XmlIgnore]
            public int IdTrnType { get; set; }

            [XmlIgnore]
            public DateTime DtPosted { get; set; }

            [XmlIgnore]
            public decimal TrNamt { get; set; }

            [XmlElement(ElementName="FITID")]
            public string FitId { get; set; }

            [XmlElement(ElementName="CHECKNUM")]
            public string CheckNum { get; set; }

            [XmlElement(ElementName="MEMO")]
            public string Memo { get; set; }
        }
    }
}