using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//Copyright by Hyungseok Lee
public class GameController : MonoBehaviour
{
    public GameObject ui;
    public GameObject buttons;
    // Start is called before the first frame update
    void Start()
    {
        initialize();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void initialize()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        switch(SceneManager.GetActiveScene().name)
        {
            case "StartScene":
                ui.SetActive(false);
                break;
            case "Round1Scene":
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

}
