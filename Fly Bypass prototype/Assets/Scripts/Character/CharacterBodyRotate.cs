using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on character
//Rotation for flying vertically and finish horizontally
public class CharacterBodyRotate : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField]private Transform body;

    float verticalRotationTime = 0.2f;
    float verticalRotationElapsedTime = 0;
    float horizontalRotationTime = 0.1f;
    float horizontalRotationElapsedTime = 0;

    Quaternion standQuaternion = Quaternion.identity;
    Quaternion flyingQuaternion = Quaternion.Euler(-30, 0, 0);
    Quaternion finishQuaternion = Quaternion.Euler(0, 180, 0);
    Quaternion horizontalFirstQuaternion;

    bool isGoingToFly = false;
    bool isGoingToLand = false;
    bool isGoingToFinish = false;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<GameManager>();
        horizontalFirstQuaternion = standQuaternion;
    }
    // Update is called once per frame
    void Update()
    {
        RotateHorizontally();
        RotateVertically();
    }


    private void RotateHorizontally()
    {
        if (isGoingToFinish)
        {
            horizontalRotationElapsedTime += Time.deltaTime;
            CheckFinish();

            float interpolationRatio = horizontalRotationElapsedTime / horizontalRotationTime;

            body.localRotation = Quaternion.Slerp(horizontalFirstQuaternion, finishQuaternion, interpolationRatio);
        }
    }

    

    private void RotateVertically()
    {
        if (isGoingToFly)
        {
            verticalRotationElapsedTime += Time.deltaTime;
            if (verticalRotationElapsedTime >= verticalRotationTime)
            {
                verticalRotationElapsedTime = verticalRotationTime;
                isGoingToFly = false;
            }
        }
        else if (isGoingToLand)
        {
            verticalRotationElapsedTime -= Time.deltaTime;
            if (verticalRotationElapsedTime <= 0)
            {
                verticalRotationElapsedTime = 0f;
                isGoingToLand = false;
            }
        }
        else
        {
            return;
        }

        float interpolationRatio = verticalRotationElapsedTime / verticalRotationTime;
        body.localRotation = Quaternion.Slerp(standQuaternion, flyingQuaternion, interpolationRatio);
    }

    public void Fly()
    {
        isGoingToFly = true;
        isGoingToLand = false;
        horizontalFirstQuaternion = flyingQuaternion;
    }
    public void Land()
    {
        isGoingToFly = false; ;
        isGoingToLand = true;
        horizontalFirstQuaternion = standQuaternion;

    }
    public void Finish()
    {
        isGoingToFinish = true;
    }

    private void CheckFinish()
    {
        if (horizontalRotationElapsedTime >= horizontalRotationTime)
        {
            isGoingToFinish = false;
            this.GetComponent<CharacterControl>().Isfinished = true;
            gameManager.GameOver(gameObject.tag, transform.position.z);         
        }
    }
}
