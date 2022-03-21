using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject badText;
    public GameObject goodText;
    
    [SerializeField] private GameObject stunEffect;
    [SerializeField] private GameObject dashTrail;
    
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float xClampPos, zClampPos;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;

    public float x = 5.5f;
    public float z = 5f;

    private Vector3 moveVector;
    private CharacterController characterController;

    private bool stopPlayer;
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(stopPlayer) return;
        
        HandleMovement();

        var v3 = transform.position;
        v3.x = Mathf.Clamp(v3.x, -xClampPos, xClampPos);
        v3.z = Mathf.Clamp(v3.z, -zClampPos, zClampPos);
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

    public void Stunt()
    {
        StartCoroutine(StuntCorutine());
    }

    public void Matched()
    {
        StartCoroutine(MatchCorutine());
    }
    
    private IEnumerator StuntCorutine()
    {
        stunEffect.SetActive(true);
        badText.SetActive(true);
        badText.transform.DOScale(1f, 0.75f).SetEase(Ease.OutBack).From(0.75f);

        stopPlayer = true;
        yield return new WaitForSeconds(0.75f);
        
        badText.SetActive(false);
        stopPlayer = false;
    }

    private IEnumerator MatchCorutine()
    {
        goodText.SetActive(true);
        goodText.transform.DOScale(1f, 0.75f).SetEase(Ease.OutBack).From(0.75f);
        yield return new WaitForSeconds(0.75f);
        goodText.SetActive(false);
    }

}
