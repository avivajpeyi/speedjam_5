using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnWin : MonoBehaviour
{
    ParticleSystem _particleSystem;
    
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Stop();
    }
    
    private void OnEnable()
    {
        GameManager.OnGameCompleted += Play;
    }

    private void OnDisable()
    {
        GameManager.OnGameCompleted -= Play;
    }
    
    void Play() => _particleSystem.Play();
    
}

