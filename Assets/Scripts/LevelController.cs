using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelController : MonoBehaviour
{
    private AsyncOperation asyncOperation;
    private float initialLaunch;
    private float mode; // 0 - infinity, 1+ - levels
    public int howManyLevelsDone;
    private float volume;

    public Color32 notenableButton;
   public Color32 enableButton;
    [HideInInspector] public List<Image> buttons; // levels

    [HideInInspector] public GameObject startButtons;
    [HideInInspector] public GameObject levelButtons;
    [HideInInspector] public GameObject loadingScreen;
    [HideInInspector] public GameObject fakeloadingScreen;
    [HideInInspector] public GameObject loadingFake;
    [HideInInspector] public GameObject settings;
    [HideInInspector] public Image loading;
    [HideInInspector] public Image fakeloading;

    // music
    [HideInInspector] public AudioSource ambient;
    [HideInInspector] public AudioSource tapSound;
    [HideInInspector] public GameObject volumeOn;
    [HideInInspector] public GameObject volumeOff;

    //stats
    [HideInInspector] public int howManyA1;
    [HideInInspector] public int howManyA2;
    [HideInInspector] public TMP_Text a1Count;
    [HideInInspector] public TMP_Text a2Count;
    public int a1Cost;
    public int a2Cost;


    private int maxPoints; // record
    [HideInInspector] public TMP_Text maxPointCount;

    // currency
    [HideInInspector] public TMP_Text currencyCount;
    private float currency;
    private float newCurrency;
    [HideInInspector] public TMP_Text a1Price;
    [HideInInspector] public TMP_Text a2Price;

    // levels
    private int levelCasualTarget;
    private int levelRareTarget;
    private int levelLegendTarget;

    public float chosenLevel;
    [HideInInspector] public List<ButtonScript> buttonscripts;
    [HideInInspector] public GameObject leftarrow;
    [HideInInspector] public GameObject righarrow;
    public int levelMax;

    private void Awake()
    {
        Input.multiTouchEnabled = false;
    }
    void Start()
    {
        Time.timeScale = 1;
        initialLaunch = PlayerPrefs.GetFloat("initialLaunch");
        if (initialLaunch == 0)
        {
            PlayerPrefs.SetFloat("initialLaunch", 1);
            volume = 1;
            PlayerPrefs.SetFloat("volume", volume);
            PlayerPrefs.Save();
        }
        else
        {
            volume = PlayerPrefs.GetFloat("volume");
        }

        ambient.Play();
        if (volume == 1)
        {
            Sound(true);
        }
        else
        {
            Sound(false);
        }

        maxPoints = PlayerPrefs.GetInt("maxPoints");
        newCurrency = PlayerPrefs.GetFloat("currency");
        howManyLevelsDone = PlayerPrefs.GetInt("howManyLevelsDone");
        howManyA1 = PlayerPrefs.GetInt("howManyA1");
        howManyA2 = PlayerPrefs.GetInt("howManyA2");
        a1Count.text = "You have: " + howManyA1.ToString();
        a2Count.text = "You have: " + howManyA2.ToString();
        a1Price.text = a1Cost.ToString();
        a2Price.text = a2Cost.ToString();
        maxPointCount.text = "YOUR BEST SCORE: " + maxPoints.ToString();

        buttonCheck();

        howManyLevelsDone = PlayerPrefs.GetInt("howManyLevelsDone");

        for (int i = 0; i <= howManyLevelsDone; i++)
        {
            if (i < buttons.Count)
            {
                buttons[i].color = enableButton;
            }
        }

        settings.SetActive(false);
        loadingScreen.SetActive(false);
        fakeloadingScreen.SetActive(true);
        loadingFake.SetActive(false);
        startButtons.SetActive(true);
        levelButtons.SetActive(false);

        asyncOperation = SceneManager.LoadSceneAsync("SampleScene");
        asyncOperation.allowSceneActivation = false;


        leftarrow.SetActive(false);
    }

    private void Update()
    {
        // count
        if (currency != newCurrency)
        {
            currency = newCurrency;
            PlayerPrefs.SetFloat("currency", currency);
            PlayerPrefs.Save();
        }
        currencyCount.text = newCurrency.ToString("0");

        if (loadingScreen.activeSelf == true)
        {
            ambient.volume -= 0.1f;
            tapSound.volume -= 0.1f;
            if (loading.fillAmount < 0.9f)
            {
                loading.fillAmount = Mathf.Lerp(loading.fillAmount, 1, Time.deltaTime * 2);
            }
            else
            {
                asyncOperation.allowSceneActivation = true;
            }
        }

        if (fakeloadingScreen.activeSelf == true)
        {
            if (fakeloading.fillAmount < 0.9f)
            {
                fakeloading.fillAmount = Mathf.Lerp(fakeloading.fillAmount, 1, Time.deltaTime * 2);
            }
            else
            {
                loadingFake.SetActive(true);
                fakeloadingScreen.SetActive(false);
            }
        }
    }
   public void StartGame(float mode)
    {
        // cycled
        playSound(tapSound);
        if (mode <= howManyLevelsDone + 1)
        {
            PlayerPrefs.SetInt("levelMax", levelMax);
            
            PlayerPrefs.SetFloat("chosenLevel", mode);

            // cycled
            for (int j = 1; j <= 10; j++)
            {
                for (int i = j; i <= levelMax; i += 10) // if 11, j = 1
                {
                    if (mode == i)
                    {
                        mode = j;
                    }
                    if (mode == 0)
                    {
                        mode = 1;
                    }
                }
            }

            // unique levels
            levelCasualTarget = 0;
            levelRareTarget = 0;
            levelLegendTarget = 0;
            if (mode == 1)
            {
                levelCasualTarget = 1;
            }
            else if (mode == 2)
            {
                levelCasualTarget = 1;
                levelRareTarget = 1;
            }
            else if (mode == 3)
            {
                levelCasualTarget = 2;
                levelRareTarget = 2;
            }
            else if (mode == 4)
            {
                levelCasualTarget = 2;
                levelLegendTarget = 1;
            }
            else if (mode == 5)
            {
                levelCasualTarget = 3;
                levelRareTarget = 2;
                levelLegendTarget = 1;
            }
           else if (mode == 6)
            {
                levelCasualTarget = 5;
            }
            else if (mode == 7)
            {
                levelCasualTarget = 3;
                levelRareTarget = 3;
            }
            else if (mode == 8)
            {
                levelCasualTarget = 5;
                levelRareTarget = 5;
            }
            else if (mode == 9)
            {
                levelCasualTarget = 5;
                levelLegendTarget = 5;
            }
            else if (mode == 10)
            {
                levelCasualTarget = 10;
                levelRareTarget = 5;
                levelLegendTarget = 3;
            }

            PlayerPrefs.SetInt("levelCasualTarget", levelCasualTarget);
            PlayerPrefs.SetInt("levelRareTarget", levelRareTarget);
            PlayerPrefs.SetInt("levelLegendTarget", levelLegendTarget);

            loading.fillAmount = 0;
            loadingScreen.SetActive(true);
            startButtons.SetActive(false);
            levelButtons.SetActive(false);
            PlayerPrefs.SetFloat("mode", mode);
            PlayerPrefs.Save();
        }
    }

   public void ShowLevels()
    {
        playSound(tapSound);
        loadingScreen.SetActive(false);
        startButtons.SetActive(false);
        levelButtons.SetActive(true);

    }
    public void HideLevels()
    {
        playSound(tapSound);
        settings.SetActive(false);
        loadingScreen.SetActive(false);
        startButtons.SetActive(true);
        levelButtons.SetActive(false);
    }
   public void Settings()
    {
        playSound(tapSound);
        if (!settings.activeSelf)
        {
            Time.timeScale = 0;
            HideLevels();
            startButtons.SetActive(false);
            settings.SetActive(true);
            loadingFake.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            HideLevels();
            settings.SetActive(false);
        }
    }
    public void Sound(bool volumeBool)
    {
        if (volumeBool)
        {
            volumeOn.SetActive(true);
            volumeOff.SetActive(false);
            volume = 1;

        }
        else
        {
            volume = 0;
            volumeOn.SetActive(false);
            volumeOff.SetActive(true);
        }
        ambient.volume = volume;
        tapSound.volume = volume;

        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }
    public void playSound(AudioSource sound)
    {
        sound.Play();
    }

    public void buyA1()
    {
        playSound(tapSound);
        if (newCurrency >= a1Cost)
        {
            newCurrency -= a1Cost;
            howManyA1 += 1;
            buttonCheck();
        }
    }
    public void buyA2()
    {
        playSound(tapSound);
        if (newCurrency >= a2Cost)
        {
            newCurrency -= a2Cost;
            howManyA2 += 1;
            buttonCheck();
        }
    }

    private void buttonCheck()
    {
        a1Count.text = "You have: " + howManyA1.ToString();
        a2Count.text = "You have: " + howManyA2.ToString();

        PlayerPrefs.SetInt("howManyA1", howManyA1);
        PlayerPrefs.SetInt("howManyA2", howManyA2);
        PlayerPrefs.Save();
    }

    public void leftArrow()
    {
        foreach (ButtonScript button in buttonscripts)
        {
            // changes text
            button.thisLevelNumber -= 9;
            button.thisLevelNumberText.text = button.thisLevelNumber.ToString("0");
            // changes level
            button.gameObject.SetActive(true);
            if(button.thisLevelNumber <= howManyLevelsDone + 1)
            {
                button.thisImage.color = enableButton;
            }
            else
            {
                button.thisImage.color = notenableButton;
            }
        }
        if(buttonscripts[0].thisLevelNumber <= 1)
        {
            leftarrow.SetActive(false);
        }
        righarrow.SetActive(true);

    }

    public void rightArrow()
    {
        foreach (ButtonScript button in buttonscripts)
        {
            // changes text
            button.thisLevelNumber += 9;
            button.thisLevelNumberText.text = button.thisLevelNumber.ToString("0");
            // changes level
            if (button.thisLevelNumber <= howManyLevelsDone + 1)
            {
                button.thisImage.color = enableButton;
            }
            else
            {
                button.thisImage.color = notenableButton;
            }
            if (button.thisLevelNumber > levelMax)
            {
                button.gameObject.SetActive(false);
                righarrow.SetActive(false);
            }
        }
        leftarrow.SetActive(true);

    }
}
