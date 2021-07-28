using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;

    private AudioSource _explosionSound;

    private Shake camShakeAnim;

    // flip these variables to true depending on 
    // what is firing.
    public int _laserDistance = -8;

    void Start()
    {
        camShakeAnim = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

    // Update is called once per frame
    void Update()
    {
        // find out if player or enemy called this laser.
        // call player script and then enemy script to see
        // which one fired
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y <= _laserDistance)
        {
            if (transform != null)
            {
                foreach (Transform child in transform)
                {
                    //GameObject.Destroy(child.gameObject);
                }
            }
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
                camShakeAnim.CamShake();
            }

            //_explosionSound.Play();

            // Now destroy this Laser.
            Destroy(this.gameObject, 2.8f);
        }
    }
}
