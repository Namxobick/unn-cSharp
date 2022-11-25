using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    private const string GroundTag = "Ground";
    private const string RunAnimation = "Running";
    private const string JumpAnimation = "Jumping";
    private const string FallAnimation = "Falling";

    [SerializeField] private float _runSpeed = 12f;
    [SerializeField] private float _jumpSpeed = 65f;

    [SerializeField] private Transform _groundCheckBottomMiddle;
    [SerializeField] private Transform _groundCheckBottomLeft;
    [SerializeField] private Transform _groundCheckBottomRight;

    [SerializeField] private Transform _groundCheckLeftMiddle;
    [SerializeField] private Transform _groundCheckLeftTop;
    [SerializeField] private Transform _groundCheckLeftBottom;

    [SerializeField] private Transform _groundCheckRightMiddle;
    [SerializeField] private Transform _groundCheckRightTop;
    [SerializeField] private Transform _groundCheckRightBottom;

    [SerializeField] private KeyCode _right;
    [SerializeField] private KeyCode _left;
    [SerializeField] private KeyCode _jump;

    [SerializeField] private string _nameLayerMask;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _playerAnimator;

    private bool _isGround;
    private bool _isPlayer;
    private bool _isWall;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Physics2D.Linecast(transform.position, _groundCheckBottomMiddle.position, 1 << LayerMask.NameToLayer(GroundTag)) ||
            Physics2D.Linecast(transform.position, _groundCheckBottomLeft.position, 1 << LayerMask.NameToLayer(GroundTag)) ||
            Physics2D.Linecast(transform.position, _groundCheckBottomRight.position, 1 << LayerMask.NameToLayer(GroundTag)))
        {
            _isGround = true;
        }
        else
        {
            _isGround = false;
        }

        if (Physics2D.Linecast(transform.position, _groundCheckLeftMiddle.position, 1 << LayerMask.NameToLayer(GroundTag)) ||
            Physics2D.Linecast(transform.position, _groundCheckLeftTop.position, 1 << LayerMask.NameToLayer(GroundTag)) ||
            Physics2D.Linecast(transform.position, _groundCheckLeftBottom.position, 1 << LayerMask.NameToLayer(GroundTag)) ||
            Physics2D.Linecast(transform.position, _groundCheckRightMiddle.position, 1 << LayerMask.NameToLayer(GroundTag)) ||
            Physics2D.Linecast(transform.position, _groundCheckRightTop.position, 1 << LayerMask.NameToLayer(GroundTag)) ||
            Physics2D.Linecast(transform.position, _groundCheckRightBottom.position, 1 << LayerMask.NameToLayer(GroundTag))
            )
        {
            _isWall = true;
        }
        else
        {
            _isWall = false;
        }

        if (Physics2D.Linecast(transform.position, _groundCheckBottomMiddle.position, 1 << LayerMask.NameToLayer(_nameLayerMask)) ||
            Physics2D.Linecast(transform.position, _groundCheckBottomLeft.position, 1 << LayerMask.NameToLayer(_nameLayerMask)) ||
            Physics2D.Linecast(transform.position, _groundCheckBottomRight.position, 1 << LayerMask.NameToLayer(_nameLayerMask)))
        {
            _isPlayer = true;
        }
        else
        {
            _isPlayer = false;
        }

        if (Input.GetKey(_right))
        {
            _rigidbody2D.velocity = new Vector2(_runSpeed, _rigidbody2D.velocity.y);
            _spriteRenderer.flipX = false;
        }

        if (Input.GetKey(_left))
        {
            _rigidbody2D.velocity = new Vector2(-_runSpeed, _rigidbody2D.velocity.y);
            _spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(_jump) && (_isGround || _isPlayer || _isWall))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpSpeed);
        }

        ChangeAnimation();
    }

    private void ChangeAnimation()
    {
        _playerAnimator.SetBool(RunAnimation, Mathf.Abs(_rigidbody2D.velocity.x) >= 0.1f);
        _playerAnimator.SetBool(JumpAnimation, _rigidbody2D.velocity.y >= 0.1f);
        _playerAnimator.SetBool(FallAnimation, _rigidbody2D.velocity.y <= -0.1f);
    }

}
