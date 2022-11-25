using UnityEngine;

public class Gems : MonoBehaviour
{
    [SerializeField] private PlayerType _type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (player.Type == _type)
            {
                Destroy(gameObject);
            }
        }
    }
}

