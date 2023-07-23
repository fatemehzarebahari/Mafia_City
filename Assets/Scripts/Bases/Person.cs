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
        protected int _inJailTime = 20;
        protected int _currentWayPointIndex = 0; 
        protected bool _haveBeenRubbed = false;

        protected List<Transform> _wayPoints;
        
        protected bool activeMoving = true;

        protected float speed = 10;
        protected float accelaration = 20;

        [SerializeField] protected NavMeshAgent agent;
        
        [SerializeField] private Transform jailTransform;
        private Transform positionBeforeJail;
        protected override void  Start()
        {
            UpdateTag();
            UpdateWayPoints();
            _currentWayPointIndex = Random.Range(0, _wayPoints.Count);
            MoveToWayPoint();
            agent.speed = speed;
            agent.acceleration = accelaration; 
        }

        protected virtual void Update()
        {
            if (!_isDead && _wayPoints.Count > 0 &&!_inJail){
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
            SetWaitingTime();
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

        public virtual void UpdateWayPoints()
        {
            // update wayPoints with new ones
        }

        public virtual void Heal()
        {
            _isDead = false;
            UpdateTag();
            resetPerson();
            listManager.AddToAlivePeople(transform);
        }
        public bool GetRubberyStatus()
        {
            return _haveBeenRubbed;
        }

        public void JustBeenRubbed(Transform thief)
        {
            StartCoroutine(RubberyWaiting(rubberyTime,thief));
        }
        private IEnumerator RubberyWaiting(float time,Transform thief)
        {
            listManager.AddToRubberyList(transform,thief);
            _haveBeenRubbed = true;
            yield return new WaitForSeconds(time);
            listManager.RemoveFromRubbedPeople(transform,thief);
            _haveBeenRubbed = false;
        }
        
        public void SendToJail()
        {
            if (!_isDead)
            {
                StopAllCoroutines();
                StartCoroutine(JailWaiting(_inJailTime));
            }

        }
        private IEnumerator JailWaiting(float time)
        {
            _inJail = true;
            agent.speed = 100;
            positionBeforeJail = transform;
            agent.SetDestination(jailTransform.position);
            yield return new WaitForSeconds(time);
            _inJail = false;
            target = positionBeforeJail;
            agent.speed = speed;
            resetPerson();

        }
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            // if(other.transform != target){}
        }

        protected virtual void SetWaitingTime()
        {
            // how much should we wait for this contract
        }

        protected void resetPerson()
        {
            listManager.resetFromRobberyLists(transform);
            _haveBeenRubbed = false;
            activeMoving = true;
            agent.isStopped = false;
            UpdateWayPoints();
            agent.speed = speed;
            agent.acceleration = accelaration;
        }
        public override void Kill()
        {
            _isDead = true;
            StopAllCoroutines();
            UpdateTag();
            agent.isStopped = true;
            activeMoving = false;
            listManager.AddToDeadPeople(transform);
        }

        public bool IsInJail()
        {
            return _inJail;
        }

    }
}



