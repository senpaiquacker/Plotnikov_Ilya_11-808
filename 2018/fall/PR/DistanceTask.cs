using System;

namespace DistanceTask
{
	public static class DistanceTask
	{
		// Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
		public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
		{
            //Building triangle (saving variables without sqrt fives us more accuracy in calc)
            double ABsq;
            ABsq = (ax - bx) * (ax - bx) + (ay - by) * (ay - by);
            double AB = Math.Sqrt(ABsq);
            double BCsq;
            BCsq = (x - ax) * (x - ax) + (y - ay) * (y - ay);
            double BC = Math.Sqrt(BCsq);
            double ACsq;
            ACsq = (x - bx) * (x - bx) + (y - by) * (y - by);
            double AC = Math.Sqrt(ACsq);
            //finding square of triangle
            double halfPer = (AB + BC + AC) / 2;
            double STriangle = Math.Sqrt(halfPer * (halfPer - AB) * (halfPer - BC) * (halfPer - AC));
            //If we have no triangle, do exceptions
            if(STriangle == 0)
            {
                if (x > ax && x > bx || x < ax && x < bx)
                {
                    double var1 = Math.Sqrt((ax - x) * (ax - x) + (ay - y) * (ay - y));
                    double var2 = Math.Sqrt((bx - x) * (bx - x) + (by - y) * (by - y));
                    return Math.Min(var1, var2);
                }
                if(ax - bx == 0)
                {
                    if (y > ay && y > by || y < ay && y < by)
                    {
                        double var1 = Math.Sqrt((ax - x) * (ax - x) + (ay - y) * (ay - y));
                        double var2 = Math.Sqrt((bx - x) * (bx - x) + (by - y) * (by - y));
                        return Math.Min(var1, var2);
                    }
                }
                return 0.0;
            }
            //finging height
            double Answ = 2 * STriangle / AB;
            //cos theorema (checking, do we have our height base out of line)
            //if we have base out of line, we just equal our answer as one of the triangle sides
            double cosAngleABC = (BCsq + ABsq - ACsq) / (2 * AB * BC);
            double cosAngleBAC = (ABsq + ACsq - BCsq) / (2 * AB * AC);
            if(cosAngleABC < 0)
            {
                Answ = BC;
            }
            if(cosAngleBAC < 0)
            {
                Answ = AC;
            }
            //if no, then just output our height
            return Answ;
		}
	}
}