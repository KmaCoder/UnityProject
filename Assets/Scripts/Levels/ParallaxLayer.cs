using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float Slowdown = 0.5f;
    private Vector3 _lastPosition;

    void Awake()
    {
        _lastPosition = Camera.main.transform.position;
    }

    void LateUpdate()
    {
        Vector3 newPosition = Camera.main.transform.position;
        Vector3 diff = newPosition - _lastPosition;
        _lastPosition = newPosition;
        Vector3 myPos = transform.position;
        myPos += Slowdown * diff;
        transform.position = myPos;
    }
}