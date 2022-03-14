using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

	public static int maxLives = 3;
	public static int maxHealth = 100;

	public int lives = 3;
	public int health;

	public TextMeshProUGUI playerLifeText;
	public TextMeshProUGUI playerHealthText;
	public Slider healthSlider;

	private PlayerController playerController;


	void Start()
	{
		//	Set the health to maxHp on start
		health = maxHealth;
		playerController = GetComponent<PlayerController>();
		UpdateUI();
	}

	void UpdateText()
	{
		playerLifeText.SetText(lives.ToString());
		playerHealthText.SetText(health.ToString());
	}
		

	public void OnDeath() {
		lives -= 1;
		if (lives <= 0) {
			//	Trigger game end
		}
		health = maxHealth;
		playerController.Respawn();
	}

	public void UpdateUI() {
		float newVal = ((float)health / (float)maxHealth) * 100f;
		healthSlider.value = newVal;
	}

	public void DealDamage(int damage)
	{
		health -= damage;
		if (health < 0) {
			OnDeath();
		}
		UpdateUI();
	}

	public void Heal(int hp) {
		health = Mathf.Min(health + hp, maxHealth);
		UpdateUI();
	}

}



