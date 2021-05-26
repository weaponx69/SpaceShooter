using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] //<--Need to serialize so it will appear in inspector
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 3.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldsPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    [SerializeField]
    private float _nextFire = 0f;
    int _damage = 0;

    // The amount of dammage the player's ship
    // has taken so far.
    [SerializeField]
    private int _playerDammage = 0;

    // Get the engine dammage ainimations to 
    // play them when player gets dammaged.
    [SerializeField]
    private GameObject _leftEngineDammage;
    [SerializeField]
    private GameObject _rightEngineDammage;

    // The amount of damage the player's
    // ship can take.
    [SerializeField]
    private int _hullIntegrity = 3;
    private bool _playerDead = false;
    private Spawn_Manager _spawnManager;
    private Shields _shields;

    [SerializeField]
    private bool isTripleShotEnabled = false;
    [SerializeField]
    private bool _areShieldsEnabled = false;
    [SerializeField]
    private bool isSpeedBoostEnabled = false;

    [SerializeField]
    private int _playerScore;
    
    UI_Manager _uiManager;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("onInitStart");
        _shieldsPrefab.SetActive(false);

        // get a reference to the UI manager script
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        if (_uiManager == null)
        {
            Debug.Log("The UI manager is Null.");
        }

        // Send a message to the spawn manager script
        // to tell the Spawner to stop since player is dead now.
        // get an instance of the Spawn_Manager.
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        if (_spawnManager == null)
        {
            Debug.Log("Spawn Manager is NULL.");
        }
    }


    IEnumerable onInitStart()
    {
        yield return null;
        // start the player at position 0,-8,0
        transform.position = new Vector3(0, -8f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            ShootLaser();
        }
        CalculateMovemement();
    }

    void ShootLaser()
    {
        // Make a laser bolt when the player presses the spacebar.
        // only allow the laser to be shot at its specific 
        // fire rate.
        if (isTripleShotEnabled == true)
        {
            _nextFire = Time.time + _fireRate;
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            // Add this frame's time to _nextFire
            _nextFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
    }

    public void setScore(int points)
    {
        // enemy is hit so increase the score.
        _playerScore += points;

        // Tell the UI_Manager to update the visual score.
        if (_uiManager != null)
        {
            _uiManager.displayPlayerScore(_playerScore);
        }
    }

    public float getPlayerDamage()
    {
        return _playerDammage;
    }

    // Call this from tripleshot component when it collides.
    public void enableTripleShot()
    {
        isTripleShotEnabled = true;
    }

    public void enableShields()
    {
        _areShieldsEnabled = true;
        _shieldsPrefab.SetActive(true);
        //Debug.Log("Shields On!");
    }

    // Call this from tripleshot component when it collides.
    public void enableSpeedBoost()
    {
        isSpeedBoostEnabled = true;
    }

    public void startTimeUntilDisableTriShot()
    {
        StartCoroutine(powerupLifetime());
    }

    public void startTimeUntilDisableSpeed()
    {
        StartCoroutine(powerupLifetime());
    }

    // if destroy shields are called then it must be
    // in memory.
    public void destroyShields()
    {
        _areShieldsEnabled = false;
        _shieldsPrefab.SetActive(false);
        return;
    }

    public void setPlayerDamage(int newDamage)
    {
        // if the shields are not on then just dammage 
        // the player like normal if player is hit.
        if (_areShieldsEnabled == false)
        {
            _playerDammage += newDamage;

            // get a reference to the UI_Manager and
            // call display lives
             _damage =  _hullIntegrity - _playerDammage;
            
            _uiManager.displayPlayerLives(_damage);

            playEngineDammage(_damage);            
            
            // If this player takes too  much dammage
            // then destroy this player.
            if (_playerDammage >= _hullIntegrity)
            {
                // have something end this game
                // and display game over.
                // loop back to the title screen.
                _spawnManager.setPlayerDead();

                _uiManager.flashGameOver();

                // This has to be last because
                // once this is called.  Its null.
                Destroy(this.gameObject);
            }
        }
        else if (_areShieldsEnabled == true)
        {
            // tell the shields prefab to destroy itself
            destroyShields();
            return;
        }
    }

    public bool isPlayerDead()
    {
        return _playerDead;
    }

    public float getHullIntegrity()
    {
        return _hullIntegrity;
    }

    public void playEngineDammage(int dammage)
    {
        switch(dammage)
        {
            case 1:
                _leftEngineDammage.SetActive(true);
                break;
            case 2:
                _rightEngineDammage.SetActive(true);
                break;
        }
    }

    // spawn manager calls this to keep
    // the effects of tripleshot going.
    IEnumerator powerupLifetime()
    {
        yield return new WaitForSeconds(10);
        isTripleShotEnabled = false;
        isSpeedBoostEnabled = false;
    }

    void CalculateMovemement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // make speed greater if speed boost in enabled.
        if (isSpeedBoostEnabled == true)
        {
            transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * _speedMultiplier * Time.deltaTime);
        }
        else //just go back to normal speed.
        {
            transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);
        }

        //stop movement when going too far up or down.
        // if player position on Y is >0 then y position == 0;
        float upperBoundry = -3f;  // 48f <-- open world boundries
        float lowerBoundry = -8f;    //-22.3f

        float leftBoundry = -8f; //-50 <--- open world boundries 
        float rightBoundry = 7f; // 50

        // Clamp movement on the Y-axis between upper and lower boundry instead of using if-thens above.
        // Mathf.Clamp(transform.position.y, upperBoundry, lowerBoundry)
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, lowerBoundry, upperBoundry), 0);

        // keep left and right movement inside the play area.
        if (transform.position.x <= leftBoundry)
        {
            // Warp player to the other side of the screen if they move too far left.
            //transform.position = new Vector3(rightBoundry, transform.position.y, 0);
            transform.position = new Vector3(leftBoundry, transform.position.y, 0);
        }
        else if (transform.position.x >= rightBoundry)
        {
            // Warp player to the other side of the screen if they move too far right.
            // transform.position = new Vector3(leftBoundry, transform.position.y, 0);
            transform.position = new Vector3(rightBoundry, transform.position.y, 0);
        }
    }
}

