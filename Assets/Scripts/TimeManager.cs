using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [Range(0.1f,30f)]
    [SerializeField] float timeScale = 1f;
    [SerializeField] Slider slider;
    [SerializeField] Text text;
    NumberFormatInfo precision;
    void Start()
    {
        Time.timeScale = timeScale;
        precision = new NumberFormatInfo();
        precision.NumberDecimalDigits = 2;
    }

    void Update()
    {
        //Time.timeScale = timeScale;
    }

    public void ChangeTimeScale()
    {
        //Debug.Log("lul");
        Time.timeScale = slider.value;
        text.text = Time.timeScale.ToString("N", precision);
    }
}
