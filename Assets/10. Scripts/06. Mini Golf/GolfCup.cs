using UnityEngine;

namespace Study3D.MiniGolf
{
    public class GolfCup : MonoBehaviour
    {
        public Transform nextTee;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GolfBall ball) == false) return;

            if (nextTee != null)
                ball.PlaceAt(nextTee.position);
        }
    }
}
