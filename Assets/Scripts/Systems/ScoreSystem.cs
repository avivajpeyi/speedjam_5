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
    public float score;
    public string metadata;
}


public class ScoreSystem : StaticInstance<ScoreSystem>
{
    const string LEADERBOARD_KEY = "leaderboard";
    public string player_id;
    const int scoreConversion = 100;
    
    bool SessionStarted = false;
    public List<ScoreData> downloadedScores = new List<ScoreData>();


    public static event Action OnDownloadScoresCompleted;
    public static event Action OnSessionInitialized;
    public static event Action OnPlayerIdInitialized;


    void Start()
    {
        StartLootLockerSession();
    }

    private void OnEnable()
    {
        OnSessionInitialized += StartGuestSession;
        OnPlayerIdInitialized += DownloadScores;
    }

    private void OnDisable()
    {
        OnSessionInitialized -= StartGuestSession;
        OnPlayerIdInitialized -= DownloadScores;
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

            OnSessionInitialized?.Invoke();
            SessionStarted = true;
            Debug.Log("successfully started LootLocker session");
        });
    }


    public void DownloadScores()
    {
        Debug.Log("Starting DownloadScores");

        downloadedScores = new List<ScoreData>();

        int count = 50;

        LootLockerSDKManager.GetScoreList(LEADERBOARD_KEY, count, 0, (response) =>
        {
            Debug.Log("Got response from GetScoreList");
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");
                foreach (var leaderboardMember in response.items)
                {
                    // format message of  Id: {id} Score: {score} Metadata: {metadata}
                    Debug.Log(
                        $"Id: {leaderboardMember.member_id} " +
                        $"Score: {leaderboardMember.score} " +
                        $"Metadata: {leaderboardMember.metadata}"
                    );
                    downloadedScores.Add(new ScoreData
                    {
                        playerId = leaderboardMember.member_id,
                        score = leaderboardMember.score / scoreConversion,
                        metadata = leaderboardMember.metadata
                    });
                }

                OnDownloadScoresCompleted?.Invoke();
            }
            else
            {
                Debug.Log("failed: " + response.errorData);
            }
        });
    }

    public void UploadScore(float score, string metadata)
    {
        LootLockerSDKManager.SubmitScore(
            memberId: player_id,
            score: (int)score * scoreConversion,
            leaderboardKey: LEADERBOARD_KEY,
            metadata: metadata,
            (response) =>
            {
                if (response.statusCode == 200)
                {
                    Debug.Log($"Successfully uploaded scores {score} for {player_id}");
                    DownloadScores();
                }
                else
                {
                    Debug.Log("Score upload failed: " + response.errorData);
                }
            });
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
                player_id = response.player_id.ToString();
                Debug.Log($"PlayerLootID established {player_id}");
                OnPlayerIdInitialized?.Invoke();
            }
            else
            {
                Debug.Log($"Error: {response.errorData.message}");
            }
        });
    }
}