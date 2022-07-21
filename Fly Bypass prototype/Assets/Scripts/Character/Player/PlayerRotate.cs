using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on Player
//Horizontal movement on platform by swerve input
public class PlayerRotate : MonoBehaviour, ICharacterRotate
{
    Swerve swerve;

    private float horizontalRotationSpeed = 120f;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<GameManager>();
        swerve = new Swerve(Camera.main);
    }
    public void Rotate()
    {
        swerve.CheckSwerve();
        if (swerve.IsSwerving)
        {
            float rotationRate = swerve.SwerveRate * horizontalRotationSpeed;
            transform.Rotate(Vector3.up * rotationRate * Time.deltaTime);
        }
    }

}
