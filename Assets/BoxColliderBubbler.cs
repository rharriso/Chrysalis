using UnityEngine;
using System.Collections;

namespace Chrysalis
{
    public class BoxColliderBubbler : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Trigger Enter "+other.name);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("Trigger exit" + other.name);
        }
    }
}
