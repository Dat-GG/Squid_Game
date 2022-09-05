using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  DataAccount;

namespace UI.WinLose
{


    public class ArrowMove : MonoBehaviour
    {
        public bool isMove = true;
        [SerializeField] private Transform pos1;
        [SerializeField] private Transform pos6;
        [SerializeField] private float speed;
        private int _dir = 1;
        private void OnEnable()
        {
            isMove = true;
        }
        
        private void FixedUpdate()
        {
            Arrow();
        }
        private void Arrow()
        {
            if (!isMove) return;
            var move = Vector2.left * _dir;
            transform.Translate(move * speed);

            if (transform.position.x < pos1.position.x || transform.position.x > pos6.position.x)
            {
                _dir *= -1;
            }
        }
    }
}
