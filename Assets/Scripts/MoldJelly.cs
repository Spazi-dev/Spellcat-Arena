using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldJelly : CreatureBase
{
	// MoldSlime is a rigidbody-based creature that hops towards the player naively and attempts to bump into them 
	[SerializeField] GameObject corpsePrefab;
	Rigidbody rb;
	Animator anim;
	Vector3 jumpDirection;
	float jumpTimer = 2f;
	public override void Start()
	{
		base.Start();
		rb = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
		jumpDirection = transform.forward;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		jumpTimer = Mathf.MoveTowards(jumpTimer, 0, Time.deltaTime);
		if(jumpTimer <= 0f)
		{
			jumpDirection = Random.insideUnitSphere;
			jumpDirection.y = 0f; //flatten
			Vector3 jumpForce = (jumpDirection.normalized + Vector3.up) *4f; //upwards
			rb.AddForce(jumpForce, ForceMode.Impulse);
			jumpTimer = 2f;
		}
		
		rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, Quaternion.LookRotation(jumpDirection.normalized), 2f));
	}
	public override void ApplyForce(Vector3 _forceDirection)
	{
		rb.AddForce(_forceDirection, ForceMode.VelocityChange);
		//Debug.Log($"MoldJelly got force of {_forceDirection}");
	}
	public override void Die()
	{
		base.Die();
		Instantiate(corpsePrefab, transform.position, transform.rotation);
		this.gameObject.SetActive(false);
	}
}
