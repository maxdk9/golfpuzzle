
using UnityEngine;
[CreateAssetMenu(menuName = "Golfpuzzle/PrefabDatabase", fileName =  "PrefabDatabase")]
public class PrefabDatabase : ScriptableObject
{
    [SerializeField] public GameObject RedExplosionPrefab;
    [SerializeField] public GameObject BlueExplosionPrefab;
    [SerializeField]  public GameObject TutorialPointPrefab;
    [SerializeField] public GameObject TutorialArrowPrefab;
    [SerializeField]public GameObject MessageWindowCommonGO;
}
