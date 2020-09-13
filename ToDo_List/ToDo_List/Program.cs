using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ToDo_List
{
    class Program
    {
        static void Main(string[] args)
        {

            int op = 10;
            do
            {
                Console.WriteLine("---------Generador de TO-DO LISTs---------");
                Console.WriteLine("1. Crear TO-DO List");
                Console.WriteLine("2. Ver TO-DO List");
                Console.WriteLine("3. Eliminar TO-DO List");
                Console.WriteLine("0. Salir");
                Console.Write("Escoja una opcion:");
                op = Convert.ToInt16(Console.ReadLine());
                switch (op)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("-----Crear una nueva TO-DO LIST ----");
                        Console.Write("Escribir el nombre de la lista:");
                        string nombre = Console.ReadLine();
                        TextWriter archivo = new StreamWriter(nombre + ".txt");
                        Console.Write("Añadir una breve descripcion:");
                        string d = Console.ReadLine();
                        archivo.WriteLine("--------" + nombre + "--------");
                        archivo.WriteLine("Descripcion: " + d);
                        Console.Write("¿Cuantas tareas desea tener en la lista?:");
                        int tareas = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("Añada las tareas a la lista:");
                        for (int i = 1; i <= tareas; i++)
                        {
                            Console.Write("- ");
                            string mensaje = Console.ReadLine();
                            archivo.WriteLine("- " + mensaje);
                        }
                        archivo.Close();
                        StreamWriter l = File.Exists("lists.txt") ? File.AppendText("lists.txt") : new StreamWriter("lists.txt");
                        l.WriteLine(nombre);
                        l.Close();
                        Console.WriteLine("...Lista creada!!");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("-----Ver TO-DO list----");
                        if (File.Exists("lists.txt"))
                        {
                            string[] lines = File.ReadAllLines("lists.txt");
                            for (int i = 0; i < lines.Length; i++)
                            {
                                Console.WriteLine(i + 1 + ". " + lines[i]);
                            }
                            Console.Write("Seleccione una lista:");
                            int b = Convert.ToInt16(Console.ReadLine());
                            if (b > lines.Length || b < 1)
                            {
                                Console.WriteLine("Opcion invalida!");
                            }
                            else
                            {
                                StreamReader archivo2 = new StreamReader(lines[b - 1] + ".txt");
                                string mensa = archivo2.ReadToEnd();
                                Console.WriteLine("\n" + mensa);
                                Console.Write("¿Desea añadir tareas a la lista? (si/no):");
                                string o = Console.ReadLine();
                                archivo2.Close();
                                if (o == "si")
                                {
                                    StreamWriter contenido = File.AppendText(lines[b - 1] + ".txt");
                                    Console.Write("¿Cuantas tareas desea añadir en la lista?:");
                                    int n = Convert.ToInt16(Console.ReadLine());
                                    Console.WriteLine("Añada las tareas a la lista:");
                                    for (int i = 1; i <= n; i++)
                                    {
                                        Console.Write("- ");
                                        string mensaje = Console.ReadLine();
                                        contenido.WriteLine("- " + mensaje);
                                    }
                                    contenido.Close();
                                    Console.WriteLine("...Tareas Añadidas!!");
                                }
                                else
                                {
                                    if (o != "no")
                                    {
                                        Console.WriteLine("Opcion invalida!");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Actualmente no existen listas para mostrar.");
                        }

                        Console.WriteLine("Presione una tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("-----Borrar TO-DO list----");
                        string[] lists = File.ReadAllLines("lists.txt");
                        for (int i = 0; i < lists.Length; i++)
                        {
                            Console.WriteLine(i + 1 + ". " + lists[i]);
                        }
                        Console.Write("Seleccione la lista a borrar:");
                        int c = Convert.ToInt16(Console.ReadLine());
                        if (c > lists.Length || c < 1)
                        {
                            Console.WriteLine("Opcion invalida!");
                        }
                        else
                        {
                            File.Delete(lists[c - 1] + ".txt");
                            File.WriteAllLines("lists.txt", File.ReadLines("lists.txt").Where(line => line != lists[c - 1]).ToList());
                            Console.WriteLine("Lista Eliminada");
                        }
                        Console.Write("Presione una tecla para continuar...");
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
                Console.Clear();
            } while (op != 0);
        }
    }
}
