using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class PlatformBase : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer _platformSprite;
    
    [SerializeField]
    protected BoxCollider2D _collider2D;

    public abstract void Disable();
    public abstract void Enable();

}
