using System;
using UnityEngine;

public class Card : MonoBehaviour
{
   [SerializeField] private SpriteRenderer cardSprites;
   private bool isTouchingCard;

   public void ChangeCardAlpha(float newValue)
   {
      Color cardColor = cardSprites.color;
      cardColor.a = newValue;
      cardSprites.color = cardColor;
   }
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         isTouchingCard = true;
         ChangeCardAlpha(1f);
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         isTouchingCard = false;
         ChangeCardAlpha(0.2f);
      }
   }
}
