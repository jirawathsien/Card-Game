using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject dashTrail;
    
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float xClampPos, zClampPos;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;
    
    private Vector3 moveVector;
    private CharacterController characterController;
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveVector = new Vector3(x, 0, z);
        
      
        characterController.Move(moveVector * (moveSpeed * Time.deltaTime));
        
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(DashCorutine());
        }
        
        Vector3 clampPos = transform.position;
        clampPos.x = Mathf.Clamp(clampPos.x, -xClampPos, xClampPos);
        clampPos.z = Mathf.Clamp(clampPos.z, -zClampPos, zClampPos);
        transform.position = clampPos;
    }

    private IEnumerator DashCorutine()
    {
        dashTrail.SetActive(true);
        
        float startTime = Time.time;
        while (startTime + dashTime > Time.time)
        {
            characterController.Move(moveVector * (Time.deltaTime * dashSpeed));
            yield return null;
        }
        dashTrail.SetActive(false);
    }

}
