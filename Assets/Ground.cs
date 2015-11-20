using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chrysalis
{
    public class Ground : MonoBehaviour
    {

        [SerializeField]
        GameObject initGroundCell;

        int bufferSize = 5;
        int bufferZero = 2;
        int cellCenter = 0;
        List<GameObject> groundCells = new List<GameObject>();

        
        Vector3 size;
        Vector3 zeroPosition;
        int prevCell = 0;

        // Use this for initialization
        void Start()
        {
            // create three ground cells
            size = initGroundCell.GetComponent<RectTransform>().rect.size;
            zeroPosition = initGroundCell.transform.localPosition;

            for (int i = 0; i < bufferSize; i++)
            {
                GameObject cellObj = initGroundCell;

                // use the existing cell for the center
                var offset = i - bufferZero;
                if (offset != 0)
                    cellObj = GameObject.Instantiate(initGroundCell);

                groundCells.Add(cellObj);
                cellObj.name += " " + i;
                cellObj.transform.SetParent(transform);
                PositionCell(cellObj, offset);
            }
        }

        // Update is called once per frame
        void Update()
        {
            var b = CameraBounds.bounds;
            var normal_x = b.center.x / size.x;

            // calculate what offset cell the gr
            int cell = 0;
            if (normal_x > 0)
                cell = Mathf.FloorToInt(normal_x + 0.5f);
            else
                cell = Mathf.CeilToInt(normal_x - 0.5f);

            // shift cells if needed
            if (cell != prevCell)
            {
                ShiftCells(cell - prevCell);
                prevCell = cell;
            }

        }

        /// <summary>
        /// Shift the ground cells by the passed magnitude 
        /// </summary>
        /// <param name="magnitude"></param>
        void ShiftCells(int magnitude)
        {
            for(int i = 0; i < Mathf.Abs(magnitude); i++)
            {
                if(magnitude > 0)
                {
                    cellCenter++;
                    var cell = groundCells[0];
                    groundCells.RemoveAt(0);
                    groundCells.Add(cell);
                    PositionCell(cell, cellCenter + bufferZero);
                } else
                {
                    cellCenter--;
                    var cell = groundCells[groundCells.Count - 1];
                    groundCells.Remove(cell);
                    groundCells.Insert(0, cell);
                    PositionCell(cell, cellCenter - bufferZero);
                }
            }
        }

        /// <summary>
        /// Position the passed cell given an object
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="position"></param>
        void PositionCell(GameObject cell, int position)
        {
            var pos = zeroPosition;
            pos.x = position * size.x;
            cell.transform.localPosition = pos;
            cell.GetComponent<GroundCell>().refresh();
        }
    }
}