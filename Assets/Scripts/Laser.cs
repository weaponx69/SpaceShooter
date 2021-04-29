using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    private float laserBoltLiveDist = 10;

    // Update is called once per frame
    void Update()
    {
        // move the laser up
        transform.Translate(Vector3.up * Time.deltaTime * _speed, 0);

        if (transform.position.y >= laserBoltLiveDist)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }
}
