using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExcitementBar : MonoBehaviour
{
    [Range(0.1f, 10.0f)]
    [SerializeField] private float sliderGainRate;
    // Start is called before the first frame update
    [SerializeField] private Slider exciteSlider;

    private Score scoreObject;

    void Start()
    {
        exciteSlider = GetComponent<Slider>();
        scoreObject = GameObject.FindObjectOfType<Score>();

        exciteSlider.value = 0;
        exciteSlider.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (exciteSlider.value < exciteSlider.maxValue)
        {
            MonitorBar();
        } else
        {
            scoreObject.ScoreMultiplier += 0.25f;
            ResetBar();
        }
    }

    void MonitorBar()
    {
        exciteSlider.value += Time.deltaTime * sliderGainRate;
    }
    
    void ResetBar()
    {
        exciteSlider.value = 0;
    }
}
