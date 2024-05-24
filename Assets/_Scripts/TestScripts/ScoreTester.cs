using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTester : MonoBehaviour
{
    
    
    [SerializeField] private TMP_Text _scoreTableText;
    
    
    void Start()
    {
        ScoreSystem.Instance.DownloadScores();
    }

    
}
