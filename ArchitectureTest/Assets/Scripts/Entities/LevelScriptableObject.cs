using Array2DEditor;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "Levels/Level Data")]
    public class LevelScriptableObject : ScriptableObject
    {
        public uint bricksCount;
        public uint livesCount;
        public float ballSpeed;
        public float playerSpeed;
        public Array2DBool brickLayout;
    }
}
