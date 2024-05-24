using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

public class DistanceTracker : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public TMP_Text distanceText; // UI Text to display current vertical distance
    public Image bestDistanceLine; // UI Image for the horizontal line representing best 
    
    

    private float currentDistance;
    private float bestDistance;

    void Start()
    {
        // Initialize distances
        currentDistance = 0f;
        bestDistance = PlayerPrefs.GetFloat("BestDistance", 0f); // Load best distance from PlayerPrefs

        // Set initial UI values
        UpdateDistanceUI();
    }

    void Update()
    {
        // Update current distance based on player's vertical position
        currentDistance = Mathf.Max(player.position.y, 0);
        bestDistance = Mathf.Max(currentDistance, bestDistance); 

        // Update UI
        UpdateDistanceUI();
    }

    void UpdateDistanceUI()
    {
        // Display current distance on the bottom right of the screen
        distanceText.text = Mathf.Round(currentDistance).ToString() + "m";

        // Update the horizontal line position to represent the best distance
        bestDistanceLine.rectTransform.localPosition  = new Vector3(0f, bestDistance, 0f);
    }

    public void SaveBestDistance()
    {
        // Save the best distance to PlayerPrefs
        PlayerPrefs.SetFloat("BestDistance", bestDistance);
        PlayerPrefs.Save();
    }
}
