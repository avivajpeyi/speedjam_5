using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 70;

    private void OnCollisionStay2D(Collision2D other)
    {
        // if other has playerController
        if (other.transform.TryGetComponent(out PlayerController controller))
        {
            // Calculate the bounce direction (opposite of the collision normal)
            Vector2 bounceDirection = other.contacts[0].normal * -1;

            Rigidbody2D rb = other.rigidbody;
            // Apply the bounce force
            rb.AddForce(bounceDirection * _bounceForce, ForceMode2D.Impulse);
        }
    }
}