using UnityEngine;


namespace Gameplay
{
    public class Player : MonoBehaviour
    {
        CharacterController m_CharacterController;

        [SerializeField][Range(1, 10)] float m_MovementSpeed = 2;
        [SerializeField][Range(1, 20)] float m_HitForce = 2;

        void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
        }

        private void Update()
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

        private void OnTriggerEnter(Collider other)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (other.CompareTag("Ball") && rb is not null)
            {
                rb.transform.position = transform.position + Vector3.up * m_CharacterController.height;
                rb.velocity = default;
                rb.AddForce((transform.forward + transform.up) * (m_HitForce * 2), ForceMode.Impulse);

                Debug.Log("Hit!");
            }
        }
    }
}
