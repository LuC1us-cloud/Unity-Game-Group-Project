using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    public float smoothing = 5f;
    public float zoom = 5f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    /*
    Zoom function is in the update function because FixedUpdate doesn't catch every change of the Input.GetAxisRaw("Mouse ScrollWheel")
    and zooming in and out doesn't work as intended.
    */
    private void Update() {
        // control zoom with scrollwheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            zoom -= scroll * zoom * 1f;
            zoom = Mathf.Clamp(zoom, 5f, 15f);
        }
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoom, 0.1f);
    }
    /*
    Camera movement is in FixedUpdate() because player movement is also in FixedUpdate()
    Having them in separete types of updates makes the player GameObject jittery when moving, because animations don't line up with game updates.
    */
    private void FixedUpdate()
    {

        Vector3 targetCamPos = target.position;
        targetCamPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
