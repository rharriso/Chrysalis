using UnityEngine;
using System.Collections;

namespace Chrysalis
{
    public class CameraBounds : MonoBehaviour
    {
        public static Bounds bounds;

        // Use this for initialization
        void Start()
        {
            var vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;
            var horzExtent = vertExtent * Screen.width / Screen.height;

            bounds = new Bounds(transform.position,
                new Vector2(horzExtent, vertExtent));
        }

        /// <summary>
        /// update the center of the camera bounds
        /// </summary>
        void Update()
        {
            bounds.center = transform.position;
        }
    }
}