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
}


public class ScoreSystem : StaticInstance<ScoreSystem>
{
    const string LEADERBOARD_KEY = "123";


    void Start()
    {
        StartLootLockerSession();
    }

    void StartLootLockerSession()
    {
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


    public List<ScoreData> DownloadScores()
    {
        List<ScoreData> scores = new List<ScoreData>();

        int count = 50;

        LootLockerSDKManager.GetScoreList(LEADERBOARD_KEY, count, 0, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");
                foreach (var score in response.items)
                {
                    Debug.Log(score.player + " " + score.score);
                }
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
    
    
    
    public void StartGuestSession()
    {
        /* Start guest session without an identifier.
         * LootLocker will create an identifier for the user and store it in PlayerPrefs.
         * If you want to create a new player when testing, you can use PlayerPrefs.DeleteKey("LootLockerGuestPlayerID");
         */
        PlayerPrefs.DeleteKey("LootLockerGuestPlayerID");
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            // if (response.success)
            // {
            //     infoText.text = "Guest session started";
            //     playerIDText.text = "Player ID:" + response.player_id.ToString();
            //     memberID = response.player_id.ToString();
            //     UpdateLeaderboardTop10();
            //     UpdateLeaderboardCentered();
            // }
            // else
            // {
            //     infoText.text = "Error" + response.errorData.message;
            // }
        });
    }

}