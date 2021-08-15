using Array2DEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "Level Data", menuName = "Levels/Level Data")]
public class LevelScriptableObject : ScriptableObject
{
    public uint bricksCount;
    public float ballSpeed;
    public Array2DBool brickLayout;
}
