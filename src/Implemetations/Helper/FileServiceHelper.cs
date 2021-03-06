using Newtonsoft.Json;
using System.IO;
using System.Linq;


namespace Implementation.Helper
{
    public class FileServiceHelper
    {
        public FileServiceHelper()
        {

        }
        public static string FileFullPath(string path)
        {
           
            string fileFullFilePath= Directory.GetFiles(path).Where(a => a.EndsWith(".json") || a.Contains(".json")).FirstOrDefault();

            return fileFullFilePath;
        }

        public static T ParseFile<T>(string fileFullPath)
        {
            string content = File.ReadAllText(fileFullPath);
            return JsonConvert.DeserializeObject<T>(content);

           
        }



    }
}
