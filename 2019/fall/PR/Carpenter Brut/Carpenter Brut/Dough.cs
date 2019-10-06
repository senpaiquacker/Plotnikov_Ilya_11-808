using System;
using System.Collections.Generic;
using System.Linq;

namespace Carpenter_Brut
{
    //Dough Class
    internal class Dough : Ingredient
    {
        //initialize
        internal Dough(string newDough, double curWeight)
        {
            //Loading Library
            IngredientLibrary = CreateLibrary();
            //Reading Message
            List<string> DoughParams = newDough
                .Split(' ')
                .Select(a => a.ToLower()).ToList();
            //Checking on availability
            foreach (var dough in DoughParams)
                if (!this.IngredientLibrary.ContainsKey(dough))
                    throw new Exception("Invalid DoughType");
            double finalModifier = 1;
            //Checking on mixing
            foreach (var dough in DoughParams)
                finalModifier *= this.IngredientLibrary[dough];
            if (finalModifier > 0 && DoughParams.Count > 1)
                throw new Exception("Invalid DoughType");
            //Checking Value
            if (curWeight < 1 || curWeight > 200)
                throw new Exception("Weight should be in range [1, 200]");
            //Cleaning Mark
            caloriesModifier = Math.Abs(finalModifier);
            Weight = curWeight;
            Name = newDough;
        }
        internal override double Calories
        {
            get
            {
                return Weight * caloriesModifier * caloriesPerGramm;
            }
        }
    }
}
