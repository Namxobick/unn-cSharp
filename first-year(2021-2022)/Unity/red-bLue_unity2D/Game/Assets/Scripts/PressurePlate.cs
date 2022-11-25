using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private const string ActivatedAnimation = "Activated";

    [SerializeField] private Animator _stoneAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            _stoneAnimator.SetBool(ActivatedAnimation, true); 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
            _stoneAnimator.SetBool(ActivatedAnimation, false);
    }
}
