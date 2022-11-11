using UnityEngine;
using static Managers.GameStrings;
using static Managers.GameManager;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] Vector3 m_StartForce;

        Rigidbody m_RigidBody;

        private void Start()
        {
            m_RigidBody = GetComponent<Rigidbody>();
            m_RigidBody.AddForce(m_StartForce, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.contacts[0].otherCollider.CompareTag("Floor"))
            {
                Debug.Log("Game has ended");
            }
        }

        public void ResetBall()
        {
            m_RigidBody.velocity = default;
        }
    }
}
