using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Telephone : MonoBehaviour
{
    [SerializeField]
    private Text textSpeach;
    [SerializeField]
    private GameObject speachObj;
    [SerializeField]
    private float cpeachTime = 6.0f;
    [SerializeField]
    private string textPrefixName;
    private int textsCount = 1;
    [SerializeField]
    private GameObject shadowGirl;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private GameObject historySheatPrefab;
    [SerializeField]
    private string lang;
    [SerializeField]
    private bool hasCoin = false;
    [SerializeField]
    private bool isCharacterReadyTocall = false;
    [SerializeField]
    private bool isFirstCall = true;


    void Start()
    {
        checkLang();

        while (textsCount < 20)
        {
            try
            {
                if (Resources.Load<TextAsset>("Text/" + lang + "/" + textPrefixName + textsCount) != null)
                {
                    textsCount++;
                }
                else
                {
                    break;
                }
            }
            catch
            {
                break;
            }
        }
    }
    public void action()
    {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.tag == "Obstacle")
                {
                    try
                    {
                        colliders[i].gameObject.GetComponent<CoinControl>().isCoin();
                        if (hasCoin == false)
                        {
                            Destroy(colliders[i].gameObject);
                            hasCoin = true;
                        }
                        return;
                    }
                    catch
                    {
                    }

                    try
                    {
                        colliders[i].gameObject.GetComponent<telephoneNumberController>().destroyThis();
                        isCharacterReadyTocall = true;
                        return;
                    }
                    catch
                    {
                    }
            }
                if (colliders[i].gameObject.tag == "Player")
                {
                    if (hasCoin == true)
                    {
                        if (isCharacterReadyTocall == true)
                        {
                            speachObj.SetActive(true);
                            textSpeach.text = "I'm wait for you...\n-so many time..";
                            Invoke("closeSpeach", cpeachTime);
                            Instantiate(shadowGirl, spawnPoint.transform.position, Quaternion.identity, gameObject.GetComponentInParent<Transform>());
                            hasCoin = false;
                        }
                        else
                        {
                            if (isFirstCall)
                            {
                                ParentsMessage();
                                hasCoin = false;
                                isFirstCall = false;
                            }
                            else
                            {
                                ShowRandomMessage();
                                hasCoin = false;
                            }
                        }
                    }
                }
            }
    }
    private void checkLang()
    {
        GameObject mainController = GameObject.FindGameObjectWithTag("MainController");
        lang = mainController.GetComponent<MainControl>().Lang;
    }
    private void ParentsMessage()
    {
        speachObj.SetActive(true);
        checkLang();
        TextAsset text = Resources.Load<TextAsset>("Text/" + lang + "/ParentsMessage");
        Instantiate(historySheatPrefab, gameObject.transform.position, Quaternion.identity);
        textSpeach.text = text.text; 
        Invoke("closeSpeach", cpeachTime);
    }
    private void ShowRandomMessage()
    {
        int num = (int)Random.Range(1, textsCount);
        checkLang();
        TextAsset text = Resources.Load<TextAsset>("Text/" + lang + "/" + textPrefixName + num.ToString());
        speachObj.SetActive(true);
        textSpeach.text = text.text;
        Invoke("closeSpeach", cpeachTime);
    }
    private void closeSpeach()
    {
        speachObj.SetActive(false);
    }
}
