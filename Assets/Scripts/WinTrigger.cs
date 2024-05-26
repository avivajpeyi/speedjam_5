using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{other.name} eneterd Trigger area");
        
        if (other.TryGetComponent(out Special2dPlayerController.PlayerController controller))
        {
            Debug.Log("Game win area reached!");
            GameManager.Instance.GameCompleted();
        }
    }
    
    
    
}
