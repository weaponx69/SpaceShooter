using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    // get a handle to the text component
    [SerializeField]
    private Text _scoreText; //drag the score text box to this

    // Gets called from Player with the updated score.
    public void displayPlayerScore(int playerScore)
    {   
        // Update the player score text box with
        // the updated scroe.
        _scoreText.text = "Score: " + playerScore.ToString();
    }
}
