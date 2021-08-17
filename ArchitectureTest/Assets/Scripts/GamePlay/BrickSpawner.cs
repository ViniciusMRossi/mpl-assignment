using System;
using Array2DEditor;
using UnityEngine;

namespace GamePlay
{
    public class BrickSpawner : MonoBehaviour
    {
        [Header("Values")]
        [Tooltip("How many columns of the standard brick prefab can the Playing Field accomodate")]
        [SerializeField] private int defaultGridWidth = 4;
    
        //I like to use these kinds of position by reference to make it easier to modify sizes and positions
        [Header("References")]
        [Tooltip("A transform with position on the left playing filed wall")]
        [SerializeField] private Transform leftWallPositionTransform;
        [Tooltip("A transform with position on the right playing filed wall")]
        [SerializeField] private Transform rightWallPositionTransform;
        [Tooltip("A transform with position on the first row of bricks height")]
        [SerializeField] private Transform firstRowSpawnHeightPositionTransform;
        [Tooltip("A transform with position on the second row of bricks height")]
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

        public void PopulateGrid(Array2DBool brickLayout, Game gameInstance)
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
                    brickGO.GetComponent<BrickController>().Constructor(gameInstance);
                
                    brickGO.transform.position = spawnPosition;
                    var brickScale = brickGO.transform.localScale;
                    brickGO.transform.localScale = new Vector3(brickScale.x * bricksXScale, brickScale.y, brickScale.z);
                }
            }
        }
        
        //Bricks need to be bigger or smaller depending on the column count
        private float CalculateBricksWidthScale(int columnsCount)
        {
            return (float)defaultGridWidth / (float)columnsCount;
        }
    }
}
