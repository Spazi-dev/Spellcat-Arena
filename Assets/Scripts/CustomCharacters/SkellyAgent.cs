using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using EasyCharacterMovement;

public class SkellyAgent : AgentCharacter
{
	[SerializeField] Transform goalTarget;
    /* protected override void Start()
    {
		base.Start();
        Debug.Log($"Skelly override active!");
    } */
	
	protected override void Update()
    {
		base.Update();
        MoveToLocation(goalTarget.position);
    }
	
}
