using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Unit
{
    [SerializeField] 
    private Camera mainCamera;

    [SerializeField]
    private GameObject guard;

    private const KeyCode shoot_key_code = KeyCode.Z;
    private const KeyCode guard_key_code = KeyCode.X;
    
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

        if (Input.GetKeyDown(shoot_key_code) || Input.GetKeyUp(guard_key_code))
            StartCoroutine(processShoot());
        
        // Todo: 클릭시 가드 발동하고 서서히 사라지기
        guard.SetActive(Input.GetKey(guard_key_code));
    }

    public override void OnKilled(Unit killedUnit)
    {
        if (killedUnit is not IHasPoint)
            return;
        
        GameStateCache.Score += ((IHasPoint)killedUnit).Point;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
            case "Bullet":
                if (ReferenceEquals(other.gameObject.GetComponent<Bullet>()?.Start, this))
                    return;
                
                this.LoadSceneAsync("ResultScene");
                break;
        }
    }

    private IEnumerator processShoot()
    {
        while (Input.GetKey(shoot_key_code) && !Input.GetKey(guard_key_code))
        {
            var bullet = BulletPoolingManager.Current.Pool.Get();
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().Initialize(this, Vector3.right, Damage, 0.04f);

            yield return new WaitForSeconds(1 / AttackSpeed);
        }
    }
}
