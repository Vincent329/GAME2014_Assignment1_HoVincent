using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    private bool detected;
    // Start is called before the first frame update
    void Start()
    {
        detected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Every subclass is going to have its own iteration of the function action
    /// </summary>
    protected abstract void Action();    
}
