using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horas_de_trabajo_de_proyectos
{

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Desarrollador> desarrolladores = new List<Desarrollador>();
            List<Proyecto> proyectos = new List<Proyecto>();
            DateTime fechaActual = DateTime.Today;

            Alta_Desarrollador(desarrolladores, fechaActual);
            desarrolladores[0].mostrar_desarrolladores(desarrolladores);
            Alta_Proyecto(proyectos, fechaActual);
            proyectos[0].mostrar_proyectos(proyectos);
            Asignar_Proyecto(desarrolladores, proyectos);
            Registrar_horas(proyectos);
            Console.WriteLine("Presiona Enter para salir...");
            Console.ReadLine();
        }
        public static void Registrar_horas(List<Proyecto> lp)
        {
            Console.WriteLine("Ingresa nombre del proyecto");
            string nomProyecto = Console.ReadLine();
            int i = lp[0].buscar_proyecto(lp, nomProyecto);
            Console.WriteLine("Ingresa las horas dedicadas al proyecto: ");
            int horas = int.Parse(Console.ReadLine());
            lp[i].Registrar_Horas(horas, lp[i].Duracion_Horas);
        }
        public static void Alta_Desarrollador(List<Desarrollador> desarrolladores, DateTime fechaActual)
        {
            Desarrollador d = new Desarrollador();
            int Categoria=0;
            Console.WriteLine("Ingresa el id: ");
            d.Id = int.Parse(Console.ReadLine());
            if (!d.buscar_desarrollador(desarrolladores, d.Id) )
            {
                Console.WriteLine("Ingresa el nombre de desarrollador: ");
                d.Nombre = Console.ReadLine();
                do
                {
                    Console.WriteLine("Ingrese una fecha de ingreso (dd/mm/aaaa):");
                    d.Fecha_de_ingreso = DateTime.Parse(Console.ReadLine());
                } while (d.Fecha_de_ingreso < fechaActual);

                Console.WriteLine("Ingresa el email: ");
                d.Email = Console.ReadLine();
                do
                {
                    Console.WriteLine("Seleccione una de las siguientes categorias: ");
                    Console.WriteLine("1) Nivel 1");
                    Console.WriteLine("2) Nivel 2");
                    Console.WriteLine("3) Nivel 3");
                    Categoria = int.Parse(Console.ReadLine());

                } while (Categoria < 1 || Categoria > 3) ;
                d.Categoria = $"Nivel {Categoria}";
                desarrolladores.Add(d);
            }
            else
            {
                Console.WriteLine("No se pudo agregar el colaborador debido a que ya existe un colaborador con el mismo id :(");
               
            }
            

        }
        public static void Alta_Proyecto(List<Proyecto> proyectos, DateTime fechaActual)
        {
            Proyecto p = new Proyecto();
            int Categoria = 0;

            Console.WriteLine("Ingresa el nombre del proyecto:");
            p.Nombre = Console.ReadLine();

            if (p.buscar_proyecto(proyectos, p.Nombre) == -1)
            {
                Console.WriteLine("Ingresa la duracion del proyecto en horas: ");
                p.Duracion_Horas = int.Parse(Console.ReadLine());
                p.Horas = 0;
                do
                {
                    Console.WriteLine("Ingrese una fecha de ingreso (dd/mm/aaaa):");
                    p.Fecha_de_inicio = DateTime.Parse(Console.ReadLine());
                } while (p.Fecha_de_inicio < fechaActual);
                do
                {
                    Console.WriteLine("Seleccione una de las siguientes categorias: ");
                    Console.WriteLine("1) Nivel 1");
                    Console.WriteLine("2) Nivel 2");
                    Console.WriteLine("3) Nivel 3");
                    Categoria = int.Parse(Console.ReadLine());

                } while (Categoria < 1 || Categoria > 3);
                p.Categoria = $"Nivel {Categoria}";
                proyectos.Add(p);
            }
            else
            {
                Console.WriteLine("No se pudo agregar el colaborador debido a que ya existe un colaborador con el mismo id :(");
            }
        }
        public static void Asignar_Proyecto(List<Desarrollador> ld, List<Proyecto> lp)
        {
            bool BDesarrollador = false;
            int posicion = 0;

            Console.WriteLine("Ingresa nombre del proyecto");
            string nomProyecto = Console.ReadLine();

            Console.WriteLine("Ingresa el id del desarrollador acargo del proyecto");
            int id = int.Parse(Console.ReadLine());

            foreach (Desarrollador desarrollador in ld)
            {
                if (desarrollador.Id == id)
                {
                    posicion = lp[0].buscar_proyecto(lp, nomProyecto);
                    if (posicion > 0)
                    {
                        lp[posicion].cambiar_desarrollador(desarrollador);
                        Console.WriteLine("Asignación de proyecto exitosa");
                        BDesarrollador = true;
                    }
                    else
                    {
                        lp.Add(new Proyecto(nomProyecto, desarrollador));
                        Console.WriteLine("Asignación de proyecto exitosa");
                        BDesarrollador = true;
                    }

                }
            }

            if (BDesarrollador == false)
            {
                Console.WriteLine("No se pudo asignar el desarrollador debido a que no se encuentra en la base de datos de desarrolladores.");
            }

        }


    }

    public class Desarrollador
    {
        public int Id;
        public string Nombre;
        public DateTime Fecha_de_ingreso;
        public string Email;
        public string Categoria;
        public Desarrollador()
        {

        }

        public Desarrollador(int id, string nombre, DateTime fecha, string email, string categoria)
        {
            Id = id;
            Nombre = nombre;
            Fecha_de_ingreso = fecha;
            Email = email;
            Categoria = categoria;
        }
   

        public void mostrar_desarrolladores(List<Desarrollador> d)
        {
            foreach (Desarrollador desarrolladores in d)
            {
                Console.WriteLine(desarrolladores.Nombre);
            }
        }

        public bool buscar_desarrollador(List<Desarrollador> ld, int id)
        {
            bool b = false;
            for (int i = 0; i < ld.Count; i++)
            {
                if (ld[i].Id == id)
                {
                    b = true;
                }
            }
            return b;
        }
    }

    public class Proyecto
    {
        public string Nombre;
        public string Categoria;
        public int Duracion_Horas;
        public int Horas;
        public DateTime Fecha_de_inicio;
        public Desarrollador Desarrollador_Asignado;

        public Proyecto() { }

        public Proyecto(string nombre)
        {
            Nombre = nombre;
        }

        public Proyecto(string nombre, Desarrollador desarrollador_Asignado)
        {
            Nombre = nombre;
            Desarrollador_Asignado = desarrollador_Asignado;
        }

        public Proyecto(string nombre, string categoria, int duracion_Horas, int horas, DateTime fecha_de_inicio, Desarrollador desarrollador_Asignado) : this(nombre, desarrollador_Asignado)
        {
            this.Categoria = categoria;
            Horas = horas;
            Duracion_Horas = duracion_Horas;
            Fecha_de_inicio = fecha_de_inicio;
        }

        public void cambiar_desarrollador(Desarrollador d)
        {
            Desarrollador_Asignado = d;
        } 

        public void mostrar_proyectos(List<Proyecto> p)
        {
            foreach(Proyecto proyectos in p){
                Console.WriteLine( proyectos.Nombre);
            }
        }

        public int buscar_proyecto(List<Proyecto> lp, string nomProyecto)
        {
            int posicion = -1;
            for (int i = 0; i < lp.Count; i++)
            {
                if (lp[i].Nombre == nomProyecto)
                {
                    posicion = i;
                }
            }
            return posicion;
        }

        public void Registrar_Horas(int horas, int dh)
        {

            if (Horas == 0 )
            {
                if (Horas + horas <= dh)
                {
                    Horas = horas;
                    Console.WriteLine("Las horas totales dedicadas al proyecto es de " + Horas + " :)");
                }
                else
                {
                    int aux = dh - horas;
                    Console.WriteLine($"Las horas agregadas sobrepasan por {aux} las horas asignadas para la duracion del proyecto ingrese un numero de horas menor.");
                }
                
            }
            else
            {
                if (Horas + horas <= dh)
                {
                    Horas = horas + Horas;
                    Console.WriteLine("Las horas totales dedicadas al proyecto es de " + Horas + " :)");
                }
                else
                {
                    int aux = dh - horas;
                    Console.WriteLine(dh);
                    Console.WriteLine($"Las horas agregadas sobrepasan por {aux} las horas asignadas para la duracion del proyecto ingrese un numero de horas menor.");
                }
            }
            
        }
    }
}
