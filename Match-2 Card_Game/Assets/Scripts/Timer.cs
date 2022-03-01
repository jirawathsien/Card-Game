using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI timeText;
   [SerializeField] private float totalTime;
    
   private void Update()
   {
      totalTime -= Time.deltaTime;

      var fromSeconds = TimeSpan.FromSeconds(totalTime);
      timeText.text = $"{fromSeconds.Minutes:00}:{fromSeconds.Seconds:00}";

   }
}
