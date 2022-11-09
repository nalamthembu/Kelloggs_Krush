using UnityEngine;

namespace Managers
{
    public class InputManager : Manager
    {
        public static InputManager INPUT_MANAGER;

        [SerializeField] KeyBindsScriptableObject m_KeyBinds;

        private void Awake()
        {
            if (INPUT_MANAGER is null)
            {
                INPUT_MANAGER = this;
            }
            else
            {
                Destroy(this);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        public Vector2 InputXY
        {
            get
            {
                return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
        }

        public bool JumpKey
        {
            get
            {
                return Input.GetKeyDown(m_KeyBinds.JumpKey);
            }
        }

        public bool StrikeKey
        {
            get
            {
                return Input.GetKeyDown(m_KeyBinds.StrikeKey);
            }
        }
    }
}