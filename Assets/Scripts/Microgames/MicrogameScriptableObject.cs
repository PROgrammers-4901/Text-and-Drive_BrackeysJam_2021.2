using UnityEngine;

namespace Microgames
{
    [CreateAssetMenu(fileName = "New Microgame", menuName = "ScriptableObjects/MicroGame", order = 1)]
    public class MicrogameScriptableObject : ScriptableObject
    {
        public string appName;
        public string notificationText;
        public Sprite appLogo;
        public Microgame microgame;
    }
}
