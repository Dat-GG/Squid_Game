using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Challenger4.TugRope
{
    public class Rope : MonoBehaviour
    {
        public Rigidbody2D hook;
        public GameObject[] prefabRopeSegs;
        public GameObject rope;
        public int numLinks;

        private void Start()
        {
            GenerateRope();
            DOVirtual.DelayedCall(2f, delegate
            {
                rope.transform.DORotate(new Vector3(0, 0, 90), 0);
            });
        }

        private void GenerateRope()
        {
            Rigidbody2D prevBod = hook;
            for (int i = 0; i < numLinks; i++)
            {
                int index = Random.Range(0, prefabRopeSegs.Length);
                GameObject newSegs = Instantiate(prefabRopeSegs[index]);
                newSegs.transform.parent = transform;
                newSegs.transform.position = transform.position;
                HingeJoint2D hj = newSegs.GetComponent<HingeJoint2D>();
                hj.connectedBody = prevBod;

                prevBod = newSegs.GetComponent<Rigidbody2D>();
            }
        }
    }
}
