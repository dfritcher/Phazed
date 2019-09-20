using UnityEngine;
using System.Collections;

public abstract class SpikeBase : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer _spikeSprite;

    [SerializeField]
    protected PolygonCollider2D _collider2D;

    public abstract void Disable();
    public abstract void Enable();
}
