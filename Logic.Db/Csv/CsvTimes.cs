using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Db.Csv.Object;

namespace Logic.Db.Csv
{
    public class CsvTimes : CsvLoader
    {
        private IList<PartialTimesObjects> _times;

        public CsvTimes(string[] fileNames) : base(fileNames) { }

        protected override IEnumerable<CsvObject> CreateObjects(IEnumerable<string[]> lines) {
            Errores = new List<string>();
            _times = new List<PartialTimesObjects>();
            IEnumerable<string[]> enumerable = lines as string[][] ?? lines.ToArray();
            for (var row = 0; row < enumerable.Count(); row++) {
                var times = new long[enumerable.ElementAt(row).Length - 2];
                for (var i = 2; i < enumerable.ElementAt(row).Length; i++)
                    try {
                        times[i - 2] = long.Parse(enumerable.ElementAt(row)[i]);
                    }
                    catch (FormatException) {
                        times[i - 2] = 0;
                    }

                try {
                    var partial = new PartialTimesObjects {
                        Dorsal = int.Parse(enumerable.ElementAt(row)[1]),
                        CompetitionId = int.Parse(enumerable.ElementAt(row)[0]),
                        Times = times
                    };

                    _times.Add(partial);
                }
                catch (FormatException) {
                    var s = "Error en el formato. \n";
                    Errores.Add($"Linea {row + 1}: {s}");
                }
            }

            return _times;
        }
    }
}
