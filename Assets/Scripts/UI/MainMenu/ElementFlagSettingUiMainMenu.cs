using UnityEngine;

namespace UI.MainMenu
{
    public class ElementFlagSettingUiMainMenu : MonoBehaviour
    {
        [SerializeField] private string country;
        protected internal string Country => country;
    }
}