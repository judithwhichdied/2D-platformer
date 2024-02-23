using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 6.0f;

    private const string _animGrounded = "Grounded";
    private const string _jumpButton = "space";
    private const string _animJump = "Jump";

    private Sensor_Bandit _groundSensor;
    private bool _grounded = false;

    private Animator _animator;
    private Rigidbody2D _player;

    private int _playerObject, _collideObject;

    private void Start()
    {
        _player = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();

        _playerObject = LayerMask.NameToLayer("Player");
        _collideObject = LayerMask.NameToLayer("Collider");
    }

    private void Update()
    {
        if (!_grounded && _groundSensor.State())
        {
            _grounded = true;
            _animator.SetBool(_animGrounded, _grounded);
        }

        if (_grounded && !_groundSensor.State())
        {
            _grounded = false;
            _animator.SetBool(_animGrounded, _grounded);
            _animator.SetTrigger(_animJump);
        }

        if (_player.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(_playerObject, _collideObject, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(_playerObject, _collideObject, false);
        }

        if (Input.GetKeyDown(_jumpButton) && _grounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _animator.SetTrigger(_animJump);
        _grounded = false;
        _animator.SetBool(_animGrounded, _grounded);
        _player.velocity = new Vector2(_player.velocity.x, _jumpForce);
        _groundSensor.Disable(0.2f);
    }
}
