using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AttributesController : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public int armor;
    public float criticalChance;
    public float criticalDamage;
    public int damage;
    public Image healthBar;
    public Canvas canvas;

    public void Damage(GameObject _unit, int _damage)
    {
        AttributesController _attributeController =  _unit.GetComponent<AttributesController>();
        if (_attributeController != null)
        {
            _attributeController.currentHealth = _attributeController.currentHealth - _damage;
        }
    }

    private void Awake()
    {
        if (canvas == null)
        {
            canvas = FindObjectOfType<GameController>().GetComponentInChildren<Canvas>();
        }

        if (healthBar == null)
        {
            healthBar = FindObjectOfType<GameController>().GetComponentInChildren<Canvas>().GetComponentInChildren<Image>().GetComponent<Image>();
        }
    }


    private void Update()
    {

        healthBar.fillAmount = (float) currentHealth / maxHealth;
        canvas.transform.rotation = Camera.main.transform.rotation;

        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }

}
