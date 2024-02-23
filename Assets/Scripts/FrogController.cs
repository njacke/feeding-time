using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float maxRotationAngle = 45f;
    TongueController tongueController;

    private void Start()
    {        
        tongueController = FindObjectOfType<TongueController>();
    }

    void Update()
    {
        RotateTowardsMouse();
/*         if (Input.GetMouseButtonDown(0) && tongueController.GetCanMove())
        {
            StartCoroutine(tongueController.ExtendAndRetractTongue());
        } */
    }

    void RotateTowardsMouse()
    {
        if(tongueController.GetCanMove())
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.z));

            Debug.DrawLine(transform.position, mouseWorldPosition, Color.blue);

            // Make sure the frog's head is pointing upwards
            Vector3 initialDirection = Vector3.up;

            // Calculate the angle between the initial direction and the mouse position
            float angle = Vector2.SignedAngle(initialDirection, mouseWorldPosition - transform.position);
            float limitedAngle = Mathf.Clamp(angle, -maxRotationAngle, maxRotationAngle);

            // Calculate the target rotation based on the angle
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, limitedAngle);

            // Smoothly rotate the frog towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}