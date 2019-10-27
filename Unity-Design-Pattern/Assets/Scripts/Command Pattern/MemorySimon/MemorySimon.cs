using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class MemorySimon : MonoBehaviour
{
   public static MemorySimon Instance;
   public List<Action> actions = new List<Action>();

   private void Start()
   {
      Instance = this;
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         StartCoroutine(Replay());
      }
   }

   private void OnGUI()
   {
      GUI.Label(new Rect(Screen.width/2f - 100, Screen.height/2f + 100, 400, 800), "Press Space To Replay");
   }

   public IEnumerator Replay()
   {
      foreach (var action in actions.ToList())
      {
         action.Invoke();
         yield return new WaitForSeconds(0.5f);
      }
   }
}
