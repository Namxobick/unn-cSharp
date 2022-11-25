using UnityEngine;

[ExecuteAlways]
public class TargetFollower : MonoBehaviour
{

    [Header("Common Settings")]
    [SerializeField]
    private Transform _targetTransform;

    [SerializeField]
    private float _distanceToTarget = 3.0f;

    [SerializeField]
    private float _followSpeed = 3.0f;

    [Space]
    [Header("Rotation Settings")]
    [SerializeField]
    private Vector3 _cameraRotation = new Vector3(70, 0, 0);

    [SerializeField]
    private float _rotationSpeed = 0.1f;

    [Space]
    [Header("Spaces Settings")]
    [SerializeField]
    private Vector3 _cameraSpace = new Vector3(0, 0, 0);

    [Space]
    [Header("Follow or No in Game")]
    [SerializeField] private bool _follow = false;

    [Space]
    [Header("Use Rotation or No in Game")]
    [SerializeField] private bool _useRotation = false;

    [Space]
    [Header("Use Auto Distance or No in Game")]
    [SerializeField] private bool _autoDistance = false;

    private Quaternion _startRotation;

    private Vector3 _startPosition;
    private Vector3 _startFollowRotation;
    private Vector3 _startFollowSpaces;

    private float _startDistance;

    private Vector3 _cameraRelativePosition
    {
        get
        {
            Vector3 relativePosition = _targetTransform.position - transform.forward * _distanceToTarget;
            return relativePosition + _cameraSpace;
        }
    }
    public void Init()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _startFollowRotation = _cameraRotation;
        _startFollowSpaces = _cameraSpace;
        _startDistance = _distanceToTarget;
    }

    public void MoveToStartPosition()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _distanceToTarget = _startDistance;
        _cameraRotation = _startFollowRotation;
        _cameraSpace = _startFollowSpaces;
    }

    public void SetRotation(Vector3 cameraRotation)
    {
        _cameraRotation = cameraRotation;
    }


    public void Follow(bool useRotation = true, float distance = -1)
    {
        _useRotation = useRotation;
        if (distance == -1)
        {
            Follow();
            return;
        }
        _distanceToTarget = distance;
        Follow();
    }

    public void Follow()
    {
        if (_autoDistance == true)
        {
            _distanceToTarget = Vector3.Distance(transform.position, _targetTransform.position);
        }
        transform.position = _cameraRelativePosition;
        _follow = true;
    }
    public void StopFollowing()
    {
        _follow = false;
    }

    public void FixedUpdate()
    {
        if (_follow == false)
            return;

        transform.position = Vector3.Lerp(transform.position, _cameraRelativePosition, _followSpeed * Time.deltaTime);

        if (_useRotation == false)
            return;

        Vector3 cameraRotation = new Vector3((_cameraRotation.x < 0) ? _cameraRotation.x + 360 : _cameraRotation.x,
                                             (_cameraRotation.y < 0) ? _cameraRotation.y + 360 : _cameraRotation.y,
                                             (_cameraRotation.z < 0) ? _cameraRotation.z + 360 : _cameraRotation.z);

        float distance = Vector3.Distance(transform.localEulerAngles, cameraRotation);
        if (distance < _rotationSpeed)
        {
            transform.localEulerAngles = _cameraRotation;
            return;
        }

        transform.localEulerAngles += (_cameraRotation - transform.eulerAngles) * Time.deltaTime * _rotationSpeed;

    }

    private void Start()
    {
        Init();
        Follow();
    }
}
