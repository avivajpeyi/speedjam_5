using UnityEngine;

namespace Special2dPlayerController {
    public class Bouncer : MonoBehaviour {
        [SerializeField] private float _bounceForce = 70;
        [SerializeField] private AudioClip _bounceSound;
        
        private void OnCollisionStay2D(Collision2D other) {
            if (other.collider.TryGetComponent(out IPlayerController controller))
            {
                AudioSystem.Instance.PlaySound(_bounceSound, vol:0.5f);
                controller.ApplyVelocity(transform.up * _bounceForce, PlayerForce.Burst);
            }
        }
    }
}