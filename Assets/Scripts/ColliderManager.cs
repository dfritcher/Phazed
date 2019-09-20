using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    #region Fields, Properties
    [SerializeField]
    private Transform _topCollider = null;

    [SerializeField]
    private Transform _bottomCollider = null;

    [SerializeField]
    private Vector3 _topColliderStartPos = Vector3.zero;

    [SerializeField]
    private Vector3 _bottomColliderStartPos = Vector3.zero;

    static private ColliderManager _instance = null;
    #endregion Fields, Properties (end)

    #region Methods

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        _topCollider.gameObject.SetActive(false);
        _bottomCollider.gameObject.SetActive(false);
    }

    static public void DisableColliders()
    {
        _instance._topCollider.gameObject.SetActive(false);
        _instance._bottomCollider.gameObject.SetActive(false);
    }

    static public void EnableColliders()
    {
        _instance._topCollider.gameObject.SetActive(true);
        _instance._bottomCollider.gameObject.SetActive(true);
        
    }

    static public void ResetColliderPositions()
    {
        _instance._topCollider.position = _instance._topColliderStartPos;
        _instance._bottomCollider.position = _instance._bottomColliderStartPos;
    }
    #endregion Methods (end)
}
