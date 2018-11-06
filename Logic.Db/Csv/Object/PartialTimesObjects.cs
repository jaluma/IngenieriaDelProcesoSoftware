using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Dto;

namespace Logic.Db.Csv.Object {
    public class PartialTimesObjects : CsvObject {

        public int Dorsal { get; set; }

        public long[] Times { get; set; }
    }
}
