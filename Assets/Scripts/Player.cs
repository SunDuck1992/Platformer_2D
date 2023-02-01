using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private TMP_Text _walletView;

    private bool _isGround;
    private float _rayDistance = 1.5f;
    private float _coordinataX;
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
            transform.Translate(_coordinataX = _speed * Time.deltaTime, 0, 0);
            _animator.SetFloat("Speed", Mathf.Abs(_speed));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_coordinataX = _speed * Time.deltaTime * -1, 0, 0);
            _animator.SetFloat("Speed", Mathf.Abs(_speed));
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }

        if (_coordinataX > 0 && !_isFaceRight)
        {
            Flip();
        }
        else if (_coordinataX < 0 && _isFaceRight)
        {
            Flip();
        }
    }

    private void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rigidbody.position, Vector2.down, _rayDistance, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
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
