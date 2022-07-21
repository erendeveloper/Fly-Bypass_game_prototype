using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PlayerRotate gets instance of this
public class Swerve
{
    private Camera _camera;

    public Swerve(Camera camera)
    {
        _camera = camera;
    }

    private float firstPositionX=0;

    private float swerveDistance = 0; //between 0 and 1
    public float SwerveRate { get => swerveDistance; }

    private bool isSwerving = false;
    public bool IsSwerving { get => isSwerving; }

    public void CheckSwerve()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSwerving = false;
            firstPositionX = _camera.ScreenToViewportPoint(Input.mousePosition).x;
        }
        else if (Input.GetMouseButton(0))
        {
            
            isSwerving = true;
            float lastPositionX = _camera.ScreenToViewportPoint(Input.mousePosition).x;
            swerveDistance = (lastPositionX - firstPositionX);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isSwerving = false;
            swerveDistance = 0;
        }
        else
        {
            isSwerving = false;
        }
    }

}
