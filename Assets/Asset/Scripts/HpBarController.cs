using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HpBarController : MonoBehaviour
{
    /// <summary>
    /// File Name: HpBarController.cs
    /// Author: Philip Lee
    /// Last Modified by: Philip Lee
    /// Date Last Modified: Nov. 29, 2019
    /// Reference: Tom Tsiliopoulos
    /// Description: Hp Bar controller
    /// Revision History:
    /// </summary>
    /// 
    private float health = 1.0f;
    private float damage = 0.0f;
    public float damageStep = 0.01f;

    public Transform hpBarFront;
    public Transform hpBarDmg;

    private float hpBarLerp;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (damage <= Mathf.Epsilon)
        {
            StopCoroutine(TakeDamage());
            if (Mathf.Approximately(health, 0.0f))
            {
                health = 0.0f;
            }
        }

        hpBarLerp =
            Mathf.Lerp(hpBarDmg.transform.localScale.x, hpBarFront.localScale.x, Time.deltaTime * 2);

        hpBarDmg.localScale = new Vector3(hpBarLerp, 1.0f, 1.0f);
        //hpBarFront.localScale = new Vector3(health, 1.0f, 1.0f);
    }

    public void SetDamage(float dmg)
    {
        if (health > 0.0f)
        {
            damage = dmg * 0.01f;
            StartCoroutine(TakeDamage());
        }

    }
    public void addHealth(float add)
    {
        if(0<health + add && health + add < 1.0f)
        {
            health += add;
        }
        else if(health + add >= 1.0f)
        {
            health = 1.0f;
        }
        else if(health + add <= 0.0f)
        {
            health = 0;
        }
        hpBarFront.localScale = new Vector3(health, 1.0f, 1.0f);
    }

    //Coroutine
    private IEnumerator TakeDamage()
    {
        for (; damage > 0.0f; damage -= damageStep)
        {
            health -= damageStep;
            if (health < 0.0f)
            {
                health = 0f;
            }
            hpBarFront.localScale = new Vector3(health, 1.0f, 1.0f);
            Debug.Log("Damaged health: " + health);
            yield return null;
        }

    }
}
