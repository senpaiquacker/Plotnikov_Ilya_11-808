using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Carpenter_Brut
{
    abstract internal class Ingredient
    {
        //Library of ingredients
        internal Dictionary<string, double> IngredientLibrary;
        internal string Name { get; set; }
        internal readonly double caloriesPerGramm = 2;
        protected double caloriesModifier { get; set; }
        internal double Weight { get; set; }
        abstract internal double Calories { get; }

        //Library Loader
        internal Dictionary<string, double> CreateLibrary()
        {
            var coreDirectory = Directory.GetCurrentDirectory();
            //File named in Directory is always named by Class Type
            var ingredientLibrary = File
                .ReadAllLines(coreDirectory + @"\Pizza" + this.GetType().Name + ".txt");
            //Library transformation to Dictionary
            var ingredientDictionary = ingredientLibrary
                .Select(a => Tuple.Create(a.Split(' ')[0], double.Parse(a.Split(' ')[1])))
                .ToDictionary(a => a.Item1, a => a.Item2);
            return ingredientDictionary;
        }
    }
}
