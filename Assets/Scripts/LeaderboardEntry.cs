using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardEntry : MonoBehaviour
{
    
    [SerializeField] private TMP_Text _rankText; 
    [SerializeField] private TMP_Text _scoreText; // The 'seconds' value of the player's best run
    [SerializeField] private TMP_Text _nameText;


    public void Initialise(ScoreData s)
    {
        _scoreText.text = s.score.ToString(); // Need to frmat into min:sec
        _nameText.text = s.playerId.ToString();
    }
    
    
    
    
}
