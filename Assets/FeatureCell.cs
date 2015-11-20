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
        GameObject Branch;

        List<GameObject> options = new List<GameObject>();

        GameObject currOption;

        // Use this for initialization
        void Start()
        {
            options.Add(Tree);
            options.Add(Flower);
            options.Add(SpiderWeb);
            options.Add(Branch);
            Reload();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// reload items in the scene
        /// </summary>
        public void Reload() {
            foreach (var o in options)
                o.SetActive(false);

            // select random options for activations
            var prob = Random.value;

            if(prob < 0.25f)
                currOption = Tree;
            else if(prob < 0.5f)
                currOption = Flower;
            else if(prob < 0.75f)
                currOption = SpiderWeb;
            else
                currOption = Branch;

            currOption.SetActive(true) ;
        }
    }


}