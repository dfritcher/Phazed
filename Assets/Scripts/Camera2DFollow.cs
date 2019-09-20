using UnityEngine;
public class Camera2DFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f; //value between 0 and 1, closer to 1 goes faster 
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;   

    [SerializeField]
    private Vector3 _cameraStartPos = Vector3.zero;

    public Vector3 CameraTargetPos { get; set; }
    public bool MoveCameraUp { get; set; }
    public bool MoveCameraDown { get; set; }

    // boolean value to be set between levels to know when to start moving the player.
    public bool Initialized { get; set; }


    // Use this for initialization
    private void Start()
    {
        m_LastTargetPosition = target.position;
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;
        CameraTargetPos = Vector3.zero;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!Initialized)
            return;

        CameraFollowStatic();
        //CameraFollowWithDampener();
    }

    private void CameraFollowWithDampener()
    {
        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (target.position - m_LastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
        {
            m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        }
        else
        {
            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

        transform.position = newPos;

        m_LastTargetPosition = target.position;
    }

    private void CameraFollowStatic()
    {
        if (!target.gameObject.activeSelf)
            return;       

        if (MoveCameraUp || MoveCameraDown)
        {
            //if (MoveCameraUp)
            //    Debug.Log("<color=#FFF700> Move UP is true.</color>");
            //if (MoveCameraDown)
            //    Debug.Log("<color=#06A250> Move DOWN is true.</color>");
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, CameraTargetPos.y, smoothSpeed), transform.position.z);
        }
        //If we are moving the camera check if we have reached our destination.
        if (MoveCameraUp && transform.position.y >= CameraTargetPos.y)
        {
            MoveCameraUp = false;
            //Debug.Log(string.Format("<color=#FF0022> Resetting MoveCameraUp Flag.</color>"));
        }
        if (MoveCameraDown && transform.position.y <= CameraTargetPos.y)
        {
            MoveCameraDown = false;
            //Debug.Log(string.Format("<color=#FF0022> Resetting MoveCameraDown Flag.</color>"));
        }
        //Debug.Log(string.Format("<color=#FFF700> Player Position: {0}</color>", target.position.y));
        //Debug.Log(string.Format("<color=#06A250> Camera Position: {0}</color>", _currentCameraYPos));

        //Keep the camera just to the right of where the player is
        transform.position = new Vector3(target.position.x + 10, transform.position.y, transform.position.z);
    }

    public void ResetCameraPosition()
    {
        transform.position = _cameraStartPos;
    }
}