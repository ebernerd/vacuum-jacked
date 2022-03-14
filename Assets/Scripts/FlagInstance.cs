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
	private FlagCountdownUI ui;

	private void Start() {
		//	Mark this flag as planted if the planter GO is not null
		//	Said GO will be set by FlagHandler.PlantFlag()
		isPlanted = planter != null;
		Debug.Log("New flag! Is planted? " + isPlanted);
		plantTimer = countdownLength;

		ui = GameObject.Find("Flag UI").GetComponent<FlagCountdownUI>();
		ui.SetVisibility(isPlanted);
	}

	private void Update() {
		if (!isPlanted) {
			return;
		}
		plantTimer -= Time.deltaTime;

		string uiTextVal = Mathf.CeilToInt(plantTimer).ToString();
		ui.SetTimerText(uiTextVal);

		if (plantTimer <= 0) {
			// End game
		}
	}
}
