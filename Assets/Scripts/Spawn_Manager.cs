using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;//<--- assign to enemy prefab in GUI
    [SerializeField]
    private GameObject _triShotPowerupPrefab;
    [SerializeField]
    private GameObject _speedPowerupPrefab;//instance of speed powerup prefab to spawn.
    [SerializeField]
    private GameObject _shieldPowerupPrefab;//instance of speed powerup prefab to spawn.
    [SerializeField]
    private int timeBtwEnemySpawns = 5;
    [SerializeField]
    private int timeBtwPowerupSpawns = 25;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _playerReference;
    private bool _playerDead = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemy");
        StartCoroutine("SpawnTriShot");
        StartCoroutine("SpawnSpeed");
        StartCoroutine("SpawnShields");
    }

    // spawn enemies objects every 5 seconds.
    IEnumerator SpawnEnemy()
    {
        while (_playerDead == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 8f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);

            // put enemies into a container to organize them.
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(timeBtwEnemySpawns);
        }
    }

    IEnumerator SpawnTriShot()
    {
        while (_playerDead == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 8f, 0);
            GameObject newTriShotPowerup = Instantiate(_triShotPowerupPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(timeBtwPowerupSpawns);
        }
    }

    IEnumerator SpawnSpeed()
    {
        while (_playerDead == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 8f, 0);
            GameObject newSpeedPowerup = Instantiate(_speedPowerupPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(timeBtwPowerupSpawns);
        }
    }

    IEnumerator SpawnShields()
    {
        while (_playerDead == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 8f, 0);
            GameObject newShieldPowerup = Instantiate(_shieldPowerupPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(timeBtwPowerupSpawns);
        }
    }

    // Player uses this to tell this object
    // this script the player is dead.
    public void setPlayerDead()
    {
        _playerDead = true;

        // If the player is dead. The spawn
        // manager can be deleted now too.
        Destroy(this.gameObject);
    }
}
