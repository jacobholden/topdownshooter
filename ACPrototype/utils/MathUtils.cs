using System;

namespace ACPrototype
{
    public class MathUtils
    {
        public static float RadiansToDegrees()
        {
            return (180 / (float) Math.PI);
        }

        public static float OneDegreeOfPi()
        {
            return MathF.Tau / 360f;
        }
    }
}