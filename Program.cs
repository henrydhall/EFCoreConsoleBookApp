using Microsoft.EntityFrameworkCore;
using System;

namespace EFCoreConsoleBookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Commands: l (list), s (search), a (add), c (make change), r (resetDb), and e (exit)");
            Console.Write("Checking if database exists... ");
            Console.WriteLine(Commands.Initialize(true) ? "created database and seeded it." : "it exists.");
            Commands.Initialize(true);
            do
            {
                Console.Write("> ");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "l":
                        listObjects();
                        break;
                    case "e":
                        return;
                    case "s":
                        Commands.SearchDb();
                        break;
                    case "c":
                        Commands.MakeChange();
                        break;
                    case "r":
                        Console.WriteLine("TODO: resetDatabase()");   
                        break;
                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            } while (true);
        }
        public static void listObjects()
        {
            Commands.ListAll();
            //Doesn't work because we need tables set up differently.
            /*
            string listOption;
            Console.WriteLine("List options: b (books), a (authors)");
            Console.Write(" >");
            listOption = Console.ReadLine();
            switch(listOption)
            {
                case "b":
                    Commands.ListAll();
                    break;
                case "a":
                    Commands.ListAuthors();
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }*/
        }
    }
}
