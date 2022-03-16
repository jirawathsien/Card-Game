using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    [SerializeField] private PlayerMovement playerMovement;
    
    public List<Card> cards;

    public int Flipped;
    public TextMeshProUGUI cardFlipText;
    
    private void Awake()
    {
        instance = this;
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
                Flipped++;

                cardFlipText.text = "Cards: " + $"{Flipped}" + "/3";

            }
            cards.Clear();
        }
        
       
    }
}

