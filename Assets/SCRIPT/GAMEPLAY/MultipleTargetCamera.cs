using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    [SerializeField] Transform[] m_Targets;
    [SerializeField] Vector3 m_Offset;
    [SerializeField] float m_SmoothTime = 0.5f;
    [SerializeField] Vector2 m_MinMaxZoom = new Vector2(40, 10);
    [SerializeField] [Min(1)] float m_ZoomLimiter = 20;
    [SerializeField] LayerMask m_ObstructionMask = -1;
    [SerializeField] float m_LookAngle = 25f;

    Vector3 m_Velocity;

    Camera m_Camera;

    private void Start()
    {
        m_Camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (m_Targets.Length <= 0)
            return;

        Move();
        //Zoom();
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(m_MinMaxZoom.y, m_MinMaxZoom.x, -GetGreatestDistance() / m_ZoomLimiter);
        m_Camera.fieldOfView = Mathf.Lerp(m_Camera.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(m_Targets[0].position, Vector3.zero);

        for (int i = 0; i < m_Targets.Length; i++)
        {
            bounds.Encapsulate(m_Targets[i].position);
        }

        return bounds.size.x;
    }

    void Move()
    {
        Vector3 centrePoint = GetCentrePoint();

        Vector3 newPos = centrePoint + m_Offset;

        CameraCollision(ref newPos, m_Targets[0].forward + Vector3.right * m_LookAngle);
        
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref m_Velocity, m_SmoothTime);
    }

    Vector3 GetCentrePoint()
    {
        if (m_Targets.Length == 1)
        {
            return m_Targets[0].position;
        }

        var bounds = new Bounds(m_Targets[0].position, Vector3.zero);

        for(int i = 0; i < m_Targets.Length; i++)
        {
            bounds.Encapsulate(m_Targets[i].position);
        }

        return bounds.center;
    }

    private void CameraCollision(ref Vector3 desiredPos, Vector3 targetRotation)
    {
        Vector3 lookDir = transform.forward;
        Vector3 rectOffset = lookDir * m_Camera.nearClipPlane;
        Vector3 rectPos = desiredPos + rectOffset;
        Vector3 castFrom = GetCentrePoint();
        Vector3 castLine = rectPos - castFrom;
        Quaternion lookRot = Quaternion.Euler(targetRotation);

        float castDist = castLine.magnitude;
        Vector3 castDir = castLine / castDist;

        if (Physics.BoxCast(castFrom, CameraHalfExtends, castDir, out RaycastHit hit,
                lookRot, castDist, m_ObstructionMask, QueryTriggerInteraction.Ignore))
        {
            rectPos = castFrom + castDir * hit.distance;
            desiredPos = rectPos - rectOffset;
        }
    }

    Vector3 CameraHalfExtends
    {
        get
        {
            Vector3 halfExtends;
            halfExtends.y = m_Camera.nearClipPlane *
                Mathf.Tan(0.5f * Mathf.Rad2Deg * m_Camera.fieldOfView);
            halfExtends.x = halfExtends.y * m_Camera.aspect;
            halfExtends.z = 0f;

            return halfExtends;
        }
    }
}
