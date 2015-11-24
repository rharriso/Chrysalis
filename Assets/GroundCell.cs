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

        int featureCellCount = 3;
        int centerIndex = 1;
        float offset = 6f;
        private bool filled;


        // Use this for initialization
        void Start()
        {
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

            filled = true;
        }

        /// <summary>
        /// Refreshes the cells reloading a new feature
        /// </summary>
        public void Refresh()
        {
            if (!filled) fillWithFeatureCells();

            foreach(var cell in featureCells)
            {
                cell.gameObject.SetActive(true);
                cell.Reload();
            }
        }

        /// <summary>
        /// Sets the active state of all the feature cells
        /// </summary>
        /// <param name="active"></param>
        public void SetFeaturesActive(bool active)
        {
            foreach(var cell in featureCells)
            {
                cell.gameObject.SetActive(active);
            }
        }
    }
}
