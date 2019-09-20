namespace Easing
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;


    class Bounce : Ease
    {
        public override double EaseOut(double t, double b, double c, double d)
        {
            /*if ((t /= d) < (1 / 2.75))
            {
                return c * (7.5625 * t * t) + b;
            }
            else if (t < (2 / 2.75))
            {
                return c * (7.5625 * (t -= (1.5 / 2.75)) * t + .75) + b;
            }
            else if (t < (2.5 / 2.75))
            {
                return c * (7.5625 * (t -= (2.25 / 2.75)) * t + .9375) + b;
            }
            else
            {
                return c * (7.5625 * (t -= (2.625 / 2.75)) * t + .984375) + b;
            }*/
            return EasingEquations.BounceOut(t, b, c, d);
        }
        public override double EaseIn(double t, double b, double c, double d)
        {
            //return c - EaseOut(d - t, 0, c, d) + b;
            return EasingEquations.BounceIn(t, b, c, d);
        }
        public override double EaseInOut(double t, double b, double c, double d)
        {
            //if (t < d / 2) return EaseIn(t * 2, 0, c, d) * .5 + b;
            //else return EaseOut(t * 2 - d, 0, c, d) * .5 + c * .5 + b;
            return EasingEquations.BounceInOut(t, b, c, d);
        }
    }
}
