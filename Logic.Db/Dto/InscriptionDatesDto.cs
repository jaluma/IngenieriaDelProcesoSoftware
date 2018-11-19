using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.Dto {
    public class InscriptionDatesDto {
        public DateTime FechaInicio;
        public DateTime FechaFin;
        public double Devolucion;
        public double precio;



        public override string ToString() {

            if(precio!=0)

                return FechaInicio.ToShortDateString() + " - " + FechaFin.ToShortDateString() + " " + precio + "€";
            else
                return FechaInicio.ToShortDateString() + " - " + FechaFin.ToShortDateString();


        }
    }
}
