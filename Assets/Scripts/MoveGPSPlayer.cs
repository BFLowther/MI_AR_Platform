using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGPSPlayer : MonoBehaviour
{
    public Vector3 targetLocalPosition;
    public float speed = 2.0f;

    void Update()
    {
        if(Vector2.Distance(transform.localPosition, targetLocalPosition) < ((speed * 1.5f) * Time.deltaTime)) {
            transform.localPosition = targetLocalPosition;
        } else {
            Vector2 direction = targetLocalPosition - transform.localPosition;
            transform.localPosition = ((Vector2)transform.localPosition) + (direction.normalized * speed * Time.deltaTime);
        }
    }
    public void Move(Vector2 _position)
    {
        Debug.Log(transform.localPosition);
        targetLocalPosition = new Vector3(_position.x, _position.y, 0.0f);
        Debug.Log(transform.localPosition);
    }
}
