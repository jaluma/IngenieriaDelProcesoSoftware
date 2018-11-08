using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Logic.Db.Csv.Object;

namespace Logic.Db.Csv {
    public abstract class CsvLoader {

        private readonly string[] _fileNamesCsv;

        public IEnumerable<CsvObject> Returned { get; private set; }

        protected CsvLoader(string[] fileNames) {
            _fileNamesCsv = fileNames;
            LoadFiles();
        }

        private void LoadFiles() {
            foreach (var csv in _fileNamesCsv) {
                LoadData(csv);
            }
        }

        private void LoadData(string filename) {
            if (filename != null && !filename.Equals("")) {
                var query = File.ReadLines(filename)
                    .SelectMany(line => line.Split('\n'))
                    .Where(csvLine => !string.IsNullOrWhiteSpace(csvLine))
                    .Select(csvLine => csvLine.Split(','));

                Returned = CreateObjects(query);
            }

        }

        protected abstract IEnumerable<CsvObject> CreateObjects(IEnumerable<String[]> lines);

    }
}
