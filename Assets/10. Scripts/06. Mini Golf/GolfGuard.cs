using UnityEngine;

namespace Study3D.MiniGolf
{
    [RequireComponent(typeof(Rigidbody))]
    public class GolfGuard : MonoBehaviour
    {
        [Header("Patrol")]
        public Transform pointA;
        public Transform pointB;
        public float moveSpeed = 1.5f;

        private Vector3 positionA;
        private Vector3 positionB;
        private bool isGoingToB = true;

        private void Awake()
        {
            positionA = pointA.position;
            positionB = pointB.position;
            
            Rigidbody rBody = GetComponent<Rigidbody>();
            rBody.isKinematic = true;
            rBody.useGravity = false;
        }

        private void Update()
        {
            Patrol();
        }

        private void Patrol()
        {
            Vector3 target = isGoingToB ? positionB : positionA;

            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

            Vector3 direction = target - transform.position;
            direction.y = 0f;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(direction);

            if (transform.position == target)
                isGoingToB = !isGoingToB;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GolfBall ball))
            {
                ball.ReturnToLasShotPosition();
            }
        }

        private void OnDrawGizmos()
        {
            if (pointA == null || pointB == null) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
