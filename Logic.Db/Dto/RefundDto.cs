using System;

namespace Logic.Db.Dto
{
    public class RefundDto
    {
        public long competition_id;
        public DateTime date_refund;
        public double refund;


        public override string ToString() {
            return date_refund.ToShortDateString() + " - " + refund + "%";
        }
    }
}
