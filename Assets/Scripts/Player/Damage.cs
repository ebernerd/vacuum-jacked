using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Damage script to be placed on any part of the body that can do damage
 */
public class Damage : MonoBehaviour
{
	public Health playerHealth;

	public float baseDamage = 10f;
	public float cooldown = 0.5f; // To ensure double-hits don't occur

	private float lastAttackTime = 0;

	void OnDealDamage(Collider2D col) {
		//	Ensure the receiving end is a player
		Health enemyHealth = col.gameObject.GetComponent<Health>();

		if (enemyHealth != null) {
			enemyHealth.DealDamage(Mathf.FloorToInt(baseDamage));
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		float currentTime = Time.time;
		bool hasSurpassedCooldown = currentTime > lastAttackTime + cooldown;
		if (hasSurpassedCooldown)
		{
			lastAttackTime = currentTime;
			OnDealDamage(col);
		}
	}
}
