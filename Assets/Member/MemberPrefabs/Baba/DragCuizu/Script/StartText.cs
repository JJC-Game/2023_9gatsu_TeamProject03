using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartText : MonoBehaviour
{
    Timer timer;
    DragCuizuBaseMane D;
    // Start is called before the first frame update
    void Start()
    {
      GameObject T= GameObject.Find("GameManager");
        timer = T.GetComponent<Timer>();
        D = T.GetComponent<DragCuizuBaseMane>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.startTimeLine == true)
        {
            this.gameObject.SetActive(false);
        }
        if (timer.startTime)
        {
            D.inGameEnable = true;
        }
    }
}
