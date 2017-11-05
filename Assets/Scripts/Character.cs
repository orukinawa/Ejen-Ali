using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Range(0.0f, 10.0f)] public float _movementSpeed;
    public float _gapPerLevel;
    public int _energy;
    
    public LayerMask _whatIsGround;
    public Animator _bottomPartAnimator;
    public Transform _groundCheck;
    public Transform _pushCheck;

    [HideInInspector] public int _currentLevel = 0;
    [HideInInspector] public bool _playerCanMove = true;
    [HideInInspector] public bool _timeFreezeActivation = false;
    
    public AudioClip _pickEnergySFX;
    public AudioClip _deathSFX;
    public AudioClip _jumpSFX;

    //-------------------------------------------------------
    Transform _transform;
    Vector3 _defaultPosition;
    Rigidbody2D _rigidbody;
    Animator _topPartAnimator;
    AudioSource _audio;
    
    float _vy;

    // player tracking
    bool _isGrounded = false;
    bool _isPushed = false;
    bool _isRunning = false;
    bool _isDefend = false;
    

    void Awake()
    {
        _transform = GetComponent<Transform>();
        _defaultPosition = _transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        _topPartAnimator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }
    
	void Update ()
    {
        if (!_playerCanMove || Time.timeScale == 0.0f)
            return;
        
        //if (!_isPushed)
        //    _rigidbody.velocity = new Vector2(_movementSpeed, _rigidbody.velocity.y);

        _isGrounded = Physics2D.Linecast(_transform.position, _groundCheck.position, _whatIsGround);
        _isPushed = Physics2D.Linecast(_transform.position, _pushCheck.position, _whatIsGround);


        //------------------------INPUT---------------------------
        if (Input.GetKeyDown(KeyCode.F1) /*press the time freeze button*/)
        {
            _timeFreezeActivation = !_timeFreezeActivation;
            StartTimeFreeze();
        }

        if(Input.GetKeyDown(KeyCode.F2) && !_isDefend)
        {
            DoDefend();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)) // swipe up
        {
            _currentLevel--;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)) // swipe down
        {
            _currentLevel++;
        }
        _currentLevel = Mathf.Clamp(_currentLevel, 0, 2);


        if(_currentLevel == 0)
        {
            transform.position = Vector2.MoveTowards(_transform.position, new Vector2(_defaultPosition.x, _defaultPosition.y), _movementSpeed * Time.deltaTime);
            _rigidbody.gravityScale = 2;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(_defaultPosition.x, _defaultPosition.y + (_gapPerLevel * _currentLevel)), _movementSpeed * Time.deltaTime);
            _rigidbody.gravityScale = 0;
        }

        //----------------------ANIMATION-------------------------
        _topPartAnimator.SetBool("isGrounded", _isGrounded);
        _bottomPartAnimator.gameObject.SetActive(_isGrounded);

        _topPartAnimator.SetBool("isDefend", _isDefend);
        //_bottomPartAnimator.gameObject.GetComponent<SpriteRenderer>().enabled = !_isDefend;
    }
    

    void DoDefend()
    {
        _isDefend = true;
        StartCoroutine("DefendRefresh");
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
    
    IEnumerator DefendRefresh()
    {
        yield return new WaitForSeconds(0.2f);
        _isDefend = false;
    }
}
