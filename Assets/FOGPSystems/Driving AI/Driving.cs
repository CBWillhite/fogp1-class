using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Driving.AI
{
    

    public class AICar : MonoBehaviour
    {
        public int maxSpeed = 100;
        public Transform path;
        private List<Transform> nodes;

        void Start()
        {
            Transform[] carPath = path.GetComponentsInChildren<Transform>();
            nodes = new List<Transform>();

            for (int i = 0; i < carPath.Length; i++)
            {
                if (carPath[i] != transform)
                {
                    nodes.Add(carPath[i]);
                }
            }
        }

        void Update()
        {

        }
    }
}
