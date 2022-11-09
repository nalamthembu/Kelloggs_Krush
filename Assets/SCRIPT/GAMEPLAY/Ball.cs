using UnityEngine;
using static Managers.GameStrings;
using static Managers.GameManager;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        Rigidbody m_Rigidbody;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();

            InitialiseRigidbody();
        }

        void InitialiseRigidbody()
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        }

        public void Hit(Vector3 playerPosition)
        {
            m_Rigidbody.AddForce(playerPosition.magnitude * -playerPosition, ForceMode.Impulse);
        }

        private void FixedUpdate()
        {
            m_Rigidbody.velocity += Vector3.down * Time.deltaTime;
        }

        public void ResetBall()
        {
            m_Rigidbody.velocity = Vector3.zero;
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag(FLOOR_TAG))
            {
                GAME_MANAGER.Ball_HitFloor();
                return;
            }

            Player player = collision.transform.GetComponent<Player>();

            if (player)
                return;

            m_Rigidbody.AddForce(-collision.contacts[0].normal * 10f, ForceMode.Impulse);
        }
    }
}
