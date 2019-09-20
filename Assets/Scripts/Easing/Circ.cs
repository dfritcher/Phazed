namespace Easing
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;


    class Circ : Ease
    {
        public override double EaseIn(double t, double b, double c, double d)
        {
            //return -c * (Math.Sqrt(1 - (t /= d) * t) - 1) + b;
            return EasingEquations.CircIn(t, d, c, d);
        }
        public override double EaseOut(double t, double b, double c, double d)
        {
            //return c * Math.Sqrt(1 - (t = t / d - 1) * t) + b;
            return EasingEquations.CircOut(t, d, c, d);
        }
        public override double EaseInOut(double t, double b, double c, double d)
        {
            /*if ((t /= d / 2) < 1) return -c / 2 * (Math.Sqrt(1 - t * t) - 1) + b;
            return c / 2 * (Math.Sqrt(1 - (t -= 2) * t) + 1) + b;*/
            return EasingEquations.CircInOut(t, d, c, d);
        }
    }
}
