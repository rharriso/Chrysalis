using UnityEngine;
using System.Collections;

namespace Chrysalis
{
    public class FollowButterFly : MonoBehaviour
    {

        [SerializeField]
        GameObject Butterfly;
        const float trackingSpeed = 10f;
        const float zoomSpeed = 5f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            var pos = transform.localPosition;
            pos.x = Butterfly.transform.position.x;
            transform.position = pos;
        }
    }
}