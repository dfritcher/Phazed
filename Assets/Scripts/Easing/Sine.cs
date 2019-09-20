namespace Easing
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;


    class Sine : Ease
    {
        public override double EaseIn(double t, double b, double c, double d)
        {
            //return -c * Math.Cos(t / d * HALF_PI) + c + b;
            return EasingEquations.SineIn(t,b,c,d);
        }

        public override double EaseOut(double t, double b, double c, double d)
        {
            //return c * Math.Sin(t / d * HALF_PI) + b;
            return EasingEquations.SineOut(t, b, c, d);
        }

        public override double EaseInOut(double t, double b, double c, double d)
        {
            //return -c / 2 * (Math.Cos(Math.PI * t / d) - 1) + b;
            return EasingEquations.SineInOut(t, b, c, d);
        }
    }
}
