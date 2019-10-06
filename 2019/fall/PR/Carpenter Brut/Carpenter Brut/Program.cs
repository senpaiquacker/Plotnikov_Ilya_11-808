using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Carpenter_Brut
{
    /*Code Bad Patches Explanation:
     So, I was trying to read some of ingredients from file to make code easier to understand
     And granting availability to change Ingredients in Easier Way. So, as we can see in task,
     You just can't mix up Wholegrain and White Doughs, so here is First patch:
     in order to check recipe, in file Wholegrain and White Doughs' multipliers are *(-1).
     I understand, that's not quite good, but if a had more time to make an realisation of
     Ingredient Editor, I would check the Type of Dough, is it a Flavor Type or Bake Type.
     But with building all of this code, I won't send it in time, so a have to leave it as it is*/
     /* just feature of password */
    class Password
    {
        public static string truePassword
        {
            get
            {
                var coreDirectory = Directory.GetCurrentDirectory();
                return File.ReadAllText(coreDirectory + @"\password.txt");
            }
            set
            {
                //Changing needs knowledge of previous, at the beginning it's null
                Console.WriteLine("The new password is: " + value + "\nEnter Old Password:");
                var confirmPassword = Console.ReadLine();
                if (confirmPassword != truePassword)
                {
                    Console.WriteLine("Wrong Password\n");
                    return;
                }
                var coreDirectory = Directory.GetCurrentDirectory();
                File.WriteAllText(coreDirectory + @"\password.txt", value);
            }
        }
        public static bool CheckPassword(string password)
        {
            if (password != Password.truePassword)
            {
                return false;
            }
            return true;
        }
    }
    
    

    
    
    class Program
    {
        //List of Pizzas
        internal static Dictionary<string,Pizza> pizzas = new Dictionary<string, Pizza>();
        internal static List<string> pizzaNames = new List<string>();
        //Pointer of Chosen Pizza
        internal static Pizza pizzaPointer;
        //Main method
        static void Main(string[] args)
        {
            var commandList = new Command { };
            commandList.LoadCommands();
            Console.WriteLine("Enter Command (if you don't kenow commands, type \"-Help\"):");
            while (true)
            {
                commandList.Commands[Console.ReadLine().ToLower()].Invoke("");
                Console.WriteLine("Cleaning Console, Press Any Key");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Enter Command:");
            }
        }
    }
}
