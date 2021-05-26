using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // get a reference for the asteroid so
    // we can do stuff with it.
    [SerializeField]
    private GameObject _Asteroid;
    [SerializeField]
    private float _rotateSpeed = 5;
    [SerializeField]
    private GameObject _asteroidExplosionFab;
    [SerializeField]
    private GameObject _playerLaser;
    [SerializeField]
    private Player _player;

    private GameObject _asteroidExplosion;

    // create a spawn manager object
    Spawn_Manager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        // get an instance of the spawn manager from the game.
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        _Asteroid.transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            // spawn the prefab explosion on the screen at the asteroid position.
            Instantiate(_asteroidExplosionFab, transform.position, Quaternion.identity);

            // when the laser collides with the
            // asteroid destroy the laser.
            Destroy(other.gameObject);

            // call the method to start spawning.
            // after asteroid is destroyed.
            _spawnManager.StartSpawning();

            // Destroy asteroid
            Destroy(this.gameObject);
        }
    }
}
