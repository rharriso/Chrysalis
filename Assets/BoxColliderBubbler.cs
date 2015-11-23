using UnityEngine;
using System.Collections;

namespace Chrysalis
{

    public delegate void BoxColliderEventHandler(object sender, Collider2D e);

    public class BoxColliderBubbler : MonoBehaviour
    {
        public event BoxColliderEventHandler triggerEntered;
        public event BoxColliderEventHandler triggerExited;

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
            if (triggerEntered != null)
                triggerEntered(this, other);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (triggerExited != null)
                triggerExited(this, other);
        }
    }
}
