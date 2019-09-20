﻿namespace Easing
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;


    class Back : Ease
    {
        public override double EaseIn(double t, double b, double c, double d)
        {
            /*double s = 1.70158;
            return c * (t /= d) * t * ((s + 1) * t - s) + b;*/
            return EasingEquations.BackIn(t,b,c,d);
        }
        public override double EaseOut(double t, double b, double c, double d)
        {
            /*double s = 1.70158;
            return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;*/
            return EasingEquations.BackOut(t, b, c, d);
        }
        public override double EaseInOut(double t, double b, double c, double d)
        {
            /*double s = 1.70158;
            if ((t /= d / 2) < 1) return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b;
            return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b;*/
            return EasingEquations.BackInOut(t, b, c, d);
        }


        public static double EaseIn(double t, double b, double c, double d, double s)
        {
            //return c * (t /= d) * t * ((s + 1) * t - s) + b;
            return EasingEquations.BackIn(t, b, c, d);
        }
        public static double EaseOut(double t, double b, double c, double d, double s)
        {
            //return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
            return EasingEquations.BackOut(t, b, c, d);
        }
        public static double EaseInOut(double t, double b, double c, double d, double s)
        {
            /*if ((t /= d / 2) < 1) return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b;
            return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b;*/
            return EasingEquations.BackInOut(t, b, c, d);
        }
    }
}
