using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Challenger5.Gameplay.Utils
{
    public class TextGoldInGame5 : MonoBehaviour
    {
        [SerializeField] private TextMeshPro goldEarnTxt;

        public void InitData(int value)
        {
            goldEarnTxt.text = value.ToString();
            transform.DOMoveY(transform.position.y + 3, 1f);
        }
    }
}
