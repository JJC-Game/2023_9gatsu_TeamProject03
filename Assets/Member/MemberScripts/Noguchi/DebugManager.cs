using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public GameObject scroll;
    RectTransform rect;

    void Start()
    {
        rect = scroll.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rect.localPosition += new Vector3(0, 10, 0);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            rect.localPosition += new Vector3(0, -10, 0);
        }
    }
}
