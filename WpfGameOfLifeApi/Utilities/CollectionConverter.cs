using Bogus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Collections;
using System.IO;
using System.Text.Json;
using System.Diagnostics;
using System.Linq;

namespace GameOfLifeApi.Utilities
{
    public static class JsonFileUtils
    {   
        private static readonly JsonSerializerSettings _options
            = new() { NullValueHandling = NullValueHandling.Ignore };

        public static void PrettyWrite(object obj, string fileName)
        {
            var jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented, _options);
            File.WriteAllText(fileName, jsonString);
        }

        /// <summary>
        /// Leer de un archivo JSON y convertirlo a un objeto.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="dest"></param>
        public static void ReadFromFile(string filename, ref List<Person> dest)
        {
            if (File.Exists(filename))
            {
                using (StreamReader file = File.OpenText(filename))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    // Dado que se generan 100 individuos. Este se crea como un array en el archivo Json. 
                    // Por lo tanto, se debe leer como un array.

                    JArray _obj = (JArray)JToken.ReadFrom(reader);

                    // Por requerimiento, el tratamiento de los datos deben ser tratados como objetos.
                    // Donde cada individuo es un objeto.

                    dest = _obj.ToObject<List<Person>>()!;


                    // Dev info - Se puede usar para ver el contenido del archivo Json.
                    foreach (var item in dest)
                    {
                        Debug.WriteLine($"[DEBUG] Se ha leido el individuo con el nombre de '{item.Name} {item.LastName}' ({item.Age} años / actualmente {item.GetEmploymentState().ToLower()}) / via JsonFileUtils.");
                    }
                }
            }
            else return;
   
        }

        
    }
}
