using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class EndGamePanelManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public TMP_Text scoreTxt;
    public TMP_Text messageTxt;

    [SerializeField] private LeaderboardEntry _lbEntryPrefab;
    [SerializeField] private GameObject _lbContent;

    
    [SerializeField] private List<GameObject> UiToShowOnWin;
    [SerializeField] private List<GameObject> UiToHideOnWin;
    
    [SerializeField] AudioClip winSound;
    

    
    
    
    private void OnEnable()
    {
        ScoreSystem.OnDownloadScoresCompleted += BuildScoreTable;
        ScoreSystem.OnPlayerIdInitialized += SetTempPlayerName;
        GameManager.OnGameCompleted += DisplayBoard;
    }

    private void OnDisable()
    {
        ScoreSystem.OnDownloadScoresCompleted -= BuildScoreTable;
        ScoreSystem.OnPlayerIdInitialized -= SetTempPlayerName;
        GameManager.OnGameCompleted -= DisplayBoard;
    }


    public void SubmitScoreButton()
    {
        SubmitScore();
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DisplayBoard()
    {
        AudioSystem.Instance.PlaySound(winSound);
        
        scoreTxt.text = TimerSystem.Instance.FormattedTime;
        messageTxt.text = "You made it! Submit time?";
        
        foreach (var ui in UiToShowOnWin)
        {
            ui.SetActive(true);
            
            // DoTween to zoom the UI in from zero
            ui.transform.localScale = Vector3.zero;
            ui.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack);
        }
        
        
        foreach (var ui in UiToHideOnWin)
        {
            ui.SetActive(false);
        }
        
    }


    public void SubmitScore()
    {
        Debug.Log("Submitting score");
        float score = TimerSystem.Instance.elapsedTime;
        string name = playerNameInput.text;
        ScoreSystem.Instance.UploadScore(score, name);
        Debug.Log("Uploaded score, going to download");
        ScoreSystem.Instance.DownloadScores();
    }


    private void BuildScoreTable()
    {
        Debug.Log("Building score table function called ");

        List<ScoreData> scores = ScoreSystem.Instance.downloadedScores;


        foreach (Transform child in _lbContent.transform)
        {
            Destroy(child.gameObject);
        }

        
        int myRank = 999;
        for (int i = 0; i < scores.Count; i++)
        {
            int rank = i + 1;
            LeaderboardEntry entry = Instantiate(_lbEntryPrefab, _lbContent.transform);
            entry.Initialise(scores[i], rank);

            if (scores[i].playerId == ScoreSystem.Instance.player_id)
            {
                if (myRank > rank)
                {
                    myRank = rank;
                    messageTxt.text = $"Your rank: {rank}";    
                }
                
            }
        }

    }

    private void SetTempPlayerName()
    {
        playerNameInput.text = "Player" + ScoreSystem.Instance.player_id;
    }
}