using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class ARObjectPlacement : MonoBehaviour
{
    public GameObject objСity;
    private ARRaycastManager arRaycastManager;
    private ARPlaneManager arPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject city;

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
    }

    void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    void FingerDown(EnhancedTouch.Finger finger) {
        if (finger.index != 0) return;
        if (arRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon)){
            var hit = hits[hits.Count - 1];
            Pose pose = hit.pose;
            Destroy(city);
            GameObject obj = Instantiate(objСity, pose.position, pose.rotation);
            city = obj;
            obj.transform.localScale *= 0.1f;
            if (arPlaneManager.GetPlane(hit.trackableId).alignment == PlaneAlignment.HorizontalUp){
                Vector3 position = obj.transform.position;
                Vector3 cameraPosition = Camera.main.transform.position;
                cameraPosition.y = 0f;
                Vector3 direction = cameraPosition - position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                obj.transform.rotation = targetRotation;
            }
        }
    }
}
