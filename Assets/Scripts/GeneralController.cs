using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GeneralController : MonoBehaviour
{
    private AsyncOperation asyncOperation;
    public float mode; // unique level
    private int howManyLevelsDone;
    private bool reloadThis;
    private bool reload;

    private float volume;
    public List<AudioSource> sounds;

    public float platformspeed;
    [HideInInspector] public float platformIndex = 1; // t ostop
    [HideInInspector] public float timerIndex = 1; // slow

    // environment
    [HideInInspector] public Platform lastPlatform;
    [HideInInspector] public Platform previousPlatform;
    [HideInInspector] public Animator playerAnimator;

    // buttons
    [HideInInspector] public GameObject settings;
    [HideInInspector] public GameObject loadingScreen;
    [HideInInspector] public GameObject playAgain;
    [HideInInspector] public GameObject winScreen;
    [HideInInspector] public Image loading;
    [HideInInspector] public bool pause;
    public Color32 enableButton;
    public Color32 normalButton;
    public Color32 shapeColor;
    public Color32 wrongColor;
    public Color32 winColor;
    [HideInInspector] public GameObject volumeOn;
    [HideInInspector] public GameObject volumeOff;

    // skills
    public int a1Cost;
    public int a2Cost;
    private float a1Timer;
    public float a1TimerMax;
    private float a2Timer;
    public float a2TimerMax;
    [HideInInspector] public TMP_Text a1Price;
    [HideInInspector] public TMP_Text a2Price;
    [HideInInspector] public Image a1Scale;
    [HideInInspector] public Image a2Scale;
    public bool a1active;
    public bool a2active;
    [HideInInspector] public SphereCollider catcher;

    [HideInInspector] public int howManyA1;
    [HideInInspector] public int howManyA2;
    [HideInInspector] public TMP_Text a1Count;
    [HideInInspector] public TMP_Text a2Count;

    // currency
    [HideInInspector] public TMP_Text currencyCount;
    private float currency;
    private float newCurrency;

    // points
    [HideInInspector] public TMP_Text pointCount;
    [HideInInspector] public TMP_Text maxPointCount;
    [HideInInspector] public TMP_Text newBestPoint;
    private int points;
    private int maxPoints; // record

    // main mechanic
    [HideInInspector] public Drawing drawing;
    private bool shaping;
    private float shapingTimer;
    public float shapingTimerMax;
    [HideInInspector] public TMP_Text shapingTimerText;
    [HideInInspector] public GameObject shapeScreen;
    [HideInInspector] public List<GameObject> shapes;
    private int shapeIndex = -1;
   [HideInInspector] public GameObject hitObject;
    [HideInInspector] public GameObject playerGo;
    [HideInInspector] public GameObject follower;
    [HideInInspector] public GameObject follower2;
    [HideInInspector] public List<Fading> shapesColor;
    [HideInInspector] public List<Image> shapesColored;
    [HideInInspector] public bool win;
    private bool lost;
    private string flyType;

    // levels
    public int levelCasualTarget;
    public int levelRareTarget;
    public int levelLegendTarget;
    private int yellowBugs;
    private int greenBugs;
    private int violetBugs;
    [HideInInspector] public GameObject levelTarget;
    [HideInInspector] public GameObject levelTarget1;
    [HideInInspector] public GameObject levelTarget2;
    [HideInInspector] public GameObject levelTarget3;
    [HideInInspector] public TMP_Text yellowCount;
    [HideInInspector] public TMP_Text greenCount;
    [HideInInspector] public TMP_Text violetCount;

    // tutorial
    [HideInInspector] public int catchFly;
    [HideInInspector] public int traceShape;
    [HideInInspector] public Animator tutorialAnimator;
    [HideInInspector] public GameObject tutorialPointer;

    public float chosenLevel; // real number of level

    public int levelMax;

    private void Start()
    {
        Time.timeScale = 1;
        asyncOperation = SceneManager.LoadSceneAsync("LoadingScene");
        asyncOperation.allowSceneActivation = false;
        maxPoints = PlayerPrefs.GetInt("maxPoints");
        mode = PlayerPrefs.GetFloat("mode");
        levelMax = PlayerPrefs.GetInt("levelMax");
        volume = PlayerPrefs.GetFloat("volume");
        newCurrency = PlayerPrefs.GetFloat("currency");
        chosenLevel = PlayerPrefs.GetFloat("chosenLevel");
        howManyLevelsDone = PlayerPrefs.GetInt("howManyLevelsDone");
        howManyA1 = PlayerPrefs.GetInt("howManyA1");
        howManyA2 = PlayerPrefs.GetInt("howManyA2");
        a1Count.text = "You have: " + howManyA1.ToString();
        a2Count.text = "You have: " + howManyA2.ToString();

        buttonCheck();

        pointCount.text = "POINTS:\n-";

        a1Price.text = a1Cost.ToString();
        a2Price.text = a2Cost.ToString();
        maxPointCount.text = "YOUR BEST SCORE: " + maxPoints.ToString();
        newBestPoint.text = "";

        sounds[0].Play();
        if (volume == 1)
        {
            Sound(true);
        }
        else
        {
            Sound(false);
        }

        pause = false;
        settings.SetActive(false);
        loadingScreen.SetActive(false);
        shapeScreen.SetActive(false);
        playAgain.SetActive(false);
        winScreen.SetActive(false);
        follower.SetActive(false);
        catcher.enabled = false;
        levelTarget.SetActive(false);
        levelTarget1.SetActive(false);
        levelTarget2.SetActive(false);
        levelTarget3.SetActive(false);
        tutorialPointer.SetActive(false);

        // levels
        if (mode == 0) // infinity
        {

        }
        else
        {
            levelTarget.SetActive(true);
            levelCasualTarget = PlayerPrefs.GetInt("levelCasualTarget");
            levelRareTarget = PlayerPrefs.GetInt("levelRareTarget");
            levelLegendTarget = PlayerPrefs.GetInt("levelLegendTarget");
            yellowCount.text = yellowBugs.ToString() + "/" + levelCasualTarget.ToString();
            greenCount.text = greenBugs.ToString() + "/" + levelRareTarget.ToString();
            violetCount.text = violetBugs.ToString() + "/" + levelLegendTarget.ToString();
            if (levelCasualTarget > 0)
            {
                levelTarget1.SetActive(true);
            }
            if (levelRareTarget > 0)
            {
                levelTarget2.SetActive(true);
            }
            if (levelLegendTarget > 0)
            {
                levelTarget3.SetActive(true);
                if(levelRareTarget <= 0)
                {
                    levelTarget3.transform.localPosition += new Vector3(0, 80, 0);
                }
            }
        }
        catchFly = PlayerPrefs.GetInt("catchFly");
        traceShape = PlayerPrefs.GetInt("traceShape");
        if(catchFly == 0)
        {
            catcher.enabled = true;
        }
    }

    private void checkInput()
    {
        if (!shaping)
        {
            if (Input.touchCount > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitObject != null)
                    {
                        Destroy(hitObject);
                    }
                    hitObject = hitInfo.collider.gameObject;
                    hitObject.transform.SetParent(playerGo.transform);
                    flyTapped(hitObject.tag);
                    Debug.Log("Cathed" + hitObject.tag);
                }
            }
            if (Application.isEditor)
            {
                if (Input.GetMouseButton(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitObject != null)
                        {
                            Destroy(hitObject); // destroys old
                        }
                        hitObject = hitInfo.collider.gameObject;
                        hitObject.transform.SetParent(playerGo.transform);
                        flyTapped(hitObject.tag);
                        Debug.Log("Cathed" + hitObject.tag);
                    }
                }
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    follower.transform.position = touch.position;
                    follower.SetActive(true);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    if (follower.activeSelf && !pause && !win && !lost)
                    {
                        Collider2D collider2D = Physics2D.OverlapPoint(touch.position);

                        if (collider2D != null)
                        {
                            string colliderTag = collider2D.tag;
                            Debug.Log("Touch detected on object with tag: " + colliderTag);
                            // colliderTag == "Shape"

                        }
                        else
                        {
                            lost = true;
                            foreach (Fading script in shapesColor)
                            {
                                script.targetColor = wrongColor;
                                script.enabled = true;
                                StartCoroutine(loosingScreen());
                            }
                            Debug.Log("No touch detected ");
                        }
                    }
                    else if (win && !lost)
                    {
                        win = false;

                        follower.SetActive(false);
                        follower2.transform.position = touch.position;
                        follower2.SetActive(true);
                        foreach (Fading script in shapesColor)
                        {
                            script.targetColor = winColor;
                            script.enabled = true;
                        }
                        StartCoroutine(Successing());
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    follower.SetActive(false);
                }

            }
            if (Application.isEditor)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    follower.transform.position = Input.mousePosition;
                       follower.SetActive(true);
                }
                else if (Input.GetMouseButton(0))
                {
                    if(follower.activeSelf && !pause && !win && !lost)
                    {
                        Collider2D collider2D = Physics2D.OverlapPoint(Input.mousePosition);

                        if (collider2D != null)
                        {
                            string colliderTag = collider2D.tag;
                            Debug.Log("Touch detected on object with tag: " + colliderTag);
                            // colliderTag == "Shape"

                        }
                        else
                        {
                            lost = true;
                            foreach (Fading script in shapesColor)
                            {
                                script.targetColor = wrongColor;
                                script.enabled = true;
                                StartCoroutine(loosingScreen());
                            }
                            Debug.Log("No touch detected ");
                        }
                    }
                    else if(win && !lost)
                    {
                        win = false;

                        follower.SetActive(false);
                        follower2.transform.position = Input.mousePosition;
                        follower2.SetActive(true);
                        foreach (Fading script in shapesColor)
                        {
                            script.targetColor = winColor;
                            script.enabled = true;
                        }
                        StartCoroutine(Successing());
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    follower.SetActive(false);
                }
            }
        }
    }
    IEnumerator loosingScreen()
    {
        yield return new WaitForSeconds(1f);
        lose();
        yield break;
    }

    IEnumerator Successing()
    {
        yield return new WaitForSeconds(1f);
        Success();
        yield break;
    }

    private void Update()
    {
        // input
        if (!winScreen.activeSelf)
        {
            checkInput();
        }
       

        // count
        if (currency != newCurrency)
        {
            currency = newCurrency;
            PlayerPrefs.SetFloat("currency", currency);
            PlayerPrefs.Save();
        }
        currencyCount.text = newCurrency.ToString("0");
        pointCount.text = "POINTS:\n" + points.ToString("0");
        if (points > maxPoints)
        {
            maxPointCount.text = "YOUR BEST SCORE: " + points.ToString();
        }


        // timer
        if (shapingTimer > 0 && shaping)
        {
            shapingTimer -= Time.deltaTime * timerIndex;
            shapingTimerText.text = shapingTimer.ToString("0") + "S";
        }
        else if (shaping)
        {
            hideShapes();
            // didnt catch
        }
        else if(!shaping && hitObject != null && traceShape != 0)
        {
            hitObject.transform.localScale = Vector3.Lerp(hitObject.transform.localScale, new Vector3(0, 0, 0), 2 * Time.deltaTime);
        }

        // A1
        if (a1active && a1Timer > 0)
        {
            a1Timer -= 1 * Time.deltaTime;
            a1Scale.fillAmount = a1Timer / a1TimerMax;
        }
        else if (a1active || (!a1active && a1Timer != 0))
        {
            a1off();
            a1Scale.fillAmount = 1;
            a1Timer = 0;
        }

        // A2
        if (a2active && a2Timer > 0)
        {
            a2Timer -= 1 * Time.deltaTime;
            a2Scale.fillAmount = a2Timer / a2TimerMax;
        }
        else if (a2active || (!a2active && a2Timer != 0))
        {
            a2off();
            a2Scale.fillAmount = 1;
            a2Timer = 0;
        }

        // music
        if (loadingScreen.activeSelf == true)
        {
            foreach (AudioSource audio in sounds)
            {
                audio.volume = 0;
            }

            if (loading.fillAmount < 0.9f)
            {
                loading.fillAmount = Mathf.Lerp(loading.fillAmount, 1, Time.deltaTime * 2);
            }
            else
            {
                if (!reload)
                {
                    reload = true;
                    if (reloadThis)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    else
                    {
                        asyncOperation.allowSceneActivation = true;
                    }
                }
            }
        }
        else
        {
            foreach (AudioSource audio in sounds)
            {
                audio.volume = volume;
            }
        }

        //// levels
        if (mode != 0)
        {
            if (levelCasualTarget <= yellowBugs && levelRareTarget <= greenBugs && levelLegendTarget <= violetBugs)
            {
                winScreen.SetActive(true);
                recordCheck();
                winCount();
            }
        }
    }

    public void flyTapped(string whatFly)
    {
        flyType = whatFly;
        if (!shaping && !a2active)
        {
            if (catchFly == 0 && traceShape == 0)
            {
                platformIndex = 0;
                catcher.enabled = false;
                catchFly = 1;
                PlayerPrefs.SetInt("catchFly", catchFly);
                PlayerPrefs.Save();

                tutorialPointer.transform.position = Camera.main.WorldToScreenPoint(hitObject.transform.position);
                tutorialPointer.SetActive(true);
                tutorialAnimator.Play("catch");

                // nex part of tutor by tap
            }
            else
            {
                if (catchFly != 0 && traceShape == 0)
                {
                    tutorialPointer.transform.localPosition = Vector3.zero;
                      tutorialPointer.SetActive(true);
                    tutorialAnimator.Play("trace");
                }

                win = false;
                shaping = true;
                shapingTimer = shapingTimerMax;
                shapeScreen.SetActive(true);
                foreach (GameObject child in shapes)
                {
                    child.SetActive(false);
                    foreach (Transform children in child.transform)
                    {
                        children.gameObject.SetActive(true);
                    }
                }
                foreach (Fading script in shapesColor)
                {
                    script.enabled = false;
                }
                foreach (Image img in shapesColored)
                {
                    img.color = shapeColor;
                }

                shapeIndex += 1;
                if (shapeIndex < shapes.Count)
                {
                    shapes[shapeIndex].SetActive(true);
                }
                else
                {
                    shapes[Random.Range(0, shapes.Count)].SetActive(true);
                }
            }
        }
        else if (!shaping && a2active)
        {
            follower2.transform.position = Camera.main.WorldToScreenPoint(hitObject.transform.position);
            follower2.SetActive(true);
            foreach (Fading script in shapesColor)
            {
                script.targetColor = winColor;
                script.enabled = true;
            }
            StartCoroutine(Successing());

        }
    }

    public void Success()
    {
        sounds[1].Play();
        hideShapes();
        if (flyType == "Bug")
        {
            points += 10;
            if (yellowBugs < levelCasualTarget)
            {
                yellowBugs += 1;
            }
            newCurrency += 5;
        }
        else if (flyType == "BugGreen")
        {
            points += 20;
            if (greenBugs < levelRareTarget)
            {
                greenBugs += 1;
            }
            newCurrency += 5;

        }
        else if (flyType == "BugViolet")
        {
            points += 30;
            if (violetBugs < levelLegendTarget)
            {
                violetBugs += 1;
            }
            newCurrency += 5;

        }
        yellowCount.text = yellowBugs.ToString() + "/" + levelCasualTarget.ToString();
        greenCount.text = greenBugs.ToString() + "/" + levelRareTarget.ToString();
        violetCount.text = violetBugs.ToString() + "/" + levelLegendTarget.ToString();
    }
    public void lose()
    {
        platformIndex = 0;
        hideShapes();
        playAgain.SetActive(true);
        recordCheck();
    }


    public void hideShapes()
    {
        if (catchFly != 0 && traceShape == 0)
        {
            platformIndex = 1;
            traceShape = 1;
            PlayerPrefs.SetInt("traceShape", traceShape);
            PlayerPrefs.Save();
            tutorialPointer.SetActive(false);
        }
        drawing.ClearCanvas();
        follower.SetActive(false);
        shapingTimer = 0;
        shaping = false;
      // platformIndex = 1f;
        shapeScreen.SetActive(false);
    }        

    public void recordCheck()
    {
        if (points > maxPoints)
        {
            newBestPoint.text = "NEW RECORD!\n" + points.ToString();
            PlayerPrefs.SetInt("maxPoints", points);
            PlayerPrefs.Save();
        }
    }

    public void NextLevel()
    {
        sounds[3].Play();
        if (chosenLevel <= howManyLevelsDone + 1)
        {
            chosenLevel += 1;
            PlayerPrefs.SetFloat("chosenLevel", chosenLevel);

            // cycled
            for (int j = 1; j <= 10; j++)
            {
                for (int i = j; i <= levelMax; i += 10)
                {
                    if (chosenLevel == i)
                    {
                        mode = j;
                    }
                    if(mode == 0)
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

            PlayerPrefs.SetFloat("mode", mode);

            PlayerPrefs.SetInt("levelCasualTarget", levelCasualTarget);
            PlayerPrefs.SetInt("levelRareTarget", levelRareTarget);
            PlayerPrefs.SetInt("levelLegendTarget", levelLegendTarget);

            PlayerPrefs.Save();
            reloadScene();
        }
    }

    public void ExitMenu()
    {

        shaping = false;
        sounds[3].Play();
        asyncOperation.allowSceneActivation = true;
        loading.fillAmount = 0;
        loadingScreen.SetActive(true);
        loading.enabled = false;
    }

    public void Settings()
    {
        sounds[3].Play();
        if (!pause)
        {
            Time.timeScale = 0;
            pause = true;
            settings.SetActive(true);
        }
        else
        {
            if (drawing.enabled && shapeScreen.activeSelf)
            {
                drawing.ClearCanvas();
            }
                follower.SetActive(false);
                Time.timeScale = 1;
                pause = false;
                settings.SetActive(false);
            
        }
    }

    public void reloadScene()
    {
        Time.timeScale = 1;
        sounds[3].Play();
        loading.fillAmount = 0;
        reloadThis = true;
        loadingScreen.SetActive(true);
    }
    public void a1on()
    {
        sounds[3].Play();
        if (!a1active && howManyA1 > 0)
        {
            playerAnimator.speed = 0.07f;
               howManyA1 -= 1;
            platformIndex = 0.01f;
            timerIndex = 0f;
            a1Scale.fillAmount = 1;
            a1active = true;
            a1Timer = a1TimerMax;
            buttonCheck();
        }
    }
    public void a1off()
    {
        playerAnimator.speed = 0.15f;
        platformIndex = 1;
           timerIndex = 1;
        a1active = false;
        buttonCheck();
    }
    public void a2on()
    {
        sounds[3].Play();
        if (howManyA2 > 0 && !a2active)
        {
            catcher.enabled = true;
            howManyA2 -= 1;
            a2active = true;
            a2Scale.fillAmount = 1;
            a2Timer = a2TimerMax;
            buttonCheck();
        }
    }
    public void a2off()
    {
        catcher.enabled = false;
        a2active = false;
        buttonCheck();
    }

    private void buttonCheck()
    {
        a1Count.text = "You have: " + howManyA1.ToString();
        a2Count.text = "You have: " + howManyA2.ToString();
        if (howManyA1 <= 0 && !a1active)
        {
            a1Scale.color = enableButton;
        }
        else
        {
            a1Scale.color = normalButton;
        }
        if (howManyA2 <= 0 && !a2active)
        {
            a2Scale.color = enableButton;
        }
        else
        {
            a2Scale.color = normalButton;
        }
        PlayerPrefs.SetInt("howManyA1", howManyA1);
        PlayerPrefs.SetInt("howManyA2", howManyA2);
        PlayerPrefs.Save();
    }

    private void winCount()
    {
        if(chosenLevel > howManyLevelsDone)
        {
            PlayerPrefs.SetInt("howManyLevelsDone", (int)chosenLevel);
            PlayerPrefs.Save();
        }       
    }


    public void Sound(bool volumeBool)
    {
        if(volumeBool)
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

        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public void buyA1()
    {
        sounds[3].Play();
        if (newCurrency >= a1Cost)
        {
            newCurrency -= a1Cost;
            howManyA1 += 1;
            buttonCheck();
        }
    }
    public void buyA2()
    {
        sounds[3].Play();
        if (newCurrency >= a2Cost)
        {
            newCurrency -= a2Cost;
            howManyA2 += 1;
            buttonCheck();
        }
    }
}
