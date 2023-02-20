using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldBeast : CreatureBase
{
	// MoldSlime is a rigidbody-based creature that hops towards the player naively and attempts to bump into them 
	[SerializeField] GameObject corpsePrefab;
	[SerializeField] Transform wallRadar;
	//[SerializeField] Collider bodyCollider;
	[SerializeField] float wallRadarStep;
	Rigidbody _rb;
	//Animator anim;
	Vector3 _forceVector;
	Vector3 _smoothForceVector;
	float _wallRadarDelta;
	float _bodyColliderRadius;
	public override void Start()
	{
		base.Start();
		_rb = GetComponent<Rigidbody>();
		_bodyColliderRadius = GetComponent<CapsuleCollider>().radius;
		//anim = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		AvoidWalls();
	}

	void AvoidWalls()
	{
		wallRadar.rotation = Quaternion.Euler(0, _wallRadarDelta, 0); //Stepped direction
		_wallRadarDelta += wallRadarStep;

		RaycastHit hit;
		Vector3 _fromPoint = wallRadar.position + wallRadar.forward*1.003f*_bodyColliderRadius;

        if (Physics.Raycast(_fromPoint, wallRadar.forward, out hit, 1f))
		{
			Vector3 toHitPoint = (hit.point - _fromPoint);
			_forceVector = -(toHitPoint.normalized-toHitPoint);
			_smoothForceVector = Vector3.MoveTowards(_smoothForceVector, _forceVector, Time.fixedDeltaTime*12f);
			Debug.DrawLine(_fromPoint, hit.point, Color.red, Time.fixedDeltaTime);
		}
		else
		{
			Debug.DrawRay(wallRadar.position + wallRadar.forward*1.1f*_bodyColliderRadius, wallRadar.forward, Color.green, Time.fixedDeltaTime);
			_forceVector = Vector3.zero;
			_smoothForceVector = Vector3.MoveTowards(_smoothForceVector, _forceVector, Time.fixedDeltaTime/2f);
		}

		Debug.DrawRay(wallRadar.position + Vector3.up, _smoothForceVector, Color.cyan, Time.fixedDeltaTime);

		_rb.AddForce(_smoothForceVector,ForceMode.VelocityChange);
	}
	public override void ApplyForce(Vector3 _forceDirection)
	{
		_rb.AddForce(_forceDirection, ForceMode.VelocityChange);
		//Debug.Log($"MoldJelly got force of {_forceDirection}");
	}
	public override void Die()
	{
		base.Die();
		Instantiate(corpsePrefab, transform.position, transform.rotation);
		this.gameObject.SetActive(false);
	}
}
