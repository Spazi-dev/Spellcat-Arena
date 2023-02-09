using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldJelly : CreatureBase
{
	// MoldSlime is a rigidbody-based creature that hops towards the player naively and attempts to bump into them 

	Rigidbody rb;
	Animator anim;
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	/* void Update()
	{
		if(Dead)
		{

		}
	} */
}
