using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Csv.Object;

namespace Logic.Db.Csv {
    public class CsvTimes : CsvLoader {
        private IList<PartialTimesObjects> _times;

        public CsvTimes(string[] fileNames) : base(fileNames) {
        }
        protected override IEnumerable<CsvObject> CreateObjects(IEnumerable<string[]> lines) {
            _times = new List<PartialTimesObjects>();
            IEnumerable<string[]> enumerable = lines as string[][] ?? lines.ToArray();
            for (int row = 0; row < enumerable.Count(); row++) 
            {
                try {

                long[] times = new long[enumerable.ElementAt(row).Length - 2];
                for (int i = 2; i < enumerable.ElementAt(row).Length; i++) {
                    times[i-2] = long.Parse(enumerable.ElementAt(row)[i]);
                }
                    PartialTimesObjects partial = new PartialTimesObjects() {
                        Dorsal = int.Parse(enumerable.ElementAt(row)[1]),
                        CompetitionId = int.Parse(enumerable.ElementAt(row)[0]),
                        Times = times
                    };

                    _times.Add(partial);
                } catch (FormatException) { }
            }
            return _times;
        }
    }
}
