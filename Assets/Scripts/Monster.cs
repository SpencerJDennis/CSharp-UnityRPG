using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRPG.Scripts
{
    public class Monster : MonoBehaviour
    {
        [SerializeField] public float knockBackSpeed = 30f;
        [SerializeField] public int HP = 40;
        [SerializeField] GameObject player;
        [SerializeField] public float moveSpeed = 20f;


        void Start()
        {
            HP = Random.Range(60, 100);
            moveSpeed = Random.Range(3f, 5f);
        }

        // Update is called once per frame
        void Update()
        {
            if (player != null)
            {
                if (HP > 0)
                {
                    Vector3 playerDistance = player.transform.position - transform.position;
                    if (playerDistance.magnitude < 15)
                    {
                        playerDistance.Normalize();
                        transform.Translate(playerDistance * Time.deltaTime * moveSpeed);
                    }
                }
                
                if (HP <= 0)
                {
                    GameObject monster = GameObject.Find("Evil Quatrangle");
                    Destroy(monster);
                }
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "SwordHit")
            {
                Vector3 knockBack = other.transform.position - transform.position;
                HP = HP - 20;
                transform.Translate(-(knockBack *Time.deltaTime * knockBackSpeed), other.transform);
            }
        }
    }
}
