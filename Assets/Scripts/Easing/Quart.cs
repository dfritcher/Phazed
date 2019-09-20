namespace Easing
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;


    class Quart : Ease
    {
        public override double EaseIn(double t, double b, double c, double d)
        {
            //return c * (t /= d) * t * t * t + b;
            return EasingEquations.QuartIn(t,b,c,d);
        }
        public override double EaseOut(double t, double b, double c, double d)
        {
            //return -c * ((t = t / d - 1) * t * t * t - 1) + b;
            return EasingEquations.QuartOut(t, b, c, d);
        }
        public override double EaseInOut(double t, double b, double c, double d)
        {
            //if ((t /= d / 2) < 1) return c / 2 * t * t * t * t + b;
            //return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
            return EasingEquations.QuartInOut(t, b, c, d);
        }
    }
}
