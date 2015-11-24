using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chrysalis
{
    public class FeatureCell : MonoBehaviour
    {
        [SerializeField]
        GameObject Tree;
        [SerializeField]
        GameObject Flower;
        [SerializeField]
        GameObject SpiderWeb;
        [SerializeField]
        GameObject SpiderWebFloat;
        [SerializeField]
        GameObject Branch;

        List<GameObject> options = new List<GameObject>();

        GameObject currOption;

        // Use this for initialization
        void Start()
        {
            options.Add(Tree);
            options.Add(Flower);
            options.Add(SpiderWeb);
            options.Add(SpiderWebFloat);
            options.Add(Branch);
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// reload items in the scene
        /// </summary>
        public void Reload() {
            HideAllFeatures();
            // select random options for activations
            var prob = Random.value;

            if(prob < 0.15f)
                currOption = Tree;
            else if(prob < 0.40f)
                currOption = Flower;
            else if(prob < 0.65f)
                currOption = SpiderWeb;
            else if(prob < 0.85f)
                currOption = SpiderWebFloat;
            else
                currOption = Branch;

            currOption.SetActive(true) ;
        }

        /// <summary>
        /// Hides all the child features
        /// </summary>
        void HideAllFeatures()
        {
            foreach (var o in options)
                o.SetActive(false);
        }
    }
}