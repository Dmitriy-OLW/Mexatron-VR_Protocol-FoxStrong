using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSX.UniversalVehicleCombat
{
    /// <summary>
    /// Creates a directional thruster effect based on the steering and movement of a Vehicle Engines 3D component.
    /// </summary>
    public class Thruster_Controller_Base : MonoBehaviour
    {
        public float trust_Control_Level;

        [Tooltip("The engines component that this directional thruster references.")]
        [SerializeField]
        protected VehicleEngines3D engines;

        [Tooltip("The transform representing the position and orientation of the thruster.")]
        [SerializeField]
        protected Transform thrusterTransform;

        [Tooltip("The transform representing the center of mass of the vehicle.")]
        [SerializeField]
        protected Transform centerOfMass;

        [SerializeField]
        protected Axis steeringAxis;

        [SerializeField]
        protected Axis movementAxis;

        protected float level;
        public float Level { get { return level; } }

        public enum Axis
        {
            All,
            None,
            X,
            Y,
            Z
        }

        [SerializeField]
        protected float effectMultiplier = 4;

       protected virtual void Reset()
        {
            thrusterTransform = transform;
            engines = transform.root.GetComponentInChildren<VehicleEngines3D>();
            if (engines != null) centerOfMass = engines.transform;
        }


        // Update is called once per frame
        protected virtual void Update()
        {
            if (trust_Control_Level < 0)
            {
                level = 0;
            }
            else
            {
                level = trust_Control_Level; 
            }
            
            
            //Создай скрипт подключающийся именно к этим компонента и изменяй. Или перепиши два скриптаподключаемся к level и задаём значения при движкение и всё. отклучив предыдущие значение по возможности, чтоб не нагружать. 

        }
    }
}






















 /*Vector3 thrusterLocalPos = centerOfMass.InverseTransformPoint(thrusterTransform.position);
            Vector3 thrusterLocalDirection = centerOfMass.InverseTransformDirection(thrusterTransform.forward);

            // Movement

            Vector3 translationAxis;
            switch (movementAxis)
            {
                case Axis.All:

                    translationAxis = engines.MovementInputs;
                    break;

                case Axis.X:

                    translationAxis = new Vector3(engines.MovementInputs.x, 0, 0);
                    break;

                case Axis.Y:

                    translationAxis = new Vector3(0, engines.MovementInputs.y, 0);
                    break;

                case Axis.Z:

                    translationAxis = new Vector3(0, 0, engines.MovementInputs.z);
                    break;

                default:

                    translationAxis = Vector3.zero;
                    break;

            }
            float movementAmount = Mathf.Clamp(-Vector3.Dot(thrusterLocalDirection, translationAxis), 0, 1);

            // Steering

            Vector3 rotationAxis;
            switch (steeringAxis)
            {
                case Axis.All:
                    rotationAxis = engines.SteeringInputs;
                    break;

                case Axis.X:
                    rotationAxis = new Vector3(engines.SteeringInputs.x, 0, 0);
                    break;

                case Axis.Y:
                    rotationAxis = new Vector3(0, engines.SteeringInputs.y, 0);
                    break;

                case Axis.Z:
                    rotationAxis = new Vector3(0, 0, engines.SteeringInputs.z);
                    break;

                default:

                    rotationAxis = Vector3.zero;
                    break;

            }

            Vector3 tmp = Vector3.ProjectOnPlane(thrusterLocalPos, thrusterLocalDirection).normalized;
            if (Mathf.Abs(tmp.x) > 0.01f) tmp.x = Mathf.Sign(tmp.x) * (tmp.x / tmp.x);
            if (Mathf.Abs(tmp.y) > 0.01f) tmp.y = Mathf.Sign(tmp.y) * (tmp.y / tmp.y);
            if (Mathf.Abs(tmp.z) > 0.01f) tmp.z = Mathf.Sign(tmp.z) * (tmp.z / tmp.z);

            float steeringAmount = Mathf.Clamp(-Vector3.Dot(Vector3.Cross(rotationAxis, tmp), thrusterLocalDirection.normalized), 0, 1);

            // Calculate thruster level
            

            level = Mathf.Min((movementAmount + steeringAmount) * effectMultiplier, 1);*/