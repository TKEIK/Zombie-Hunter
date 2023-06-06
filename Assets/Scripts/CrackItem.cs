using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackItem : MonoBehaviour
{

    public bool needToDestroy =false;
    public float secondToDestroy = 2f;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (needToDestroy)
        {
            Destroy(gameObject,secondToDestroy);
        }
    }
}
