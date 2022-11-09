using UnityEngine;
using Gameplay;

namespace Managers
{
    [RequireComponent(typeof(InputManager))]
    public class GameManager : Manager
    {
        public static GameManager GAME_MANAGER;

        Vector3 RespawnArea = new Vector3(0, 5, 0);

        private Ball m_Ball;

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


        public void Ball_HitFloor()
        {
            m_Ball.ResetBall();
            m_Ball.transform.position = RespawnArea;
        }

    }

    public class GameStrings
    {
        public static string FLOOR_TAG = "Floor";
    }
}