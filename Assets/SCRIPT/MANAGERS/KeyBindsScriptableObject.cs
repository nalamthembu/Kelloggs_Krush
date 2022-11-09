using UnityEngine;

namespace Managers
{
    [CreateAssetMenu(fileName = "Keybinds", menuName = "Scriptable Objects/Binds")]
    public class KeyBindsScriptableObject : ScriptableObject
    {
        public KeyCode JumpKey = KeyCode.Space;
        public KeyCode StrikeKey = KeyCode.LeftShift;
    }
}