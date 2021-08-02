using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    // get a handle to the text component
    [SerializeField]
    private Text _scoreText; //drag the score text box to this

    // get a reference to the game over text
    // its dragged in by editor.
    [SerializeField]
    private Text _gameOverTextfab;

    [SerializeField]
    private Text _retryGameText;//<--drag into here from editor to reference it

    Player _player;
    Game_Manager _gameManager;


    void Start()
    {
        // get a reference to the player script
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("player is Null.");
        }

        _gameOverTextfab.gameObject.SetActive(false);

        _gameManager = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
        if (_gameManager == null)
        {
            Debug.Log("Game Manager is Null.");
        }

        _gameManager.GameOver();
    }

    // Drag in the image that will stand for player
    // lives here to this spot.  This is the image 
    // that will change representing the player's lives
    // in the game as they increase and decrease.
    [SerializeField]
    private Image _livesOnCanvasRef;

    [SerializeField]
    private Sprite[] _livesSpriteArray;

    // Method that displays the current player lives
    // to the source image for lives.
    public void displayPlayerLives(int currentLives)
    {
        // Switch the picture displayed on the canvas
        // depending on how many lives the player
        // has left.
        switch(currentLives)
        {
            case 3:
                _livesOnCanvasRef.sprite = _livesSpriteArray[3];
                break;
            case 2:
                _livesOnCanvasRef.sprite = _livesSpriteArray[2];
                break;
            case 1:
                _livesOnCanvasRef.sprite = _livesSpriteArray[1];
                break;
            case 0:
                _livesOnCanvasRef.sprite = _livesSpriteArray[0];
                break;
        }
    }

    // Gets called from Player with the updated score.
    public void displayPlayerScore(int playerScore)
    {   
        // Update the player score text box with
        // the updated scroe.
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    // called instead of displayGameOver so
    // gameover can flash.
    public void flashGameOver()
    {
        StartCoroutine(displayGameOver());

        // Since the game is over, give player
        // option to restart the game.
        _retryGameText.gameObject.SetActive(true);

        _gameManager.GameOver();
    }

    IEnumerator displayGameOver()
    {
        // keep turning on/off while player is dead.
        // make this while gameover == true.
        while (true)
        {
            _gameOverTextfab.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            _gameOverTextfab.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
