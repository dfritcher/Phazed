using UnityEngine;
using System.Collections;

public class SpikeUtility {
    public static Color BlackSpike => Color.black;
    public static Color GraySpike => Color.gray;
    public static Color WhiteSpike => Color.white;

    public static Color FadedBlackSpike
    {
        get
        {
            if (_blackFadedSpike.Equals(default(Color)))
            {
                _blackFadedSpike = new Color(0f, 0f, 0f, .45f);
            }

            return _blackFadedSpike;
        }
    }
    private static Color _blackFadedSpike;

    public static Color FadedWhiteSpike
    {
        get
        {
            if (_whiteFadedSpike.Equals(default(Color)))
            {
                _whiteFadedSpike = new Color(1f, 1f, 1f, .55f);
            }

            return _whiteFadedSpike;
        }
    }
    private static Color _whiteFadedSpike;


}
