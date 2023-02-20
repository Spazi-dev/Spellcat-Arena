using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellRay : MonoBehaviour
{
	[SerializeField] GameObject spellProjectile; 
	[SerializeField] GameObject spellImpact;
	[SerializeField] ParticleSystem spellImpactFX; 
	[SerializeField] ParticleSystem spellDamageFX; 
	[SerializeField] Transform spellCastPoint; 
	[SerializeField] Transform spellCastTarget; 
	[SerializeField] LineRenderer rayLineRenderer; 
	[SerializeField] LayerMask spellHitRayLayers; 
	[SerializeField] float spellDamageCooldown; 
	Vector3 spellHitPoint; 
	Vector3 spellHitNormal; 
	GameObject spellHitGameobject; 
	Transform mainCamTransform; 
	public float spellDamageCooldownTimer;
	bool spellFiring;
	bool spellHitPointValid;
	void Start()
	{
		mainCamTransform = Camera.main.transform;
	}
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
		
		if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, spellHitRayLayers)) //found hittable target
		{
			//print($"Found an object({hit.transform.gameObject}) - distance: {hit.distance}");
			spellHitPoint = hit.point;
			spellHitNormal = hit.normal;
			spellHitGameobject = hit.collider.gameObject;

			Debug.DrawLine(transform.position, spellHitPoint, Color.magenta, Time.fixedDeltaTime);
		}
		else //If raycast misses
		{
			//Debug.Log($"Spell missed everything");
			spellHitPoint = (transform.position + transform.forward * 50f); //Set hit point to arbitrary faraway forward position
			spellHitNormal = (-transform.forward); //impact particles fire backwards as if it hit a wall head-on

			Debug.DrawLine(transform.position, spellHitPoint, Color.white, Time.fixedDeltaTime);
		}

		spellHitPointValid = true; //We have produced an up-to-date hit point either way
	}
	void Update()
	{
		if(spellDamageCooldownTimer > 0f)
		{
			spellDamageCooldownTimer = Mathf.MoveTowards(spellDamageCooldownTimer, 0f, Time.deltaTime);
		}
		if(spellFiring && spellHitPointValid) //Draw effects, do also damage for now
		{
			if(spellDamageCooldownTimer <= 0f) //Do damage only when cooldown ready
			{
				DamageTarget(spellHitGameobject);
				spellDamageCooldownTimer = spellDamageCooldown; //Reset cooldown
			}

			SpellFXActive(true);
		}
		else
		{
			SpellFXActive(false);
		}
	}

	void SpellFXActive(bool active)
	{
		if(active)
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
	void DamageTarget(GameObject targetToDamage)
    {
        if (targetToDamage.TryGetComponent(out IDamageable<float, Vector3, float> damagedTarget))
        {
			Vector3 vectorToSelf = (spellHitPoint - transform.position);
            damagedTarget.DamageBurst(1f, transform.up + (vectorToSelf.normalized));
			Instantiate(spellDamageFX, spellHitPoint, Quaternion.LookRotation(-(spellHitPoint - mainCamTransform.position), Vector3.up));
            //damagedTarget.DamageBurst(1f, Vector3.up * 5f);
        }
    }
}
