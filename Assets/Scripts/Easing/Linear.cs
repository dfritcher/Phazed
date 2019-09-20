namespace Easing
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;


    class Linear : Ease
    {
        public double EaseNone(double t, double b, double c, double d)
        {
            //return c * t / d + b;
            return EasingEquations.LinearNone(t, d, c, d);
        }
        public override double EaseIn(double t, double b, double c, double d)
        {
            //return c * t / d + b;
            return EasingEquations.LinearIn(t, d, c, d);
        }
        public override double EaseOut(double t, double b, double c, double d)
        {
            //return c * t / d + b;
            return EasingEquations.LinearOut(t, d, c, d);
        }
        public override double EaseInOut(double t, double b, double c, double d)
        {
            //return c * t / d + b;
            return EasingEquations.LinearInOut(t, d, c, d);
        }
    }
}
