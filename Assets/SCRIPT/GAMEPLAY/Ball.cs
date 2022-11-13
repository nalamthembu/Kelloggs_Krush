using UnityEngine;
using static Match;
using static SoundManager;
using static Managers.GameStrings;
using static Managers.GameManager;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] Vector3 m_StartForce;

        Rigidbody m_RigidBody;

        MeshRenderer m_Renderer;

        Match MATCH;

        private void Start()
        {
            m_RigidBody = GetComponent<Rigidbody>();
            m_RigidBody.AddForce(m_StartForce, ForceMode.Impulse);

            m_Renderer = GetComponent<MeshRenderer>();

            MATCH = FindObjectOfType<Match>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.contacts[0].otherCollider.CompareTag("Floor"))
            {
                MATCH.SetGameOver();
                Debug.Log("Game has ended");
            }

            if (collision.relativeVelocity.magnitude > .5f)
                SOUND_MANAGER.PlaySound(SOUND_MANAGER.GetSound(SOUND.SFX_BALL_HIT), transform.position);
        }

        public void ResetBall()
        {
            m_RigidBody.velocity = default;
        }

        public void ChangeColour(RESPONSIBILITY playerResponsible)
        {
            m_Renderer.material.color = (playerResponsible == RESPONSIBILITY.PLAYER) ?
                MATCH.GetPlayerColours()[0] :
                MATCH.GetPlayerColours()[1];
        }
    }
}
