using Core.Common;
using Core.Common.GameResources;
using Core.Pool;
using TMPro;
using UnityEngine;

public class FlyingMoney : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private SpriteRenderer icon;
    private const float LivingTime = 2;
    private const float SpeedFloat = 0.5f;

    public void InitData(MoneyType moneyType, int value)
    {
        textMesh.text = value.ToString();
        // icon.sprite = LoadResourceController.Instance.LoadIconSprite(ResourceType.Money, (int) moneyType);
        this.StartDelayMethod(LivingTime, () =>
        {
            SmartPool.Instance.Despawn(gameObject);
        });
    }

    private void Update()
    {
        transform.Translate(Vector2.up * SpeedFloat * Time.deltaTime);
    }
}
