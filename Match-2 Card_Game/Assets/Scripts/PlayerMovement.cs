using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;

    private Vector2 moveVector;
    
    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveVector = new Vector2(x, y);
        
        if (x != 0f)
        {
            transform.localScale = new Vector3(Mathf.Sign(-x), 1f, 1f);
        }
        
        Transform transform1;
        (transform1 = transform).Translate(moveVector * (moveSpeed * Time.deltaTime));

        Vector3 clampPos = transform1.position;
        clampPos.x = Mathf.Clamp(clampPos.x, -8.35f, 8.35f);
        clampPos.y = Mathf.Clamp(clampPos.y, -4.35f, 4.35f);
        transform.position = clampPos;
    }

   
}
