using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] Text txt;
    float setTime = 180;
    int min;
    int sec;

    private void Update()
    {
        setTime -= Time.deltaTime;

        if(setTime >= 60)
        {
            min = (int)setTime / 60;
            sec = (int)setTime % 60;
            txt.text = $"{min} m {sec} s";
        }
        else if(setTime <= 60)
        {
            sec = (int)setTime;
            txt.text = $"{sec} s";
        }
        else if(setTime <= 0)
        {
            txt.text = $"0";
        }
    }
}
