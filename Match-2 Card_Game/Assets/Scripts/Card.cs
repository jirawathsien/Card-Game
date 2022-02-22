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
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Card"))
      {
         isTouchingCard = true;
         ChangeCardAlpha(1f);
      }
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      if (other.CompareTag("Card"))
      {
         isTouchingCard = false;
         ChangeCardAlpha(0.2f);
      }
   }
}
