using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanZoomBounds : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mapSprite;
    [SerializeField] private float zoomStep;
    [SerializeField] private float minCamSize;
    [SerializeField] private float maxCamSize;
    private Camera cam;
    private Vector3 dragOrigin;
    private Vector2 minMap, maxMap;

    void Awake()
    {
        minMap.x = mapSprite.transform.position.x - mapSprite.bounds.size.x / 2.0f;
        maxMap.x = mapSprite.transform.position.x + mapSprite.bounds.size.x / 2.0f;

        minMap.y = mapSprite.transform.position.y - mapSprite.bounds.size.y / 2.0f;
        maxMap.y = mapSprite.transform.position.y + mapSprite.bounds.size.y / 2.0f;
    }

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        PanCamera();
    }

    void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += difference;

            Vector3? pos = ClampCamera(cam.transform.position, cam.orthographicSize);
            if (pos != null) {
                cam.transform.position = (Vector3)pos;
            }
        }
    }

    public void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoomStep;
        newSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
        
        Vector3? pos = ClampCamera(cam.transform.position, newSize);
        if (pos != null) {
            cam.orthographicSize = newSize;
            cam.transform.position = (Vector3)pos;
        }
    }

    public void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoomStep;
        newSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
        
        Vector3? pos = ClampCamera(transform.position, newSize);
        if (pos != null) {
            cam.orthographicSize = newSize;
            transform.position = (Vector3)pos;
        }
    }

    private Vector3? ClampCamera(Vector3 _target, float _size)
    {
        float camHeight = _size;
        float camWidth = _size * cam.aspect;

        float minX = minMap.x + camWidth;
        float maxX = maxMap.x - camWidth;
        float minY = minMap.y + camHeight;
        float maxY = maxMap.y - camHeight;

        if (minX >= maxX)
            return null;
        
        if (minY >= maxY)
            return null;

        float x = Mathf.Clamp(_target.x, minX, maxX);
        float y = Mathf.Clamp(_target.y, minY, maxY);

        return new Vector3(x,y,_target.z);
    }
}
