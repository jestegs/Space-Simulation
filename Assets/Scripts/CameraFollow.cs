using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool isFollow;
    public CelestialBody targetBody;
    public Camera camera;
    public Vector3 zPosition = new Vector3(0,0,-10);

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(isFollow)
        {
            camera.transform.position = targetBody.transform.position + zPosition;
        }
        else
        {
            camera.transform.position = new Vector3(0, 0, 0) + zPosition;
        }
    }
}
