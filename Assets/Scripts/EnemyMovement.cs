using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float damage;
    public NavMeshAgent agent;
    public GameObject target;
    public Animator animator;
    public BoxCollider playerCheck;
    public GameController controller;

    public AudioSource stepSound;
    public AudioSource attackSound;

    [SerializeField]private bool isAttacking;
    [SerializeField]private bool isDied = false;
    // Start is called before the first frame update
    void Start()
    {
        stepSound.Play();
        controller = FindObjectOfType<GameController>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        playerCheck= GetComponent<BoxCollider>();
        isAttacking = false;
        agent.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.isPause) { return; }
        if (isDied) { return; }
        agent.transform.LookAt(new Vector3(target.transform.position.x,transform.position.y, target.transform.position.z));
        if (!isAttacking)
        {
            
            agent.SetDestination(target.transform.position);
        }
        

        
    }

    public void Attack()
    {
        attackSound.Play();
        if (isAttacking)
        {
            controller.Hurt(damage);
        }
    }

    public void DieAnimation()
    {
        animator.SetTrigger("Die");
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag.Equals("Player"))
        {
            
            isAttacking = true;
            stepSound.mute = true;
            animator.SetBool("isAttacking", true);
            Debug.Log("Player Detected");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isAttacking = false;
            stepSound.mute = false;
            animator.SetBool("isAttacking", false);
            Debug.Log("Player ran");
        }
    }


}
