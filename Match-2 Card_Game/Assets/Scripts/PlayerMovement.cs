using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float xClampPos, zClampPos;

    private Vector3 moveVector;
    
    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveVector = new Vector3(x, 0, z);
        
        Transform transform1;
        (transform1 = transform).Translate(moveVector * (moveSpeed * Time.deltaTime));

        Vector3 clampPos = transform1.position;
        clampPos.x = Mathf.Clamp(clampPos.x, -xClampPos, xClampPos);
        clampPos.z = Mathf.Clamp(clampPos.z, -zClampPos, zClampPos);
        transform.position = clampPos;
    }

   
}
