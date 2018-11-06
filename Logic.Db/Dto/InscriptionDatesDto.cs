using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.Dto
{
    public class InscriptionDatesDto
    {
        public DateTime fechaInicio;
        public DateTime fechaFin;
        public double devolucion;



        public override string ToString()
        {
            if(devolucion > 0)
                return this.fechaInicio.ToShortDateString() + " - " + this.fechaFin.ToShortDateString()+ " " +devolucion.ToString() + "% REFUND";
            else
                return this.fechaInicio.ToShortDateString() + " - " + this.fechaFin.ToShortDateString() + " NO REFUND";

        }
    }
}
