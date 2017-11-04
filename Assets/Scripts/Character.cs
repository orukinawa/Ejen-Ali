using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Range(0.0f, 10.0f)] public float _movementSpeed;
    public float _jumpStrength;
    public int _energy;
    
    public LayerMask _whatIsGround;
    public Animator _bottomPartAnimator;
    public Transform _groundCheck;
    public Transform _pushCheck;
    
    [HideInInspector] public bool _playerCanMove = true;
    [HideInInspector]  public bool _timeFreezeActivation = false;
    
    public AudioClip _pickEnergySFX;
    public AudioClip _deathSFX;
    public AudioClip _jumpSFX;

    //-------------------------------------------------------
    Transform _transform;
    Rigidbody2D _rigidbody;
    Animator _topPartAnimator;
    AudioSource _audio;
    
    float _vy;

    // player tracking
    bool _isGrounded = false;
    bool _isPushed = false;
    bool _isRunning = false;
    bool _canJump = false;
    

    void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _topPartAnimator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }
    
	void Update ()
    {
        if (!_playerCanMove || Time.timeScale == 0.0f)
            return;
        
        if (!_isPushed)
            _rigidbody.velocity = new Vector2(_movementSpeed, _rigidbody.velocity.y);

        _isGrounded = Physics2D.Linecast(_transform.position, _groundCheck.position, _whatIsGround);
        _isPushed = Physics2D.Linecast(_transform.position, _pushCheck.position, _whatIsGround);
        
        if (_isGrounded)
            _canJump = true;

        _topPartAnimator.SetBool("isGrounded", _isGrounded);
        _bottomPartAnimator.SetBool("isGrounded", _isGrounded);

        _topPartAnimator.SetBool("isPushed", _isPushed);
        _bottomPartAnimator.SetBool("isPushed", _isPushed);

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space)/* swipe up */ )
        {
            DoJump();
            _canJump = false;
        }

        if(Input.GetKeyDown(KeyCode.F1) /*press the time freeze button*/)
        {
            _timeFreezeActivation = !_timeFreezeActivation;
            StartTimeFreeze();
        }
    }

    void DoJump()
    {
        _vy = 0f;
        _rigidbody.AddForce(new Vector2(0, _jumpStrength));

        // play the sound
    }

    public void StartTimeFreeze()
    {
        if (!_timeFreezeActivation)
        {
            CancelInvoke("ReduceEnergy");
            return;
        }

        if (_energy >= 3)
            InvokeRepeating("ReduceEnergy", 0.0f,1.0f);
    }

    public void CollectEnergy(int _amount)
    {
        //playsound

        _energy += _amount;
    }
    
    void ReduceEnergy()
    {
        _energy--;
        if (_energy <= 0)
            CancelInvoke("ReduceEnergy");
    }

    public void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Enemy") || _collision.gameObject.CompareTag("DeadZone") || _collision.gameObject.CompareTag("Bullet"))
        {
            //play death sfx
            StartCoroutine(KillPlayer());
        }
    }

    IEnumerator KillPlayer()
    {
        if (_playerCanMove)
        {
            //FreezeMotion();
            //set animation

            yield return new WaitForSeconds(3.0f);

            //reset game or set to check point
        }
    }
}
