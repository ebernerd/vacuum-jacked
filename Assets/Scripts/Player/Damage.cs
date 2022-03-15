using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Damage script to be placed on any part of the body that can do damage
 */
public class Damage : MonoBehaviour
{ 
	public int baseDamage = 10;
	public float cooldown = 0.5f; // To ensure double-hits don't occur

	private float lastAttackTime = 0;

	//	To be set in Start()
	private Health playerHealth;

	public AudioClip hitSound;

	private void Start() {
		playerHealth = GetComponent<Health>();
	}

	void PlayHitSound() {
		AudioSource src = GetComponentInParent<AudioSource>();
		src.clip = hitSound;
		src.Play();
	}

	void OnDealDamage(Collider2D col) {
		PlayerController enemyController = col.gameObject.GetComponentInParent<PlayerController>();
		//	Ensure the receiving end is a player
		if (enemyController == null) {
			return;
		}

		int calculatedDamage = baseDamage;

		Health enemyHealth = enemyController.GetHealth();
		enemyHealth.DealDamage(calculatedDamage);
		PlayHitSound();

		//	If the player does more than the flag handler's limit, throw the flag
		if (calculatedDamage >= FlagHandler.damageNeededToDrop) {
			enemyController.GetFlagHandler().ThrowFlag();
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("Enter col");
		float currentTime = Time.time;
		//bool hasSurpassedCooldown = currentTime > lastAttackTime + cooldown;
		//if (hasSurpassedCooldown)
		//{
			lastAttackTime = currentTime;
			OnDealDamage(col);
		//}
	}
}
