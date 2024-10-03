using UnityEngine;

namespace MyBird
{
    public class CameraController : MonoBehaviour
    {
        public Transform PlayerX;
        [SerializeField] private float offset = 1.5f;
        private void LateUpdate()
        {
            this.transform.position = new Vector3(PlayerX.position.x + offset , transform.position.y, transform.position.z);
        }
    }
}