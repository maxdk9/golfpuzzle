using System;
using UnityEngine;

namespace Tutorial
{
    public class TutorialMonobeh: MonoBehaviour
    {
        private void Start()
        {
            gameObject.tag = "Untagged";
        }


        public static void RemoveAll()
        {
            TutorialMonobeh [] stcomponents = Resources.FindObjectsOfTypeAll<TutorialMonobeh>();
            foreach (TutorialMonobeh sc in stcomponents)
            {
                if (sc.gameObject.tag.Equals("Prefab"))
                {
                    continue;
                }
                
                GameObject.DestroyImmediate(sc.gameObject);
            }   
        }
        
        

    }
}