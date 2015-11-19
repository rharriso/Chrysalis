using UnityEngine;
using System.Collections;

namespace Chrysalis
{
    public class Bootstrap : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            GameState.sharedInstance.start();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
