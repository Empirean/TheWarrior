using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesController : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public int armor;
    public float criticalChance;
    public float criticalDamage;
    public int damage;

    public void Damage(GameObject _unit, int _damage)
    {
        AttributesController _attributeController =  _unit.GetComponent<AttributesController>();
        if (_attributeController != null)
        {
            _attributeController.currentHealth = _attributeController.currentHealth - _damage;
        }
    }

    private void Update()
    {
        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }

}
