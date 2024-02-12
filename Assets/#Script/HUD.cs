using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health}
    public InfoType type;

    TextMeshProUGUI myText;
    Slider expSlider;

    private void Awake()
    {
        expSlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        InfoTypeList();
    }

    void InfoTypeList()
    {
        switch(type)
        {
            case InfoType.Exp:
            float currentExp = GameManager.instance.exp;
            float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
            expSlider.value = currentExp / maxExp;
            break;
            case InfoType.Level:
            break;
            case InfoType.Kill:
            break;
            case InfoType.Time:
            break;
            case InfoType.Health:
            break;
        }
    }
    
}
