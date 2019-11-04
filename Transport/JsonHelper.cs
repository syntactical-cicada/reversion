using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TkMiddleware.Helpers
{
    public class JsonHelper
    {
        public HttpContent GetContent(dynamic obj)
        {
            var records = new List<string>();
            foreach (var data in obj.data)
            {
                records.Add(JsonConvert.SerializeObject(data));
            }

            var serializableObject = new TkSerializable() {
                version = obj.version,
                data = records
            };
            
            HttpContent content = new StringContent(JsonConvert.SerializeObject(serializableObject), Encoding.UTF8, "application/json");
            return content;
        }
    }
    internal class TkSerializable
    {
        public int version;
        public List<string> data;
    }
}