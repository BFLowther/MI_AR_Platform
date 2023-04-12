using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGPSPlayer : MonoBehaviour
{
    public void Move(Vector2 _position)
    {
        transform.position = new Vector3(_position.x, _position.y, 0.0f);
    }
}
