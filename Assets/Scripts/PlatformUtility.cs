using UnityEngine;
public static class PlatformUtility 
{
    public static Color BlackPlatform => Color.black;
    public static Color GrayPlatform => Color.gray;
    public static Color WhitePlatform => Color.white;

    public static Color FadedBlackPlatform
    {
        get
        {
            if (_blackFadedPlatform.Equals(default(Color)))
            {
                _blackFadedPlatform = new Color(0f, 0f, 0f, .5f);
            }

            return _blackFadedPlatform;
        }
    }
    private static Color _blackFadedPlatform;

    public static Color FadedWhitePlatform
    {
        get
        {
            if (_whiteFadedPlatform.Equals(default(Color)))
            {
                _whiteFadedPlatform = new Color(1f, 1f, 1f, .5f);
            }

            return _whiteFadedPlatform;
        }
    }
    private static Color _whiteFadedPlatform;

}
