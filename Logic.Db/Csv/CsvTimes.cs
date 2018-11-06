using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Csv.Object;

namespace Logic.Db.Csv {
    public class CsvTimes : CsvLoader {
        private readonly PartialTimesObjects _times;

        public CsvTimes(string[] fileNames) : base(fileNames) {
            _times = new PartialTimesObjects();
        }
        protected override CsvObject CreateObjects(IEnumerable<string[]> lines) {
            foreach (string[] line in lines) {
                _times.Dorsal = int.Parse(line[0]);
                _times.Times = new int[line.Length - 1];
                for (int i = 1; i < line.Length; i++) {
                    _times.Times[i - 1] = int.Parse(line[i]);
                }
            }
            return _times;
        }
    }
}
