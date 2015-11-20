using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chrysalis
{
    public class GroundCell : MonoBehaviour
    {
        [SerializeField]
        FeatureCell sourceCell;
        List<FeatureCell> featureCells = new List<FeatureCell>();

        Vector3 featureCellSize;
        Vector3 featureCellPos;
        Vector3 size;
        int featureCellCount = 3;
        int centerIndex = 1;
        float offset = 6f;


        // Use this for initialization
        void Start()
        {
            size = GetComponent<RectTransform>().rect.size;
            featureCellPos = sourceCell.transform.localPosition;
            featureCellSize = sourceCell.GetComponent<RectTransform>().rect.size;
            fillWithFeatureCells();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Fills the cell with feature cells
        /// </summary>
        void fillWithFeatureCells() {
            for(int i = 0; i < featureCellCount; i++)
            {
                var cell = sourceCell;
                if (i != centerIndex) cell = GameObject.Instantiate(cell);

                cell.transform.SetParent(transform);
                var pos = new Vector3(offset * (i - centerIndex), 3, 0);
                cell.transform.localPosition = pos;
                featureCells.Add(cell);
            }
        }

        public void refresh()
        {
            foreach(var cell in featureCells)
            {
                cell.Reload();
            }
        }
    }
}
