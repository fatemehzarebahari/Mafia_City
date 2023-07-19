using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Bases
{
    public class Person : Contractable
    {
        protected bool _inJail = false;
        protected int _currentWayPointIndex = 0; 
        protected bool _haveBeenRubbed = false;

        protected List<Transform> _wayPoints;
        
        private bool activeMoving = true;

        protected float speed = 10;
        protected float accelaration = 20;

        [SerializeField] protected ListManagers listManager;
        [SerializeField] private NavMeshAgent agent;
        protected override void  Start()
        {
            UpdateTag();
            UpdateWayPoints();
            _currentWayPointIndex = Random.Range(0, _wayPoints.Count );
            MoveToWayPoint();
            agent.speed = speed;
            agent.acceleration = accelaration; 
        }

        private void Update()
        {
            if (!_isDead && _wayPoints.Count > 0){
                float distance = Vector3.Distance(transform.position, target.position);
                if (distance < 10 && activeMoving)
                {
                    // transform.DOKill();
                    //contractManager.ManageContract(transform.GetComponent<Contractable>(),target.GetComponent<Contractable>());
                    ContractAction(target);
                    MoveToWayPoint();

                }
                else if (activeMoving)
                {
                    // transform.DOMove(targetDestination,5f);
                    agent.SetDestination(target.position);
                } 
            }

        }

        protected virtual void MoveToWayPoint()
        {
            if (_wayPoints.Count > 0)
            {
                _currentWayPointIndex %= _wayPoints.Count;
                target = _wayPoints[_currentWayPointIndex];
                _currentWayPointIndex++;
                _currentWayPointIndex %= _wayPoints.Count;
            }
            
        }

    
        
        public override IEnumerator WaitAndCall(Transform target)
        {
            agent.isStopped = true;
            activeMoving = false;
            yield return new WaitForSeconds(waitingTime);
            ActionInWayPoint(target);
            activeMoving = true;
            agent.isStopped = false;
            UpdateWayPoints();
            agent.speed = speed;
            agent.acceleration = accelaration;

        }
        
        protected virtual void ActionInWayPoint(Transform target)
        {
            // what to do in each way point
        }

        protected virtual void UpdateWayPoints()
        {
            // update wayPoints with new ones
        }

        protected virtual void Heal()
        {
            _isDead = false;
        }
        public bool GetRubberyStatus()
        {
            return _haveBeenRubbed;
        }

        public void JustBeenRubbed()
        {
            StartCoroutine(RubberyWaiting(rubberyTime));
        }
        private IEnumerator RubberyWaiting(float time)
        {
            listManager.AddToRubberyList(transform);
            _haveBeenRubbed = true;
            yield return new WaitForSeconds(time);
            listManager.RemoveFromRubbedPeople(transform);
            _haveBeenRubbed = false;
        }
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            // if(other.transform != target){}
        }


    }
}



