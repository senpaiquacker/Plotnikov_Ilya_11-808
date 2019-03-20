using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellmanFordAlgo
{
    //создадим класс для ребер, в которых будут данные о первой вершине, второй вершине и вес
    class GraphEdge : IComparable
    {
        public int FirstVertex;
        public int SecondVertex;
        public double Weight;
        public GraphEdge(int first, int second, double weight)
        {
            FirstVertex = first;
            SecondVertex = second;
            Weight = weight;
        }

        //метод сравнения ребер графа (для сортировки)
        public int CompareTo(object obj)
        {
            var edge = (GraphEdge)obj;
            return FirstVertex.CompareTo(edge.FirstVertex);
        }
    }
    public class Algorythm
    {
        //сам алгоритм Беллмана-Форда
        private static void BellmanFordDynamic(double[] dist, GraphEdge[] edgeList)
        {
            //переменная, которая показывает, была ли произведена релаксация хотя бы одного ребра
            bool isRelaxation = false;
            //dist.Length это количество вершин, так что этот цикл рассчитан, по сути, на V итераций
            //Нам нужно V итераций, чтобы на последней проверить, произведет ли алгоритм
            //еще одну релаксацию
            for (int i = 0; i < dist.Length; i++)
            {
                isRelaxation = false;
                //прохождение по всем ребрам
                foreach (var edge in edgeList)
                {
                    //проверка условия (попытка релаксации)
                    if (dist[edge.SecondVertex] > dist[edge.FirstVertex] + edge.Weight)
                    {
                        isRelaxation = true;
                        dist[edge.SecondVertex] = dist[edge.FirstVertex] + edge.Weight;
                    }
                }
                //проверка релаксации
                if (!isRelaxation)
                {
                    break;
                }
            }
            //проверка наличия отрицательного цикла
            //исключение нужно для сообщения о негативном цикле, то бишь алгоритм не может
            //вычислить минимальные расстояния до каждого ребра
            if (isRelaxation)
            {
                throw new Exception("Negative Cycle Detected");
            }
        }
        public static double[] BellmanFordAlgorythm(int inputGraphSize, double[,] matrix)
        {
            //выписываем все ребра и сортируем их
            var edgeList = new List<GraphEdge>(0);
            for (int i = 0; i < inputGraphSize; i++)
            {
                for (int j = 0; j < inputGraphSize; j++)
                {
                        edgeList.Add(new GraphEdge(i, j, matrix[i,j]));
                }
            }
            var edgeListSorted = edgeList.ToArray();
            Array.Sort(edgeListSorted);
            //заводим массив, в котором будут записаны расстояния от изначальной точки
            //до всех точек
            var dist = new double[inputGraphSize];
            //естественно расстояние от точки до самой себя будет 0
            dist[0] = 0;
            for (int i = 1; i < inputGraphSize; i++)
            {
                //а до других пока не известно, так что пускай оно будет наихудшим
                //то есть double.PositiveInfinity
                dist[i] = double.PositiveInfinity;
            }
            //запускаем неподсредственно сам алгоритм (да-да, все предыдущее было подготовкой)
            BellmanFordDynamic(dist, edgeListSorted);
            return dist;
        }
    }
}
