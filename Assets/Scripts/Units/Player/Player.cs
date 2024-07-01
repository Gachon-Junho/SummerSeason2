using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = GameObject.Find("Main Camera")
                                        .GetComponent<Camera>()
                                        .ScreenToWorldPoint(Input.mousePosition);
        
        gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }
}
