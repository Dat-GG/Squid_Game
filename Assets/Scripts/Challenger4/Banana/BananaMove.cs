using System.Collections;
using UnityEngine;

namespace Challenger4.Banana
{
    public class BananaMove : MonoBehaviour
    {
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private float duration;
        [SerializeField] private float heightY;
        public IEnumerator Curve(Vector3 start, Vector2 target)
        {
            float timePassed = 0;
            Vector2 end = target;

            while (timePassed < duration)
            {
                timePassed += Time.deltaTime;
                float linearT = timePassed / duration;
                float heightT = curve.Evaluate(linearT);
                float height = Mathf.Lerp(0, heightY, heightT);
                transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0, height);
                yield return null;
            }
        }
    }
}
