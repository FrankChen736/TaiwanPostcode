﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Postcode
{
    public class PostcodeImporter : IPostcodeImporter
    {

        protected readonly string[] CityKeyWord = new string[] { "市", "縣", "島", "臺" };
        protected readonly string[] AreaKeyWord = new string[] { "鄉", "鎮", "市", "區", "臺", "島" };

        /// <summary>
        /// 匯入資料
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task ImportAsync(string fileName)
        {
            var originData = ParseXMLData(fileName);

            var targetData = ParseOriginalData(originData);



        }

        protected virtual List<OriginalPostcode> ParseXMLData(string fileName)
        {
            return
                 XDocument
                .Load(fileName)
                .Descendants("Xml_10510")
                .Select(m => new
                        OriginalPostcode
                {
                    postCode = m.Element("欄位1").Value,
                    area = m.Element("欄位4").Value,
                    road = m.Element("欄位2").Value,
                    number = m.Element("欄位3").Value,
                }).ToList();

        }

        protected virtual List<TargetPostcode> ParseOriginalData(List<OriginalPostcode> originalData)
        {
            var targetData = new List<TargetPostcode>();

            originalData.ForEach(data =>
            {
                TargetPostcode rec = new TargetPostcode();
                rec.city = SplitCity(data.area);
                rec.area = SplitArea(data.area);
                targetData.Add(rec);

            });

            Debug.WriteLine($"城市空白筆數 {targetData.Where(m => (m.city ?? "") == string.Empty).Count()}");
            Debug.WriteLine(targetData.Select(m => m.city).Distinct().Aggregate((cur, nex) => cur + Environment.NewLine + nex));
            
            return targetData;
        }

        /// <summary>
        /// 切區城市
        /// </summary>
        /// <param name="str"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected virtual string SplitCity(string str)
        {
            return str.Substring(0, 3);
        }

        /// <summary>
        /// 切割區域
        /// </summary>
        /// <param name="str"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected virtual string SplitArea(string str)
        {
            return str.Substring(3, str.Length - 3);
        }
    }
}
