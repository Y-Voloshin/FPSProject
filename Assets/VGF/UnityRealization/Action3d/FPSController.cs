using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.FromUnityStandartAssets;
namespace VGF.Action3d
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class FPSController : CachedBehaviour
    {
        [SerializeField]
        FPSPlayerModel Model;
        [SerializeField]
        Transform rotationPivotTransform;

        bool IsRunning,
            m_Jumping,
            m_IsGrounded,
            
            MoveForward,
            MoveBackward,
            MoveLeft,
            MoveRight;
        float CurForvardSpeed,
            CurRightSpeed,
            CurVerticalSpeed,
            
            //JumpForce = 5, 
            DownSpeedLimit;

        Vector3 MoveCommandVectorGrounded = Vector3.zero,
            //MoveCommandVectorPreJump;
            PreJumpVelocity;

        #region copypaste from standart asset
        public MouseLook mouseLook = new MouseLook();

        private Vector3 m_GroundContactNormal;
        private bool m_Jump, m_PreviouslyGrounded;

        [System.Serializable]
        public class AdvancedSettings
        {
            public float groundCheckDistance = 0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )
            public float stickToGroundHelperDistance = 0.5f; // stops the character
            //public float slowDownRate = 20f; // rate at which the controller comes to a stop when there is no input
            public bool airControl; // can the user control the direction that is being moved in the air
            [Tooltip("set it to 0.1 or more if you get stuck in wall")]
            public float shellOffset = 0.1f; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
        }
        public AdvancedSettings advancedSettings = new AdvancedSettings();
        #endregion

        #region Cache of values required for down spherecast. No need to calc em every time
        RaycastHit downHitInfo;
        bool DownRaycastResult;
        float DownSphereCastRadius,
            DownRaycastDistance,
            ForwardCastRadius;//avoiding stuck in walls
        Vector3 downV3 = Vector3.down;
        bool moving;
        #endregion

        // Use this for initialization
        void Start()
        {
            var myCapsuleRadius = (myCollider as CapsuleCollider).radius;
            var myCapsuleHeightHalf = (myCollider as CapsuleCollider).height * 0.5f;
            DownRaycastDistance = myCapsuleHeightHalf 
                - myCapsuleRadius * 0.5f
                + advancedSettings.groundCheckDistance;
            DownSphereCastRadius = myCapsuleRadius * (1.0f - advancedSettings.shellOffset);
            ForwardCastRadius = myCapsuleRadius * 1.3f;
            mouseLook.Init(transform, rotationPivotTransform);
        }

        // Update is called once per frame
        void Update()
        {
            RotateView();
            CheckInput();
            
        }

        private void FixedUpdate()
        {
            DownRaycastResult = Physics.SphereCast(myTransform.position, DownSphereCastRadius, downV3, out downHitInfo,
                                   DownRaycastDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore);

            GroundCheck();
            if (m_PreviouslyGrounded && !m_IsGrounded && !m_Jumping)
                StickToGroundHelper();
            DoAllMovements();
        }

        void DoAllMovements()
        {
            if (m_Jumping)
            {
                VGF.DebugTools.DebugLabel.ShowValue("Jump");
                Jump();
            }
            else if (m_IsGrounded)
            {
                DebugTools.DebugLabel.ShowValue("Is Grounded");
                if (moving)
                    Move();
                else
                {
                    myRigidbody.velocity = new Vector3(0, myRigidbody.velocity.y, 0);
                }
            }
            else
            {
                MoveInJump();
            }
        }

        void CheckInput()
        {            
            if (Input.GetKeyDown(KeyCode.Space) && m_IsGrounded)
            {
                //Save desired move for getting over obstacles
                SetPreJumpVelocity();
                m_Jumping = true;
            }
            else
                moving = CheckMoveInput();
        }

        bool CheckMoveInput()
        {
            MoveBackward = Input.GetKey(KeyCode.S);
            MoveForward = Input.GetKey(KeyCode.W);
            MoveLeft = Input.GetKey(KeyCode.A);
            MoveRight = Input.GetKey(KeyCode.D);
            if (MoveBackward == MoveForward)
                MoveBackward = MoveForward = false;
            if (MoveLeft == MoveRight)
                MoveLeft = MoveRight = false;

            MoveCommandVectorGrounded.z = MoveForward ? 1 : MoveBackward ? -1 : 0;
            MoveCommandVectorGrounded.x = MoveRight ? 1 : MoveLeft ? -1 : 0;

            return MoveBackward || MoveForward || MoveLeft || MoveRight;
        }

        void Jump()
        {
            if (!m_IsGrounded)
            {
                m_Jumping = false;
                return;
            }
            myRigidbody.drag = 0;
            myRigidbody.AddForce(0, Model.JumpForce, 0, ForceMode.Impulse);
        }        

        #region  Copied from UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController
        void GroundCheck()
        {
            m_PreviouslyGrounded = m_IsGrounded;
            if (DownRaycastResult)
            {
                m_IsGrounded = true;
                m_GroundContactNormal = downHitInfo.normal;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundContactNormal = Vector3.up;
            }
            if (!m_PreviouslyGrounded && m_IsGrounded && m_Jumping)
            {
                m_Jumping = false;
            }

            //If we just fell from somewhere we need to keep horizontal velocity
            if (m_PreviouslyGrounded && !m_IsGrounded)
                SetPreJumpVelocity();
        }

        private void StickToGroundHelper()
        {
            if (DownRaycastResult)
            {
                //myRigidbody.velocity
                if (Mathf.Abs(Vector3.Angle(downHitInfo.normal, Vector3.up)) < 85f)
                {
                    myRigidbody.velocity = Vector3.ProjectOnPlane(myRigidbody.velocity, downHitInfo.normal);
                }
            }
        }

        private void RotateView()
        {
            //avoids the mouse looking if the game is effectively paused
            if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

            // get the rotation before it's changed
            float oldYRotation = transform.eulerAngles.y;

            mouseLook.LookRotation(transform, rotationPivotTransform);

            if (m_IsGrounded || advancedSettings.airControl)
            {
                // Rotate the rigidbody velocity to match the new direction that the character is looking
                Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
                //m_RigidBody.velocity = velRotation * m_RigidBody.velocity;
            }
        }
        #endregion

        Vector3 GetVelocityFromWASD()
        {
            return myTransform.forward * MoveCommandVectorGrounded.z * Model.ForwardSpeed
                + myTransform.right * MoveCommandVectorGrounded.x * Model.StrafeSpeed;
        }

        void Move()
        {
            myRigidbody.velocity = GetVelocityFromWASD();
        }

        /// <summary>
        /// For climing in windows and on low obstacles
        /// </summary>
        void MoveInJump()
        {
            //var curVelocity = new Vector3(0, myRigidbody.velocity.y, 0);
            Vector3 force = PreJumpVelocity;
            
           // Physics.BoxCast()
            RaycastHit hit;
            if (Physics.BoxCast(myTransform.position,
                new Vector3(.4f, .6f, .25f), force, out hit, Quaternion.Euler(force), .5f, Physics.AllLayers))
            {
                //Debug.DrawRay(myTransform.position, force.normalized, Color.red, 3);
                // Debug.DrawLine(myTransform.position, hit.point, Color.red, 3);
                //Debug.Log(hit.transform.name);
                //return;
                force = -force  * 0.25f;
                if (hit.distance > 0.25f)
                    return;
            }

            force.y = myRigidbody.velocity.y;
            myRigidbody.velocity = force;
        }

        void SetPreJumpVelocity()
        {
            CheckMoveInput();
            PreJumpVelocity = GetVelocityFromWASD();
        }
    }
}