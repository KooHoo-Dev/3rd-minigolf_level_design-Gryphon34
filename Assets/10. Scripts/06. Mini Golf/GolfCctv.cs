using UnityEngine;

namespace Study3D.MiniGolf
{
    public class GolfCctv : MonoBehaviour
    {
        [Header("Ref")]
        public GolfBall ball;
        public Transform head;
        public Transform viewArea;

        [Header("Scan")]
        public float leftRightAngle = 45f;
        public float frontBackAngle = 0f;
        public float rotateSpeed = 30f;
        public float holdTime = 5f;

        [Header("Detection")]
        public float detectTime = 3f;
        public Color idleColor = new Color(1f, 0.9f, 0.2f, 0.25f);
        public Color alertColor = new Color(1f, 0.1f, 0.1f, 0.5f);

        private Quaternion rotationA;
        private Quaternion rotationB;
        private bool isGoingToB = true;
        private float holdTimer;
        private float seenTime;
        private Material viewMaterial;

        private void Awake()
        {
            if (head == null) head = transform;
            
            Quaternion baseRotation = head.localRotation;
            rotationA = baseRotation * Quaternion.Euler(-frontBackAngle, -leftRightAngle, 0f);
            rotationB = baseRotation * Quaternion.Euler(frontBackAngle, leftRightAngle, 0f);

            viewMaterial = viewArea.GetComponent<Renderer>().material;
            viewMaterial.color = idleColor;
        }

        private void Update()
        {
            Scan();
            Detect();
        }

        private void Scan()
        {
            if (holdTimer > 0f)
            {
                holdTimer -= Time.deltaTime;
                return;
            }

            Quaternion target = isGoingToB ? rotationB : rotationA;
            
            head.localRotation = Quaternion.RotateTowards(head.localRotation, target, rotateSpeed * Time.deltaTime);
            
            if (Quaternion.Angle(head.localRotation, target) < 0.01f)
            {
                isGoingToB = !isGoingToB;
                holdTimer = holdTime;
            }
        }

        private void Detect()
        {
            seenTime = IsBallInViewArea() ? seenTime + Time.deltaTime : 0f;

            if (seenTime >= detectTime)
            {
                seenTime = 0f;
                ball.ReturnToLasShotPosition();
            }

            viewMaterial.color = Color.Lerp(idleColor, alertColor, seenTime / detectTime);
        }

        private bool IsBallInViewArea()
        {
            Vector3 local = viewArea.InverseTransformPoint(ball.transform.position);
            return Mathf.Abs(local.x) <= 0.5f
                && Mathf.Abs(local.y) <= 0.5f
                && Mathf.Abs(local.z) <= 0.5f;
        }
    }
}
