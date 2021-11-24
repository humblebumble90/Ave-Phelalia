using System.Collections;
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
    public GameObject boss1;
    public int numOfE1;
    public int numOfE2;
    public GameObject player;
    public float e1SpawningRate;
    private float e1SpawningCool = 0.0f;
    public float e2SpawningRate;
    private float e2SpawningCool = 0.0f;
    public Boundary boundary;
    [Header("Player initial setting")]
    public int numOfLives;
    [SerializeField]
    private int _lives;
    [SerializeField]
    private int _score;
    [SerializeField]
    private int _hp;
    private HpBarController hc;
    private bool bossSpawned = false;
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            scoreLabel.text = "Score: " + _score.ToString();
        }
    }
    public int Lives
    {
        get => _lives;
        set
        {
            _lives = value;
            livesLabel.text = "Life :" + _lives;
        }
    }
    public int Hp
    {
        get => _hp;
        set => _hp = value;
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
        //_lives = GameObject.FindWithTag("Player").GetComponent<playerController>().Lives;
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
                GameObject hco = GameObject.FindGameObjectWithTag("HpStatus");
                hc = hco.GetComponent<HpBarController>();
                e1s = new List<GameObject>();
                e2s = new List<GameObject>();
                Lives = numOfLives;
                Hp = 100;
                spawnPlayer();
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
        if(stageTime > 0)
        {
            time += Time.deltaTime;
        }
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
            if(!bossSpawned)
            {
                for (int i = 0; i < numOfE1; i++)
                {

                    float rndYPos = UnityEngine.Random.Range(boundary.Top, boundary.Bottom);
                    e1s.Add(
                        Instantiate(e1, new Vector3(boundary.Right, rndYPos, 0), Quaternion.identity));
                }
                e1SpawningCool = 0;
            } 
        }
        if(e2SpawningCool < e2SpawningRate)
        {
            e2SpawningCool += Time.deltaTime;
        }
        else
        {
            if(!bossSpawned)
            {
                for (int i = 0; i < numOfE2; i++)
                {
                    float rndYPos = UnityEngine.Random.Range(boundary.Top, boundary.Bottom);
                    e2s.Add(
                        Instantiate(e2, new Vector3(boundary.Right, rndYPos, 0), Quaternion.identity));
                }
                e2SpawningCool = 0;
            } 
        }
        if(stageTime == 0 && !bossSpawned)
        {
            bossSpawned = true;
            Instantiate(boss1, new Vector2(boundary.Right, 0), Quaternion.identity);
        }  
    }
    public void setHp(int hp)
    {
        if(Hp+hp > 0 && Hp+hp < 100)
        {
            Hp += hp;
        }
        else if(Hp + hp <= 0)
        {
            hc.addHealth(-1.0f);
            if(Lives > 0)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().destroyPlayer();
                Lives -= 1;
                StartCoroutine(respawnPlayer());
            }
            else
            {
                playerDied();
            }
        }
        else if(Hp + hp > 100)
        {
            Hp = 100;
            hc.addHealth(1.0f);
        }
    }
    private IEnumerator respawnPlayer()
    {
        yield return new WaitForSeconds(3.0f);
        hc.addHealth(1.0f);
        Hp = 100;
        spawnPlayer();
    }
    public void playerDied()
    {

    }

}
