using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int maxLifePoints = 3;
	public int maxHealthPoints = 10;

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
			healthPoints = maxHealthPoints;
			UpdateText();
		}

		if (lifePoints <= 0)
		{
			Debug.Log(this.gameObject + " is dead :(");
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



