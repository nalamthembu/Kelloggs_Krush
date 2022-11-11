using UnityEngine;
using Gameplay;

namespace Managers
{
    enum RESPONSIBILITY
    {
        PLAYER,
        OPPONENT
    }

    [RequireComponent(typeof(InputManager))]
    public class GameManager : Manager
    {
        public static GameManager GAME_MANAGER;

        private Ball m_Ball;

        [SerializeField][Range(-1, -20)] float m_Gravity = -18;
        [SerializeField][Range(0, 20)] float m_h;

        private void Awake()
        {
            if (GAME_MANAGER is null)
            {
                GAME_MANAGER = this;
            }
            else
            {
                Destroy(this);
                return;
            }

            m_Ball = FindObjectOfType<Ball>();

            DontDestroyOnLoad(gameObject);
        }

        public Ball GetBall()
        {
            return m_Ball;
        }

        public float GetGravity()
        {
            return m_Gravity;
        }

        public float GetHeight()
        {
            return m_h;
        }
    }

    public class GameStrings
    {
        public static string FLOOR_TAG = "Floor";
    }
}