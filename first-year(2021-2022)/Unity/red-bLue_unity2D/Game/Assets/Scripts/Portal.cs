using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private PlayerType _type;

    private bool _busy = false;

    public Action<bool> OnBusyChanged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SwtichBusy(collision, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SwtichBusy(collision, false);
    }

    private void SwtichBusy(Collider2D collision, bool  swithMode)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (_type == player.Type)
            {
                _busy = swithMode;
                OnBusyChanged?.Invoke(_busy);
            }
        }
    }
}
