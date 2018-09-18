using System;

namespace Billiards
{
    public static class BilliardsTask
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directionRadians">Угол направелния движения шара</param>
        /// <param name="wallInclinationRadians">Угол</param>
        /// <returns></returns>
        public static double BounceWall(double directionRadians, double wallInclinationRadians)
        {
            //TODO
            //At first, we need to find angle between horizontal and vertical to wall
            //it will be 180 - 90 (angle between vertical to wall and wall) - wallInclinationRadians
            //so our fall angle will be directionRadians + that angle
            //then we will double it and do -directionRadians so we will find angle between horizontal
            //and deflection angle
            double ang1 = (Math.PI / 2 - wallInclinationRadians + directionRadians) * 2 - directionRadians;
            //then we will do 180 - ang1, and it will be angle which we need
            ang1 = Math.PI - ang1;
            return ang1;
        }
    }
}