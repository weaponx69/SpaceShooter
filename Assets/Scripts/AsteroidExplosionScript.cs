using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidExplosionScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _asteroidExplosion;

    // Update is called once per frame
    void Update()
    {
        // when object is instantiated, clean up
        // by deleting the explosion.
        if (_asteroidExplosion)
        {
            Destroy(this.gameObject, 2f);
        }
    }
}
