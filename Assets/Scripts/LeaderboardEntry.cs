using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardEntry : MonoBehaviour
{
    
    [SerializeField] private TMP_Text _rankText; 
    [SerializeField] private TMP_Text _scoreText; // The 'seconds' value of the player's best run
    [SerializeField] private TMP_Text _nameText;


    public void Initialise(ScoreData s, int rank)
    {
        _scoreText.text = TimerSystem.Instance.FormatTime(s.score); 
        _nameText.text = $"{s.metadata} ({s.playerId})";
        _rankText.text = rank.ToString();
    }
    
    
    
    
}
