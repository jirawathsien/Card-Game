using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class PlayerHealthTimer : MonoBehaviour
{
   [SerializeField] private ProceduralImage proceduralImage;
   
   private void Update()
   {
      proceduralImage.fillAmount -= Time.deltaTime / 60f;

      if (proceduralImage.fillAmount <= 0.05f && !CardManager.instance.isGameOver)
      {
         CardManager.instance.isGameOver = true;
         CardManager.instance.GameOver();
      }
      
   }
}
