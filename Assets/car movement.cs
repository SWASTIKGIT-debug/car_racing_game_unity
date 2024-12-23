using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carmovement : MonoBehaviour
{
    public Rigidbody car_body;
    public int forward_force,break_force;
    float horizontal_input;
    public float max_rotation;
    public WheelCollider front_left_wheel_collider, back_left_wheel_collider, front_right_wheel_collider, back_right_wheel_collider;
    public Transform front_left_wheel, back_left_wheel, front_right_wheel, back_right_wheel;
    private void Awake()
    {
        car_body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //float forward_input = Input.GetAxisRaw("Vertical");
        horizontal_input = Input.GetAxisRaw("Horizontal");
        forward_backward();
        turning();

        movement(front_right_wheel_collider, front_right_wheel);
        movement(front_left_wheel_collider, front_left_wheel);
        movement(back_right_wheel_collider, back_right_wheel);
        movement(back_left_wheel_collider, back_left_wheel);
    }

    private void movement(WheelCollider collider_wheel,Transform actual_wheel)
    {
        Vector3 wheel_position;
        Quaternion wheel_rotation;
        
        collider_wheel.GetWorldPose(out wheel_position,out wheel_rotation);

        actual_wheel.position = wheel_position;
        actual_wheel.rotation = wheel_rotation;
    }
    private void forward_backward()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            back_left_wheel_collider.motorTorque = -forward_force;
            back_right_wheel_collider.motorTorque = -forward_force;
        }
        else
        {
            back_left_wheel_collider.motorTorque = forward_force;
            back_right_wheel_collider.motorTorque = forward_force;
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            back_left_wheel_collider.brakeTorque = break_force;
            back_right_wheel_collider.brakeTorque = break_force;
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            back_left_wheel_collider.brakeTorque = break_force;
            back_right_wheel_collider.brakeTorque = break_force;
        }
        else
        {
            back_left_wheel_collider.brakeTorque = 0;
            back_right_wheel_collider.brakeTorque = 0;
        }
    }

    private void turning()
    {
        front_left_wheel_collider.steerAngle = horizontal_input * max_rotation;
        front_right_wheel_collider.steerAngle = horizontal_input * max_rotation;
    }

}
