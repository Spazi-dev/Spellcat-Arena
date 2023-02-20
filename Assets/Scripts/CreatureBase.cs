using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBase : MonoBehaviour, IDamageable<float, Vector3, float>
{
	// CreatureBase is meant to handle creature life cycle from spawning to destruction as well as damage, healing and statuses. It could also handle target detection.
	[SerializeField] float MaxHealth;
	[SerializeField] float CurrentHealth;
	public bool Dead{get; private set;}
	[SerializeField] bool _inPain;
	[SerializeField] float _inPainTimer;

	public virtual void Start()
	{
		CurrentHealth = MaxHealth;
		//Debug.Log($"{CurrentHealth}");
	}

	void Update()
	{
		DamageIndicatorUpdate();
	}

	public void DamageBurst(float _damageAmount, Vector3 _forceDirection) //IDamageable interface
	{
		CurrentHealth -= _damageAmount;
		ApplyForce(_forceDirection);

		//Debug.Log($"{CurrentHealth}");
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

	public virtual void ApplyForce(Vector3 _forceDirection)
	{
		//Defined by override
	}
	public virtual void Die()
	{
		Dead = true;
	}

	void DamageIndicatorUpdate()
	{
		if(_inPain)
		{
			_inPainTimer = 1f;
		}
		else if(_inPainTimer >= 0f)
		{
			_inPainTimer = Mathf.MoveTowards(_inPainTimer, 0f, Time.deltaTime *4f);
		}
	}
	private void OnDrawGizmos()
	{
		//Gizmos.DrawIcon(transform.position + transform.up, "impact.png", false);
		//Gizmos.color = new Color(1, 1, 0, 0.75F);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position + transform.up *2f, _inPainTimer *.5f);
	}

}
