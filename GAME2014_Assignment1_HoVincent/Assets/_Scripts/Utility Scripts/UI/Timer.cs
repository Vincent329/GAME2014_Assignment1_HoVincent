using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float timeElapsed; 

    // Getter for elapsed time
    public float GetElapsedTime
    {
        get => timeElapsed;
    }

    private TextMeshProUGUI tmp;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        timeElapsed = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckTime();
    }

    private void CheckTime()
    {
        timeElapsed += Time.deltaTime;

        int minutes = Mathf.RoundToInt(timeElapsed / 60); // using Unity's round to int functionality
        int seconds = Mathf.RoundToInt(timeElapsed % 60); // https://www.youtube.com/watch?v=HmHPJL-OcQE
        tmp.text = ": " + minutes.ToString() + ":" + seconds.ToString("00"); // string format functionality found in microsoft documentation
        
    }
}
