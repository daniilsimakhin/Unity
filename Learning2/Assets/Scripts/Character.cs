using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float _speed = 10;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {   
        if (Input.GetKey(KeyCode.LeftArrow))
        {        
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
            _spriteRenderer.flipX = true;
            _animator.SetFloat("Speed", 2);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
            _spriteRenderer.flipX = false;           
            _animator.SetFloat("Speed", 2);
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
    }
}
