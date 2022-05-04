using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AiCar.Driving
{
    

    public class Driving : MonoBehaviour
    {
        public float maxSpeed = 100f;
        public float currentSpeed;
        public float maxTourqe = 300f;
        public float maxBrakeTourqe = 150f;
        public float maxTurn = 45f;
        public WheelCollider wheelFL;
        public WheelCollider wheelFR;
        public WheelCollider wheelRL;
        public WheelCollider wheelRR;
        public Transform path;
        private List<Transform> nodes;
        private int curNode = 0;
        public Vector3 centerOfMass;
        public bool isBraking = false;
        public Material normalMat;
        public Material brakeMat;
        public Renderer carRenderer;

        [Header("Sensors")]
        public float sensorLength = 3f;
        public Vector3 sensorFront = new Vector3(0f, 1f, 3.66f);
        public float sensorSide = 0.75f;
        public float sensorAngle = 30f;


        private void Start()
        {
            GetComponent<Rigidbody>().centerOfMass = centerOfMass;
            Transform[] carPath = path.GetComponentsInChildren<Transform>();
            nodes = new List<Transform>();

            for (int i = 0; i < carPath.Length; i++)
            {
                if (carPath[i] != path.transform)
                {
                    nodes.Add(carPath[i]);
                }
            }
        }

        private void FixedUpdate()
        {
            Sensors();
            ApplySteer();
            Drive();
            waypointCheck();
            braking();
        }

        private void Sensors()
        {
            RaycastHit hit;
            Vector3 sensorStartPos = transform.position + sensorFront;

            // Front center
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
            }
 

            // Front right
            sensorStartPos.x += sensorSide;
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
            }
            

            // right angle
            if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(sensorAngle, transform.up) * transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
            }
            

            // Front left center
            sensorStartPos.x -= 2*sensorSide;
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
            }
            

            // left angle
            if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-sensorAngle, transform.up) * transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
            }
            
        }

        private void ApplySteer()
        {
            Vector3 relativeVector = transform.InverseTransformPoint(nodes[curNode].position);
            float steerNew = (relativeVector.x / relativeVector.magnitude)*maxTurn;
            wheelFL.steerAngle = steerNew;
            wheelFR.steerAngle = steerNew;
        }

        private void Drive()
        {
            currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

            if (currentSpeed < maxSpeed && !isBraking)
            {
                wheelFL.motorTorque = maxTourqe;
                wheelFR.motorTorque = maxTourqe;
            }
            else
            {
                wheelFL.motorTorque = 0;
                wheelFR.motorTorque = 0;
            }
            
        }

        private void waypointCheck()
        {
            if(Vector3.Distance(transform.position, nodes[curNode].position) < 2)
            {
                if (curNode == nodes.Count - 1)
                {
                    curNode = 0;
                }
                else
                {
                    curNode += 1;
                }
            }
        }

        private void braking()
        {
            if (isBraking)
            {
                carRenderer.material = brakeMat;
                wheelRL.brakeTorque = maxBrakeTourqe;
                wheelRR.brakeTorque = maxBrakeTourqe;
            }
            else
            {
                carRenderer.material = normalMat;
                wheelRL.brakeTorque = 0;
                wheelRR.brakeTorque = 0;
            }
        }
    }
}
