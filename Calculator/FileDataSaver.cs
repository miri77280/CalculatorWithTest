using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator
{
    public class FileDataSaver : IDataSaver
    {
        private string lastStoredValue="";

        private void WriteToJsonFile(string data)
        {
            CalculationResult result = new CalculationResult() {Result=data,TimeStamp=DateTime.Today };
            JsonSerializer serializer = new JsonSerializer();
           
            serializer.NullValueHandling = NullValueHandling.Ignore;
            string filePath= System.IO.Path.GetFullPath(@"..\..\..\..\") +"data.json";
            var jsonData = System.IO.File.ReadAllText(filePath);
            var resultList = JsonConvert.DeserializeObject<List<CalculationResult>>(jsonData)
                                  ?? new List<CalculationResult>();
            resultList.Add(result);
            using (StreamWriter sw = new StreamWriter(filePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                try
                {
                 serializer.Serialize(writer, resultList);
                }

                catch (Exception ex)
                {
                    throw new Exception("error in saving data to file" + ex.Message);
                }

            }
            }
        public void SaveData(string data)
        {
            WriteToJsonFile(data);
            this.lastStoredValue = data;
        }
            public string RestoreData()
        {
            WriteToJsonFile(lastStoredValue);
            return this.lastStoredValue;
        }

       
    }
}
