using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private GameObject _powerupPrefab;
    [SerializeField]
    private int powerupID;

    private AudioSource _powerUpSound;

    // Start is called before the first frame update
    void Start()
    {
        // Start the powerup at the top of the screen at any x position.
        transform.position = new Vector3(Random.Range(-8f, 8f), 8f, 0);

        _powerUpSound = GameObject.Find("PowerUP_Sound").GetComponent<AudioSource>();
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
        if (transform.position.y <= -8f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // enable tripleshot powerup boolean
            //Create an id for powerups
            /// 0 = tripleshot
            /// 1 = speedboost
            /// 2 = shields
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.enableTripleShot();
                        _powerUpSound.Play();
                        player.startTimeUntilDisableTriShot();
                        break;
                    case 1:
                        player.enableSpeedBoost();
                        _powerUpSound.Play();
                        player.startTimeUntilDisableSpeed();
                        break;
                    case 2:
                        player.enableShields();
                        _powerUpSound.Play();
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }

}
