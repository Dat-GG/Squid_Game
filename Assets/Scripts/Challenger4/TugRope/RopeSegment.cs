using System;
using DG.Tweening;
using UnityEngine;

namespace Challenger4.TugRope
{
    public class RopeSegment : MonoBehaviour
    {
        public GameObject connectedAbove;
        public GameObject connectedBelow;
        
        private void Start()
        {
            connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
            RopeSegment aboveSegment = connectedAbove.GetComponent<RopeSegment>();
            if (aboveSegment != null)
            {
                aboveSegment.connectedBelow = gameObject;
                float spriteBottom = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y;
                GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBottom *-1 / 2);
            }
            else
            {
                GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
            }
        }
    }
}
