using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//Copyright by Hyungseok Lee
public class GameController : MonoBehaviour
{
    public Button startBtn;
    public Button optBtn;
    public Button exitBtn;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
    }

    public void startBtnClicked()
    {
        SceneManager.LoadScene("Round1Scene");
    }

}
