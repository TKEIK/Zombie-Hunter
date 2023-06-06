using UnityEngine;
using Unity.Collections;
using System.Collections;

public class Gun : MonoBehaviour
{
    [Header("Gun Info")]
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireCD = 0.1f;

    [Header("Ammo")]
    public int currentAmmo;
    public int currentAmmoBackUp;
    public int maxAmmo;
    public int maxAmmoBackUp;
    public float reloadTime;
    public bool isReloading = false;
    public Animator animator;
    public PlayerMovementScript playerMovementScript;

    [Header("UI")]
    public TMPro.TextMeshProUGUI currentAmmoText;
    public TMPro.TextMeshProUGUI currentBackUpText;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject bloodEffect;
    private float nextTimeToFire = 0f;

    public AudioSource gunSound;
    public AudioSource noBulletSound;
    public AudioSource reloadingSound;
    private float noBulletCD = .5f;
    private float time;

    void Start()
    {
        currentAmmo = maxAmmo;
        currentAmmoBackUp = maxAmmoBackUp;
        currentAmmoText.text = currentAmmo.ToString();
        currentBackUpText.text = currentAmmoBackUp.ToString();
        time = 0f;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameController.isPause) { return; }

        if (isReloading) { return; }

        if (Input.GetButtonDown("Reloading") && currentAmmo != maxAmmo && currentAmmoBackUp >0)
        {
            StartCoroutine(Reload());
        }
        //reload back up
        if(currentAmmoBackUp < maxAmmoBackUp && playerMovementScript.atStation && Input.GetButtonDown("BackUpReload"))
        {
            currentAmmoBackUp = maxAmmoBackUp;
            currentBackUpText.text = currentAmmoBackUp.ToString();

        }

        if (currentAmmo > 0)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + fireCD;
                Shoot();
            }
        }
        else // no bullet
        {
            time += Time.deltaTime;
            if (time > noBulletCD && Input.GetButton("Fire1"))
            {
                noBulletSound.Play();
                time = 0f;
            }
        }

        
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);
        Debug.Log("Reloading");

        yield return new WaitForSeconds(reloadTime * .1f);

        reloadingSound.Play();

        yield return new WaitForSeconds(reloadTime * .8f);
        int ammoToReload = maxAmmo - currentAmmo;
        if (ammoToReload <= currentAmmoBackUp)
        {
            currentAmmo = maxAmmo;
            currentAmmoBackUp -= ammoToReload;
        }
        else
        {
            currentAmmo += currentAmmoBackUp;
            currentAmmoBackUp= 0;

        }
        currentBackUpText.text = currentAmmoBackUp.ToString();
        currentAmmoText.text = currentAmmo.ToString();



        yield return new WaitForSeconds(reloadTime * .1f);
        isReloading = false;
        animator.SetBool("Reloading",false);




    }

    void Shoot()
    {
        currentAmmo--;
        currentAmmoText.text = currentAmmo.ToString();
        muzzleFlash.Play();
        gunSound.Play();
        float currentDamage = damage;
        bool isEnemy = false;
        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            switch (hit.transform.tag)
            {
                case "head":
                    currentDamage = damage * 1.5f;
                    break;
                case "leg":
                    currentDamage = damage / 1.5f; 
                    break;
                default: 
                    break; 
            }

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
            target.TakeDamage(currentDamage);

            }
            else
            {
                Target parent = hit.transform.root.GetComponent<Target>(); 
                if(parent != null && parent.tag.Equals("Enemy"))
                {
                    isEnemy= true;
                    parent.TakeDamage(currentDamage);
                }

            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal* impactForce);
            }
            if (isEnemy)
            {
                GameObject impactGO = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f);
            }
            else
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f);
            }

        }
    }
}
