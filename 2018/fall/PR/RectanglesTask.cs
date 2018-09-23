using System;

namespace Rectangles
{
	public static class RectanglesTask
	{
		// Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
		public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
            bool vertsides = (r1.Right >= r2.Left && r1.Left <= r2.Right) || (r2.Right >= r1.Left && r2.Left <= r1.Right);
            bool horsides = (r1.Bottom >= r2.Top && r1.Top <= r2.Bottom) || (r2.Bottom >= r1.Top && r2.Top <= r1.Bottom);
            return vertsides && horsides;
		}

		// Площадь пересечения прямоугольников
		public static int IntersectionSquare(Rectangle r1, Rectangle r2)
		{
            if (AreIntersected(r1, r2))
            {
                int xLConstructIntersec;
                int xRConstructIntersec;
                int yTConstructIntersec;
                int yBConstructIntersec;
                xLConstructIntersec = Math.Max(r1.Left, r2.Left);
                xRConstructIntersec = Math.Min(r1.Right, r2.Right);
                yTConstructIntersec = Math.Max(r1.Top, r2.Top);
                yBConstructIntersec = Math.Min(r1.Bottom, r2.Bottom);
                return Math.Abs(xRConstructIntersec - xLConstructIntersec) * Math.Abs(yTConstructIntersec - yBConstructIntersec);
            }
            return 0;
		}
        //return is first inside second
        public static bool is1Inside2 (Rectangle r1, Rectangle r2)
        {
            return (r1.Left >= r2.Left) && (r1.Right <= r2.Right) && (r1.Top >= r2.Top) && (r1.Bottom <= r2.Bottom);
        }

        public static bool is2Inside1 (Rectangle r1, Rectangle r2)
        {
            return (r2.Left >= r1.Left) && (r2.Right <= r1.Right) && (r2.Top >= r1.Top) && (r2.Bottom <= r1.Bottom);
        }

		// Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
		// Иначе вернуть -1
		// Если прямоугольники совпадают, можно вернуть номер любого из них.
		public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
		{
			if ((is1Inside2(r1,r2) && is2Inside1(r1,r2)) || is1Inside2(r1,r2))
            {
                return 0;
            }
            else if(is2Inside1(r1,r2))
            {
                return 1;
            }
            return -1;
		}
	}
}