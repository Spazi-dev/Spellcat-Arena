using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnim : MonoBehaviour
{
	[SerializeField] ParticleSystem magicArcs;
	public float magicArcsEmission;
	[SerializeField] ParticleSystem magicRandomArcs;
	public float magicRandomArcsEmission;
	[SerializeField] ParticleSystem magicPlasma;
	public float magicPlasmaSize;
	[SerializeField] ParticleSystem magicOrbPlasma;
	public float magicOrbPlasmaSize;
	[SerializeField] Light orbLight;
	[SerializeField] GameObject camera2;
	public float orbLightPower;
	public float magicGlowPower;
	//[SerializeField] Material orbMat;
	public float orbPower;
	//[SerializeField] Material retinaMat;
	public float retinaPower;
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		var em1 = magicArcs.emission;
		em1.rateOverTime = magicArcsEmission;
		var em2 = magicRandomArcs.emission;
		em2.rateOverTime = magicRandomArcsEmission;
		var mainPlasma = magicPlasma.main;
		mainPlasma.startSize = magicPlasmaSize;
		var mainOrb = magicOrbPlasma.main;
		mainOrb.startSize = magicOrbPlasmaSize;

		orbLight.intensity = orbLightPower;
		
		Shader.SetGlobalFloat("_MagicIntensity", orbPower);
		Shader.SetGlobalFloat("_RetinaIntensity", retinaPower);
	}

	public void SwapCameraEvent()
	{
		camera2.SetActive(true);
	}
}
