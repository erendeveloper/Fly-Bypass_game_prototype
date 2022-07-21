using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on wing prefab
//Disable and go to the pOol after dropping
public class Wing : MonoBehaviour
{
    WingObjectPoolManager wingObjectPoolManager;
    private void Awake()
    {
        wingObjectPoolManager = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<WingObjectPoolManager>();
    }
    public void DisableWtithDelay(float delay)
    {
        Invoke("Disable",delay);
    }
    private void Disable()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        wingObjectPoolManager.Add(this.transform);
        this.gameObject.SetActive(false);
        
    }
}
