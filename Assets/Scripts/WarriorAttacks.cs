using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WarriorAttacks : MonoBehaviour
{

    #region PublicVariables
    //************************************************************************************
    //  Public Variables
    //************************************************************************************
    [Tooltip("Number of different attacks"), Range(Constants.AttackCycle.MIN,Constants.AttackCycle.MAX)]
    public int maxAttackCount = 3;
    [Tooltip("Time before reseting the attack to it's first iteration")]
    public float attackReset = 2f;
    [Tooltip("Attack backswing")]
    public float attackCooldown = 1.0f;
    [Tooltip("Knockback distance")]
    public float knockbackDistance = 10;
    [Tooltip("Attack range of each attacks")]
    public int[] attackRange = { 3, 3, 6 };
    #endregion

    #region PrivateVariables
    //************************************************************************************
    //  Private Variables
    //************************************************************************************

    // local variable for handling the attack reset time
    private float _attackReset;

    // local variable for handling the attack cooldown time
    private float _attackCooldown;

    // local variable for handling the attack counter
    private int _currentAttack = 0;

    // local variable that stores attack state
    private enum _attackState
    {
        initialAttack,
        secondaryAttack,
        finishAttack
    };
    #endregion

    private void Update ()
    {
        
        // checks if the attack animation needs to be reset to primary strike
        // this will be useful when we are finally going to do the animation
        if (!IsReset())
        {
            // this is attack state
            _currentAttack = (int) _attackState.initialAttack;
        }
        
        // checks if mouse is being clicks and the attack cooldown is down
		if (Input.GetKey(KeyCode.Mouse0) && !IsCooldown())
        {
            Vector3 _attackPoint;
            // get the position of the attack point
            if (_currentAttack == (int) _attackState.finishAttack)
            {

                // if the attack state is the finish attack
                _attackPoint = transform.position;

            }
            else
            {

                // if the attack is not the finish attack
                _attackPoint = GetComponentInChildren<Transform>().Find("AttackPoint").transform.position;

            }

            // calls the attack function
            Attack(_attackPoint, attackRange[_currentAttack]);

        }

	}

    #region PrivateFunctions

    // check if it's allowed to attack
    private bool IsCooldown()
    {
       
        // if the time exceeds the attack cooldown it's allowed to attack
        if (Time.time > _attackCooldown)
        {

            // sets a new _attackcooldown
            _attackCooldown = Time.time + attackCooldown;

            // resets the attack timer
            _attackReset = Time.time + attackReset;

            return false;
        }

        return true;
    }


    // checks if the animation needs to be reset to the first animation
    private bool IsReset()
    {
        
        // if the player is not attack for a while the attack needs to reset
        if (Time.time > _attackReset)
        {

            // resets the attack count
            _currentAttack = (int) _attackState.initialAttack;


            return false;
        }

        return true;
    }

    /// <summary>
    /// Simple knockback function. Accepts Rigidbody and a float.
    /// </summary>
    /// <param name="_ObjectBody">object to be knockedbacky</param>
    /// <param name="_knockBackForce">knockback force</param>
    private void Knockback(Rigidbody _ObjectBody, float _knockBackForce)
    {
        Vector3 direction = (_ObjectBody.position - transform.position).normalized;
        _ObjectBody.velocity = direction * _knockBackForce;
    }

    /// <summary>
    /// Selects all objects tagged as "Enemy" from the _attackpoint to the _range
    /// </summary>
    /// <param name="_attackPoint">checks the range of enemies from this point</param>
    /// <param name="_range">distance from the enemy position to the attack point</param>
    private void Attack(Vector3 _attackPoint, float _range)
    {
        Debug.Log(_currentAttack.ToString());
        // this is a counter that determines what attack is executed
        _currentAttack++;

        // selects all the enemies
        List<GameObject> _damageGroup = GameObject.FindGameObjectsWithTag("Enemy").ToList();

        // holder variables
        Rigidbody _pickedBody;

        // pick all the enemies
        foreach (GameObject _objects in _damageGroup)
        {

            // if an enemy is within range
            if (Vector3.Distance(_objects.transform.position, _attackPoint) <= _range)
            {

                //  gets the rigidbody of the object
                _pickedBody = _objects.GetComponent<Rigidbody>();

                //  knockback the object
                Knockback(_pickedBody, knockbackDistance);
            }
        }

        //  if the attack state exceeds the maximum attack count reset
        if (_currentAttack >= maxAttackCount)
        {
            _currentAttack = (int) _attackState.initialAttack;
        }
    }

    #endregion

}
