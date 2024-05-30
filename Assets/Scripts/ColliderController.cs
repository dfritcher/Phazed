using UnityEngine;

public class ColliderController : MonoBehaviour
{
    #region Fields, Properties
    [SerializeField]
    private Transform _followTarget = null;

    [SerializeField]
    private Camera2DFollow _cameraFollow = null;

    [SerializeField]
    private float _yOffset = 0f;

    private float _colliderAdjustment;
    #endregion Fields, Properties (end)

    #region Methods
    private void FixedUpdate()
    {
        transform.position = new Vector3(_cameraFollow.transform.position.x - 10f,_cameraFollow.transform.position.y + _yOffset, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Player")
            return;
        if (this.tag == "Up")
        {
            //Move this item up by a specific amount and trigger the camera needs to move up some amount.
            _cameraFollow.MoveCameraUp = true;
            //ColliderManager.MoveCollidersUp();
            _cameraFollow.CameraTargetPos = new Vector3(_followTarget.position.x, _followTarget.position.y - 2f, _followTarget.position.z);
        }
        else if (tag == "Down")
        {

            _cameraFollow.MoveCameraDown = true;
            //ColliderManager.MoveCollidersDown();
            _cameraFollow.CameraTargetPos = new Vector3(_followTarget.position.x, _followTarget.position.y - 2f, _followTarget.position.z);
        }
    }
    #endregion Methods (end)
}
