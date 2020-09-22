using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ToDo_List
{
    class ToDoList
    {
        string _fileListName = "lists.txt";
        private void ShowMenu()
        {
            Console.WriteLine("---------Generador de TO-DO LISTs---------");
            Console.WriteLine("1. Crear TO-DO List");
            Console.WriteLine("2. Ver TO-DO List");
            Console.WriteLine("3. Eliminar TO-DO List");
            Console.WriteLine("0. Salir");
            Console.Write("Escoja una opcion:");
        }

        private void EditNamesListFile(string fileName)
        {
            if (File.Exists(_fileListName))
            {
                if (!File.ReadAllText(_fileListName).Contains(fileName))
                {
                    StreamWriter editNamesListFile = File.AppendText(_fileListName);
                    editNamesListFile.WriteLine(fileName);
                    editNamesListFile.Close();
                }
            }
            else
            {
                StreamWriter createNamesListFile = new StreamWriter(_fileListName);
                createNamesListFile.WriteLine(fileName);
                createNamesListFile.Close();
            }
        }
        private void CreateToDoList()
        {
            string fileName;
            string description;
            int numberOfTasks;
            string task;
            Console.WriteLine("-----Crear una nueva TO-DO LIST ----");
            Console.Write("Escribir el nombre de la lista:");
            fileName = Console.ReadLine();
            Console.Write("Añadir una breve descripcion:");
            description = Console.ReadLine();
            TextWriter newFile = new StreamWriter(fileName + ".txt");
            newFile.WriteLine("--------" + fileName + "--------");
            newFile.WriteLine("Descripcion: " + description);
            Console.Write("¿Cuantas tareas desea tener en la lista?:");
            numberOfTasks = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Añada las tareas a la lista:");
            for (int i = 1; i <= numberOfTasks; i++)
            {
                Console.Write("- ");
                task = Console.ReadLine();
                newFile.WriteLine("- " + task);
            }
            newFile.Close();
            EditNamesListFile(fileName);
            Console.WriteLine("...Lista creada!!");
        }

        private string ChooseFile()
        {
            string fileName = null;
            if (File.Exists(_fileListName))
            {
                string[] linesOfTetxt = File.ReadAllLines(_fileListName);
                for (int i = 0; i < linesOfTetxt.Length; i++)
                {
                    Console.WriteLine(i + 1 + ". " + linesOfTetxt[i]);
                }
                Console.Write("Seleccione una lista:");
                int selectedFile = Convert.ToInt16(Console.ReadLine());
                if (selectedFile > linesOfTetxt.Length || selectedFile < 1)
                {
                    Console.WriteLine("Opcion invalida!");
                }
                else
                {
                    fileName = linesOfTetxt[selectedFile - 1];
                }
            }
            else
            {
                Console.WriteLine("Actualmente no existen listas para mostrar.");
            }
            return fileName;
        }

        private void ReadAndEditTextFile()
        {
            string fileName;
            string option;
            Console.WriteLine("-----Ver TO-DO list----");
            fileName = ChooseFile();
            if (fileName != null)
            {
                StreamReader file = new StreamReader(fileName + ".txt");
                Console.WriteLine(file.ReadToEnd());
                file.Close();
                Console.Write("¿Desea añadir tareas a la lista? (si/no):");
                option = Console.ReadLine();
                if (option == "si")
                {
                    EditFile(fileName);
                    Console.WriteLine("...Tareas Añadidas!!");
                }
                else
                {
                    if (option != "no")
                    {
                        Console.WriteLine("Opcion invalida!");
                    }
                }
            }
        }

        private void EditFile(string fileName)
        {
            StreamWriter file = File.AppendText(fileName + ".txt");
            Console.Write("¿Cuantas tareas desea añadir en la lista?:");
            int numberOfTasks = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Añada las tareas a la lista:");
            for (int i = 1; i <= numberOfTasks; i++)
            {
                Console.Write("- ");
                string text = Console.ReadLine();
                file.WriteLine("- " + text);
            }
            file.Close();
        }

        private void DeleteFile()
        {
            string fileName;
            Console.WriteLine("-----Borrar TO-DO list----");
            fileName = ChooseFile();
            if (fileName != null)
            {
                File.Delete(fileName + ".txt");
                File.WriteAllLines(_fileListName, File.ReadLines(_fileListName).Where(line => line != fileName).ToList());
                Console.WriteLine("Lista Eliminada");
            }

        }

        private void ChooseOption(int option)
        {
            switch (option)
            {
                case 1:
                    Console.Clear();
                    CreateToDoList();
                    Console.Write("Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
                case 2:
                    Console.Clear();
                    ReadAndEditTextFile();
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    DeleteFile();
                    Console.Write("Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }
        
        public void StartToDoList()
        {
            int option;
            do
            {
                ShowMenu();
                option = Convert.ToInt16(Console.ReadLine());
                ChooseOption(option);
                Console.Clear();
            } while (option != 0);
        }
    }
}
