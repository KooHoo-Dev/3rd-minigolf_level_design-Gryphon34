using UnityEngine;

namespace Study3D.MiniGolf
{
    public class GolfLaser : MonoBehaviour
    {
        public float currentTime = 0f;
        public float fixedTime = 3f;
        
        private MeshRenderer laserRenderer;
        private Collider laserCollider;
        private bool isOn = true;
        
        private void Awake()
        {
            laserCollider = GetComponent<Collider>();
            laserRenderer = GetComponent<MeshRenderer>();
        }
        
        private void Update()
        {
            TurnStateLaser();
        }

        private void TurnStateLaser()
        {
            if (currentTime >= fixedTime)
            {
                isOn = !isOn;
                laserRenderer.enabled = isOn;
                laserCollider.enabled = isOn;
                currentTime = 0f;
                return;
            }
            currentTime += Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            // TryGetComponent로 other의 컴포넌트가 GolfBall인지 확인해서
            // 맞으면 GolfBall의 ReturnToLastPosition을 호출

            if (other.TryGetComponent(out GolfBall ball))
            {
                ball.ReturnToLasShotPosition();
            }
        }
    }
}