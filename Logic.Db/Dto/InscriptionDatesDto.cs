using System;

namespace Logic.Db.Dto
{
    public class InscriptionDatesDto
    {
        public double Devolucion;
        public DateTime FechaFin;
        public DateTime FechaInicio;
        public double precio;


        public override string ToString() {
            if (precio != 0)

                return FechaInicio.ToShortDateString() + " - " + FechaFin.ToShortDateString() + " " + precio + "€";
            return FechaInicio.ToShortDateString() + " - " + FechaFin.ToShortDateString();
        }
    }
}
