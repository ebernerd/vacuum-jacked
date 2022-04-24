using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

public class ComboHandler : MonoBehaviour {

	public enum ComboSequenceItem {
		PrimaryPunch,
		SecondaryPunch
	}
	private bool isRecordingCombo = false;
	private List<ComboSequenceItem> comboSequence = new List<ComboSequenceItem>();
	private List<List<ComboSequenceItem>> comboSequenceLookup = new List<List<ComboSequenceItem>>();
	private Dictionary<List<ComboSequenceItem>, string> comboAttackTypeMap = new Dictionary<List<ComboSequenceItem>, string>();

	private float comboRecordStartTime = 0f;
	public static float maxComboTime = 1.5f;
	public Transform indicatorGraphic;

	public bool IsRecording() {
		float comboRecordTime = Time.time - comboRecordStartTime;
		return isRecordingCombo && comboRecordTime < maxComboTime;
    }

	// Use this for initialization
	void Start() {

		List<ComboSequenceItem> doublePunch = new List<ComboSequenceItem>();
		doublePunch.Add(ComboSequenceItem.PrimaryPunch);
		doublePunch.Add(ComboSequenceItem.SecondaryPunch);
		comboSequenceLookup.Add(doublePunch);
		
		List<ComboSequenceItem> kick = new List<ComboSequenceItem>();
		kick.Add(ComboSequenceItem.SecondaryPunch);
		kick.Add(ComboSequenceItem.SecondaryPunch);
		kick.Add(ComboSequenceItem.PrimaryPunch);
		comboSequenceLookup.Add(kick);

		List<ComboSequenceItem> super = new List<ComboSequenceItem>();
		super.Add(ComboSequenceItem.PrimaryPunch);
		super.Add(ComboSequenceItem.PrimaryPunch);
		super.Add(ComboSequenceItem.SecondaryPunch);
		super.Add(ComboSequenceItem.SecondaryPunch);
		super.Add(ComboSequenceItem.PrimaryPunch);
		super.Add(ComboSequenceItem.SecondaryPunch);
		comboSequenceLookup.Add(super);

		//	Last minute cheat code to add life to health
		List<ComboSequenceItem> healthCheat = new List<ComboSequenceItem> {
			ComboSequenceItem.PrimaryPunch,
			ComboSequenceItem.PrimaryPunch,
			ComboSequenceItem.SecondaryPunch,
			ComboSequenceItem.PrimaryPunch,
			ComboSequenceItem.PrimaryPunch,
			ComboSequenceItem.PrimaryPunch,
			ComboSequenceItem.SecondaryPunch,
		};
		comboSequenceLookup.Add(healthCheat);

		comboAttackTypeMap.Add(doublePunch, "Trigger Double Punch");
		comboAttackTypeMap.Add(kick, "Trigger Kick");
		comboAttackTypeMap.Add(super, "Trigger Super");
		comboAttackTypeMap.Add(healthCheat, "special_addhealth");
	}

	void AddToComboSequence(ComboSequenceItem item) {
		comboSequence.Add(item);
	}

	void OnPrimaryPunch() {
		if (IsRecording()) {
			AddToComboSequence(ComboSequenceItem.PrimaryPunch);
        }
    }

	void OnSecondaryPunch() {
		if (IsRecording()) {
			AddToComboSequence(ComboSequenceItem.SecondaryPunch);
        }
    }

	void StartRecordingCombo() {
		//	Start recording
		isRecordingCombo = true;
		comboSequence = new List<ComboSequenceItem>();
		indicatorGraphic.localScale = new Vector3(1f, 1f, 1f);
		comboRecordStartTime = Time.time;
	}

	private void Update() {
		if (IsRecording()) {
			float percComplete = (Time.time - comboRecordStartTime) / maxComboTime;
			float maxIndicatorScale = 10f;
			float indicatorScale = (maxIndicatorScale * percComplete) + (1 - percComplete);
			indicatorGraphic.localScale = new Vector3(indicatorScale, indicatorScale, indicatorScale);
		} else {
			indicatorGraphic.localScale = new Vector3(0f, 0f, 0f);
		}
	}

	void StopRecordingCombo() {
		isRecordingCombo = false;
		if (Time.time - comboRecordStartTime < maxComboTime) {
			foreach (List<ComboSequenceItem> cs in comboSequenceLookup) {
				if (comboSequence.SequenceEqual(cs)) {
					//	Initiate this combo!
					string attackAnimationName;
					bool found = comboAttackTypeMap.TryGetValue(cs, out attackAnimationName);
					if (found) {
						if (attackAnimationName.StartsWith("special_")) {
							switch (attackAnimationName) {
								default: case "special_addhealth": {
									Debug.Log("Resetting health!");
									GetComponent<Health>().ResetHealth();
									break;
								}
							}
						} else {
							GetComponent<Animator>().SetTrigger(attackAnimationName);
						}
					}
				}
			}
		}
	}

	void OnComboToggle() {
		//	DONT use the IsRecording method here
		if (isRecordingCombo) {
			StopRecordingCombo();
		} else {
			StartRecordingCombo();
		}
    }
}
