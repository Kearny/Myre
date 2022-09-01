using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform cameraPivot; // The object camera uses to pivot (look up and down) 
    public LayerMask collisionLayers; // The layers that the camera will collide with
    public float cameraFollowSpeed = 0.2f;
    public float lookAngle; // Camera looking up and down
    public float pivotAngle;
    public float minimumPivotAngle = -35;
    public float maximumPivotAngle = 35;
    public float cameraLookSpeed = 2;
    public float cameraPivotSpeed = 2;
    public float cameraCollisionOffset = 0.2f; // How much the camera will jump off of the collision object
    public float minimumCollisionOffset = 0.2f; // The minimum distance the camera will be from the collision object

    Vector3 _cameraFollowVelocity = Vector3.zero;
    Transform _cameraTransform; // The transform of the actual camera object in the scene
    Vector3 _cameraVectorPosition;
    float _defaultPosition;
    InputManager _inputManager;
    Transform _targetTransform; // The object camera will follow

    void Awake()
    {
        _inputManager = FindObjectOfType<InputManager>();
        _targetTransform = FindObjectOfType<PlayerManager>().transform;
        Debug.Assert(Camera.main != null, "Camera.main != null");
        _cameraTransform = Camera.main.transform;
        _defaultPosition = _cameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }

    void FollowTarget()
    {
        var targetPosition =
            Vector3.SmoothDamp(transform.position, _targetTransform.position, ref _cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }

    void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle += _inputManager.cameraInputX * cameraLookSpeed;
        pivotAngle -= _inputManager.cameraInputY * cameraPivotSpeed;
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);

        cameraPivot.localRotation = targetRotation;
    }

    void HandleCameraCollisions()
    {
        var targetPosition = _defaultPosition;

        RaycastHit hit;
        var direction = _cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast
                (cameraPivot.transform.position, cameraCollisionOffset, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            var distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = -(distance - cameraCollisionOffset);
        }

        if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
            targetPosition -= minimumCollisionOffset;

        _cameraVectorPosition.z = Mathf.Lerp(_cameraTransform.localPosition.z, targetPosition, 0.2f);
        _cameraTransform.localPosition = _cameraVectorPosition;
    }
}