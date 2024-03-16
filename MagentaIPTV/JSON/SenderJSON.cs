using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace MagentaIPTV
{
    public abstract class SenderJSON
    {
        public static void Serialize<T>(string destination, T obj, Formatting formatting = Formatting.Indented)
        {
            JsonSerializer serializer = new JsonSerializer()
            {
                Formatting = formatting
            };

            using (StreamWriter sw = new StreamWriter(destination))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, obj);
            }
        }

        public static async Task<T> Deserialize<T>(string pathToFile)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<T>((new StreamReader(pathToFile)).ReadToEnd()));
        }

        public static async Task<T> DeserializeString<T>(string stringValue)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(stringValue));
        }
    }
}
