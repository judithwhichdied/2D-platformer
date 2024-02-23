using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;

    private Animator _animator;
    private Rigidbody2D _player;

    private const string _animState = "AnimState";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");

        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        _player.velocity = new Vector2 (inputX * _speed, _player.velocity.y);

        if (Mathf.Abs(inputX) > Mathf.Epsilon)
            _animator.SetInteger(_animState, 2);
        else
            _animator.SetInteger(_animState, 0);
    }
}
