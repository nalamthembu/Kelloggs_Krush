using static Managers.GameManager;
using System.Collections;
using static Match;
using UnityEngine;

namespace Gameplay {
    public class AI : MonoBehaviour
    {
        CharacterController m_CharacterController;

        [SerializeField][Range(1, 10)] float m_MovementSpeed = 2;
        [SerializeField][Range(1, 20)] float m_Distance = 15f;
        [SerializeField] Transform m_Target;
        [SerializeField] LayerMask m_Mask;
        [SerializeField][Range(1, 100)] float m_SidewaysMovementCap = 45;

        private Player m_Player;

        private Rigidbody m_Ball;

        private bool m_CanHitBall;

        [SerializeField] Transform m_OriginalPosition;

        Match MATCH;

        void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();

            m_Ball = GAME_MANAGER.GetBall().GetComponent<Rigidbody>();

            m_Ball.useGravity = false;

            m_Ball.isKinematic = true;

            m_Player = FindObjectOfType<Player>();
        }

        private void Update()
        {
            if (m_CanHitBall)
            {
                Launch();
            }

            Movement();
        }


        void Movement()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit groundHit))
                if (!groundHit.transform.CompareTag("Floor"))
                    return;

            if (Vector3.Distance(transform.position, m_Ball.position) > 6f)
            {
                ULerp(m_OriginalPosition.position);
            }
           

            foreach (Collider c in Physics.OverlapSphere(transform.position, 10f))
            {
                if (c.CompareTag("Ball"))
                {
                    ULerp(c.transform.position);
                }
            }

            TLerp(m_Target, m_Player.transform.position + Vector3.right * 3F);
        }

        void ULerp(Vector3 b)
        {
            b.y = 0;
            transform.position = Vector3.Lerp(transform.position, b, Time.deltaTime * m_MovementSpeed * 4);
        }

        void TLerp(Transform t, Vector3 b)
        {
            t.position = Vector3.Lerp(t.position, b, Time.deltaTime * m_MovementSpeed);
        }

        void Launch()
        {
            Physics.gravity = Vector3.up * GAME_MANAGER.GetGravity();
            m_Ball.useGravity = true;
            m_Ball.isKinematic = false;
            m_Ball.velocity = CalculateLaunchVelocity();

            if (MATCH is null)
                MATCH = FindObjectOfType<Match>();

            MATCH.SetResponsibility(RESPONSIBILITY.PLAYER);
        }

        Vector3 CalculateLaunchVelocity()
        {
            float h = GAME_MANAGER.GetHeight();
            float g = GAME_MANAGER.GetGravity();
            float displacementY = m_Target.position.y - m_Ball.position.y;
            Vector3 displacementXZ = new(m_Target.position.x - m_Ball.position.x, 0, m_Target.position.z - m_Ball.position.z);

            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * g * h);

            Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * h / g) + Mathf.Sqrt(2 * (displacementY - h) / g));

            return velocityXZ + velocityY;
        }

        private void OnTriggerEnter(Collider other)
        {
            Rigidbody rb = other.attachedRigidbody;
            m_CanHitBall = rb is not null && other.CompareTag("Ball");
        }

        private void OnTriggerExit(Collider other) => m_CanHitBall = false;

        private void OnDrawGizmos()
        {
            if (m_Target)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(m_Target.position, Vector3.one * 1f);
            }

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, 10f);
        }
    }
}