using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoSingleton<GameDirector>
{
    public bool IsPlaying { get; private set; }
    public float ElapsedTime { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame(GameObject ui)
    {
        ui.SetActive(false);
        IsPlaying = true;
    }

    public void FinishGame()
    {
        IsPlaying = false;
        GameStateCache.Time = (float)Math.Round(ElapsedTime, 2);
        this.LoadSceneAsync("ResultScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPlaying)
            return;

        ElapsedTime += Time.deltaTime;
    }
}
