using UnityEngine;

public class BallControler : MonoBehaviour
{
    [Header("Force Settings")]
    [SerializeField]
    private float _maxForce;

    [SerializeField]
    private float _forceModifier;

    [Space]
    [Header("Common Settings")]
    [SerializeField] 
    private LayerMask _rayLayer;

    [SerializeField] 
    private LineRenderer _lineRenderer;

    private Rigidbody _rigidbody;

    private bool _isAiming = false, _isShooting = false;

    private Vector3 _startPositionMouse, _endPositionMouse;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_isAiming)
        {
            _endPositionMouse = GetPositionMouse();
            DrawLine(_startPositionMouse, _endPositionMouse);
        }

        if (_rigidbody.velocity.magnitude < new Vector3(0.05f, 0.05f, 0.05f).magnitude)
            _isShooting = false;
        else
            _isShooting = true;
    }

    private void OnMouseDown()
    {
        if (_isShooting == false)
        {
            _isAiming = true;
            _startPositionMouse = _rigidbody.transform.position;
        }
    }

    private void OnMouseUp()
    {
        if (_isAiming)
        {
            _isAiming = false;
            _isShooting = true;
            _lineRenderer.gameObject.SetActive(false);
            Hit();
        }
    }

    private void Hit()
    {
        Vector3 heading = _startPositionMouse - _endPositionMouse;

        var force = Mathf.Min((heading * _forceModifier).magnitude, _maxForce);
        heading = heading / heading.magnitude * force;
        _rigidbody.AddForce(heading * _forceModifier, ForceMode.Impulse);
    }

    private Vector3 GetPositionMouse()
    {
        Vector3 positionMouse = Vector3.zero;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _rayLayer))
        {
            positionMouse = hit.point;
        }

        return positionMouse;
    }

    private void DrawLine(Vector3 start, Vector3 end)
    {
        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);
        _lineRenderer.gameObject.SetActive(true);
    }
}
