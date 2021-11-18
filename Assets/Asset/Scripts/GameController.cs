﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//Copyright by Hyungseok Lee
public class GameController : MonoBehaviour
{
    public GameObject ui;
    public Text timeLabel;
    public Text livesLabel;
    public Text scoreLabel;
    public GameObject buttons;
    private float time;
    public int stageTime;
    public GameObject e1;
    private List<GameObject> e1s;
    public GameObject e2;
    private List<GameObject> e2s;
    public int numOfE1;
    public int numOfE2;
    public GameObject player;
    public float e1SpawningRate;
    private float e1SpawningCool = 0.0f;
    public float e2SpawningRate;
    private float e2SpawningCool = 0.0f;
    public Boundary boundary;
    private int _lives;
    private int _score;
    private bool bossSpawned = false;
    public int Lives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = value;
            scoreLabel.text = "Lives: " + _lives.ToString();
        }
    }
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            scoreLabel.text = "Score: " + _score.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initialize();
    }

    // Update is called once per frame
    void Update()
    {
        checkTime();
        spawnEnemy();
    }
    void initialize()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        switch(SceneManager.GetActiveScene().name)
        {
            case "StartScene":
                ui.SetActive(false);
                buttons.SetActive(true);
                break;
            case "Round1Scene":
                spawnPlayer();
                e1s = new List<GameObject>();
                e2s = new List<GameObject>();
                break;
        }
    }
    public void startBtnClicked()
    {
        SceneManager.LoadScene("Round1Scene");
    }
    public void optBtnClicked()
    {
        SceneManager.LoadScene("OptionScene");
    }
    public void exitBtnClicked()
    {
        Application.Quit();
    }
    void checkTime()
    {
        time += Time.deltaTime;
        if(time >= 1.0f)
        {
            stageTime -= 1;
            time = 0;
        }
        timeLabel.text = "Time: " + (stageTime);
    }
    void spawnPlayer()
    {
        Instantiate(player, new Vector3(-7,0,0), Quaternion.identity);
    }
    void spawnEnemy()
    {
        if (e1SpawningCool < e1SpawningRate)
        {
            e1SpawningCool += Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < numOfE1; i++)
            {

                float rndYPos = UnityEngine.Random.Range(boundary.Top, boundary.Bottom);
                e1s.Add(
                    Instantiate(e1, new Vector3(boundary.Right, rndYPos, 0), Quaternion.identity));
            }
            e1SpawningCool = 0;
        }
        if(e2SpawningCool < e2SpawningRate)
        {
            e2SpawningCool += Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < numOfE2; i++)
            {
                float rndYPos = UnityEngine.Random.Range(boundary.Top, boundary.Bottom);
                e1s.Add(
                    Instantiate(e2, new Vector3(boundary.Right, rndYPos, 0), Quaternion.identity));
            }
            e2SpawningCool = 0;
        }
        
        //switch (SceneManager.GetActiveScene().name)
        //{
        //    case "StartScene":
        //        break;
        //    case "Round1Scene":
        //        if (e1SpawningCool < e1SpawningRate)
        //        {
        //            e1SpawningCool += Time.deltaTime;
        //        }
        //        else
        //        {
        //            float rndYPos = UnityEngine.Random.Range(boundary.Top, boundary.Bottom);
        //            Instantiate(e1, new Vector3(boundary.Right, rndYPos, 0), Quaternion.identity);
        //            Debug.Log("Enemy1 spawned");
        //            e1SpawningCool = 0;
        //        }
        //        if (e2SpawningCool < e2SpawningRate)
        //        {
        //            e2SpawningCool += Time.deltaTime;
        //        }
        //        else
        //        {
        //            float rndYPos = UnityEngine.Random.Range(boundary.Top, boundary.Bottom);
        //            Instantiate(e2, new Vector3(boundary.Right, rndYPos, 0), Quaternion.identity);
        //            Debug.Log("Enemy2 spawned");
        //            e2SpawningCool = 0;
        //        }
        //        break;
        //    default:
        //        break;
        //}

    }

}
