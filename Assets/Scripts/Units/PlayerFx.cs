using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFx : MonoBehaviour
{
    
    [SerializeField] private Transform _jumpParticlesParent;
    [SerializeField] private TrailRenderer _jumpTrialRenderer;
    [SerializeField] private ParticleSystem _jumpParticles, _launchParticles, _landParticles;
    bool isLandFxPlaying = false;

    GroundCheck _groundCheck;
    PlayerController _playerController;
    
    private readonly RaycastHit2D[] _groundHits = new RaycastHit2D[2];
    private ParticleSystem.MinMaxGradient _currentGradient;
    
    private void Update()
    {
        SetParticleColor(Vector2.down, _jumpParticles);
    }


    private void Awake()
    {
        _groundCheck = GetComponent<GroundCheck>();
        _playerController = GetComponent<PlayerController>();

    }


    // Start is called before the first frame update
    // void Start()
    // {
    //     _playerController.
    //     
    // }
    //
    private void SetParticleColor(Vector2 detectionDir, ParticleSystem system) {
        var hitCount = Physics2D.RaycastNonAlloc(transform.position, detectionDir, _groundHits, 2);
        for (var i = 0; i < hitCount; i++) {
            var hit = _groundHits[i];
            if (!hit.collider || hit.collider.isTrigger || !hit.transform.TryGetComponent(out SpriteRenderer r)) continue;
            var color = r.color;
            color = Color.white;
            _currentGradient = new ParticleSystem.MinMaxGradient(color * 0.9f, color * 1.2f);
            SetColor(system); 
            return;
        }
    }

    private void SetColor(ParticleSystem ps) {
        var main = ps.main;
        main.startColor = _currentGradient;
    }

    public void PlayJumpFX()
    {
        // Set the rotation of the particles to match the player's up vector
        _jumpParticles.transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.up);
        SetColor(_jumpParticles);
        SetColor(_launchParticles);
        _jumpParticles.Play();
    }


    // public void PlayLandFX()
    // {
    //     // If the player is grounded, and the player was not grounded in the last few frames
    //     // And the fx not currently playing
    //     if (_groundCheck.isGrounded && !_groundCheck.wasGrounded && !isLandFxPlaying)
    //     {
    //         Instantiate(_landFx, transform.position, Quaternion.identity);
    //         isLandFxPlaying = true;
    //         StartCoroutine(ResetLandFx());
    //     }
    // }

    private IEnumerator ResetLandFx()
    {
        // Assuming the FX has a duration, e.g., 1 second, before it can be played again
        yield return new WaitForSeconds(1.0f);
        isLandFxPlaying = false;
    }
}