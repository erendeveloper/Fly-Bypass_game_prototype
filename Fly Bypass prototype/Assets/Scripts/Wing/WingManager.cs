using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on character
public class WingManager : MonoBehaviour
{
    WingObjectPoolManager wingObjectPoolManager;

    [SerializeField] private GameObject wingPrefab;
    [SerializeField] private GameObject wingsContainer;//select transform on character
    private Transform leftWingContainer;
    private Transform rightWingContainer;
    [SerializeField] private Transform wingsContainerParent;//parent of left and right containers

    float wingSizeX = 0;

    float wingMoveTime = 1f;
    float wingsMoveElapsedTime = 0;  //0 means that wings are closed.1 means that wings are open
    float wingDropTimeInterval = 1f;
    float wingDisableTimeAfterDrop = 1f;

    bool areWingsOpening = false;
    bool areWingsClosing = false;

    int wingsCount = 0;
    public int WingsCount { get => wingsCount; }


    List<Transform> leftWings = new List<Transform>();
    List<Transform> rightWings = new List<Transform>();


    private void Awake()
    {
        wingObjectPoolManager = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<WingObjectPoolManager>();
        wingSizeX = wingPrefab.transform.localScale.x;
    }
    private void Start()
    {
        CreateWingsContainer();
    }
    private void Update()
    {
        MoveWings();
    }
    private void CreateWingsContainer()
    {
        GameObject wings = Instantiate(wingsContainer, wingsContainerParent);
        wings.transform.localPosition = Vector3.zero;
        wings.transform.localRotation = Quaternion.identity;
        leftWingContainer = wings.transform.GetChild(0);
        rightWingContainer = wings.transform.GetChild(1);
    }
    public void GenerateWings()
    {
        GameObject leftWing, rightWing;
        if (wingObjectPoolManager.IsItemAtPool)  //take from object pool
        {
            leftWing = wingObjectPoolManager.Take().gameObject;
            rightWing = wingObjectPoolManager.Take().gameObject;
            leftWing.SetActive(true);
            rightWing.SetActive(true);

        }
        else                                     //instantiate
        {
            leftWing = Instantiate(wingPrefab);
            rightWing = Instantiate(wingPrefab);
        }
        leftWing.transform.parent = leftWingContainer;
        leftWing.transform.localPosition = Vector3.zero;
        leftWing.transform.localRotation = Quaternion.identity;
        leftWings.Add(leftWing.transform);
        rightWing.transform.parent = rightWingContainer;
        rightWing.transform.localPosition = Vector3.zero;
        rightWing.transform.localRotation = Quaternion.identity;
        rightWings.Add(rightWing.transform);
        

        wingsCount++;

    }

    public void MoveWings()
    {
        if (areWingsOpening)
        {
            wingsMoveElapsedTime += Time.deltaTime;
        }
        else if (areWingsClosing)
        {
            wingsMoveElapsedTime -= Time.deltaTime;
        }
        else
        {
            return;
        }
        

        float interpolationRatio = wingsMoveElapsedTime / wingMoveTime;
        
        
            for (int i = 0; i < wingsCount; i++)
            {
                leftWings[i].localPosition = Vector3.Lerp(Vector3.zero, new Vector3(wingSizeX * i * (-1), 0, 0),interpolationRatio);
                rightWings[i].localPosition = Vector3.Lerp(Vector3.zero, new Vector3(wingSizeX * i, 0, 0), interpolationRatio);
            }
        

        if (wingsMoveElapsedTime <= 0)
        {
            areWingsClosing = false;
        }
        else if (wingsMoveElapsedTime >= 1)
        {
            areWingsOpening = false;

            StartDropingWings();     
        }
    }
    public void OpenWings()
    {
        areWingsOpening = true;
        areWingsClosing = false;
        if (wingsMoveElapsedTime <= 0)
        {
            wingsMoveElapsedTime = 0f;
        }
        
    }
    public void CloseWings()
    {
        areWingsOpening = false;
        areWingsClosing = true;
        if (wingsMoveElapsedTime >= wingMoveTime)
        {
            wingsMoveElapsedTime = wingMoveTime;
        }
        StopAllCoroutines();
    }

    private void StartDropingWings()
    {
        for (int i = wingsCount-1; i >= 0; i--)
        {
            StartCoroutine(DropWing(leftWings[i], leftWings, wingDropTimeInterval * (wingsCount-i),1));
            StartCoroutine(DropWing(rightWings[i], rightWings, wingDropTimeInterval * (wingsCount - i),0));

        }
    }


    IEnumerator DropWing(Transform wing,List<Transform> wingsList,float waitTime, int decreasedWing)
    {
        //because there are couple of wings, use decreasedWing once to decrease to wingsCount accuretly
        yield return new WaitForSeconds(waitTime);
        wing.transform.parent = null;
        wing.GetComponent<Rigidbody>().isKinematic = false;
        wing.GetComponent<Wing>().DisableWtithDelay(wingDisableTimeAfterDrop);
        wingsList.Remove(wing);
        wingsCount -= decreasedWing;
    }


}
