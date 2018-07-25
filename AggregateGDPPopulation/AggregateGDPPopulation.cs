using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AggregateGDPPopulation
{
    public class Aggregate
    {
        public float GDP_2012 { get; set; }
        public float POPULATION_2012 { get; set; }
        public Aggregate()
        {
            GDP_2012 = 0;
            POPULATION_2012 = 0;
        }
    }
    public class fileProcessing
    {
        public static async Task<string> Reader(string filepath)
        {
            string content;
            using (StreamReader reader = new StreamReader(filepath))
            {
                content = await reader.ReadToEndAsync();
            }
            return content;
        }

        public static async Task Writer(Dictionary<string, Aggregate> finalsolution)
        {
            string outputlocation = @"../../../../AggregateGDPPopulation/data/output.json";
            string outputJsonString = JsonConvert.SerializeObject(finalsolution);
            using (StreamWriter writer = new StreamWriter(outputlocation))
            {
                await writer.WriteAsync(outputJsonString);
            }

        }

    }


    public class GDPaggregate
    {
        
        public static void addExistingContinenetInfo(Dictionary<string, Aggregate> output, string continent,string[] RowOfData, int IndexOfCountry, int GDP, int POPULATION)
        {
           
            {
                // Console.WriteLine(mapper.GetValue(RowOfData[IndexOfCountry].ToString()); 
                output[continent].POPULATION_2012 += float.Parse(RowOfData[GDP]);
                output[continent].GDP_2012 += float.Parse(RowOfData[POPULATION]);
            }

        }


        public static void addCurrentContinenetInfo(Dictionary<string, Aggregate> output, string continent, string[] RowOfData, int IndexOfCountry, int GDP, int POPULATION)
        {
            
            {
                Aggregate agr = new Aggregate() { POPULATION_2012 = float.Parse(RowOfData[POPULATION]), GDP_2012 = float.Parse(RowOfData[GDP]) };
                output.Add(continent, agr);

            }

        }


        public static async Task solution()
        {
            string dataFilepath = @"../../../../AggregateGDPPopulation/data/datafile.csv";
            string mappinFilepath = @"../../../../AggregateGDPPopulation/data/mapping.json";
            var readTask1 = fileProcessing.Reader(dataFilepath);
            var readTask2 = fileProcessing.Reader(mappinFilepath);
            await Task.WhenAll(readTask1, readTask2);
            string[] InputData = (readTask1.Result).Split('\n');

            JObject mapper = JObject.Parse(readTask2.Result);
            string header = InputData[0];
            string[] RowOfData;
            Dictionary<string, Aggregate> output = new Dictionary<string, Aggregate>();
            string[] headers = header.Replace("\"", string.Empty).Trim().Split(',');
            int IndexOfCountry = Array.IndexOf(headers, "Country Name");
            int GDP = Array.IndexOf(headers, "GDP Billions (USD) 2012");
            int POPULATION = Array.IndexOf(headers, "Population (Millions) 2012");
            for (int i = 1; i < InputData.Length - 2; i++)
            {
                RowOfData = InputData[i].Replace("\"", string.Empty).Trim().Split(',');
                
                string continent = mapper.GetValue(RowOfData[IndexOfCountry]).ToString();
                // Console.WriteLine(RowOfData[1]);

                try { 
                     if (output.ContainsKey(continent))
                     {
                        addExistingContinenetInfo(output,continent, RowOfData, IndexOfCountry, GDP,  POPULATION);
                     }
                    else { 
                            addCurrentContinenetInfo(output, continent, RowOfData, IndexOfCountry, GDP,  POPULATION);
                         }
                }

                catch {
                           Console.WriteLine("error");
                      }

            }

            await fileProcessing.Writer(output);
        }





    }






}