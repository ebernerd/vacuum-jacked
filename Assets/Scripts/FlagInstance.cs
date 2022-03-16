using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlagInstance : MonoBehaviour
{
	public static float countdownLength = 10f;
    public GameObject planter;

	private bool isPlanted;

	private float spawnedAt;
	private float plantTimer;

	private void Start() {
		//	Mark this flag as planted if the planter GO is not null
		//	Said GO will be set by FlagHandler.PlantFlag()
		isPlanted = planter != null;
		plantTimer = countdownLength;
	}

	private void Update() {
		if (!isPlanted) {
			return;
		}
		plantTimer -= Time.deltaTime;

		FlagCountdownUI ui = GameObject.Find("Flag UI").GetComponent<FlagCountdownUI>();
		string uiTextVal = Mathf.CeilToInt(plantTimer).ToString();
		ui.SetTimerText(uiTextVal);

		if (plantTimer <= 0) {
			GameManager.GetGameManager().EndGame();
		}
	}

	public void StartUI() {
		FlagCountdownUI ui = GameObject.Find("Flag UI").GetComponent<FlagCountdownUI>();
		ui.SetVisibility(true);
		plantTimer = countdownLength;
		ui.SetTimerText(plantTimer.ToString());
	}

	public void EndUI() {
		FlagCountdownUI ui = GameObject.Find("Flag UI").GetComponent<FlagCountdownUI>();
		ui.SetVisibility(false);
	}
}
