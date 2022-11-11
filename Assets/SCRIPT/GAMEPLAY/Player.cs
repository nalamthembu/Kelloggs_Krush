using UnityEngine;
using static Match;
using static Managers.GameManager;


namespace Gameplay
{
    public class Player : MonoBehaviour
    {
        CharacterController m_CharacterController;

        [SerializeField][Range(1, 10)] float m_MovementSpeed = 2;
        [SerializeField][Range(1, 20)] float m_Distance = 15f;
        [SerializeField] Transform m_Target;
        [SerializeField] LayerMask m_Mask;
        [SerializeField][Range(1, 100)] float m_SidewaysMovementCap = 45;

        private Rigidbody m_Ball;

        private bool m_CanHitBall;

        void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();

            m_Ball = GAME_MANAGER.GetBall().GetComponent<Rigidbody>();

            m_Ball.useGravity = false;

            m_Ball.isKinematic = true;

        }

        private void Update()
        {
            Movement();

            Aim();

            if (m_CanHitBall)// && Input.GetKeyDown(KeyCode.Space))
                Launch();

            MATCH.SetPlayerAimIndicatorLocation(m_Target.position);
        }

        void Movement()
        {
            Vector2 input = new
                (
                    Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical")
                );

            Vector2 inputNormalised = input.normalized;

            float inputMagnitude = inputNormalised.magnitude;

            Vector3 velocity = Vector3.right * inputNormalised.x + Vector3.forward * inputNormalised.y;

            velocity *= (m_MovementSpeed * 2) * inputMagnitude;

            velocity *= Time.deltaTime;

            velocity += 10 * Time.deltaTime * Vector3.down;

            m_CharacterController.Move(velocity);
        }

        void Aim()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, m_Mask))
            {
                m_Target.position =
                Vector3.forward * -transform.position.z + hitInfo.point.x * Vector3.right;
            }
        }

        void Launch()
        {
            Physics.gravity = Vector3.up * GAME_MANAGER.GetGravity();
            m_Ball.useGravity = true;
            m_Ball.isKinematic = false;
            m_Ball.velocity = CalculateLaunchVelocity();

            MATCH.SetResponsibility(RESPONSIBILITY.ENEMY);
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
                Gizmos.color = Color.red;
                Gizmos.DrawCube(m_Target.position, Vector3.one * 1f);
            }
        }
    }
}
