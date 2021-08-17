using System;
using Array2DEditor;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private int defaultGridWidth = 4;
    [SerializeField] private LevelScriptableObject levelScriptableObjectTest;
    
    [Header("References")]
    [SerializeField] private Transform leftWallPositionTransform;
    [SerializeField] private Transform rightWallPositionTransform;
    [SerializeField] private Transform firstRowSpawnHeightPositionTransform;
    [SerializeField] private Transform secondRowSpawnHeightPositionTransform;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject brickPrefab;
    
    private Vector3 _spawnFirstRowHeightPosition;
    private Vector3 _spawnSecondRowHeightPosition;

    private float _leftStartXPosition;
    private float _rightStartXPosition;
    private float _gameFieldWidth;
    private float _rowsDistance;


    private void Start()
    {
        _leftStartXPosition = leftWallPositionTransform.position.x;
        _rightStartXPosition = rightWallPositionTransform.position.x;
        _gameFieldWidth = _rightStartXPosition - _leftStartXPosition;
        _spawnFirstRowHeightPosition = firstRowSpawnHeightPositionTransform.position;
        _spawnSecondRowHeightPosition = secondRowSpawnHeightPositionTransform.position;
        _rowsDistance = Math.Abs(_spawnFirstRowHeightPosition.y) - Math.Abs(_spawnSecondRowHeightPosition.y);
    }

    public void PopulateGrid(Array2DBool brickLayout, GamePlay gamePlayInstance)
    {
        var rowsCount = brickLayout.GridSize.y;
        var columnsCount = brickLayout.GridSize.x;

        var bricksXScale = CalculateBricksWidthScale(columnsCount);

        var brickXDistance = _gameFieldWidth / columnsCount;

        var firstBrickXPosition = _leftStartXPosition + brickXDistance / 2;

        for (var i = 0; i < rowsCount; i++)
        {
            for (var j = 0; j < columnsCount; j++)
            {
                if (!brickLayout.GetCell(j, i)) continue;
                
                var spawnPosition = new Vector3(
                    firstBrickXPosition + brickXDistance * j,
                    _spawnFirstRowHeightPosition.y - _rowsDistance * i,
                    _spawnFirstRowHeightPosition.z);

                var brickGO = Instantiate(brickPrefab, transform);
                brickGO.GetComponent<BrickController>().Constructor(gamePlayInstance);
                
                brickGO.transform.position = spawnPosition;
                var brickScale = brickGO.transform.localScale;
                brickGO.transform.localScale = new Vector3(brickScale.x * bricksXScale, brickScale.y, brickScale.z);
            }
        }
    }

    private float CalculateBricksWidthScale(int columnsCount)
    {
        return (float)defaultGridWidth / (float)columnsCount;
    }
}
