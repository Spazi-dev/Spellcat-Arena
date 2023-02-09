using UnityEngine;
using System.Collections;


//This is a generic interface where T is a placeholder
//for a data type that will be provided by the 
//implementing class.
public enum ElementEnum
{
	Kinetic, Shock, Fire
}

public interface IDamageable<Amount, Force, Duration> // add Element
{
	void DamageBurst(Amount _damageAmount, Force _forceDirection);
	void DamageOverTime(Amount _damageAmount, Duration _damageDuration);
	//void DamageConstant(Amount _damageAmount);
}
