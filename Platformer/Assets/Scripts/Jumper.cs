using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 6.0f;

    private const string AnimGrounded = "Grounded";
    private const string JumpButton = "space";
    private const string AnimJump = "Jump";
    private const string GroundSensorName = "GroundSensor";
    private const string PlayerName = "Player";
    private const string ColliderName = "Collider";

    private Sensor_Bandit _groundSensor;
    private bool _grounded = false;

    private Animator _animator;
    private Rigidbody2D _player;

    private int _playerObject, _collideObject;

    private void Start()
    {
        _player = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _groundSensor = transform.Find(GroundSensorName).GetComponent<Sensor_Bandit>();

        _playerObject = LayerMask.NameToLayer(PlayerName);
        _collideObject = LayerMask.NameToLayer(ColliderName);
    }

    private void Update()
    {
        if (!_grounded && _groundSensor.State())
        {
            _grounded = true;
            _animator.SetBool(AnimGrounded, _grounded);
        }

        if (_grounded && !_groundSensor.State())
        {
            _grounded = false;
            _animator.SetBool(AnimGrounded, _grounded);
            _animator.SetTrigger(AnimJump);
        }

        if (_player.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(_playerObject, _collideObject, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(_playerObject, _collideObject, false);
        }

        if (Input.GetKeyDown(JumpButton) && _grounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        float delay = 0.2f;

        _animator.SetTrigger(AnimJump);
        _grounded = false;
        _animator.SetBool(AnimGrounded, _grounded);
        _player.velocity = new Vector2(_player.velocity.x, _jumpForce);
        _groundSensor.Disable(delay);
    }
}
