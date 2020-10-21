using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class LifePlayer : MonoBehaviour
{
    public Image barLife_Blood;
    public float health = 100;
    [SerializeField] private Color _goodColor;
    [SerializeField] private Color _middleColor;
    [SerializeField] private Color _badColor;


    // Start is called before the first frame update
    void Start()
    {
        SetColor();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("w"))
        {
            HealthPlayer(10);
        }
    }

    public void HealthPlayer(float val)
    {
        health -= val;
        health = Mathf.Clamp(health, 0, 100);
        barLife_Blood.fillAmount = health / 100;
        float totalVal = barLife_Blood.fillAmount = health / 100;
        SetColor(totalVal);
    }

    void SetColor(float value = 1)
    {
        if(value >= 0.5f)
        {
            barLife_Blood.color = _goodColor;
        }
        else if (value >= 0.25f && value < 0.5f)
        {
            barLife_Blood.color = _middleColor;
        }
        else
        {
            barLife_Blood.color = _badColor;
        }
    }

}
