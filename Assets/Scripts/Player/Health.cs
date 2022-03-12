using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int lifePoints = 3;

	public int maxHealth = 100;
	public int health;

	public TextMeshProUGUI playerLifeText;
	public TextMeshProUGUI playerHealthText;

	void Start()
	{
		//	Set the health to maxHp on start
		health = maxHealth;
		//UpdateText();
	}

	void UpdateText()
	{
		playerLifeText.SetText(lifePoints.ToString());
		playerHealthText.SetText(health.ToString());
	}
		

	public void OnDeath() {
		lifePoints -= 1;
		if (lifePoints == 0) {
			//	Trigger game end
		}
		health = maxHealth;
	}

	public void DealDamage(int damage)
	{
		Debug.Log(gameObject.name + " taking damage: " + damage);
		health -= damage;
		if (health < 0) {
			OnDeath();
		}
		//UpdateText();
	}

	public void Heal(int hp) {
		health = Mathf.Min(health + hp, maxHealth);
	}

}



