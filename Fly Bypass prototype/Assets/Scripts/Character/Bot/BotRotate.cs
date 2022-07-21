using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on Bot
//Bot rotates when it triggers "Bot Rotation Point" box collier and slerps to qauternion of "Rotation Point"
//Bot Rotation Point has tag called "BotRotationPoint"
public class BotRotate : MonoBehaviour, ICharacterRotate
{

    GameManager gameManager;

    private float horizontalRotationSpeed = 120f;
    private float elapsedRotationTime = 0;
    private float totalRotationTime = 0;
    Quaternion firstQuaternion = Quaternion.identity;
    Quaternion targetQuaternion = Quaternion.identity;

    bool isRotating = false;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<GameManager>();
    }

    public void Rotate()
    {
        if (isRotating)
        {
            elapsedRotationTime += Time.deltaTime;
            float interpolationRatio = elapsedRotationTime / totalRotationTime;
            transform.rotation = Quaternion.Slerp(firstQuaternion, targetQuaternion, interpolationRatio);
            if (interpolationRatio >= 1)
            {
                isRotating = false;
                elapsedRotationTime = 0;
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BotRotationPoint"))
        {
            totalRotationTime = Mathf.Abs(other.transform.rotation.eulerAngles.y - this.transform.rotation.eulerAngles.y) /horizontalRotationSpeed;
            firstQuaternion = this.transform.rotation;
            targetQuaternion = other.transform.rotation;//burada scrippten alabilir, 
            isRotating = true;
        }
    }

}
