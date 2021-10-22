using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        Detected = true; 
        player = GameObject.FindObjectOfType<PlayerBehaviour>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Detected == true)
        {
            Action();
        }
    }

    protected override void Action()
    {
        Debug.Log("Turret Action");
        base.Action();
    }
}
