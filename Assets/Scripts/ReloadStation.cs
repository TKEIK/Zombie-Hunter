using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadStation : MonoBehaviour
{
    public GameObject panel;
    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.GetComponent<PlayerMovementScript>().SetAtStation(true);
            panel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.GetComponent<PlayerMovementScript>().SetAtStation(false);
            panel.SetActive(false);
        }
    }


}
