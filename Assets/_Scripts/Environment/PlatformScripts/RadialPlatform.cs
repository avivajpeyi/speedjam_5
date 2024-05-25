using UnityEngine;

namespace Special2dPlayerController {
    public class RadialPlatform : PlatformBase {
        [SerializeField] private float _speed = 1.5f;
        [SerializeField] private float _size = 2;

        protected override void FixedUpdate()
        {
            var newPos = StartPos + new Vector2(Mathf.Cos(Time.time * _speed), Mathf.Sin(Time.time * _speed)) * _size;
            Rb.MovePosition(newPos);

            base.FixedUpdate();
        }

        private void OnDrawGizmos() {
            if (Application.isPlaying)
                Gizmos.DrawWireSphere(StartPos, _size);
            else
                Gizmos.DrawWireSphere(transform.position, _size);
        }
    }
}