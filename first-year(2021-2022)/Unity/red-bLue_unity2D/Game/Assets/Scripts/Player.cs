using UnityEngine;

public enum PlayerType { Blue, Red };

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerType _type;

    public PlayerType Type => _type;

}
