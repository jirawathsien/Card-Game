using System;
using DG.Tweening;
using UnityEngine;

[SelectionBase]
public class Card : MonoBehaviour
{
   [SerializeField] private GameObject glowEffect;
   [SerializeField] private SpriteRenderer cardFrontSprite;
   [SerializeField] private GameObject cardBackSprite;
   [SerializeField] private float timeToFlip = 2f;
   
   private bool isTouchingCard;
   private bool isFlipped;
   float x = 0;
   float y = 0;

   private bool startFlip;

   public int thisCardIndex;

   private float timer;

   private void Start()
   {
      timer = timeToFlip;
   }

   private void Update()
   {

      if (!startFlip)
      {
         timer -= Time.deltaTime;
         if (timer <= 0)
         {
            startFlip = true;
            FlipFront();
         }
      }
   
      
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
      transform.DOScale(0.75f, 0.75f).SetEase(Ease.InBack);
      transform.DOMoveY(1.25f, 0.75f);
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
      transform.DOScale(1f, 0.75f).SetEase(Ease.OutBack);
      transform.DOMoveY(0.2f, 0.75f);
      transform.DORotate(new Vector3(0f, 0f, 0f), 0.75f).OnUpdate(() =>
      {
         x += Time.deltaTime;

         if (x > 0.3f)
         {
            isFlipped = false;
            cardFrontSprite.gameObject.SetActive(true);
            cardBackSprite.SetActive(false);
         }
      });
   }
   
   public void FlipFront()
   {
      transform.DOScale(1f, 0.75f).SetEase(Ease.OutBack);
      transform.DOMoveY(0.2f, 0.75f);
      transform.DORotate(new Vector3(0f, 0f, 0f), 0.75f).OnUpdate(() =>
      {
         y += Time.deltaTime;

         if (y > 0.3f)
         {
            isFlipped = false;
            cardFrontSprite.gameObject.SetActive(true);
            cardBackSprite.SetActive(false);
         }
      });
   }
   
   public void Dissolve()
   {
      transform.DOMove(new Vector3(0f, 0.2f, 0f), 0.75f).SetEase(Ease.InBack);
      glowEffect.SetActive(true);
      transform.DOScale(0.2f, 0.75f).SetEase(Ease.InBack);
      cardFrontSprite.DOFade(0f, 0.75f).OnComplete(() =>
      {
         gameObject.SetActive(false);
      });
   }
   
   public void ChangeCardAlpha(float newValue, bool isActive = false)
   {
      Color cardColor = cardFrontSprite.color;
      cardColor.a = newValue;
      cardFrontSprite.color = cardColor;
      glowEffect.SetActive(isActive);
   }
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         isTouchingCard = true;
         ChangeCardAlpha(1f, true);
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         isTouchingCard = false;
         ChangeCardAlpha(0.45f);
      }
   }
}

