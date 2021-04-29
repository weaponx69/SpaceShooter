using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 2f;
    private float _bottomOfScreen = -6f;

    // Update is called once per frame
    void Update()
    {
        // instead of waiting for the spacebar, the
        // enemies will spawn by their own military fashion
        MoveEnemy();
    }

    void gotHitByLaser()
    {
        Destroy(this.gameObject);
    }

    void MoveEnemy()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);

        // if the enemy reaches the bottom, respawn at the
        // top of the screen.
        if (transform.position.y < _bottomOfScreen)
        {
            // move back to the top of the screen
            // at a random position at the top.
            // randomize the x-value.
            float randomX = Random.Range(-8, 8);
            transform.position = new Vector3(randomX ,8f , 0);
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
            }

            // Now destroy this enemy object.
            Destroy(this.gameObject);
        }
        // if collider is laser then destroy
        // the laser.
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
