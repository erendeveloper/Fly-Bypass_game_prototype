using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on Stack prefab
public class Stack : MonoBehaviour
{
    private float recycleTime=0.5f;

    GameObject body;
    BoxCollider boxCollider;

    private void Awake()
    {
        body = transform.GetChild(0).gameObject;
        boxCollider = this.GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        boxCollider.enabled = false;
        body.SetActive(false);
        Invoke("Recycle", recycleTime);
        other.GetComponent<WingManager>().GenerateWings();
    }

    private void Recycle()
    {
        body.SetActive(true);
        boxCollider.enabled = true;
    }
}
