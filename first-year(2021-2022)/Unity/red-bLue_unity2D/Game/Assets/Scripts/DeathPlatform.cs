using System;
using UnityEngine;

public class DeathPlatform : MonoBehaviour
{
    [SerializeField] private PlayerType _type;
    [SerializeField] private bool _generalDeath;

    public Action OnPlayerTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (_generalDeath || player.Type == _type)
                OnPlayerTrigger?.Invoke();
        }
    }
}
