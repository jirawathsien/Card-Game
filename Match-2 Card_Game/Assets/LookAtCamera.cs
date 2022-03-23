using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
   private Camera main;

   private void Awake()
   {
      main = Camera.main;
   }

   private void LateUpdate()
   {

      transform.forward = main.transform.forward;
   }
}
