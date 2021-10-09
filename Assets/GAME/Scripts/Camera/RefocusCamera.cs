using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefocusCamera : MonoBehaviour
{
    Camera camComponent;

    [SerializeField] Transform focusedObject;

    float defaultCamSize;

    [SerializeField] Vector3 centerOffset;
    
    Vector3 velocity = Vector3.zero;

    [SerializeField] [Range(0.1f, 1f)]
    private float smoothSpeed = 0.125f;


    // Camera size lerp values
    float prevCamSize;
    float targetCamSize;

    void Awake()
    {
        camComponent = GetComponent<Camera>();
    }

    void Start()
    {
        defaultCamSize = camComponent.orthographicSize;
        targetCamSize = defaultCamSize;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = focusedObject.position + centerOffset;
        
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }


    public void Refocus(Transform target, Vector2 focusOffset, float camSize = -1, float duration = 1) {
        focusedObject = target;
        centerOffset = new Vector3(focusOffset.x, focusOffset.y, centerOffset.z);

        prevCamSize = targetCamSize;
        if (camSize < 0)
            targetCamSize = defaultCamSize;
        
        else
            targetCamSize = camSize;

        if (targetCamSize != prevCamSize)
        {
            StartCoroutine(AdjustCamSize(duration));
        }
        
    }

    IEnumerator AdjustCamSize(float duration)
    {
        float CamSize;
        float alpha = 0, t;

        while (alpha < duration)
        {
            t = alpha / duration;
            t = t * t * (3f - 2f * t);

            CamSize = Mathf.Lerp(prevCamSize, targetCamSize, t);
            camComponent.orthographicSize = CamSize;

            alpha += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        camComponent.orthographicSize = targetCamSize;


        yield return null;
    }
}
