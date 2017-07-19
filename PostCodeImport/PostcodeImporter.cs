using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Postcode
{
    public class PostcodeImporter : IPostcodeImporter
    {

        public async Task ImportAsync(string fileName)
        {
            var originData = ParseXMLData(fileName);





        }

        private List<OriginalPostCost> ParseXMLData(string fileName)
        {
            return
                 XDocument
                .Load(fileName)
                .Descendants("Xml_10510")
                .Select(m => new
                        OriginalPostCost
                {
                    postCode = m.Element("欄位1").Value,
                    area = m.Element("欄位4").Value,
                    road = m.Element("欄位2").Value,
                    number = m.Element("欄位3").Value,
                }).ToList();

        }
    }
}
