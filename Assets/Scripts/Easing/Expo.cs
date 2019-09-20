namespace Easing
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;


    class Expo : Ease
    {
        public override double EaseIn(double t, double b, double c, double d)
        {
            //return (t == 0) ? b : c * Math.Pow(2, 10 * (t / d - 1)) + b - c * 0.001;
            return EasingEquations.ExpoIn(t, d, c, d);
        }
        public override double EaseOut(double t, double b, double c, double d)
        {
            //return (t == d) ? b + c : c * (-Math.Pow(2, -10 * t / d) + 1) + b;
            return EasingEquations.ExpoOut(t, d, c, d);
        }
        public override double EaseInOut(double t, double b, double c, double d)
        {
            /*if (t == 0) return b;
            if (t == d) return b + c;
            if ((t /= d / 2) < 1) return c / 2 * Math.Pow(2, 10 * (t - 1)) + b;
            return c / 2 * (-Math.Pow(2, -10 * --t) + 2) + b;*/
            return EasingEquations.ExpoInOut(t, d, c, d);
        }
    }
}
