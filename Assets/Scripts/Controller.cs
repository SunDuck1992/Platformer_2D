using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Controller : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private bool _isGround;
    private float _rayDistance = 1.5f;
    private float _coordinataX;
    private bool _isFaceRight = true;
    private Rigidbody2D _rigidbody;    

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_coordinataX = _speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_coordinataX = _speed * Time.deltaTime * -1, 0, 0);
        }

        if (_coordinataX > 0 && !_isFaceRight)
        {
            Flip();
        }
        else if (_coordinataX < 0 && _isFaceRight)
        {
            Flip();
        }

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
