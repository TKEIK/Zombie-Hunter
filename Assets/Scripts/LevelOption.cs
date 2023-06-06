using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelOption : MonoBehaviour
{
    public GameController gameController;
    

    public void SetEnemySpeed(float speed)
    {
        gameController.enemySpeed = speed;
    }

    public void SetEnemyHealth(float health)
    {
        gameController.enemyHealth = health;
    }

    public void SetEnemyDamage(float damage)
    {
        gameController.enemyDamage = damage;
    }

    public void SetEnemyToSpawn(int enemyCount)
    {
        gameController.enemyToSpawn = enemyCount;
    }

    public void SpawnCD(float cd)
    {
        gameController.spawnCD = cd;
    }

    public void PlayerHealth(float playerHealth)
    {
        gameController.playerHealth = playerHealth;
    }

    public void SetController()
    {
        StartCoroutine(ActiveController());
    }

    public IEnumerator ActiveController()
    {
        yield return new WaitForSeconds(.1f);
        gameController.gameObject.SetActive(true);
    }

    
}
