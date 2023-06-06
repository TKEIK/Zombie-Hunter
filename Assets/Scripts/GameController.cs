using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static bool isPause = false;
    public GameObject enemy;
    public int enemyToSpawn;
    public float spawnCD;

    [Header("Enemy")]
    public float enemySpeed;
    public float enemyHealth;
    public float enemyDamage;

    [Header("Player")]
    public float playerHealth;
    private float playerMaxHealth;
    public Image playerHealthImage;
    public GameObject player;

    public UnityEvent setting;
    public TextMeshProUGUI resultText;
    public GameObject resultPanel;

    [Header("Zombie")]
    public TextMeshProUGUI maxCount;
    public TextMeshProUGUI currentCount;

    private float time = 0;
    private Vector3[] positionToSpawn = {new Vector3(33.5f,0f,-33.5f),
                                         new Vector3(33.5f,0f,33.5f),
                                         new Vector3(-33.5f,0f,-33.5f),
                                         new Vector3(-33.5f,0f,33.5f)};

    public static GameController instance;
    private bool isWin = false;

    private void Awake()
    {
        
            instance = this;

    }

    private void OnEnable()
    {
        playerMaxHealth = playerHealth;
        SetHealthImage();
        enemy.GetComponent<NavMeshAgent>().speed= enemySpeed;
        enemy.GetComponent<Target>().health = enemyHealth;
        enemy.GetComponent<EnemyMovement>().damage = enemyDamage;
        player.SetActive(true);
        maxCount.SetText(enemyToSpawn.ToString());
        currentCount.SetText(enemyToSpawn.ToString());

    }

    private void Update()
    {
        if (isPause) { return; }

        if (Input.GetButtonDown("Setting"))
        {
            SetPause(true);
            setting.Invoke();
            
        }

        time+= Time.deltaTime;
        if (enemyToSpawn > 0) 
        {
            if(time >= spawnCD)
            {
                SpawnEnemy();


                time = 0f;
                
            }
        
        }

        if(enemyToSpawn == 0 && GameObject.FindWithTag("Enemy") == null && !isWin)
        {
            isWin= true;
            Win();
        }
    }

    public void SetPause(bool pause)
    {
        isPause= pause;
        if (pause)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            AudioListener.pause = true;
        }
        else
        {
            Time.timeScale= 1f;
            Cursor.lockState = CursorLockMode.Locked;
            AudioListener.pause = false;

        }
    }

    void Win()
    {
        resultText.SetText("You Win");
        SetPause(true);
        Time.timeScale = 1f;
        resultPanel.SetActive(true);
    }

    void SpawnEnemy()
    {

        GameObject zombie = Instantiate(enemy, positionToSpawn[Random.Range(0,3)], Quaternion.Euler(0f,0f,0f));
        enemyToSpawn--;
        currentCount.SetText(enemyToSpawn.ToString());
    }



    public void Hurt(float damage)
    {
        playerHealth -= damage;
        SetHealthImage();

        if (playerHealth <=0)
        {
            Lose();
        }

    }

    void Lose()
    {
        resultText.SetText("You Lose");
        SetPause(true);
        Time.timeScale = 1f;
        resultPanel.SetActive(true);
    }
    
    public void Home()
    {
        SetPause(false);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }

    void SetHealthImage()
    {
        playerHealthImage.fillAmount = playerHealth / playerMaxHealth;
    }
}
