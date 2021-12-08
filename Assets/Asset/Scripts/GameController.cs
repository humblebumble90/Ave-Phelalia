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
    public GameObject gameOverSceneButtons;
    public GameObject backgrounds;
    public Storage storage;
    private int stage;
    private float time;
    public int stageTime;
    public GameObject e1;
    private List<GameObject> e1s;
    public GameObject e2;
    private List<GameObject> e2s;
    public GameObject e3;
    private List<GameObject> e3s;
    public GameObject e4;
    private List<GameObject> e4s;
    public GameObject boss1;
    public GameObject boss2;
    public GameObject boss3;
    private bool bossSpawned = false;
    public int numOfE1;
    public int numOfE2;
    public int numOfE3;
    public int numOfE4;
    public GameObject player;
    public float e1SpawningRate;
    private float e1SpawningCool = 0.0f;
    public float e2SpawningRate;
    private float e2SpawningCool = 0.0f;
    public float e3SpawningRate;
    private float e3SpawningCool = 0.0f;
    public float e4SpawningRate;
    private float e4SpawningCool = 0.0f;
    public Boundary boundary;
    [Header("Player initial setting")]
    public int numOfLives;
    [SerializeField]
    private int _lives;
    [SerializeField]
    private int _score;
    [SerializeField]
    private int _hp;
    private GameObject hco;
    private HpBarController hc;
    [Header("Audio Sources")]
    public AudioSource[] audioSources;
    public GameObject explosion;
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
                gameOverSceneButtons.SetActive(false);
                backgrounds.SetActive(true);
                audioSources[(int)SoundClip.START_SCENE_THEME].loop = true;
                audioSources[(int)SoundClip.START_SCENE_THEME].Play();
                break;
            case "Round1Scene":
                ui.SetActive(true);
                buttons.SetActive(false);
                gameOverSceneButtons.SetActive(false);
                backgrounds.SetActive(true);
                hco = GameObject.FindGameObjectWithTag("HpStatus");
                hc = hco.GetComponent<HpBarController>();
                stage = 1;
                e1s = new List<GameObject>();
                e2s = new List<GameObject>();
                storage.lives = numOfLives;
                Lives = storage.lives;
                Hp = 100;
                spawnPlayer();
                audioSources[(int)SoundClip.GAME_THEME1].loop = true;
                audioSources[(int)SoundClip.GAME_THEME1].Play();
                break;
            case "Round2Scene":
                ui.SetActive(true);
                buttons.SetActive(false);
                gameOverSceneButtons.SetActive(false);
                backgrounds.SetActive(true);
                hco = GameObject.FindGameObjectWithTag("HpStatus");
                hc = hco.GetComponent<HpBarController>();
                stage = 2;
                e1s = new List<GameObject>();
                e2s = new List<GameObject>();
                e3s = new List<GameObject>();
                Lives = storage.lives;
                Hp = 100;
                spawnPlayer();
                audioSources[(int)SoundClip.GAME_THEME2].loop = true;
                audioSources[(int)SoundClip.GAME_THEME2].Play();
                break;
            case "Round3Scene":
                ui.SetActive(true);
                buttons.SetActive(false);
                gameOverSceneButtons.SetActive(false);
                backgrounds.SetActive(true);
                hco = GameObject.FindGameObjectWithTag("HpStatus");
                hc = hco.GetComponent<HpBarController>();
                stage = 3;
                e1s = new List<GameObject>();
                e2s = new List<GameObject>();
                e3s = new List<GameObject>();
                e4s = new List<GameObject>();
                Lives = storage.lives;
                Hp = 100;
                spawnPlayer();
                audioSources[(int)SoundClip.GAME_THEME3].loop = true;
                audioSources[(int)SoundClip.GAME_THEME3].Play();
                break;
            case "GameoverScene":
                ui.SetActive(false);
                buttons.SetActive(false);
                gameOverSceneButtons.SetActive(true);
                backgrounds.SetActive(false);
                audioSources[(int)SoundClip.GAME_OVER_THEME].loop = true;
                audioSources[(int)SoundClip.GAME_OVER_THEME].Play();
                break;
            default:
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
    public void retryBtnClicekd()
    {
        switch(storage.lastScene)
        {
            case 1:
                SceneManager.LoadScene("Round1Scene");
                break;
            case 2:
                SceneManager.LoadScene("Round2Scene");
                break;
            case 3:
                SceneManager.LoadScene("Round3Scene");
                break;
            default:
                break;
        }
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
            //Enemy1 spawning
            if (e1SpawningCool < e1SpawningRate)
        {
            e1SpawningCool += Time.deltaTime;
        }
        else
        {
            if(!bossSpawned && stage > 0)
            {
                for (int i = 0; i < numOfE1; i++)
                {
                    float rndXpos = UnityEngine.Random.Range(boundary.Right, boundary.Right+2.0f);
                    float rndYPos = UnityEngine.Random.Range(boundary.Top, boundary.Bottom);
                    e1s.Add(
                        Instantiate(e1, new Vector3(rndXpos, rndYPos, 0), Quaternion.identity));
                }
                e1SpawningCool = 0;
            } 
        }
        //Enemy2 spawning
        if(e2SpawningCool < e2SpawningRate)
        {
            e2SpawningCool += Time.deltaTime;
        }
        else
        {
            if(!bossSpawned && stage > 0)
            {
                for (int i = 0; i < numOfE2; i++)
                {
                    float rndXpos = UnityEngine.Random.Range(boundary.Right, boundary.Right + 2.0f);
                    float rndYPos = UnityEngine.Random.Range(boundary.Top, boundary.Bottom);
                    e2s.Add(
                        Instantiate(e2, new Vector3(rndXpos, rndYPos, 0), Quaternion.identity));
                }
                e2SpawningCool = 0;
            } 
        }
        //Enemy3 spawning
        if (e3SpawningCool < e3SpawningRate)
        {
            e3SpawningCool += Time.deltaTime;
        }
        else
        {
            if (!bossSpawned && stage > 1)
            {
                for (int i = 0; i < numOfE3; i++)
                {
                    float rndXpos = UnityEngine.Random.Range(boundary.Right, boundary.Right + 2.0f);
                    float rndYPos = UnityEngine.Random.Range(boundary.Top, boundary.Bottom);
                    e3s.Add(
                        Instantiate(e3, new Vector3(rndXpos, rndYPos, 0), Quaternion.identity));
                }
                e3SpawningCool = 0;
            }
        }

        if (stageTime == 0 && !bossSpawned && SceneManager.GetActiveScene().name == "Round1Scene")
        {
            bossSpawned = true;
            Instantiate(boss1, new Vector2(boundary.Right, 0), Quaternion.identity);
        }
        if (stageTime == 0 && !bossSpawned && SceneManager.GetActiveScene().name == "Round2Scene")
        {
            bossSpawned = true;
            Instantiate(boss2, new Vector2(boundary.Right, 0), Quaternion.identity);
        }
        //Enemy4 spawning
        if (e4SpawningCool < e4SpawningRate)
        {
            e4SpawningCool += Time.deltaTime;
        }
        else
        {
            if (!bossSpawned && stage > 2)
            {
                for (int i = 0; i < numOfE4; i++)
                {
                    float rndXpos = UnityEngine.Random.Range(boundary.Right, boundary.Right + 2.0f);
                    float rndYPos = UnityEngine.Random.Range(boundary.Top, boundary.Bottom);
                    e4s.Add(
                        Instantiate(e4, new Vector3(rndXpos, rndYPos, 0), Quaternion.identity));
                }
                e4SpawningCool = 0;
            }
        }
        //Boss spawning
        if (stageTime == 0 && !bossSpawned)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Round1Scene":
                    bossSpawned = true;
                    Instantiate(boss1, new Vector2(boundary.Right, 0), Quaternion.identity);
                    break;
                case "Round2Scene":
                    bossSpawned = true;
                    Instantiate(boss2, new Vector2(boundary.Right, 0), Quaternion.identity);
                    break;
                case "Round3Scene":
                    bossSpawned = true;
                    Instantiate(boss3, new Vector2(boundary.Right, 0), Quaternion.identity);
                    break;
                default:
                    break;
            }
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
        switch(SceneManager.GetActiveScene().name)
        {
            case "Round1Scene":
                storage.lastScene = 1;
                SceneManager.LoadScene("GameoverScene");
                break;
            case "Round2Scene":
                storage.lastScene = 2;
                SceneManager.LoadScene("GameoverScene");
                break;
            case "Round3Scene":
                storage.lastScene = 3;
                SceneManager.LoadScene("GameoverScene");
                break;
            default:
                break;
        }
    }
    public void clearStage()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "Round1Scene":
                storage.lastScene = 2;
                storage.lives = Lives;
                SceneManager.LoadScene("Round2Scene");
                break;
            case "Round2Scene":
                storage.lastScene = 3;
                storage.lives = Lives;
                SceneManager.LoadScene("Round3Scene");
                break;
            default:
                break;
        }
    }

}
