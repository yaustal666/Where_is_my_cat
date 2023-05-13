using UnityEngine;

public class Girl : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 1f;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sprite;

    private States State
    {
        get => (States)_animator.GetInteger("State");
        set => _animator.SetInteger("State", (int)value);
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            State = States.run;
            Run();
        }

        if (IsGrounded() && !Input.GetButton("Horizontal"))
        {
            State = States.idle;
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        _sprite.flipX = direction.x < 0.0f;
        transform.position =
            Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void Jump()
    {
        _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        return _rb.velocity.y == 0;
    }
}

public enum States
{
    idle,
    turn,
    run
}