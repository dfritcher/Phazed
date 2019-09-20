using UnityEngine;

public class BlackSpike : SpikeBase
{
    public override void Disable()
    {
        _spikeSprite.color = SpikeUtility.FadedBlackSpike;
        _spikeSprite.sortingOrder = 0;
        _collider2D.enabled = false;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public override void Enable()
    {
        _spikeSprite.sortingOrder = 1;
        _spikeSprite.color = PlatformUtility.BlackPlatform;
        _collider2D.enabled = true;
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
}
