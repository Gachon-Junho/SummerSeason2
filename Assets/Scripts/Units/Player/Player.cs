using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] 
    private Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x = Mathf.Clamp(mousePos.x, -10.25f, 10.25f);
        mousePos.y = Mathf.Clamp(mousePos.y, -4, 4);

        gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                Debug.Log("Game Over!");
                // Todo: 게임 끝.
                break;
            
            case "Bullet":
                other.gameObject.GetComponent<Bullet>().IsHited = true;
                // Todo: 게임 끝.
                break;
        }
    }
}
