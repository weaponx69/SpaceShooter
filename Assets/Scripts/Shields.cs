using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shields : MonoBehaviour
{
    [SerializeField]
    private GameObject _shieldsPrefab;

 
    // This is called from the Powerups script
    // when the player picks up the shields
    // powerup in the game so that shields are
    // drawn around the player.

    // This is called from the powerups script when the
    // player is hit while the shields are up.
    public void destroyShields()
    {
        // cause this prefab to disappear since the
        // shields are down now.
        Debug.Log("Shields down!");
        Destroy(this.gameObject);
    }
}
