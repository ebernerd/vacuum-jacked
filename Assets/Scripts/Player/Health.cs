using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int lifePoints = 3;
	public int healthPoints = 10;

	public TextMeshProUGUI playerLifeText;
	public TextMeshProUGUI playerHealthText;

	void Start()
	{
		UpdateText();
	}

	void Update()
	{
		if (healthPoints <= 0)
		{
			lifePoints -= 1;
			healthPoints = 10;
			UpdateText();
		}
	}

	void UpdateText()
	{
		playerLifeText.SetText(lifePoints.ToString());
		playerHealthText.SetText(healthPoints.ToString());
	}

	public void SetDamage(int d)
	{
		healthPoints -= d;
		UpdateText();
	}

}



