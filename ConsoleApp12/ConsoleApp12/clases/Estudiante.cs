using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12.clases
{
     public class Estudiante
    {
        public Estudiante()
        {

        }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Anio { get; set; }
        public int Id { get; set; }
        public List<int> Notas { get; set; }
    }
}
