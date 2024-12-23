using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class aicarmovement : MonoBehaviour
{
    public float maxsteer;
    public float forward_force;
    public int index = 0;
    public bool increase;
    public Transform[] waypoints;
    public WheelCollider ai_npc_wheels_collider_left, ai_npc_wheels_collider_right, ai_npc_wheels_collider_rear_right, ai_npc_wheels_collider_rear_left;
    public Transform ai_npc_wheelsleft, ai_npc_wheelsright, ai_npc_wheelsrear_right, ai_npc_wheelsrear_left;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void movement(WheelCollider collider_wheel, Transform actual_wheel)
    {
        Vector3 wheel_position;
        Quaternion wheel_rotation;

        collider_wheel.GetWorldPose(out wheel_position, out wheel_rotation);

        actual_wheel.position = wheel_position;
        actual_wheel.rotation = wheel_rotation;
    }
    // Update is called once per frame
    void Update()
    {
        
        Move();
        waypoints_funtions();
        //Debug.Log(steering);
        movement(ai_npc_wheels_collider_left,ai_npc_wheelsleft.transform);
        movement(ai_npc_wheels_collider_right, ai_npc_wheelsright.transform);
        movement(ai_npc_wheels_collider_rear_right, ai_npc_wheelsrear_right.transform);
        movement(ai_npc_wheels_collider_rear_left, ai_npc_wheelsrear_left.transform);

        Debug.DrawLine(transform.position, waypoints[0].position, Color.red);
        Debug.DrawRay(transform.position, transform.forward * 5, Color.blue);

    }
    private void Move()
    {
        ai_npc_wheels_collider_rear_left.motorTorque = forward_force;
        ai_npc_wheels_collider_rear_right.motorTorque = forward_force;
    }
    private void waypoints_funtions()
    {
        

        Vector3 localTarget = transform.InverseTransformPoint(waypoints[index].position);
        float steering = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg; // Calculate angle
        steering = Mathf.Clamp(steering, -maxsteer, maxsteer); // Clamp steering
        ai_npc_wheels_collider_left.steerAngle = steering;
        ai_npc_wheels_collider_right.steerAngle = steering;
        print(index);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("waypoint"))
        {
            if (index < (waypoints.Length-1))
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }
    }
}
