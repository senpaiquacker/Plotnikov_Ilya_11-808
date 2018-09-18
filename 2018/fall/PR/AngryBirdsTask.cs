using System;

namespace AngryBirds
{
	public static class AngryBirdsTask
	{
		//  Ниже — это XML документация, её использует ваша среда разработки, 
		// чтобы показывать подсказки по использованию методов. 
		// Но писать её естественно не обязательно.
		/// <param name="v">Начальная скорость</param>
		/// <param name="distance">Расстояние до цели</param>
		/// <returns>Угол прицеливания в радианах от 0 до Pi/2</returns>
		public static double FindSightAngle(double v, double distance)
		{
            //adding g
            double g = 9.8;
            //we have formula L = v * v * sin(2a) / g
            //so we tranform it in sin(2a) =  (L * g) / (v * v)
            //then if we have our right half > 1 (it always >= 0, so we don't need and absolute) we have no such angle of which sin will be > 1
            //else we will take arcsin of right half so
            double rightEq = (distance * g) / (v * v);
            if(rightEq > 1)
            {
                return double.NaN;
            }
            return Math.Asin(rightEq) / 2;
		}
	}
}