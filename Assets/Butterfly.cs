using UnityEngine;
using System;
using System.Collections;

namespace Chrysalis
{
    public class Butterfly : MonoBehaviour
    {
        enum FlapDirection { Right, Left }

        [SerializeField]
        GameObject Body;

        const float OSCILATE_MAGNITUDE = 0.1f;
        const float OSCILATE_PERIOD = 250f;
        const float GRAVITY = 5f; // units per millisecond
        const float FLAP_ACCEL = 20f; // units per millisecond
        static float FLAP_DURATION = 150f; // milliseconds
        static float FLAP_DELAY = 50f; // milliseconds
        static float CLAMP_Y = 1.5f;
        static float MAX_VY = 1.5f; // maximum y velocity
        static float MAX_VX = 0.1f; // maximum X velocity
        static float DELTA_VX = 0.05f; // maximum X velocity

        float initBY;
        float initBX;
        float initBZ;
        float velocity_y = 0.0f;
        float velocity_x = 0.0f;
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

            // go right or left
            if (Input.GetKeyDown(KeyCode.RightArrow)) flap(FlapDirection.Right);
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) flap(FlapDirection.Left);

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


            // calculate accell
            var duration = (tick - prevTick).TotalSeconds;
            if (duration > 1) duration = 0;

            velocity_y += (float)(accel * duration);

            // calculate position from velocity
            var pos = transform.localPosition;
            pos.y += velocity_y;
            pos.x += velocity_x;

            // clamp by stopping velocity and postion
            if (pos.y > -CLAMP_Y || pos.y < CLAMP_Y)
                velocity_y = 0;
            pos.y = Mathf.Clamp(pos.y, -CLAMP_Y, 3 * CLAMP_Y);

            // set position and move on
            transform.localPosition = pos;
            prevTick = tick;
        }

        /// <summary>
        /// flap makes the butterfly flap, defaults to the right
        /// </summary>
        /// <param name="rightwards"></param>
        private void flap(FlapDirection dir)
        {
            if (!canFlap) return;
            var rightwards = dir == FlapDirection.Right;
            flapping = true;
            canFlap = false;
            flapStart = DateTime.Now;

            // turn the bird based on direction pressed
            var s = transform.localScale;
            s.x = Mathf.Abs(s.x) * (rightwards ? 1f : -1f);
            transform.localScale = s;

            // adjust x velocity
            velocity_x += rightwards ? DELTA_VX : -DELTA_VX;
            velocity_x = Mathf.Clamp(velocity_x, -MAX_VX, MAX_VX);
        }

/// <summary>
/// Colotion started, handle for different objects
/// </summary>
/// <param name="collision"></param>
        void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision.gameObject.name);
        }
    }
}
