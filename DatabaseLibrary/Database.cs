using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DatabaseLibrary
{
    public static class Database<T>
    {
        private static readonly string CurrentDir = @"C:\Users\public";

        public static void Initialize(string[] files)
        {
            Directory.CreateDirectory($@"{CurrentDir}\files");
            foreach (var file in files)
            {
                if (!File.Exists($@"{CurrentDir}\files\{file}.json"))
                {
                    File.Create($@"{CurrentDir}\files\{file}.json");
                }
            }
        }

        public static bool SaveData(T data, string filename)
        {
            DateTime currentDateTime = DateTime.Now;
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter($@"{CurrentDir}\files\{filename}.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, data);
            }
            DateTime fileLastWriteTime = new FileInfo($@"{CurrentDir}\files\{filename}.json").LastWriteTime;
            return currentDateTime < fileLastWriteTime;
        }
        
        public static T GetData(string filename)
        {
            FileStream fr = File.OpenRead($@"{CurrentDir}\files\{filename}.json");
            StreamReader sw = new StreamReader(fr);
            string jsonData = sw.ReadToEnd();
            sw.Close();
            fr.Close();
            T data = JsonConvert.DeserializeObject<T>(jsonData);
            return data;
        }
    }
}
