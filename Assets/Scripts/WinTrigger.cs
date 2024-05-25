using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController controller))
        {
            Debug.Log("You win!");
            Debug.Log("<Upload Time to Leaderboard>");
        }
    }
}
