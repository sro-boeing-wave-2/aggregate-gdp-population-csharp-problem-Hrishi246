using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AggregateGDPPopulation
{
    public class Aggregate
    {
        public float GDP_2012 { get; set; }
        public float POPULATION_2012 { get; set; }
    }
    public class Program
    {
        public void solution()
        {
            string dataFilepath = @"../../../../AggregateGDPPopulation/data/datafile.csv";
            string mappinFilepath = @"../../../../AggregateGDPPopulation/data/mapping.json";
            string[] InputData = File.ReadAllLines(dataFilepath, Encoding.UTF8);


            JObject mapper = JObject.Parse(File.ReadAllText(mappinFilepath, Encoding.UTF8));
            //Console.WriteLine(mapper.GetValue("India"));
            //string mappingstring = mapper.ToString();

            //Dictionary<string, string> countryContinetMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(mappingstring);
            Dictionary<string, Aggregate> output = new Dictionary<string, Aggregate>();


            string[] RowOfData;
            string header = InputData[0];
            string[] headers = header.Replace("\"", string.Empty).Trim().Split(',');



            int IndexOfCountry = Array.IndexOf(headers, "Country Name");
            int GDP = Array.IndexOf(headers, "GDP Billions (USD) 2012");
            int POPULATION = Array.IndexOf(headers, "Population (Millions) 2012");
            // Console.WriteLine("{0} {1} {2}", IndexOfCountry, GDP, POPULATION);




            for (int i = 1; i < InputData.Length - 1; i++)
            {
                RowOfData = InputData[i].Replace("\"", string.Empty).Trim().Split(',');
                //Console.WriteLine(RowOfData[0]);

                //countryContinetMap[RowOfData[IndexOfCountry]]

                try//if (output.ContainsKey(countryContinetMap[RowOfData[IndexOfCountry]]) == true)
                {
                    output[mapper.GetValue(RowOfData[IndexOfCountry]).ToString()].POPULATION_2012 += float.Parse(RowOfData[GDP]);
                    output[mapper.GetValue(RowOfData[IndexOfCountry]).ToString()].GDP_2012 += float.Parse(RowOfData[POPULATION]);
                }

                catch (Exception)
                {
                    Aggregate agr = new Aggregate() { POPULATION_2012 = float.Parse(RowOfData[POPULATION]), GDP_2012 = float.Parse(RowOfData[GDP]) };
                    output.Add(mapper.GetValue(RowOfData[IndexOfCountry]).ToString(), agr);

                }



                //Console.WriteLine(RowOfData[GDP]);
                //Console.WriteLine(RowOfData[POPULATION]);
            }
            var outputJsonString = JsonConvert.SerializeObject(output);
            //Console.WriteLine(outputJsonString);
           
            try
            {
                File.WriteAllText("D:/workspace/C# Assignments/aggregate-gdp-population-csharp-problem-Hrishi246/AggregateGDPPopulation/data/output.json", outputJsonString);
            }
            catch (Exception)
            {
                Console.WriteLine("errrrrrrrror");
            }
           
        }


    }









}

