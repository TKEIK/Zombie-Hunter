using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float health = 50f;
    private float maxhealth;
    public bool showHealth =false;

    public Image healthImage;
    public GameObject canvas;
    public Camera playerCam;

    private bool isDie = false;

    private void OnEnable()
    {
        playerCam = FindObjectOfType<Camera>();
        maxhealth = health;
        if (showHealth)
            SetHealthImage();
    }

    private void Update()
    {
        if (GameController.isPause) { return; }

        if (showHealth)
            canvas.transform.LookAt(playerCam.transform);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (showHealth) { SetHealthImage(); }
        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (showHealth) { canvas.SetActive(false); }
        if (GetComponent<Destructible>() != null)
        {
            GetComponent<Destructible>().Crack();
        }
        else
        {
            if (!isDie)
            {
                Destroy(gameObject, 1.5f);
                GetComponent<EnemyMovement>().DieAnimation();
                isDie= true;
            }
        

        }
    }

    void SetHealthImage()
    {
        healthImage.fillAmount = health / maxhealth;
    }
}
