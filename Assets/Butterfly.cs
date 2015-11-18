using UnityEngine;
using System;
using System.Collections;

namespace Chrysalis
{
    public class Butterfly : MonoBehaviour
    {
        [SerializeField]
        GameObject Body;

        const float OSCILATE_MAGNITUDE = 0.1f;
        const float OSCILATE_PERIOD = 250f;
        const float GRAVITY = 5f; // units per millisecond
        const float FLAP_ACCEL = 20f; // units per millisecond
        static float FLAP_DURATION = 250f; // milliseconds
        static float FLAP_DELAY = 300f; // milliseconds
        static float CLAMP_Y = 1.5f;

        float initBY;
        float initBX;
        float initBZ;
        float velocity = 0.0f;
        bool facingRight;
        bool flapping = false;
        bool canFlap = true;
        DateTime flapStart;
        DateTime prevTick;

        // Use this for initialization
        void Start()
        {
            Debug.Log("SUP FROM THE BUTTERFLY");
            initBY = Body.transform.localPosition.y;
            initBX = Body.transform.localPosition.x;
            initBZ = Body.transform.localPosition.z;
            transform.localScale = Vector3.one * 3;
        }

        // Update is called once per frame
        void Update()
        {
            var tick = DateTime.Now;
            // girate the butterfly aroun
            var periodicComponent = OSCILATE_MAGNITUDE * Mathf.Sin((float)GameState.sharedInstance.currentTime()
                .TotalMilliseconds / OSCILATE_PERIOD) - 0.5f;

            Body.transform.localPosition = new Vector3(initBX,
                initBY + periodicComponent, initBZ);

            // face left or right 
            if ((!facingRight && Input.GetKeyDown(KeyCode.LeftArrow)) ||
                (facingRight && Input.GetKeyDown(KeyCode.RightArrow)))
            {
                var s = Body.transform.localScale;
                s.x = -s.x;
                Body.transform.localScale = s;
                facingRight = !facingRight;
            }

            var accel = -GRAVITY;
            var now = DateTime.Now;

            // flapping flies up
            if (flapping)
                accel += FLAP_ACCEL;

            // flap for a duration
            if (flapping && (now - flapStart).TotalMilliseconds > FLAP_DURATION)
                flapping = false;

            // only flap every once in a while
            if (!canFlap && (now - flapStart).TotalMilliseconds > FLAP_DELAY) 
                canFlap = true;

            // start flapping
            if (canFlap && Input.GetKeyDown(KeyCode.LeftArrow) ||
                    Input.GetKeyDown(KeyCode.RightArrow))
            {
                flapping = true;
                canFlap = false;
                flapStart = DateTime.Now;
            }

            // calculate accell
            var duration = (tick - prevTick).TotalSeconds;
            if (duration > 1) duration = 0;

            velocity += (float)(accel * duration);

            // calculate position from velocity
            var pos = transform.localPosition;
            pos.y += velocity;

            // clamp by stopping velocity and postion
            if (pos.y > -CLAMP_Y || pos.y < CLAMP_Y)
                velocity = 0;
            pos.y = Mathf.Clamp(pos.y, -CLAMP_Y, CLAMP_Y);

            // set position and move on
            transform.localPosition = pos;
            prevTick = tick;
        }
    }

}
