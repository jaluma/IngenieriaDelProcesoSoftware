using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.Dto {
    public class AbsoluteCategory {
        public long Id;
        public string Name;
        public CategoryDto CategoryM;
        public CategoryDto CategoryF;

        public override string ToString() {
            return Name.Replace('_', ' ');
        }
    }
}
