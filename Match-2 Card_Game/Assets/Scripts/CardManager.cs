using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    public Card[] cards;

    public int count;
    
    private void Awake()
    {
        instance = this;
    }

    public void CheckMatch()
    {
        count++;

        if (count == 2)
        {
            if (cards[0].hasRotated && cards[1].hasRotated)
            {
                Debug.Log("Is Matched");
                
                cards[0].gameObject.SetActive(false);
                cards[1].gameObject.SetActive(false);
               
            } 
            else if (cards[1].hasRotated && cards[2].hasRotated)
            {
                Debug.Log("not matched");
                cards[1].FlipBack();
                cards[2].FlipBack();
                
               
            }
            else if (cards[0].hasRotated && cards[3].hasRotated)
            {
                Debug.Log("not matched");
                cards[0].FlipBack();
                cards[3].FlipBack();
                
            }
            else if (cards[2].hasRotated && cards[3].hasRotated)
            {
                Debug.Log("Is Matched");
                
                cards[2].gameObject.SetActive(false);
                cards[3].gameObject.SetActive(false);
            }
            
            count = 0;
        }
    }
    
    
}