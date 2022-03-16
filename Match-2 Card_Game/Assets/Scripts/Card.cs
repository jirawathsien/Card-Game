using DG.Tweening;
using UnityEngine;

[SelectionBase]
public class Card : MonoBehaviour
{
   [SerializeField] private SpriteRenderer cardFrontSprite;
   [SerializeField] private GameObject cardBackSprite;
   private bool isTouchingCard;
   private bool isFlipped;
   float x = 0;

   public int thisCardIndex;
   
   private void Update()
   {
      if (isTouchingCard)
      {
         if (Input.GetKeyDown(KeyCode.Space) && !isFlipped)
         {
            Flip();
            isFlipped = true;
         } 
      }
   }

   void Flip()
   {
      transform.DORotate(new Vector3(0f, 0f, 180f), 1f).OnUpdate(() =>
      {
         x += Time.deltaTime;

         if (x > 0.49f)
         {
            cardFrontSprite.gameObject.SetActive(false);
            cardBackSprite.SetActive(true);
         }
      }).OnComplete(()=>{
      {
         CardManager.instance.cards.Add(this);
         CardManager.instance.CheckMatch();
      }});
   }

   public void FlipBack()
   {
      transform.DORotate(new Vector3(0f, 0f, 0f), 1f).OnUpdate(() =>
      {
         x += Time.deltaTime;

         if (x > 0.49f)
         {
            isFlipped = false;
            cardFrontSprite.gameObject.SetActive(true);
            cardBackSprite.SetActive(false);
         }
      });
   }
   
   public void Dissolve()
   {
      gameObject.SetActive(false);
   }
   
   public void ChangeCardAlpha(float newValue)
   {
      Color cardColor = cardFrontSprite.color;
      cardColor.a = newValue;
      cardFrontSprite.color = cardColor;
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

