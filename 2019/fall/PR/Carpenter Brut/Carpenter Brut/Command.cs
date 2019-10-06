using System;
using System.Collections.Generic;
using System.Linq;

namespace Carpenter_Brut
{
    class Command
    {
        //List of Commands (doesn't have Realisation to read them from file, no time)
        internal Dictionary<string, Action<string>> Commands;
        //Process of Loading Commands
        internal Dictionary<string, Action<string>> LoadCommands()
        {
            var CommandAction = new[]
            {
               
               //Quits from program
               new Tuple<string, Action<string>>
               ("-quit", new Action<string>(a => Environment.Exit(0))),
               //Shows all commands
               new Tuple<string, Action<string>>
               ("-help", new Action<string>(a => ListCommands())),
               //Shows all existing pizzas
               new Tuple<string, Action<string>>
               ("-listpizzas", new Action<string>(a => ListPizzas())),
               //Selects Pizza
               new Tuple<string, Action<string>>
               ("-selectpizza", new Action<string>(a => SelectPizza())),
               //Makes new Pizza recipe
               new Tuple<string, Action<string>>
               ("-makenewpizza",new Action<string>(a => MakePizza())),
               //Gets chosen pizza recipe
               new Tuple<string, Action<string>>
               ("-getpizzarecipe", new Action<string>(a => GetPizzaRecipe())),
               //Gets chosen pizza calories
               new Tuple<string, Action<string>>
               ("-getpizzacalories", new Action<string>(a => GetPizzaCalories())),
               //Deletes chosen pizza
               new Tuple<string, Action<string>>
               ("-deletepizza", new Action<string>(a => DeletePizza())),
               //Changes password
               new Tuple<string, Action<string>>
               ("-changepassword", new Action<string>(a => Password.truePassword = Console.ReadLine()))
            };
            return CommandAction.ToDictionary(a => a.Item1, a => a.Item2);
        }
        //Listing Pizzas
        private void ListPizzas()
        {
            foreach (var i in Program.pizzaNames)
                Console.WriteLine(i);
        }

        //Little Trick for -help
        private void ListCommands()
        {
            foreach (var i in Commands.Keys)
                Console.WriteLine(i);
        }

        //Selects Pizza
        private void SelectPizza()
        {
            Console.WriteLine("Type pizza name:");
            var input = Console.ReadLine();
            if (!Program.pizzas.ContainsKey(input))
            {
                Console.WriteLine
                ("No such Pizza, Select existing one " +
                "(NOTE: See -ListPizzas to see all pizzas");
                return;
            }
            Program.pizzaPointer = Program.pizzas[input];
            Console.WriteLine("Selected pizza " + input +
            "(NOTE: All next commands will apply on this pizza. To change pizza type -SelectPizza again)");
        }

        //Deletes pizza
        private void DeletePizza()
        {
            if (Program.pizzaPointer == null)
            {
                Console.WriteLine("You must select pizza first");
                SelectPizza();
            }
            Console.WriteLine("Enter password");
            if (Password.CheckPassword(Console.ReadLine()))
            {
                Console.WriteLine("Pizza " + Program.pizzaPointer.myName + "successfuly removed.");
                Program.pizzas.Remove(Program.pizzaPointer.myName);
            }
            else
                Console.WriteLine("WrongPassword");
        }

        //Gets Recipe
        private void GetPizzaRecipe()
        {
            if (Program.pizzaPointer == null)
            {
                Console.WriteLine("You must select pizza first");
                SelectPizza();
            }
            Console.WriteLine("Enter password");
            Console.WriteLine(Password.CheckPassword(Console.ReadLine()) ?
            Program.pizzaPointer.GetPizzaRecipe() :
            "Wrong Password");
        }

        //Gets calories
        private void GetPizzaCalories()
        {
            if (Program.pizzaPointer == null)
            {
                Console.WriteLine("You must select pizza first");
                SelectPizza();
            }
            Console.WriteLine(Program.pizzaPointer.GetPizzaCalories());
        }

        //Makes new recipe
        private void MakePizza()
        {
            Console.WriteLine("Enter password:");
            if (!Password.CheckPassword(Console.ReadLine()))
            {
                Console.WriteLine("Wrong password");
                return;
            }
            Console.WriteLine("Enter Pizza Name:");
            var input1 = Console.ReadLine();
            Console.WriteLine("Enter Dough:");
            var input2 = Console.ReadLine();
            var input2Value = input2.Split(' ').Last();
            input2 = input2.Replace(' ' + input2Value, "");
            Console.WriteLine("Enter Topping (NOTE: To finish topping enter -end):");
            var insideList = new Dictionary<string, double>();
            var input3 = Console.ReadLine();
            while (input3 != "-end")
            {
                var input3Value = input3.Split(' ').Last();
                input3 = input3.Replace(' ' + input3Value, "");
                insideList.Add(input3, double.Parse(input3Value));
                input3 = Console.ReadLine();
            }
            Program.pizzas.Add(input1, new Pizza
                (input1,
                new Dough(input2, double.Parse(input2Value)),
                new Inside(insideList)));
            Program.pizzaNames.Add(input1);
        }
    }
}
