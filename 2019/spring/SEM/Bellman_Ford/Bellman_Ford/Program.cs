using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellmanFordAlgo;
using System.IO;
namespace Bellman_Ford
{
    class Program
    {
        //алгоритм перевода текста из файла в матрицу
        static double[,] TransformToMatrix(string input)
        {
            var value = new StringBuilder();
            int sizeCounter = 1;
            //узнаем размер матрицы
            for(int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\n')
                    break;
                if(input[i] == ' ')
                {
                    sizeCounter++;
                }
            }
            var matrix = new double[sizeCounter,sizeCounter];
            int linePointer = 0;
            int elemPointer = 0;
            //забиваем матрицу данными 
            //пробел - переход на следующее число
            //\n - переход на следующую строку
            for(int  i = 0; i < input.Length; i ++)
            {
                if(input[i] == '\n')
                {
                    linePointer++;
                    elemPointer = 0;
                    if (value.ToString() == "-")
                    {
                        matrix[linePointer, elemPointer] = double.PositiveInfinity;
                    }
                    else
                    {
                        matrix[linePointer, elemPointer] = double.Parse(value.ToString());
                    }
                }
                else if(input[i] == ' ')
                {
                    if (value.ToString() == "-")
                    {
                        matrix[linePointer, elemPointer] = double.PositiveInfinity;
                    }
                    else
                    {
                        matrix[linePointer, elemPointer] = double.Parse(value.ToString());
                    }
                    elemPointer++;
                }
                else
                {
                    value.Append(input[i]);
                }
            }
            return matrix;
        }
        static void Main(string[] args)
        {
            //для начала мы считываем данные со всех файлов, которые есть (они специально пронумерованы)
            for(int i = 0; i < 100; i++)
            {
                string newTest = File.ReadAllText
                    (@"C:\Users\Варфоломеев\source\repos\Bellman_Ford\Generator\bin\Debug\"
                    + i.ToString() + ".txt");
                //переводим данные (они хорошо структурированны) в реальную матрицу
                var test = TransformToMatrix(newTest);
                Algorythm.BellmanFordAlgorythm(test.Length, test);
            }
        }
    }
}
