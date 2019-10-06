
namespace Carpenter_Brut
{
    public class Pizza
    {
        public string myName { get; private set; }
        private Dough myDough { get; set; }
        private Inside myTopping { get; set; }
        //initialize
        internal Pizza(string Name, Dough Testo, Inside Nachinka)
        {
            myName = Name;
            myDough = Testo;
            myTopping = Nachinka;
        }

        //returns pizza summary calories
        internal double GetPizzaCalories()
        {
            return myDough.Calories + myTopping.Calories;
        }

        //returns pizza recipe (if you do that command, you're gonna need password)
        internal string GetPizzaRecipe()
        {
            return myName + ":\n" + myDough.Name + " " + myTopping.Name;
        }
    }
}
