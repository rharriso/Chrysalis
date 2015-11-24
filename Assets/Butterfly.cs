using UnityEngine;
using System;
using System.Collections;

namespace Chrysalis
{
    public delegate void ButterflyEventHandler(object sender, EventArgs e);

    public class Butterfly : MonoBehaviour
    {
        enum FlapDirection { Right, Left, Down }

        [SerializeField]
        BoxColliderBubbler Body;

        const float OSCILATE_MAGNITUDE = 0.1f;
        const float OSCILATE_PERIOD = 250f;
        const float GRAVITY = 0.1f; // units per millisecond
        const float FLAP_ACCEL = 1f; // units per millisecond
        static float FLAP_DURATION = 50f; // milliseconds
        static float FLAP_DELAY = 25f; // milliseconds
        static float MAX_Y = 6f;
        static float MIN_Y = -1.5f;
        static float MAX_VY = 0.15f; // maximum y velocity
        static float MAX_VX = 0.075f; // maximum X velocity
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

        // listen to the butterfly dying
        public event ButterflyEventHandler Died;

        /// <summary>
        /// Reset the butterfly position
        /// </summary>
        public void Reset()
        {
            velocity_x = 0;
            velocity_y = 0;
            flapping = false;
            transform.localPosition = new Vector3(0, 0, -5f);
        }

        // Use this for initialization
        void Start()
        {
            Debug.Log("SUP FROM THE BUTTERFLY");
            initBY = Body.transform.localPosition.y;
            initBX = Body.transform.localPosition.x;
            initBZ = Body.transform.localPosition.z;
            transform.localScale = Vector3.one * 3;

            Body.triggerEntered +=
                new BoxColliderEventHandler(OnBodyTriggerEntered);
        }

        /// <summary>
        /// Remove event listeners on detach
        /// </summary>
        public void Detach()
        {
            Body.triggerEntered -=
                new BoxColliderEventHandler(OnBodyTriggerEntered);
        }

        // Update is called once per frame
        void Update()
        {
            if (GameState.Current != GameStateOption.playing) return;

            // girate the butterfly aroun
            var periodicComponent = OSCILATE_MAGNITUDE * Mathf.Sin(Time.fixedTime / OSCILATE_PERIOD) - 0.5f;

            Body.transform.localPosition = new Vector3(initBX,
                initBY + periodicComponent, initBZ);

            // go right or left
            if (Input.GetKeyDown(KeyCode.RightArrow)) flap(FlapDirection.Right);
            if (Input.GetKeyDown(KeyCode.DownArrow)) flap(FlapDirection.Down);
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) flap(FlapDirection.Left);
            // tab left or right
            if (Input.touchCount == 1)
            {
                var thirdScreen = Screen.width / 3;
                var touch = Input.touches[0];
                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x > thirdScreen)
                        flap(FlapDirection.Right);
                    else if (touch.position.x > Screen.width - thirdScreen)
                        flap(FlapDirection.Down);
                    else
                        flap(FlapDirection.Left);
                }
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


            // calculate accell
            var duration = Time.deltaTime;
            if (duration > 1) duration = 0;

            velocity_y += (float)(accel * duration);
            velocity_y = Mathf.Clamp(velocity_y, MIN_Y, MAX_VY);

            // calculate position from velocity
            var pos = transform.localPosition;
            pos.y += velocity_y;
            pos.x += velocity_x;

            // clamp by stopping velocity and postion
            if (pos.y > MAX_Y || pos.y < MIN_Y)
                velocity_y = 0;
            pos.y = Mathf.Clamp(pos.y, MIN_Y, MAX_Y);

            // set position and move on
            transform.localPosition = pos;
        }

        /// <summary>
        /// flap makes the butterfly flap, defaults to the right
        /// </summary>
        /// <param name="dir">Direction of the swipe</param>
        private void flap(FlapDirection dir)
        {
            if (!canFlap) return;
            flapping = true;
            canFlap = false;
            flapStart = DateTime.Now;

            // turn the bird based on direction pressed
            var s = transform.localScale;

            switch (dir)
            {
                case FlapDirection.Right:
                    velocity_x += DELTA_VX;
                    s.x = Mathf.Abs(s.x);
                    break;
                case FlapDirection.Down:
                    velocity_x = 0;
                    velocity_y = 0;
                    flapping = false;
                    break;
                case FlapDirection.Left:
                    velocity_x -= DELTA_VX;
                    s.x = -Mathf.Abs(s.x);
                    break;
            }

            transform.localScale = s;
            velocity_x = Mathf.Clamp(velocity_x, -MAX_VX, MAX_VX);
        }

        /// <summary>
        /// Listener for body box collition 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="other"></param>
        void OnBodyTriggerEntered(object sender, Collider2D other)
        {
            switch (other.name)
            {
                case "Spiderweb":
                case "Branch":
                    Die(EventArgs.Empty);
                    break;
            }
        }

        /// <summary>
        /// Kill the butterfly
        /// </summary>
        void Die(EventArgs e)
        {
            if (Died != null)
                Died(this, e);
        }
    }
}
