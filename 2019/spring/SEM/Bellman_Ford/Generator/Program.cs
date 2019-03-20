using System;
using System.IO;
using System.Text;
namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var key = new Random();
            //генератор рандомных тестов
            for(int i = 0; i < 100; i++)
            {
                //рандомный размер
                int vortexes = key.Next(100, 10000);
                var input = new StringBuilder();
                for(int j = 0; j < vortexes; j++)
                {
                    for(int q = 0; q < vortexes; q++)
                    {
                        //вторая часть условия специально хитро выполнена
                        //Во-первых, это нужно для появления тестов, где некоторые ребра отсутствуют
                        //Во-вторых, рандом от нуля до 9 был выбран не зря
                        //Если бы от 0 до 1, то тогда бы шанс не добавления ребра был бы слишком высоким
                        //А значит граф был бы примерно на половину пуст, а так неинтересно
                        //поэтому шанс был выбран в 10%
                        if(j == q || key.Next(0, 9) == 0)
                        {
                            input.Append("-");
                        }
                        else
                        {
                            input.Append(key.Next(-20, 20).ToString());
                        }
                        input.Append(" ");
                    }
                    //в конце пробел остается, это некрасиво
                    input.Remove(input.Length - 1, 1);
                    input.Append("\n");
                }
                //записываем матрицу в документ (для каждой отдельный плюс еще пронумерованный)
                File.WriteAllText(i.ToString() + ".txt", input.ToString());
            }
        }
    }
}
