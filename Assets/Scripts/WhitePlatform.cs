public class WhitePlatform : PlatformBase
{    
    public override void Disable()
    {
        _platformSprite.color = PlatformUtility.FadedWhitePlatform; 
        _platformSprite.sortingOrder = 0;
        _collider2D.enabled = false;
    }

    public override void Enable()
    {
        _platformSprite.sortingOrder = 1;
        _platformSprite.color = PlatformUtility.WhitePlatform;
        _collider2D.enabled = true;
    }
}