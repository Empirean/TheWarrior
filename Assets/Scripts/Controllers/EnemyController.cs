using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyController : MonoBehaviour
{

    public float detectionRadius = 20f;

    GameObject _target;

    NavMeshAgent _navMesh;

	void Start ()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _target = GameObject.Find("Player");
       
	}
	
	void Update ()
    {
        float _distance = Vector3.Distance(transform.position, _target.transform.position);
        if (_distance <= detectionRadius)
        {
            _navMesh.SetDestination(_target.transform.position);
        }
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
