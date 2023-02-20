using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldHeadAnim : MonoBehaviour
{
	[SerializeField] ParticleSystemForceField forceField;
	public float ForceFieldGravity;
	float _currentGravity;

	// Update is called once per frame
	void Update()
	{
		if(ForceFieldGravity != _currentGravity)
		{
			forceField.gravity = ForceFieldGravity;
			_currentGravity = ForceFieldGravity;
		}
	}
}
