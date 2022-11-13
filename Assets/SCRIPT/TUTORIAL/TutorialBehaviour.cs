using UnityEngine;
using Gameplay;
using TMPro;

namespace Tutorial
{
    public class TutorialBehaviour : MonoBehaviour
    {
        [SerializeField] SequenceScriptable m_TutorialScript;

        [SerializeField] Ball m_Ball;

        [SerializeField] Camera m_Camera;

        [SerializeField] TMP_Text m_TutorialInformation;
        [SerializeField] TMP_Text m_TutorialContext;

        [SerializeField] GameObject m_PreviousButton;
        [SerializeField] GameObject m_NextButton;

        [SerializeField] GameObject m_EndOfTutorialPrompt;

        bool m_LerpingCamera = false;

        int m_CurrentSequence = 0;

        private void Start()
        {
            m_LerpingCamera = true;
        }

        private void Update()
        {
            if (m_LerpingCamera)
                LerpCameraTo(
                    m_TutorialScript.sequences[m_CurrentSequence].cameraLocation,
                    m_TutorialScript.sequences[m_CurrentSequence].cameraRotation,
                    m_TutorialScript.sequences[m_CurrentSequence].cameraLerpSpeed
                    );

            ErrorChecks();
        }

        private void ErrorChecks()
        {
            //Make sure the previous button doesn't work if there is no sequence before it.
            m_PreviousButton.SetActive(!(m_CurrentSequence - 1 < 0));
        }

        public void LerpCameraTo(Vector3 position, Quaternion rotation, float lerpSpeed)
        {
            m_Camera.transform.position = Vector3.Lerp(m_Camera.transform.position, position, Time.deltaTime * lerpSpeed);
            m_Camera.transform.rotation = Quaternion.Lerp(m_Camera.transform.rotation, rotation, Time.deltaTime * lerpSpeed);

            m_Ball.ChangeColour(m_TutorialScript.sequences[m_CurrentSequence].m_BallResponsibility);
            DisplayText(
                m_TutorialScript.sequences[m_CurrentSequence].textToDisplay, 
                m_TutorialScript.sequences[m_CurrentSequence].tutorialContext);

            //if the camera is close enough to the location, stop lerping.
            if ((m_Camera.transform.position - position).magnitude < 0.1f)
            {
                m_LerpingCamera = false;
            }
        }

        public void CameraCutTo(Vector3 position, Quaternion rotation)
        {
            m_Camera.transform.SetPositionAndRotation(position, rotation);
            m_Ball.ChangeColour(m_TutorialScript.sequences[m_CurrentSequence].m_BallResponsibility);
            DisplayText(
                m_TutorialScript.sequences[m_CurrentSequence].textToDisplay,
                m_TutorialScript.sequences[m_CurrentSequence].tutorialContext);
        }

        public void DisplayText(string information, string context)
        {
            m_TutorialInformation.text = information;
            m_TutorialContext.text = context;
        }

        public void NextSequence()
        {
            m_CurrentSequence++;

            if (m_CurrentSequence > m_TutorialScript.sequences.Length - 1)
            {
                m_CurrentSequence = m_TutorialScript.sequences.Length - 1;
                m_EndOfTutorialPrompt.SetActive(true);
                Debug.Log("We've reached the end of the tutorial script");
                return;
            }

            if (!m_TutorialScript.sequences[m_CurrentSequence].cameraCut)
                m_LerpingCamera = true;
            else
                CameraCutTo(
                    m_TutorialScript.sequences[m_CurrentSequence].cameraLocation,
                    m_TutorialScript.sequences[m_CurrentSequence].cameraRotation);
        }

        public void PreviousSequence()
        {
            m_CurrentSequence--;

            if (m_CurrentSequence < 0)
            {
                m_CurrentSequence++;
            }

            if (!m_TutorialScript.sequences[m_CurrentSequence].cameraCut)
                m_LerpingCamera = true;
            else
                CameraCutTo(
                    m_TutorialScript.sequences[m_CurrentSequence].cameraLocation,
                    m_TutorialScript.sequences[m_CurrentSequence].cameraRotation);
        }
    }
}