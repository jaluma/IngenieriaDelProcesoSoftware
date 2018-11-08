using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.Dto {
    public class RefundDto {
        public long competition_id;
        public DateTime date_refund;
        public double refund;



        public override string ToString() {

            return this.date_refund.ToShortDateString() + " - " + refund.ToString() + "%";

        }

    }
}
