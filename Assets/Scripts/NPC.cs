using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRPG.Scripts
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] public int HP = 20;
        // Start is called before the first frame update
        void Start()
        {
            HP = 20;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
