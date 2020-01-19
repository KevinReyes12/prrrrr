using System;
using ConsoleApp12.clases;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Modalidad> modalidades = new List<Modalidad>()
            {
                new Modalidad(){Id=1,Descripcion="Contrato" },
                new Modalidad(){Id=2,Descripcion="Fijo" },
                new Modalidad(){Id=3,Descripcion="Por horas" }
            };
            List<Empleado> empleados = new List<Empleado>()
            {
                new Empleado(){Id=1,Nombre="Empleado1",IdModalidad=1,IdSexo=1},
                new Empleado(){Id=2,Nombre="Empleado2",IdModalidad=1,IdSexo=2},
                new Empleado(){Id=3,Nombre="Empleado3",IdModalidad=2,IdSexo=1},
                new Empleado(){Id=4,Nombre="Empleado4",IdModalidad=2,IdSexo=5},
                new Empleado(){Id=5,Nombre="Empleado5",IdModalidad=3,IdSexo=2}
            };
            List<Sexo> sexos = new List<Sexo>()
            {
                new Sexo(){ Id=1,Descripcion="Masc"},
                new Sexo(){ Id=2,Descripcion="Fem"}
            };

            //JOIN
            //Consulta modalidades,sexo y nombre empleados
            var resultado = from emp in empleados
                            join mod in modalidades
                            on emp.IdModalidad equals mod.Id
                            join sex in sexos
                            on emp.IdSexo equals sex.Id
                            select new
                            {
                                ModalidadDes = mod.Descripcion,
                                NombreEmpleado = emp.Nombre, SexDes=sex.Descripcion
                            };
            resultado.ToList().ForEach(x => {
                Console.WriteLine("{0} {1} {2}", x.ModalidadDes, x.NombreEmpleado, x.SexDes);
            });

            /*LEFTJOIN
            */
            var resultado1 = from emp in empleados
                            join mod in modalidades
                            on emp.IdModalidad equals mod.Id into relacion2
                            join sex in sexos
                            on emp.IdSexo equals sex.Id into relacion
                            from item in relacion.DefaultIfEmpty(new Sexo() { Id=0,Descripcion="Otro género"})
                            from item2 in relacion2 .DefaultIfEmpty(new Modalidad() { Id=0,Descripcion="Sin Modalidad"})
                            //from sex in relacion.DefaultIfEmpty()
                            select new
                            {
                                ModalidadDes = item2.Descripcion,
                                NombreEmpleado = emp.Nombre,
                                SexDes = item.Descripcion
                            };

            //subsconsultas
            List<Estudiante> estudiantes = new List<Estudiante>()
            {
                new Estudiante(){ Id=1,Nombre="Estudiante1",Apellido="A",Anio=1999,Notas=new List<int>(){ 7,8,2,5,10} },
                new Estudiante(){ Id=2,Nombre="Estudiante2",Apellido="A",Anio=1998,Notas=new List<int>(){ 9,8,10,9,10} },
                new Estudiante(){ Id=3,Nombre="Estudiante3",Apellido="B",Anio=1999,Notas=new List<int>(){ 3,8,1,10,10} },
                new Estudiante(){ Id=4,Nombre="Estudiante4",Apellido="B",Anio=2000,Notas=new List<int>(){ 10,9,7,5,10} }
            };

            //consulta
            var resultado3 =
            from estudiante in estudiantes
            group estudiante by estudiante.Apellido into resumen
            select new
            {
                apellido= resumen.Key,
                promediomayor=(
                   from res in resumen select res.Notas.Average()).Min()
   
            };

            resultado3.ToList().ForEach(x =>
            {
                Console.WriteLine("Apellido {0} Promedio {1}", x.apellido, x.promediomayor);
            });

            Console.ReadKey();
        }
    }
}
