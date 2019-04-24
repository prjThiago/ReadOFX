using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;
using NiboOFX.Models.DAO;

namespace NiboOFX.Tools
{
    public static class NiboTools
    {
        public static string SaveXml(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            //use LINQ TO GET ONLY THE LINES THAT WE WANT
            var tags = from line in File.ReadAllLines(path)
                       where line.Contains("<STMTTRN>") ||
                       line.Contains("<TRNTYPE>") ||
                       line.Contains("<DTPOSTED>") ||
                       line.Contains("<TRNAMT>") ||
                       line.Contains("<FITID>") ||
                       line.Contains("<CHECKNUM>") ||
                       line.Contains("<MEMO>")
                       select line;

            XDocument xml = new XDocument(new XDeclaration("1.0", "utf-8", null));

            XNamespace ns = "https://github.com/prjThiago/ReadOFX";

            xml.Add(new XElement("BANKTRANLIST", 
                new XAttribute(XNamespace.Xmlns + "ReadOFX", ns)
            ));

            XElement son = null;

            foreach (var l in tags)
            {
                if (l.IndexOf("<STMTTRN>") != -1)
                {
                    son = new XElement("STMTTRN");
                    xml.Root.Add(son);
                    continue;
                }

                var tagName = getTagName(l);
                var elSon = new XElement(tagName);
                elSon.Value = getTagValue(l);
                son.Add(elSon);
            }

            string xmlPath = Path.Combine(HttpContext.Current.Server.MapPath("/Files/XML"), Guid.NewGuid().ToString() + ".xml");            

            xml.Save(xmlPath);

            return xmlPath;
            //return el;
        }
        /// <summary>
        /// Get the Tag name to create an Xelement
        /// </summary>
        /// <param name="line">One line from the file</param>
        /// <returns></returns>
        private static string getTagName(string line)
        {
            int pos_init = line.IndexOf("<") + 1;
            int pos_end = line.IndexOf(">");
            pos_end = pos_end - pos_init;
            return line.Substring(pos_init, pos_end);
        }
        /// <summary>
        /// Get the value of the tag to put on the Xelement
        /// </summary>
        /// <param name="line">The line</param>
        /// <returns></returns>
        private static string getTagValue(string line)
        {
            int pos_init = line.IndexOf(">") + 1;
            string retValue = line.Substring(pos_init).Trim();
            if (retValue.IndexOf("[") != -1)
            {
                //date--lets get only the 8 date digits
                retValue = retValue.Substring(0, 14);
            }
            return retValue;
        }

        public static OFXList ReadOfx(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            XmlSerializer XmlToList = new XmlSerializer(typeof(OFXList));

            OFXList listOFX = new OFXList();

            //using (FileStream stream = File.OpenRead(path))
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                listOFX = (OFXList)XmlToList.Deserialize(stream);
            }

            return listOFX;
        }
    }
}