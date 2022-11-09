using static Managers.InputManager;
using UnityEngine;


namespace Gameplay
{
    public enum PLAYER_NO
    {
        ONE,
        TWO,
        THREE,
        FOUR
    }

    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        [SerializeField] PLAYER_NO m_PlayerNumber;
        [SerializeField][Range(0, 20)] float m_MovementSpeed = 7f;
        [SerializeField][Range(-50, 0)] float m_Gravity = -12f;
        [SerializeField][Range(0, 20)] float m_JumpHeight = 2;
        [SerializeField] LayerMask m_GroundLayers = -1;

        CharacterController m_Controller;
        Vector3 m_Velocity;
        Vector3 m_Origin;
        float m_VelocityY;
        readonly float rayLength = 0.08f;

        int JumpCount = 0; //FOR DOUBLE JUMP.

        RaycastHit m_Hit;
        bool m_IsGrounded = true;

        private void Awake()
        {
            m_Controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (m_Controller is null)
                return;

            Move();
        }

        private void Move()
        {
            m_Velocity = (INPUT_MANAGER.InputXY.x * Vector3.right) * m_MovementSpeed;

            m_VelocityY += m_Gravity * Time.deltaTime;

            if (INPUT_MANAGER.JumpKey)
                Jump();

            m_Velocity += Vector3.up * m_VelocityY;

            m_Controller.Move(m_Velocity * Time.deltaTime);

            m_Origin = transform.position;

            m_IsGrounded = Physics.Raycast(m_Origin, -transform.up, out m_Hit, rayLength, m_GroundLayers);

            if (m_IsGrounded)
            {
                if (JumpCount >= 2)
                    JumpCount = 0;

                m_VelocityY = 0;
            }

            Debug.DrawLine(m_Origin, m_Origin + Vector3.down * rayLength, Color.magenta);
        }

        private void Jump()
        {
            if (JumpCount >= 2)
            {
                if (m_IsGrounded)
                {
                    JumpCount = 0;
                    return;
                }

                return;
            }

            m_VelocityY = Mathf.Sqrt(-2 * m_Gravity * m_JumpHeight);
            JumpCount++;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!hit.rigidbody)
                return;

            hit.rigidbody.AddForce(-hit.normal * transform.position.magnitude, ForceMode.Impulse);
        }
    }
}
