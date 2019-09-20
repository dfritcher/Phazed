namespace Easing
{
    using UnityEngine;
    using System.Collections;
    using System;

    //public delegate double EaseDelegate(double t, double b, double c, double d);

    static public class EasingEquations
    {
        static private readonly double TWO_PI = Math.PI * 2;
        static private readonly double HALF_PI = Math.PI / 2;


        //Back, Bounce, Circ, Cubic, Elastic, Expo, Linear, Quad, Quart, Quint, Sine, Strong

        #region Back

        public static double BackIn(double t, double b, double c, double d)
        {
            double s = 1.70158;
            return c * (t /= d) * t * ((s + 1) * t - s) + b;
        }
        public static double BackOut(double t, double b, double c, double d)
        {
            double s = 1.70158;
            return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
        }
        public static double BackInOut(double t, double b, double c, double d)
        {
            double s = 1.70158;
            if ((t /= d / 2) < 1) return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b;
            return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b;
        }


        public static double BackIn(double t, double b, double c, double d, double s)
        {
            return c * (t /= d) * t * ((s + 1) * t - s) + b;
        }
        public static double BackOut(double t, double b, double c, double d, double s)
        {
            return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
        }
        public static double BackInOut(double t, double b, double c, double d, double s)
        {
            if ((t /= d / 2) < 1) return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b;
            return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b;
        }
        #endregion

        #region Bounce

        public static double BounceOut(double t, double b, double c, double d)
        {
            if ((t /= d) < (1 / 2.75))
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
            }
        }
        public static double BounceIn(double t, double b, double c, double d)
        {
            return c - BounceOut(d - t, 0, c, d) + b;
        }
        public static double BounceInOut(double t, double b, double c, double d)
        {
            if (t < d / 2) return BounceIn(t * 2, 0, c, d) * .5 + b;
            else return BounceOut(t * 2 - d, 0, c, d) * .5 + c * .5 + b;
        }

        #endregion

        #region Circ

        public static double CircIn(double t, double b, double c, double d)
        {
            return -c * (Math.Sqrt(1 - (t /= d) * t) - 1) + b;
        }
        public static double CircOut(double t, double b, double c, double d)
        {
            return c * Math.Sqrt(1 - (t = t / d - 1) * t) + b;
        }
        public static double CircInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1) return -c / 2 * (Math.Sqrt(1 - t * t) - 1) + b;
            return c / 2 * (Math.Sqrt(1 - (t -= 2) * t) + 1) + b;
        }

        #endregion

        #region Cubic

        static public double CubicIn(double t, double b, double c, double d)
        {
            return c * (t /= d) * t * t + b;
        }
        static public double CubicOut(double t, double b, double c, double d)
        {
            return c * ((t = t / d - 1) * t * t + 1) + b;
        }
        static public double CubicInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t + 2) + b;
        }
        #endregion

        #region Elastic
        
        static public double ElasticIn(double t, double b, double c, double d)
        {
            double a = 0;
            double p = d * .3;
            double s;
            if (t == 0) return b; if ((t /= d) == 1) return b + c;
            if (a < Math.Abs(c)) { a = c; s = p / 4; }
            else
                s = p / TWO_PI * Math.Asin(c / a);
            return -(a * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - s) * TWO_PI / p)) + b;
        }
        static public double ElasticOut(double t, double b, double c, double d)
        {
            double a = 0;
            double p = d * .3;
            double s;
            if (t == 0) return b; if ((t /= d) == 1) return b + c;
            if (a < Math.Abs(c)) { a = c; s = p / 4; }
            else
                s = p / TWO_PI * Math.Asin(c / a);
            return (a * Math.Pow(2, -10 * t) * Math.Sin((t * d - s) * TWO_PI / p) + c + b);
        }
        static public double ElasticInOut(double t, double b, double c, double d)
        {
            double a = 0;
            double p = d * (.3 * 1.5);
            double s;
            if (t == 0) return b; if ((t /= d / 2) == 2) return b + c;
            if (a < Math.Abs(c)) { a = c; s = p / 4; }
            else
                s = p / TWO_PI * Math.Asin(c / a);
            if (t < 1)
                return -.5 * (a * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - s) * TWO_PI / p)) + b;
            return a * Math.Pow(2, -10 * (t -= 1)) * Math.Sin((t * d - s) * TWO_PI / p) * .5 + c + b;
        }


        public static double ElasticIn(double t, double b, double c, double d, double a, double p)
        {
            double s;
            if (t == 0)
                return b;
            if ((t /= d) == 1)
                return b + c;
            if (p == 0)
                p = d * .3;
            if (a < Math.Abs(c))
            {
                a = c;
                s = p / 4;
            }
            else
                s = p / TWO_PI * Math.Asin(c / a);
            return -(a * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - s) * TWO_PI / p)) + b;
        }
        public static double ElasticOut(double t, double b, double c, double d, double a, double p)
        {
            double s;
            if (t == 0)
                return b;
            if ((t /= d) == 1)
                return b + c;
            if (p == 0)
                p = d * .3;
            if (a < Math.Abs(c))
            {
                a = c;
                s = p / 4;
            }
            else
                s = p / TWO_PI * Math.Asin(c / a);
            return (a * Math.Pow(2, -10 * t) * Math.Sin((t * d - s) * TWO_PI / p) + c + b);
        }
        public static double ElasticInOut(double t, double b, double c, double d, double a, double p)
        {
            double s;
            if (t == 0)
                return b;
            if ((t /= d / 2) == 2)
                return b + c;
            if (p == 0)
                p = d * (.3 * 1.5);
            if (a < Math.Abs(c))
            {
                a = c;
                s = p / 4;
            }
            else
                s = p / TWO_PI * Math.Asin(c / a);
            if (t < 1)
                return -.5 * (a * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - s) * TWO_PI / p)) + b;
            return a * Math.Pow(2, -10 * (t -= 1)) * Math.Sin((t * d - s) * TWO_PI / p) * .5 + c + b;
        }
        #endregion

        #region Expo
        public static double ExpoIn(double t, double b, double c, double d)
        {
            return (t == 0) ? b : c * Math.Pow(2, 10 * (t / d - 1)) + b - c * 0.001;
        }
        public static double ExpoOut(double t, double b, double c, double d)
        {
            return (t == d) ? b + c : c * (-Math.Pow(2, -10 * t / d) + 1) + b;
        }
        public static double ExpoInOut(double t, double b, double c, double d)
        {
            if (t == 0) return b;
            if (t == d) return b + c;
            if ((t /= d / 2) < 1) return c / 2 * Math.Pow(2, 10 * (t - 1)) + b;
            return c / 2 * (-Math.Pow(2, -10 * --t) + 2) + b;
        }
        #endregion

        #region Linear
        public static double LinearNone(double t, double b, double c, double d)
        {
            return c * t / d + b;
        }
        public static double LinearIn(double t, double b, double c, double d)
        {
            return c * t / d + b;
        }
        public static double LinearOut(double t, double b, double c, double d)
        {
            return c * t / d + b;
        }
        public static double LinearInOut(double t, double b, double c, double d)
        {
            return c * t / d + b;
        }
        #endregion

        #region Quad
        public static double QuadIn(double t, double b, double c, double d)
        {
            return c * (t /= d) * t + b;
        }
        public static double QuadOut(double t, double b, double c, double d)
        {
            return -c * (t /= d) * (t - 2) + b;
        }
        public static double QuadInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t + b;
            return -c / 2 * ((--t) * (t - 2) - 1) + b;
        }
        #endregion

        #region Quart
        public static double QuartIn(double t, double b, double c, double d)
        {
            return c * (t /= d) * t * t * t + b;
        }
        public static double QuartOut(double t, double b, double c, double d)
        {
            return -c * ((t = t / d - 1) * t * t * t - 1) + b;
        }
        public static double QuartInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t * t + b;
            return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
        }
        #endregion

        #region Quint
        public static double QuintIn(double t, double b, double c, double d)
        {
            return c * (t /= d) * t * t * t * t + b;
        }
        public static double QuintOut(double t, double b, double c, double d)
        {
            return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
        }
        public static double QuintInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
        }
        #endregion

        #region Sine
        public static double SineIn(double t, double b, double c, double d)
        {
            return -c * Math.Cos(t / d * HALF_PI) + c + b;
        }

        public static double SineOut(double t, double b, double c, double d)
        {
            return c * Math.Sin(t / d * HALF_PI) + b;
        }

        public static double SineInOut(double t, double b, double c, double d)
        {
            return -c / 2 * (Math.Cos(Math.PI * t / d) - 1) + b;
        }
        #endregion

        #region Strong
        public static double StrongIn(double t, double b, double c, double d)
        {
            return c * (t /= d) * t * t * t * t + b;
        }
        public static double StrongOut(double t, double b, double c, double d)
        {
            return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
        }
        public static double StrongInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
        }
        #endregion
    }
}
