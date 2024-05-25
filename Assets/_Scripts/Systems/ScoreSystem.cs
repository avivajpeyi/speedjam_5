using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public struct ScoreData
{
    public string playerId;
    public int score;
    public string metadata;
}


public class ScoreSystem : StaticInstance<ScoreSystem>
{
    const string LEADERBOARD_KEY = "leaderboard";


    void Start()
    {
        StartLootLockerSession();
    }

    void StartLootLockerSession()
    {
        Debug.Log("Starting LootLocker session");
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");

                return;
            }

            Debug.Log("successfully started LootLocker session");
        });
    }


    public List<ScoreData> DownloadScores(
        Action<List<ScoreData>> callback
        )
    {
        Debug.Log("Starting DownloadScores");
        
        List<ScoreData> scores = new List<ScoreData>();

        int count = 50;

        LootLockerSDKManager.GetScoreList(LEADERBOARD_KEY, count, 0, (response) =>
        {
            Debug.Log("Got response from GetScoreList");
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");
                foreach (var leaderboardMember in response.items)
                {
                    
                    
                    Debug.Log(leaderboardMember.member_id + " " + leaderboardMember.score);
                    scores.Add(new ScoreData
                    {
                        playerId = leaderboardMember.member_id,
                        score = leaderboardMember.score,
                        metadata = leaderboardMember.metadata
                    });
                }
                callback(scores);
            }
            else
            {
                Debug.Log("failed: " + response.errorData);
            }
        });

        return scores;
    }

    public void UploadScore(string playerID, int score)
    {
        LootLockerSDKManager.SubmitScore(
            playerID,
            score,
            LEADERBOARD_KEY,
            (response) =>
            {
                if (response.statusCode == 200)
                {
                    Debug.Log("Successful");
                }
                else
                {
                    Debug.Log("failed: " + response.errorData);
                }
            });
    }
    //
    //
    // public void UploadScore()
    // {
    //     /*
    //      * Get the players System language and send it as metadata
    //      */
    //     string metadata = Application.systemLanguage.ToString();
    //
    //     /*
    //      * Since this is a player leaderboard, member_id is not needed, 
    //      * the logged in user is the one that will upload the score.
    //      */ 
    //     // Not working, fix!
    //     float floatScore = float.Parse(scoreInputField.text);
    //     floatScore *= AmountToDivideBy;
    //     string formattedString = Mathf.FloorToInt(floatScore).ToString();
    //
    //     LootLockerSDKManager.SubmitScore("", int.Parse(formattedString), leaderboardKey, metadata, (response) =>
    //     {
    //         if (response.success)
    //         {
    //             infoText.text = "Player score was submitted";
    //             /*
    //              * Update the leaderboards when the new score was sent so we can see them
    //              */
    //             UpdateLeaderboardCentered();
    //             UpdateLeaderboardTop10();
    //         }
    //         else
    //         {
    //             infoText.text = "Error submitting score:" + response.errorData.message;
    //         }
    //     });
    // }
    
    
    


}