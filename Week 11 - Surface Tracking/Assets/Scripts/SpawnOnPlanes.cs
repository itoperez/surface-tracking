using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]
public class SpawnOnPlanes : MonoBehaviour
{
    [SerializeField]
    GameObject placedPrefab;

    [SerializeField]
    GameObject mainCamera;


    GameObject spawnObject;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    ARRaycastManager m_raycastManager;

    private void Awake()
    {
        m_raycastManager = GetComponent<ARRaycastManager>();
    }

    bool GetTouch(out Vector2 touch_pos)
    {
        if(Input.touchCount > 0)
        {
            touch_pos = Input.GetTouch(0).position;
            return true;
        }
        touch_pos = default;
        return false;
    }

    private void Update()
    {
        if(GetTouch(out Vector2 touch_pos) == false)
        {
            return;
        }

        if(m_raycastManager.Raycast(touch_pos, hits, TrackableType.Planes))
        {
            var hitPose = hits[0].pose;

            if(spawnObject == null)
            {            
                spawnObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                
            }
            else
            {
                spawnObject.transform.position = hitPose.position;
            }

            Vector3 targetPosition = new Vector3(mainCamera.transform.position.x, hitPose.position.y, mainCamera.transform.position.z);
            spawnObject.transform.LookAt(targetPosition);
        }      
    }

}
