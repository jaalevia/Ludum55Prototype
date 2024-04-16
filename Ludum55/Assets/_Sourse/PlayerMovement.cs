using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _speed;
    [SerializeField] private Rigidbody2D _rigidbody;
    private Animator _animator;
    private string _currentAnimation;

    const string PLAYERIDLE_UP = "PlayerIdle_Up";
    const string PLAYERIDLE_DOWN = "PlayerIdle_Down";
    const string PLAYERIDLE_LEFT = "PlayerIdle_Left";
    const string PLAYERIDLE_RIGHT = "PlayerIdle_Right";

    const string PLAYERMOVE_UP = "PlayerMove_Up";
    const string PLAYERMOVE_DOWN = "PlayerMove_Down";
    const string PLAYERMOVE_LEFT = "PlayerMove_Left";
    const string PLAYERMOVE_RIGHT = "PlayerMove_Right";

    private Vector2 _movement;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        CheckAnimation();
        _rigidbody.MovePosition(_rigidbody.position + _movement * _speed * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        if (DialogueManager.GetInstance().DialogueIsActive)
        {
            _speed = 0;
        }
        else
        {
            _speed = 5;
        }
        _rigidbody.MovePosition(_rigidbody.position + _movement * _speed * Time.fixedDeltaTime);
    }

    private void CheckAnimation()
    {
        if (_movement.y == 1)
        {
            ChangeAnimationState(PLAYERMOVE_UP);
        }
        else if (_movement.y == -1)
        {
            ChangeAnimationState(PLAYERMOVE_DOWN);
        }
        else if (_movement.x == 1)
        {
            ChangeAnimationState(PLAYERMOVE_RIGHT);
        }
        else if (_movement.x == -1)
        {
            ChangeAnimationState(PLAYERMOVE_LEFT);
        }


        if (_currentAnimation == PLAYERMOVE_UP & (_movement.x == 0 & _movement.y == 0))
        {
            ChangeAnimationState(PLAYERIDLE_UP);
        }
        else if (_currentAnimation == PLAYERMOVE_DOWN & (_movement.x == 0 & _movement.y == 0))
        {
            ChangeAnimationState(PLAYERIDLE_DOWN);
        }
        else if (_currentAnimation == PLAYERMOVE_RIGHT & (_movement.x == 0 & _movement.y == 0))
        {
            ChangeAnimationState(PLAYERIDLE_RIGHT);
        }
        else if (_currentAnimation == PLAYERMOVE_LEFT & (_movement.x == 0 & _movement.y == 0))
        {
            ChangeAnimationState(PLAYERIDLE_LEFT);
        }

    }

    private void ChangeAnimationState(string animation, float crossfade = 0.2f)
    {
        if (_currentAnimation == animation) return;

        _animator.Play(animation);

        _currentAnimation = animation;
    }
}
