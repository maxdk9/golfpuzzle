using Assets.SimpleLocalization;
using UnityEngine;
using UnityEngine.UI;


    [CreateAssetMenu(menuName = "Golfpuzzle/TutorialPointData", fileName =  "TutorialPointData")]
    public class TutorialPointData : ScriptableObject
    {
        public int levelNumber;
        public string MessageKey;
        public Vector2 position;
        public string TutorialImageName;
    }
