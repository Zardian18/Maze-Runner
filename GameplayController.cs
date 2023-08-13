using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameplayController instance;
    [SerializeField]
    Text coin;
    [SerializeField]
    Text health;
    [SerializeField]
    Text gameOverText;
    int coins;
    [SerializeField]
    Text victoryText;
    public bool isPlayerAlive=true;
    bool gameFinish;

    private void Awake()
    {
        MakeInstance();
        gameOverText.gameObject.SetActive(false);
        victoryText.gameObject.SetActive(false);
    }
    void Start()
    {
        gameFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerAlive)
        {
            gameOverText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(1);
            }

        }
        if (gameFinish)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisplayHealth(int h)
    {
        health.text = "Health: " + h;
    }

    public void VicotryDisplay()
    {
        victoryText.gameObject.SetActive(true);
        gameFinish = true;
    }
}
