using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Score points on the floor
public class Point : MonoBehaviour
{
    TMP_Text PointText;
    private void Awake()
    {
        PointText = transform.GetChild(0).GetComponent<TMP_Text>();
    }
    private void Start()
    {
        PointText.text = Mathf.FloorToInt(transform.position.z).ToString();
    }
}
