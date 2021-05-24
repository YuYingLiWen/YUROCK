
#if UNITY_ANDROID || UNITY_IOS
using System;
#endif

using UnityEngine;
using UnityEngine.EventSystems;

namespace YuSystem.Gameplay
{
    public class InGamePointer : MonoBehaviour
    {
        private static InGamePointer instance;

        public static InGamePointer Instance
        {
            get
            {
                if (!instance) instance = FindObjectOfType<InGamePointer>();
                return instance;
            }
        }

        private const float distanceFromCam = 10f;
        private GameObject visual;
        private TrailRenderer trailR;
        private Vector3 touchPos;
#if UNITY_EDITOR
        private Ray r;
#endif

        public Vector3 Position => transform.position;

#if UNITY_ANDROID || UNITY_IOS
        private void Awake()
        {
            trailR = GetComponentInChildren<TrailRenderer>();
            visual = transform.GetChild(0).gameObject;
        }

        private void Start()
        {
            trailR.startWidth = transform.localScale.x * 0.5f;
            trailR.endWidth = 0f;

            touchPos.z = 20f;
        }
#endif

        private void Update()
        {


#if UNITY_EDITOR
            r = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(r.origin, r.direction * 200f, Color.white, 0.01f);
#endif

#if UNITY_STANDALONE || UNITY_EDITOR
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCam));
#endif

#if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount <= 0
                || (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))
            {
                if (visual.activeSelf) visual.SetActive(false);
                return;
            }
            if (!visual.activeSelf) visual.SetActive(true);

            //Horizontal Movements
            touchPos.x = Input.GetTouch(0).position.x;
            touchPos.y = Input.GetTouch(0).position.y;
            transform.position = Camera.main.ScreenToWorldPoint(touchPos);
#endif
        }
    }
}

