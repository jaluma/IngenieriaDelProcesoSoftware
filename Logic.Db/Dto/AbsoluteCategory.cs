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

            if (CategoryF.MaxAge > 100 && CategoryM.MaxAge>100)

                return Name + "_" + CategoryF.Gender + " (" + CategoryF.MinAge + "-SIN LIMITE) "
            + Name + "_" + CategoryM.Gender + " (" + CategoryM.MinAge + "-SIN LIMITE) ";

            if (CategoryF.MinAge == CategoryF.MaxAge && CategoryM.MinAge == CategoryM.MaxAge)

                return Name + "_" + CategoryF.Gender + " (" + CategoryF.MinAge + ") "
                     + Name + "_" + CategoryM.Gender + " (" + CategoryM.MinAge + ") ";

            if (CategoryF.MinAge == CategoryF.MaxAge)

                return Name + "_" + CategoryF.Gender + " (" + CategoryF.MinAge + ") "
                   + Name + "_" + CategoryM.Gender + " (" + CategoryM.MinAge + "-" + CategoryM.MaxAge + ") ";

            if (CategoryM.MinAge == CategoryM.MaxAge)

                return Name + "_" + CategoryF.Gender + " (" + CategoryF.MinAge + "-" + CategoryF.MaxAge + ") "
                     + Name + "_" + CategoryM.Gender + " (" + CategoryM.MinAge + ") ";

            if (CategoryF.MaxAge>100)

           return Name+ "_"+ CategoryF.Gender+ " (" + CategoryF.MinAge+ "-SIN LIMITE) "
            + Name + "_" + CategoryM.Gender + " (" + CategoryM.MinAge + "-" + CategoryM.MaxAge + ") ";

            if (CategoryM.MaxAge > 100)

                return Name + "_" + CategoryF.Gender + " (" + CategoryF.MinAge + "-" + CategoryF.MaxAge + ") "
                     + Name + "_" + CategoryM.Gender + " (" + CategoryM.MinAge +"-SIN LIMITE) ";

           else

                return Name + "_" + CategoryF.Gender + " (" + CategoryF.MinAge + "-" + CategoryF.MaxAge + ") "
                     + Name + "_" + CategoryM.Gender + " (" + CategoryM.MinAge + "-" + CategoryM.MaxAge + ") ";
        }
    }
}
