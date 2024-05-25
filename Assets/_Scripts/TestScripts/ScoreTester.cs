using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using LootLocker.Requests;

public class ScoreTester : MonoBehaviour
{
    
    public TMP_Text infoText;
    public TMP_Text playerIDText;
    public TMP_InputField playerNameInput;
    public TMP_InputField scoreInput;
    
    [SerializeField] private LeaderboardEntry _lbEntryPrefab;
    [SerializeField] private GameObject _lbContent;
    
    
    public string memberID;
    

    // Start is called before the first frame update
    void Start()
    {
        StartGuestSession();
        UpdateScoreTable();
    }

    private void Update()
    {
        // On "Enter" key press, submit score
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SubmitScore();
        }
    }

    public void StartGuestSession()
    {
        /* Start guest session without an identifier.
         * LootLocker will create an identifier for the user and store it in PlayerPrefs.
         * If you want to create a new player when testing, you can use PlayerPrefs.DeleteKey("LootLockerGuestPlayerID");
         */
        PlayerPrefs.DeleteKey("LootLockerGuestPlayerID");
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                infoText.text = "Guest session started";
                playerIDText.text = "Player ID:" + response.player_id.ToString();
                memberID = response.player_id.ToString();
            }
            else
            {
                infoText.text = "Error" + response.errorData.message;
            }
        });
    }
    

    
    
    public void SubmitScore()
    {
        Debug.Log("Submitting score");
        int score = int.Parse(scoreInput.text);
        string name = playerNameInput.text;
        ScoreSystem.Instance.UploadScore(memberID, score);
        Debug.Log("Uploaded score, going to download");
        UpdateScoreTable();
        StartGuestSession();
    }
    
    public void UpdateScoreTable()
    {
        Debug.Log("Trying to Download and then build score table");
        List<ScoreData> scores = ScoreSystem.Instance.DownloadScores(BuildScoreTable);
        
        
    }
    
    public void BuildScoreTable(List<ScoreData> scores)
    {
        Debug.Log("Building score table function called ");
        foreach (Transform child in _lbContent.transform)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var score in scores)
        {
            Debug.Log("Creating GOs for score");
            LeaderboardEntry entry = Instantiate(_lbEntryPrefab, _lbContent.transform);
            entry.Initialise(score);
        }
    } 

    
}
