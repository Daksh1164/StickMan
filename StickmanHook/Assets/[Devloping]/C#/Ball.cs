using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dax
{

    public class Ball : MonoBehaviour
    {
        private Rigidbody2D RB;

        [SerializeField] GameObject Pointsparent;
        private GameObject CurrentPoint;

        private GameObject NearestPOint;
        public List<Vector3> Lines;

        public float JumpForce;



        private void Start()
        {
            RB = GetComponent<Rigidbody2D>();
            Lines[0] = transform.position;
            GetComponent<LineRenderer>().enabled = false;
            CameraScript.instance.Player = this.gameObject;
            RB.gravityScale = 1f;
        }

        private void Update()
        {
            Lines[0] = transform.position;

            if (Input.GetMouseButtonDown(0) && !StaticData.IsLoss && !StaticData.IsWin && !StaticData.IsPaused && UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == null)
            {
                FindNearestPoint().GetComponent<RelativeJoint2D>().connectedBody = RB;
                CurrentPoint = FindNearestPoint();
                Lines[1] = CurrentPoint.transform.position;
                GetComponent<LineRenderer>().enabled = true;
                RB.gravityScale = 3f;
            }

            if (Input.GetMouseButton(0) && !StaticData.IsLoss && !StaticData.IsWin && !StaticData.IsPaused && UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == null)
            {
                //Debug.DrawLine(transform.position, CurrentPoint.transform.position);
                GetComponent<LineRenderer>().SetPositions(Lines.ToArray());
                CurrentPoint.transform.GetChild(0).gameObject.SetActive(true);
            }

            if (Input.GetMouseButtonUp(0) && !StaticData.IsLoss && !StaticData.IsWin && !StaticData.IsPaused && UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == null)
            {
                RemoveRigidBody();
                GetComponent<LineRenderer>().enabled = false;
                CurrentPoint.transform.GetChild(0).gameObject.SetActive(false);
                RB.gravityScale = 1f;
            }

        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Win"))
            {
                Dax.SoundManager.instance.Sound.PlayOneShot(Dax.SoundManager.instance.WinEnter);
                RemoveRigidBody();
                StaticData.IsTouched = true;
                StaticData.IsWin = true;
                StartCoroutine(WinDelay());
            }

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Border"))
            {
                StaticData.IsLoss = true;
                if(StaticData.IsLoss)
                    UIManager.instance.OpenGameLossPanel();
            }
        }

        public GameObject FindNearestPoint()
        {
            float distanceToclosestPoint = Mathf.Infinity;

            foreach (Transform points in Pointsparent.transform)
            {
                float distanceToPoint = (points.transform.position - this.transform.position).sqrMagnitude;

                if (distanceToPoint < distanceToclosestPoint)
                {
                    distanceToclosestPoint = distanceToPoint;

                    NearestPOint = points.gameObject;
                }
            }



            return NearestPOint;
        }

        public void RemoveRigidBody()
        {
            for (int i = 0; i < Pointsparent.transform.childCount; i++)
            {
                Pointsparent.transform.GetChild(i).GetComponent<RelativeJoint2D>().connectedBody = null;
            }
        }

        public IEnumerator WinDelay()
        {
            yield return new WaitForSeconds(0.4f);
            StaticData.IsTouched = false;

            if(StaticData.IsWin)
                UIManager.instance.OpenGameWinPanel();

            Camera.main.orthographicSize = 5f;
        }

    }
}
