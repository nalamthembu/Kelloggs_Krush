using UnityEngine;
using Gameplay;

namespace Managers
{
    public class Manager : MonoBehaviour
    {
        protected Player m_Player;
         
        private void Start()
        {
            m_Player = FindObjectOfType<Player>(); //this will only work with one player vs AI's.
            Debug.Log(this.name + " is initialised");
        }
    }
}