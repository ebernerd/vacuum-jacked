using UnityEngine;

public class Instructions : MonoBehaviour
{
	public GameObject panel;

	void Start()
	{
		panel.SetActive(false);
	}

	public void OpenInstructions()
	{
		panel.SetActive(true);
	}

	public void CloseInstructions()
	{
		panel.SetActive(false);
	}
}
