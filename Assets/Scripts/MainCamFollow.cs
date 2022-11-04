using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRPG.Scripts
{
    public class MainCamFollow : MonoBehaviour
    {
        [SerializeField] GameObject followObject;

        // Update is called once per frame
        void Update()
        {
            transform.position = followObject.transform.position + new Vector3 (0,0,-12);
        }
    }
}
