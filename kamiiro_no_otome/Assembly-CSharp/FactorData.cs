using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FactorData : SerializedScriptableObject
{
	public enum FactorType
	{
		attackUp = 0,
		magicAttackUp = 10,
		accuracyUp = 20,
		criticalUp = 30,
		poison = 40,
		paralyze = 50,
		stagger = 60,
		death = 70,
		hpUp = 500,
		mpUp = 600,
		skillPower = 700,
		defenseUp = 200,
		magicDefenseUp = 210,
		evasionUp = 220,
		criticalResistUp = 230,
		parry = 240,
		vampire = 250,
		abnormalResistUp = 260,
		mpSaving = 270
	}

	public string factorName;

	public Sprite factorSprite;

	public FactorType factorType;

	public int factorID;

	public int factorUnlockLv;

	public float probability;

	public int factorPowerLimit;

	public int continuityTurn;

	public bool isAddPercentText;

	public List<int> powerList;
}
