using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Carpenter_Brut
{
    //Inside Class
    internal class Inside : Ingredient
    {
        Dictionary<string, double> insideList;
        //New inside can be mixed as Dough
        internal Inside(Dictionary<string,double>newInside)
        {
            if (newInside.Count > 10)
                throw new Exception("Can't be more than 10 toppings");
            //Loading Library on Insides
            IngredientLibrary = CreateLibrary();
            //Reading and translating Code Built
            //Checking in library
            foreach (var inside in newInside)
                if (!this.IngredientLibrary.ContainsKey(inside.Key))
                    throw new Exception("Invalid InsideType");
            double finalModifier = 1;
            //finding finalModifier (for now works same as Dough)
            Weight = 0;
            var name = new StringBuilder();
            foreach (var inside in newInside)
            {
                finalModifier += this.IngredientLibrary[inside.Key] * inside.Value;
                if (inside.Value < 1 || inside.Value > 50)
                    throw new Exception("Weight should be in range [1, 200]");
                Weight += inside.Value;
                name.Append(inside.Key);
            }
            insideList = newInside;
            Name = name.ToString();
        }
        internal override double Calories
        {
            get
            {
                double finalCalories = 0;
                foreach(var ingredient in insideList)
                {
                    finalCalories += 
                        ingredient.Value * IngredientLibrary[ingredient.Key];
                }
                return finalCalories * caloriesPerGramm;
            }
        }
    }
}
