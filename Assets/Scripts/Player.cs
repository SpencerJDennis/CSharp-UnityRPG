using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace UnityRPG.Scripts
{
    public enum PlayerState
    {
        walk,
        attack
    }
    public class Player : MonoBehaviour
    {
        public Animator animator;

        // IEnumerator PressDelay()
        // {
        //     yield return new WaitForSeconds(1);
        //     shiftPressed = false;
        // }
        private Rigidbody2D myRigidbody;
        public Vector3 change;
        public PlayerState currentState;
        public bool shiftPressed = false;
        public bool sprint = false;
        public HealthBarScript healthBar;
        [SerializeField] public int HP = 50;
        [SerializeField] public float moveSpeed = 20f;
        [SerializeField] public float knockBackSpeed = 50f;
        // Start is called before the first frame update
        static Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
            vec.z = 0f;
            return vec;
        }
        static Vector3 GetMouseWorldPositionWithZ()
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        }
        static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
        }
        static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }

        void Start()
        {
            healthBar.SetMaxHealth(HP);
            myRigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

            if (HP <= 0)
            {
                GameObject player = GameObject.Find("Triangle");
                Destroy(player);
            }
            if (this != null)
            {
                change = Vector3.zero;
                change.x = Input.GetAxisRaw("Horizontal");
                change.y = Input.GetAxisRaw("Vertical");
                animator.SetBool("Fire1", Input.GetButton("Fire1"));
                if (Input.GetButtonDown("attack") )
                {
                    StartCoroutine(AttackCo());
                }
                else if (currentState == PlayerState.walk)
                {
                    UpdateAnimationAndMove();
                }

                if (sprint == true)
                {
                    moveSpeed = 10f;
                    if (Input.GetKey(KeyCode.LeftShift) && shiftPressed == false)
                    {
                        // shiftPressed = true;
                        sprint = false;
                        // yield return StartCoroutine("PressDelay");
                    }
                }
                else
                {
                    moveSpeed = 6f;
                    if (Input.GetKey(KeyCode.LeftShift) && shiftPressed == false)
                    {
                        // shiftPressed = true;
                        sprint = true;
                        // yield return StartCoroutine("PressDelay");
                    }
                }

            }
        }
        private IEnumerator AttackCo()
        {
            animator.SetBool("attacking", true);
            currentState = PlayerState.attack;
            yield return null;
            animator.SetBool("attacking", false);
            yield return new WaitForSeconds(.3f);
            currentState = PlayerState.walk;
        }

        void UpdateAnimationAndMove()
        {
            if (change != Vector3.zero)
            {
                MoveCharacter();
                animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
                animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
                animator.SetBool("Moving", true);
            }
            else
            {
                animator.SetBool("Moving", false);
            }
        }

        void MoveCharacter()
        {
            // myRigidbody.MovePosition(
            //     transform.position + change * moveSpeed * Time.deltaTime
            // );
            float horizontalMove = Input.GetAxis("Horizontal")*moveSpeed*Time.deltaTime;
            float verticalMove = Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime;
            transform.Translate(horizontalMove, verticalMove, 0);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Lava")
            {
                HP = HP - 10;
                healthBar.SetHealth(HP);
                Debug.Log(this.HP);
            }

            if (other.tag == "EvilQuad")
            {
                Vector3 knockBack = other.transform.position - transform.position;
                HP = HP - 20;
                healthBar.SetHealth(HP);
                transform.Translate(-(knockBack *Time.deltaTime * knockBackSpeed), other.transform);
            }
        }
    }
}
