using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Csv.Object;

namespace Logic.Db {
    public abstract class CsvLoader {

        private readonly string[] _fileNamesCsv;

        public CsvObject Returned { get; private set; }

        protected CsvLoader(string[] fileNames) {
            this._fileNamesCsv = fileNames;
            LoadFiles();
        }

        private void LoadFiles() {
            foreach (var csv in _fileNamesCsv) {
                LoadData(csv);
            }
        }

        private void LoadData(string filename) {
            var query = File.ReadLines(filename)
                .SelectMany(line => line.Split(';'))
                .Where(csvLine => !string.IsNullOrWhiteSpace(csvLine))
                .Select(csvLine => csvLine.Split(','));

            Returned = CreateObjects(query);
        }

        protected abstract CsvObject CreateObjects(IEnumerable<String[]> lines);

    }
}
