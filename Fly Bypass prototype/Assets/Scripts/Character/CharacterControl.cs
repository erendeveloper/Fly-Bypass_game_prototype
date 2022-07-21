using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    GameManager gameManager;

    CharacterBodyRotate playerBodyRotation;

    [SerializeField]
    LayerMask groundLayerMask;

    WingManager wingManager;

    [SerializeField]
    private CharacterAnimatorController characterAnimatorController;

    bool isStanding = true;

    bool isFlying = false;
    bool isFalling = false;
    bool isFinished = false;
    public bool Isfinished { set => isFinished = value; }

    private bool isStartedToRun = false;//first run

    float groundCheckRaidous = 0.1f; //sphere radius

    private PlayerMovement playerMovement;
    private ICharacterRotate characterRotate;//Player has PlayerRotate, Bot has BotRotate

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<GameManager>();
        playerBodyRotation = GetComponent<CharacterBodyRotate>();
        wingManager = this.GetComponent<WingManager>();
        playerMovement = new PlayerMovement(GetComponent<Rigidbody>(),transform);
        characterRotate = GetComponent<ICharacterRotate>();   
    }

    void FixedUpdate()
    {
        if(gameManager.GamePlay && !isStartedToRun)
        {
            isStartedToRun = true;
        }

        if (isStartedToRun && !isFinished)
        {
            playerMovement.Move();
            
            CheckPlayerOnPlatform();
        }    
    }
    private void Update()
    {
        if (isStanding && isStartedToRun)
        {
            characterAnimatorController.Run();
            isStanding = false;
        }       

        if (!isFinished)
        {
            characterRotate.Rotate();
        }
            
    }

    //Both platform and floor have "Ground" layer
    private void CheckPlayerOnPlatform()
    {      
        bool isGrounded = GroundCheck();
        if (isGrounded)
        {
            if (isFlying)
            {
                Land();
            }
            else if(isFalling)
            {
                Finish();
            }           
        }
        else
        {
            if (wingManager.WingsCount == 0)
            {
                if((!isFlying && !isFalling) || isFlying)
                {
                    Fall();
                }            
            }
            else
            {
                if (!isFlying)
                {
                    Fly();
                }
                
            }
        }
    }

    private void Fall()
    {
        isFalling = true;
        isFlying = false;
        playerMovement.Fall();
        characterAnimatorController.Fall();
    }
    private void Fly()
    {
        isFlying = true;
        playerBodyRotation.Fly();
        wingManager.OpenWings();
        characterAnimatorController.Fly();
    }
    private void Land()
    {
        isFlying = false;
        playerBodyRotation.Land();
        wingManager.CloseWings();
        characterAnimatorController.Run();
    }
    public void Finish()
    {
        isFalling = false;
        playerMovement.Finish();
        playerBodyRotation.Finish();
        characterAnimatorController.Finish();
    }

    private bool GroundCheck()
    {
        return Physics.CheckSphere(transform.position, groundCheckRaidous, groundLayerMask);
    }
}
