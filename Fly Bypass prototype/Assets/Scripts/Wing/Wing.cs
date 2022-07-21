using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on wing prefab
//Disable and go to the ppol after dropping
public class Wing : MonoBehaviour
{
    public void DisableWtithDelay(float delay)
    {
        Invoke("Disable",delay);
    }
    private void Disable()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.SetActive(false);
        WingObjectPoolManager.Add(this.transform);
    }
}
