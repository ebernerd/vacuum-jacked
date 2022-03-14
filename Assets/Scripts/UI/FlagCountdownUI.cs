using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlagCountdownUI : MonoBehaviour
{
    public GameObject container;
    public TextMeshProUGUI text;

    public void SetTimerText(string str) {
        text.SetText(str);
	}

    public void Show() {
        container.SetActive(true);
	}

    public void Hide() {
        container.SetActive(false);
	}

    public void SetVisibility(bool vis) {
        container.SetActive(vis);
	}
}
