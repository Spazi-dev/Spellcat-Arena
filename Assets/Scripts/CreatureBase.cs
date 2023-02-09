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
	[SerializeField] bool _inPain;
	[SerializeField] float _inPainTimer;

	void Update()
	{
		if(_inPain)
		{
			_inPainTimer = 1f;
		}
		else if(_inPainTimer >= 0f)
		{
			_inPainTimer -= Time.deltaTime *4f;
		}
	}

	public void DamageBurst(float _damageAmount, Vector3 _forceDirection) //IDamageable interface
	{
		CurrentHealth -= _damageAmount;
		Debug.Log($"{CurrentHealth}");
		_inPainTimer = 1f;
		
		if (CurrentHealth <= 0f)
		{
			Die();
		}
	}
	public void DamageOverTime(float _damageAmount, float _damageDuration) //IDamageable interface
	{
		//Placeholder
	}
	/* public void DamageConstant(float _damageAmount) //IDamageable interface
	{
		//Placeholder
	} */
	public void Die()
	{
		Dead = true;
	}

	private void OnDrawGizmos()
	{
		//Gizmos.DrawIcon(transform.position + transform.up, "impact.png", false);
		//Gizmos.color = new Color(1, 1, 0, 0.75F);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position + transform.up *2f, _inPainTimer *.5f);
	}

}
