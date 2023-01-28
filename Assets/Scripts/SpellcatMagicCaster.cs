using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellcatMagicCaster : MonoBehaviour
{
	[SerializeField] GameObject spellProjectile; //Generic spell
	[SerializeField] Transform spellCastPoint; 
	[SerializeField] Transform spellCastTarget; 
	/* void Start()
	{
		
	} */

	
	public void ShootSpell()
	{
		//Generic spellcast
		Instantiate(spellProjectile, spellCastPoint.position, Quaternion.LookRotation(spellCastTarget.position - spellCastPoint.position));
	}
}
