using System;
using UnityEngine;

public class CrossingWithBall : MonoBehaviour
{
    public Action OnTriggerBall;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out BallControler ball))
            OnTriggerBall?.Invoke();
    }
}
