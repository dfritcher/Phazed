namespace Easing
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;


    class Quad : Ease
    {
        public override double EaseIn(double t, double b, double c, double d)
        {
            //return c * (t /= d) * t + b;
            return EasingEquations.QuadIn(t, b, c, d);
        }
        public override double EaseOut(double t, double b, double c, double d)
        {
            //return -c * (t /= d) * (t - 2) + b;
            return EasingEquations.QuadOut(t, b, c, d);
        }
        public override double EaseInOut(double t, double b, double c, double d)
        {
            //if ((t /= d / 2) < 1) return c / 2 * t * t + b;
            //return -c / 2 * ((--t) * (t - 2) - 1) + b;
            return EasingEquations.QuadInOut(t, b, c, d);
        }
    }
}
