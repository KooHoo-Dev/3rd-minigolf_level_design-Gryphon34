using UnityEngine;

namespace Study3D.MiniGolf
{
    // 코스 아래에 깔아 두는 낙하 구역입니다.
    // 공이 코스 밖으로 떨어지면 직전 샷 위치로 돌려보냅니다.
    public class GolfFallZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GolfBall ball))
            {
                ball.ReturnToLasShotPosition();
            }
        }
    }
}
