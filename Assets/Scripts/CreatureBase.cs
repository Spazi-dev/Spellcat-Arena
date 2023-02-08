using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBase : MonoBehaviour, IDamageable<float, Vector3, float>
{
	// CreatureBase is meant to handle creature life cycle from spawning to destruction as well as damage, healing and statuses. It could also handle target detection.
	[SerializeField]
	public float MaxHealth{get; private set;}
	public float CurrentHealth{get; private set;}
	public bool Dead{get; private set;}
	//bool _underConstantDamage

	void Update()
	{
		//process constant damage tick
	}

	public void DamageBurst(float _damageAmount, Vector3 _forceDirection) //IDamageable interface
	{
		CurrentHealth -= _damageAmount;
		
		if (CurrentHealth <= 0f)
		{
			Die();
		}
	}
	public void DamageOverTime(float _damageAmount, float _damageDuration) //IDamageable interface
	{
		//Placeholder
	}
	public void DamageConstant(float _damageAmount) //IDamageable interface
	{
		//Placeholder
	}
	public void Die()
	{
		Dead = true;
	}

}
