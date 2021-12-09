using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{ 
    GameObject mainMenu;
    MainControl gameController;
    [SerializeField]
    private Image langImage;
    [SerializeField]
    private Image soundImage;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text backText;
    [SerializeField]
    private Text exitText;
    [SerializeField]
    private Text aboutText;
    [SerializeField]
    private Text infoPageText;
    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("MainController").GetComponent<MainControl>();
        mainMenu = GameObject.FindGameObjectWithTag("Main menu");
        if (gameController.Lang == "EN")
        {
            langImage.sprite = Resources.Load<Sprite>("Prefab/UI/ENlang");
            setAllTextLangEN();
        }
        else
        {
            setAllTextLangRU();
            langImage.sprite = Resources.Load<Sprite>("Prefab/UI/RUlang");
        }
        if (gameController.soundState == true)
        {
            soundImage.sprite = Resources.Load<Sprite>("Prefab/UI/SoundOffButton");
        }
        else
        {
            soundImage.sprite = Resources.Load<Sprite>("Prefab/UI/SoundOnButton");
        }
        timeText.text = gameController.getGameTimeString();
    }
    public void backToGame()
    {
        Time.timeScale = 1.0f;
        Destroy(mainMenu, 0.5f);
    }
    public void changeLang()
    {
        gameController.GetComponent<MainControl>().changeLang();
        if (gameController.Lang == "EN")
        {
            setAllTextLangEN();
            langImage.sprite = Resources.Load<Sprite>("Prefab/UI/ENlang");
        }
        else
        {
            setAllTextLangRU();
            langImage.sprite = Resources.Load<Sprite>("Prefab/UI/RUlang");
        }
    }
    public void toggleSound()
    {
        gameController.GetComponent<MainControl>().toggleSound();
        if (gameController.soundState == false)
        {
            soundImage.sprite = Resources.Load<Sprite>("Prefab/UI/SoundOffButton");
        }
        else
        {
            soundImage.sprite = Resources.Load<Sprite>("Prefab/UI/SoundOnButton");
        }
    }
    public void exitGame()
    {
        Application.Quit();
    }
    private void setAllTextLangRU()
    {
        backText.text = "Назад";
        aboutText.text = "Описание";
        exitText.text = "Выход";
        infoPageText.text = "Игра создана @haulin007" +
            "\nпри поддержке идей и друзей";

    }
    private void setAllTextLangEN()
    {
        backText.text = "Back";
        aboutText.text = "About";
        exitText.text = "Exit";
        infoPageText.text = "Game created by @haulin007" +
            "\nWith helps and with friends";

    }
}