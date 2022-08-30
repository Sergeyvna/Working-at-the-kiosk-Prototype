using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float hp;
    public string[] reasonForFight;
    public string[] enemyResponse;
    private AudioSource damageSFX;

    private void Start() 
    {
        damageSFX = GetComponent<AudioSource>();
    }
    

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        damageSFX.Play();
    }

    public bool CheckHealth()
    {
        if(hp <= 0)
            return true;
        else
            return false;
    }
}
