using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class CameraRotationChanger : MonoBehaviour
{
    [SerializeField]
    private Vector3 _CameraAngle;

    private TargetFollower _targetFollower;

    private void Awake()
    {
        _targetFollower = Camera.main.GetComponent<TargetFollower>();
    }

    private void OnValidate()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BallControler ballControler))
        {
            _targetFollower.SetRotation(_CameraAngle);
        }
    }
}
