using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI lives;

    public void SetHealth(float value) {
        Debug.Log($"Setting {slider.name} to {value}");
        slider.value = value;
	}

    public void SetLives(int value) {
        lives.SetText($"LIVES: {value}");
	}
}
