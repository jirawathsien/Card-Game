using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    [SerializeField] private CanvasGroup gameOvergroup;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private PlayerMovement playerMovement;
    
    public List<Card> cards;

    public int Flipped;
    public TextMeshProUGUI cardFlipText;
    public TextMeshProUGUI clearText;
    public TextMeshProUGUI stageText;
    
    public int maxCardThisStage;

    public bool isGameOver;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        stageText.text = "Stage: " + (SceneManager.GetActiveScene().buildIndex + 1).ToString("");
        
        cardFlipText.text = "Cards: " + $"{Flipped}" + $" / {maxCardThisStage}";

    }

    public void CheckMatch()
    {
        if (cards.Count == 2)
        {
            if (cards[0].thisCardIndex != cards[1].thisCardIndex)
            {
                // not matched 
                cards[0].FlipBack();
                cards[1].FlipBack();
                playerMovement.Stunt();
                
            }
            else
            {
                // matched
              
                cards[0].Dissolve();
                cards[1].Dissolve();
                playerMovement.Matched();
                
                Flipped++;
                cardFlipText.text = "Cards: " + $"{Flipped}" + $" / {maxCardThisStage}";

                if (Flipped == maxCardThisStage)
                {
                    fadeAnimator.SetTrigger("FadeIn");
                    clearText.gameObject.SetActive(true);
                    clearText.DOFade(1f, 1f);
                    clearText.transform.DOScale(1f, 1f).From(0.5f).SetEase(Ease.OutBack).SetDelay(1f).OnComplete(() =>
                    {
                        if (SceneManager.GetActiveScene().buildIndex + 1 <= 8)
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                        }
                        else
                        {
                            SceneManager.LoadScene(0);
                        }
                    }); 
                }
            }
            cards.Clear();
        }
    }

    public void GameOver()
    {
        gameOvergroup.gameObject.SetActive(true);
        gameOvergroup.DOFade(1f, 1f);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

