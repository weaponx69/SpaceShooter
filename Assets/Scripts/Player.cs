using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] //<--Need to serialize so it will appear in inspector
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    [SerializeField]
    private float _nextFire = 0f;

    // The amount of dammage the player's ship
    // has taken so far.
    [SerializeField]
    private float _playerDammage = 0;

    // The amount of damage the player's
    // ship can take.
    [SerializeField]
    private float _hullIntegrity = 5;
    private bool _playerDead = false;
    private Spawn_Manager _spawnManager;
    [SerializeField]
    private bool isTripleShotEnabled = false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("onInitStart");
    }

    IEnumerable onInitStart()
    {
        yield return null;
        // start the player at position 0,0,0
        transform.position = new Vector3(0, -3.5f, 0);

        // get an instance of the Spawn_Manager.
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
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

    public float getPlayerDamage()
    {
        return _playerDammage;
    }

    // Call this from tripleshot component when it collides.
    public void enableTripleShot()
    {
        isTripleShotEnabled = true;
    }

    public void disableTripleshotPowerup()
    {
        StartCoroutine(powerupLifetime());
    }

    public void setPlayerDamage(float newDamage)
    {
        _playerDammage += newDamage;

        // If this player takes too  much dammage
        // then destroy this player.
        if (_playerDammage >= _hullIntegrity)
        {
            // tell the EnemySpawner to stop since player is dead now.
            if (_spawnManager != null)
            {
                _spawnManager.setPlayerDead();
            }
            Destroy(this.gameObject);

            // have something end this game
            // and display game over.
            // loop back to title screen.
        }
    }

    public void setPlayerDead()
    {
        _playerDead = true;
    }

    public bool isPlayerDead()
    {
        return _playerDead;
    }

    public float getHullIntegrity()
    {
        return _hullIntegrity;
    }

    // spawn manager calls this to keep
    // the effects of tripleshot going.
    IEnumerator powerupLifetime()
    {
        yield return new WaitForSeconds(10);
        isTripleShotEnabled = false;
    }

    void CalculateMovemement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        //stop movement when going too far up or down.
        // if player position on Y is >0 then y position == 0;
        float upperBoundry = 0f;
        float lowerBoundry = -3.8f;

        float leftBoundry = -9.14f;
        float rightBoundry = 9.14f;

        // Clamp movement on the Y-axis between upper and lower boundry instead of using if-thens above.
        // Mathf.Clamp(transform.position.y, upperBoundry, lowerBoundry)
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, lowerBoundry, upperBoundry), 0);

        // keep left and right movement inside the play area.
        if (transform.position.x <= leftBoundry)
        {
            // Warp player to the other side of the screen if they move too far left.
            transform.position = new Vector3(rightBoundry, transform.position.y, 0);
        }
        else if (transform.position.x >= rightBoundry)
        {
            // Warp player to the other side of the screen if they move too far right.
            transform.position = new Vector3(leftBoundry, transform.position.y, 0);
        }
    }
}

