using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "Sequence", menuName = "Data Storage/Tutorial/Sequence")]
    public class SequenceScriptable : ScriptableObject
    {
        public Sequence[] sequences;
    }

    [System.Serializable]
    public class Sequence
    {
        public string name;
        public string tutorialContext;
        public Vector3 cameraLocation;
        public Quaternion cameraRotation;
        [Min(1)] public float cameraLerpSpeed = 2;
        public bool cameraCut = false;
        public string textToDisplay;
        public RESPONSIBILITY m_BallResponsibility;
    }
}