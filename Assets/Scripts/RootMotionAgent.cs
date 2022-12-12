using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YZ.RootMotionAgent
{
    /// <summary>
    /// This is the actual class that scripts will call to make a character move
    /// </summary>
    public class RootMotionAgent : MonoBehaviour
    {
        public float arriveDecay = 0.7f;
        public float stoppingDistance = 0.15f;
        public bool canRotate = true;

        public CharacterController cc;
        //Seeker seeker;
        public Animator _animator;
        //RichAI _richAi;

        public bool enablePathfinding = true;
        // Start is called before the first frame update

        public INavAgentInterface agentInterface;

        [SerializeField]
        private bool moving = false;

        void Start()
        {
            cc = GetComponent<CharacterController>();
            des = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            des2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            des.GetComponent<Collider>().enabled = false;
            des.SetActive(false);
            des2.GetComponent<Collider>().enabled = false;
            des2.GetComponent<MeshRenderer>().material.color = Color.red;
            des2.SetActive(false);
            des.transform.localScale *= 0.5f;
            _animator = GetComponent<Animator>();

            agentInterface = GetComponent<NavAgentInterface>();
        }

        public void Move(Vector3 position)
        {
            agentInterface.SetDestination(position);
            moving = true;
            reachedDestination = false;
        }


        private void OnAnimatorMove()
        {
            if (Time.deltaTime <= 0) return;


            Vector3 delta = _animator.rootPosition - transform.position;

            cc.Move(delta + new Vector3(0f, Physics.gravity.y * Time.deltaTime, 0f));

        }

        public bool run = false;
        GameObject des;
        GameObject des2;
        public float speed = 2f;
        public float rotateSpeed = 1f;

        public float TopDownDistanceToTarget(Vector3 target)
        {
            return HelperClass.TopDownDistance(transform.position, target);
        }

        public bool tooClose = false;

        private bool reachedDestination = false;
        void DrawDebug()
        {
            Vector3 steeringTarget = agentInterface.GetSteeringTarget();
            des.transform.position = steeringTarget;
            Debug.DrawLine(transform.position, transform.position + (steeringTarget - transform.position), Color.red);
        }

        private Vector3 lastDirection;
        private void Update()
        {
            if (!enablePathfinding)
            {
                speed = 0f;
                return;
            }

            if (!moving)
            {
                return;
            }


            Vector3 steeringTarget = agentInterface.GetSteeringTarget();

            DrawDebug();

            Vector3 delta = (steeringTarget - transform.position).normalized;
            Vector3 moveDir = transform.InverseTransformDirection(delta);
            Debug.DrawLine(transform.position, transform.position + delta, Color.red);

            float dist = TopDownDistanceToTarget(agentInterface.GetDestination());
            if (!reachedDestination)
            {
                if (dist <= stoppingDistance)
                {
                    tooClose = true;
                    lastDirection = moveDir;
                    reachedDestination = true;

                }
                else
                {
                    tooClose = false;
                    if (canRotate)
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.ProjectOnPlane(steeringTarget - transform.position, Vector3.up).normalized), Time.deltaTime * rotateSpeed);
                    }
                }

            }

            if (reachedDestination)
            {
                lastDirection = lastDirection * arriveDecay;
                moveDir = lastDirection;
                if (Vector3.Distance(moveDir, Vector3.zero) <= Mathf.Epsilon)
                {
                    moving = false;
                }
            }

            _animator.SetFloat("moveX", moveDir.x);
            _animator.SetFloat("moveY", moveDir.z);

        }

        Vector2 GetMoveVector()
        {
            return new Vector2(_animator.GetFloat("moveX"), _animator.GetFloat("moveY"));
        }
    }
}