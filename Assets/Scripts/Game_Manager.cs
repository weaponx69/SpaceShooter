using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manager the game state.
public class Game_Manager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            // 1 = the main playable game now.
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

}
