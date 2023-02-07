using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private TMP_Text _walletView;
    [SerializeField] private LayerMask _layerMask;

    public readonly int speed = Animator.StringToHash("Speed");

    private bool _isGround;
    private float _rayDistance = 1.5f;
    private float _direction;
    private bool _isFaceRight = true;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private int _wallet = 0;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    public void ApplySkulls(int skull)
    {
        _wallet += skull;
        _walletView.text = _wallet.ToString();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_direction = _speed * Time.deltaTime, 0, 0);
            _animator.SetFloat(speed, Mathf.Abs(_speed));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_direction = _speed * Time.deltaTime * -1, 0, 0);
            _animator.SetFloat(speed, Mathf.Abs(_speed));
        }
        else
        {
            _animator.SetFloat(speed, 0);
        }

        if (_direction > 0 && !_isFaceRight)
        {
            Flip();
        }
        else if (_direction < 0 && _isFaceRight)
        {
            Flip();
        }
    }

    private void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rigidbody.position, Vector2.down, _rayDistance, _layerMask);

        if (hit.collider)
        {
            _isGround = true;
        }
        else
        {
            _isGround = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
    }

    private void Flip()
    {
        _isFaceRight = !_isFaceRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
