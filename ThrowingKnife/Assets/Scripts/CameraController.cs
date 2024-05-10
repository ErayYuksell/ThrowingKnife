using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<Transform> targets;

    public float offSetY;
    Vector3 currentSpeed;
    float movementSoftness = 0.8f;

    [SerializeField] Camera mainCamera;
    float minZoom = 85f;
    float maxZoom = 70f;
    float zoomLimit = 10f;
    private void LateUpdate()
    {
        Movement();
        Zoom();
    }
    void Zoom()
    {
        if (Distance() > 5)
        {
            float newZoom = Mathf.Lerp(maxZoom, minZoom, Distance() / zoomLimit);
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, newZoom, Time.deltaTime);
        }
        else
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, maxZoom, Time.deltaTime);
        }
    }
    float Distance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.y;
    }
    void Movement()
    {
        Vector3 newPos = new Vector3(transform.position.x, targets[0].position.y + offSetY, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentSpeed, movementSoftness);
    }
}
