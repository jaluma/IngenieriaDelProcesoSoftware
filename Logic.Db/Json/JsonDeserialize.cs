using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Dto;
using Newtonsoft.Json;

namespace Logic.Db.Json {
    public class JsonDeserialize<T> {

        public const string DefaultCategoriesFilename = "DefaultCategories.json";

        private readonly string _fileName;
        private readonly JsonSerializer _serializer;

        public JsonDeserialize(string fileName) {
            _fileName = fileName;
            _serializer = new JsonSerializer();
            File.WriteAllBytes(fileName, Properties.Resources.DefaultCategories);
        }

        public JsonDeserialize(string fileName, byte[] file) {
            _fileName = fileName;
            _serializer = new JsonSerializer();
            File.WriteAllBytes(fileName, file);
        }

        public IEnumerable<T> ListJson() {
            using (StreamReader file = File.OpenText(_fileName)) {
                return JsonConvert.DeserializeObject<List<T>>(file.ReadToEnd());
            }
        }

        public void Serialize(IEnumerable<T> list) {
            using (StreamWriter file = File.CreateText(_fileName)) {
                _serializer.Serialize(file, list);
            }
        }
    }
}
