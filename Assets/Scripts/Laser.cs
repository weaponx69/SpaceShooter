using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    private Shake camShake;

    // flip these variables to true depending on 
    // what is firing.
    public bool _playerFired = false;
    public bool _enemyFired = false;
    public int _laserDistance = 10;

    // Update is called once per frame
    void Update()
    {
        // find out if player or enemy called this laser.
        // call player script and then enemy script to see
        // which one fired

        transform.Translate(Vector3.up * Time.deltaTime * _speed, 0);

        if (transform.position.y >= _laserDistance)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

    // Call this when the player fires their laser
    public void playerFired(bool setPlayerFired)
    {
        _playerFired = setPlayerFired;
    }
}
