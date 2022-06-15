using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject hudContainer, gameOverPanel;

    public int countdownTime;
    public Text countdownText;

    public Text timeCounter;
    TimeSpan timePlaying;

    private float startTime, elapsedTime;

    public bool gamePlaying { get; private set; }

    public AudioSource audioController;
    public AudioClip timerSound;
    public AudioClip goSound;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timeCounter.text = "00:00:00";
        gamePlaying = false;

        StartCoroutine(CountdownToStart());
    }

    private void BeginGame()
    {
        gamePlaying = true;

        startTime = Time.time;
    }

    private void Update()
    {
        if (gamePlaying)
        {
            elapsedTime = Time.time - startTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);

            string timePlayingStr = timePlaying.ToString("mm':'ss':'ff");
            timeCounter.text = timePlayingStr;
        }
    }

    public void EndGame()
    {
        gamePlaying = false;
        Invoke("ShowGameOverScreen", .4f);

        //Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    }

    private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        hudContainer.SetActive(false);
        string timePlayingStr = timePlaying.ToString("mm':'ss':'ff");
        gameOverPanel.transform.Find("FinalTimeText").GetComponent<Text>().text = timePlayingStr;
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            // Play timer sound at each number
            AudioController.instance.Play(timerSound);

            countdownText.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;

        }

        //Play go sound
        AudioController.instance.Play(goSound);

        countdownText.text = "RUN!!";

        GameController.instance.BeginGame();

        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
    }

    
}
