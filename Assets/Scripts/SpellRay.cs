using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellRay : MonoBehaviour
{
	[SerializeField] GameObject spellProjectile; //Generic spell
	[SerializeField] GameObject spellImpact; //Generic spell
	[SerializeField] ParticleSystem spellImpactFX; //Generic spell
	[SerializeField] Transform spellCastPoint; 
	[SerializeField] Transform spellCastTarget; 
	[SerializeField] LineRenderer rayLineRenderer; 
	[SerializeField] LayerMask spellHitRayLayers; 
	Vector3 spellHitPoint; 
	Vector3 spellHitNormal; 
	bool spellFiring;
	bool spellHitPointValid;

	public void ShootSpell(bool shooting)
	{
		//Generic spellcast
		//Instantiate(spellProjectile, spellCastPoint.position, Quaternion.LookRotation(spellCastTarget.position - spellCastPoint.position));
		spellFiring = shooting;
	}
	void FixedUpdate()
	{
		if(!spellFiring) //If we are not firing the spell this frame, the hit point position is not likely to be valid for drawing effects to
		{
			spellHitPointValid = false;
		}
		else //If we are firing the spell, do the raycast
		{
			ShootSpellRay();
		}
	}

	void ShootSpellRay()
	{
		RaycastHit hit;
		
		if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, spellHitRayLayers))
		{
			//print($"Found an object({hit.transform.gameObject}) - distance: {hit.distance}");
			spellHitPoint = hit.point;
			spellHitNormal = hit.normal;

			Debug.DrawLine(transform.position, spellHitPoint, Color.magenta, Time.fixedDeltaTime);
		}
		else //If raycast misses
		{
			//Debug.Log($"Spell missed everything");
			spellHitPoint = (transform.position + transform.forward * 50f); //Set hit point to arbitrary faraway forward position
			spellHitNormal = (-transform.forward);

			Debug.DrawLine(transform.position, spellHitPoint, Color.white, Time.fixedDeltaTime);
		}

		spellHitPointValid = true; //We have produced an up-to-date hit point either way
	}
	void Update()
	{
		if(spellFiring && spellHitPointValid)
		{
			rayLineRenderer.enabled = true;
			Vector3[] points = new Vector3[2];
			points[0] = spellCastPoint.position;
			points[1] = spellHitPoint;
			rayLineRenderer.SetPositions(points);
			spellImpact.transform.position = spellHitPoint;
			spellImpact.transform.rotation = Quaternion.LookRotation(spellHitNormal, Vector3.up);
			if(!spellImpactFX.isPlaying)
				spellImpactFX.Play();
		}
		else
		{
			rayLineRenderer.enabled = false;
			spellImpactFX.Stop();
		}
	}
}
