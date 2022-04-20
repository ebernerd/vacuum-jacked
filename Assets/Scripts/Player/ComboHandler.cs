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
	private float comboStartTime = 0;
	private List<List<ComboSequenceItem>> comboSequenceLookup = new List<List<ComboSequenceItem>>();

	public bool IsRecording() {
		return isRecordingCombo;
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


	}

	public void ResetCombo() {
		comboStartTime = Time.time;
		comboSequence = new List<ComboSequenceItem>();
	}

	void AddToComboSequence(ComboSequenceItem item) {
		comboSequence.Add(item);
	}

	void OnPrimaryPunch() {
		if (isRecordingCombo) {
			AddToComboSequence(ComboSequenceItem.PrimaryPunch);
        }
    }

	void OnSecondaryPunch() {
		if (isRecordingCombo) {
			AddToComboSequence(ComboSequenceItem.SecondaryPunch);
        }
    }

	void OnComboToggle() {
		if (isRecordingCombo) {
			//Debug.Log("Combo being recorded.");
			//	Stop recording
			foreach (List<ComboSequenceItem> cs in comboSequenceLookup) {
				if (comboSequence.SequenceEqual(cs)) {
					//	Initiate this combo!
					Debug.Log("Combo was entered");
				}
			}
			isRecordingCombo = false;
		} else {
			//	Start recording
			isRecordingCombo = true;
			ResetCombo();
		}
    }
}
