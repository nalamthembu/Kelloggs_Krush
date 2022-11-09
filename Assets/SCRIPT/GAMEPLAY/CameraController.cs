using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform m_Target;
    [SerializeField] [Range(1, 20)] float m_DistanceFromTarget;
    [SerializeField][Range(0, 1)] float m_ViewAngle = 25;
    [SerializeField][Range(1, 10)] float m_FollowSpeedMultiplier = 2;

    private void Start()
    {
        if (m_Target is null)
            Debug.LogError("There is no target assigned to the camera controller");
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = m_Target.position - transform.forward * m_DistanceFromTarget;

        transform.position = ULerpPos(desiredPosition);

        transform.forward = ULerpForward(m_Target.forward + Vector3.down * m_ViewAngle);
    }

    #region HELPER METHOD
    private Vector3 ULerpPos(Vector3 b)
    {
        return Vector3.Lerp(transform.position, b, Time.deltaTime * m_FollowSpeedMultiplier);
    }

    private Vector3 ULerpForward(Vector3 b)
    {
        return Vector3.Lerp(transform.forward, b, Time.deltaTime * m_FollowSpeedMultiplier);
    }
    #endregion
}
