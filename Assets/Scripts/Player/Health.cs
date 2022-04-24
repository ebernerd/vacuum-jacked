using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public static int maxLives = 3;
	public static int maxHealth = 100;

	public int lives = 3;
	public int health;



	private PlayerController playerController;
	public HealthUI healthUI;



	void Start() {
		//	Set the health to maxHp on start
		health = maxHealth;
		playerController = GetComponent<PlayerController>();
		UpdateUI();
	}


	public void OnDeath() {
		lives -= 1;
		if (lives <= 0) {
			GameManager.GetGameManager().EndGame();
		}
		health = maxHealth;
		playerController.Respawn();
	}

	public void UpdateUI() {
		float newVal = ((float)health / (float)maxHealth) * 100f;
		healthUI.SetHealth(newVal);
		healthUI.SetLives(lives);
	}

	public void TakeDamage(int damage) {
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

	public void ResetHealth() {
		health = maxHealth;
		lives = maxLives;
		UpdateUI();
	}
}



