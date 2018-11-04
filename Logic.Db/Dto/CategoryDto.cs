using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.Dto
{
    public class CategoryDto
    {
        public String Name;
        public int Min_Age;
        public int Max_Age;



        public override string ToString()
        {

            return this.Name + " (" + this.Min_Age + "-" + this.Max_Age+ ")";
        }
    }
}
