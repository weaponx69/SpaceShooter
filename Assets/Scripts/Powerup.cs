using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private GameObject _powerupPrefab;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        // Start the powerup at the top of the screen at any x position.
        transform.position = new Vector3(Random.Range(-8f, 8f), 8f, 0);

        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePowerup();
    }

    void MovePowerup()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed, 0);

        // Destroy the powerup if it gets 
        // to the bottom of the screen.
        if (transform.position.y <= -3f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // enable tripleshot powerup boolean
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.enableTripleShot();
            }

            _player.disableTripleshotPowerup();

            Destroy(this.gameObject);
        }
    }
}
