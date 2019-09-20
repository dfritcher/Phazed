using UnityEngine;
using System.Collections;

public interface IEase 
{

    double EaseIn(double t, double b, double c, double d);

    double EaseOut(double t, double b, double c, double d);

    double EaseInOut(double t, double b, double c, double d);

}
