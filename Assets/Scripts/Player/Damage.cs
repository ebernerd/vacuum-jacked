using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
	private string enemyTag = "Player02Body";

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log(col.gameObject.name);
		//col.gameObject.GetComponent<Health>().SetDamage();
		col.gameObject.GetComponent<Health>().SetDamage(1);
	}
}
