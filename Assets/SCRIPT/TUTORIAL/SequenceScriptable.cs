using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "Sequence", menuName = "Data Storage/Tutorial/Sequence")]
    public class SequenceScriptable : ScriptableObject
    {
        public Sequence[] m_Sequences;
    }

    [System.Serializable]
    public class Sequence
    {
        public string name;
        public Vector3 cameraLocation;
        public bool cameraCut = false;
        public string textToDisplay;
    }
}