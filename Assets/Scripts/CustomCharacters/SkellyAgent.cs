using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using EasyCharacterMovement;

public class SkellyAgent : AgentCharacter
{
	[SerializeField] Transform goalTarget;
    [SerializeField] bool Pursuing;
    /* protected override void Start()
    {
		base.Start();
        Debug.Log($"Skelly override active!");
    } */
	
	protected override void Update()
    {
		base.Update();
        if(Pursuing)
            MoveToLocation(goalTarget.position);
    }
	
}
