using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WarriorAttacks : MonoBehaviour
{
    [Range(3,5)]
    public int maxAttackCount = 3;
    public float attackReset = 2f;

    private float reset;
    private float attackRate = 1.0f;
    private float nextAttack;
    private int attackCount = 0;
    private List<GameObject> damageGroup;
    private Vector3 _attackPoint;
    private int count;

    private void Update ()
    {

        if (Time.time > reset)
        {
            attackCount = 0;
        }
        
		if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            attackCount++;
            reset = Time.time + attackReset;

            damageGroup = GameObject.FindGameObjectsWithTag("Enemy").ToList();
            _attackPoint = GetComponentInChildren<Transform>().Find("AttackPoint").transform.position;
            count = 0;
            foreach (var item in damageGroup)
            {
                if (Vector3.Distance(item.transform.position,_attackPoint) < 3)
                {
                    count++;
                }
            }

            Debug.Log(count.ToString());
        }

        if (attackCount >= maxAttackCount)
        {
            attackCount = 0;
        }
        
	}

    private void OnCollisionEnter(Collision collision)
    {
        
    }

}
