using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on main Camera
public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Vector3 offsets = new Vector3(0,4f,-10f);

    private Quaternion firstRotation = Quaternion.Euler(0,0,0);

    private float distance;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.rotation = firstRotation;
        distance = offsets.z;
    }
    private void Start()
    {
        Follow();
    }

    void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 followedPosition = target.position + target.transform.forward * distance;//X and Z axes
        followedPosition.y += offsets.y;                                                 //Y axes
        transform.position = followedPosition;
        Look();
    }
    private void Look()
    {
        float rotationY = transform.rotation.y;
        transform.rotation = target.rotation;
        transform.Rotate(Vector3.up, rotationY);
    }

}

