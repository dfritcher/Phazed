namespace Easing
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    public delegate double EaseDelegate(double t, double b, double c, double d);

    abstract class Ease
    {
        protected const double TWO_PI = Math.PI * 2;
        protected const double HALF_PI = Math.PI / 2;

        public abstract double EaseIn(double t, double b, double c, double d);
        public abstract double EaseOut(double t, double b, double c, double d);
        public abstract double EaseInOut(double t, double b, double c, double d);

        public EaseDelegate GetDelegateByType(EaseType type)
        {
            switch (type)
            {
                case EaseType.EaseIn: return EaseIn;
                case EaseType.EaseOut: return EaseOut;
                case EaseType.EaseInOut: return EaseInOut;
                default: return EaseIn;
            }
        }

        public static EaseDelegate GetDelegate(EaseEquation equation, EaseType type)
        {
            switch (equation)
            {
                case EaseEquation.Back:
                    return new Back().GetDelegateByType(type);
                case EaseEquation.Bounce:
                    return new Bounce().GetDelegateByType(type);
                case EaseEquation.Circ:
                    return new Circ().GetDelegateByType(type);
                case EaseEquation.Cubic:
                    return new Cubic().GetDelegateByType(type);
                case EaseEquation.Elastic:
                    return new Elastic().GetDelegateByType(type);
                case EaseEquation.Expo:
                    return new Expo().GetDelegateByType(type);
                case EaseEquation.Linear:
                    return new Linear().GetDelegateByType(type);
                case EaseEquation.Quad:
                    return new Quad().GetDelegateByType(type);
                case EaseEquation.Quart:
                    return new Quart().GetDelegateByType(type);
                case EaseEquation.Quint:
                    return new Quint().GetDelegateByType(type);
                case EaseEquation.Sine:
                    return new Sine().GetDelegateByType(type);
                case EaseEquation.Strong:
                    return new Strong().GetDelegateByType(type);

                /*case EaseEquation.Back:
                    return new Back().GetDelegateByType(type);
                    switch (type)
                    {
                        case EaseType.EaseIn: return EasingEquations.BackIn;
                        case EaseType.EaseOut: return EasingEquations.BackOut;
                        case EaseType.EaseInOut: return EasingEquations.BackInOut;
                        default: return EasingEquations.BackIn;
                    }
                case EaseEquation.Bounce:
                    switch (type)
                    {
                        case EaseType.EaseIn: return EasingEquations.BounceIn;
                        case EaseType.EaseOut: return EasingEquations.BounceOut;
                        case EaseType.EaseInOut: return EasingEquations.BounceInOut;
                        default: return EasingEquations.BounceIn;
                    }
                case EaseEquation.Circ:
                    switch (type)
                    {
                        case EaseType.EaseIn: return EasingEquations.CircIn;
                        case EaseType.EaseOut: return EasingEquations.CircOut;
                        case EaseType.EaseInOut: return EasingEquations.CircInOut;
                        default: return EasingEquations.CircIn;
                    }
                case EaseEquation.Cubic:
                    switch (type)
                    {
                        case EaseType.EaseIn: return EasingEquations.CubicIn;
                        case EaseType.EaseOut: return EasingEquations.CubicOut;
                        case EaseType.EaseInOut: return EasingEquations.CubicInOut;
                        default: return EasingEquations.CubicIn;
                    }
                case EaseEquation.Elastic:
                    switch (type)
                    {
                        case EaseType.EaseIn: return EasingEquations.ElasticIn;
                        case EaseType.EaseOut: return EasingEquations.ElasticOut;
                        case EaseType.EaseInOut: return EasingEquations.ElasticInOut;
                        default: return EasingEquations.ElasticIn;
                    }

                case EaseEquation.Sine:
                    switch(type)
                    {
                        case EaseType.EaseIn: return EasingEquations.SineIn;
                        case EaseType.EaseOut: return EasingEquations.SineOut;
                        case EaseType.EaseInOut: return EasingEquations.SineInOut;
                        default: return EasingEquations.SineIn;
                    }*/
                

                default:
                    return EasingEquations.SineIn;
            }
        }
    }

}