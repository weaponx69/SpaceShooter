using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    void Start()
    {
        //displayPlayerLives(0);
    }

    // get a handle to the text component
    [SerializeField]
    private Text _scoreText; //drag the score text box to this

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
}
