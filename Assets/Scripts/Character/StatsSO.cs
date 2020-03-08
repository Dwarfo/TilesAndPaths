using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Stats", menuName ="Character/Stats", order =1) ]
public class StatsSO : ScriptableObject
{
    public int experience;
    public int level;
    public float maxHealth;
    public float currentHealth;
    public float maxMana;
    public float currentMana;
    public float currentWealth;

    //equipped items will go here as well


}
