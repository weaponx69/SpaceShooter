using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 2f;
    private Player _player;
    private Animator _setExplosion;

    //[SerializeField]
    //private GameObject _enemyLaser;

    [SerializeField]
    private GameObject Left_Laser;

    [SerializeField]
    private GameObject Right_Laser;

    private AudioSource _explosionSound;

    private Shake camShakeAnim;

    float _time;

    [SerializeField]
    float _interval = 3f;

    void Start()
    {
        // instantiate player here so it is not called a lot.
        _player = GameObject.Find("Player").GetComponent<Player>();

        // Get an instance of animator.
        _setExplosion = GetComponent<Animator>();

        _explosionSound = GameObject.Find("Explosion_Sound").GetComponent<AudioSource>();

        camShakeAnim = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();

        _time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // instead of waiting for the spacebar, the
        // enemies will spawn by their own military fashion
        MoveEnemy();

        _time += Time.deltaTime;
        while (_time >= _interval)
        {
            enemyLaserAttack();
            _time -= _interval;
        }
    }

    // Have enemies fire every 5 seconds.
    void enemyLaserAttack()
    {
        GameObject leftLaser = Instantiate(
            Left_Laser, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        GameObject rightLaser = Instantiate(
            Right_Laser, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        //leftLaser.transform.parent = leftLaser.transform;
        //rightLaser.transform.parent = rightLaser.transform;
    }

    void MoveEnemy()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed, 0);
       
        // if the enemy reaches the bottom, respawn at the
        // top of the screen.
        if (transform.position.y < -10)
        {
            // move back to the top of the screen
            // at a random position at the top.
            // randomize the x-value.
            float randomX = Random.Range(-8, 8);
            transform.position = new Vector3(randomX, 10f, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Get a reference to the player component.
            Player player = other.transform.GetComponent<Player>();
            // check for null.
            if (player != null)
            {
                // assign 1 damage to the player.
                player.setPlayerDamage(1);
                // shake the camera too when the player is hit.
                camShakeAnim.CamShake();
            }

            // Play the destroy animation and explosion sound.
            _setExplosion.SetTrigger("OnEnemyDestroyed");
            _enemySpeed = 0;
            _explosionSound.Play();

            // Now destroy this enemy object.
            Destroy(this.gameObject, 2.8f);
        }

        // if collider is laser then destroy
        // the laser.
        if (other.tag == "Laser")
        {
            // destroy the laser
            Destroy(other.gameObject, 2.8f);

            _explosionSound.Play();

            // add 10 to the score before destroying enemy object
            if (_player != null)
            {
                _player.setScore(10);
            }
            Destroy(GetComponent<Collider2D>());

            // Play the destroy animation
            _setExplosion.SetTrigger("OnEnemyDestroyed");
            _enemySpeed = 0;
            Destroy(this.gameObject, 2.8f);
        }
    }
}
