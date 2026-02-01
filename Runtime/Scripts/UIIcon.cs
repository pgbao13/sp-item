using UnityEngine;
using UnityEngine.UI;

namespace SP.Item
{
    public class UIIcon : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private IconDatabaseSO _iconDatabase;

        public Image Image => _image;

        public void Setup(string iconName)
        {
            if (!ValidateSetup())
                return;

            Sprite icon = _iconDatabase.GetIcon(iconName);
            SetIconSprite(icon, iconName);
        }

        public void Setup(int iconIndex)
        {
            if (!ValidateSetup())
                return;

            Sprite icon = _iconDatabase.GetIcon(iconIndex);
            SetIconSprite(icon, $"index: {iconIndex}");
        }

        private bool ValidateSetup()
        {
            if (_iconDatabase == null)
            {
                Debug.LogError("[IconUI] IconDatabase is not assigned!");
                return false;
            }

            if (_image == null)
            {
                Debug.LogError("[IconUI] Image component is not assigned!");
                return false;
            }

            return true;
        }

        private void SetIconSprite(Sprite icon, string debugInfo = "")
        {
            if (icon != null)
            {
                _image.sprite = icon;
            }
            else
            {
                // Use default icon if not found
                Sprite defaultIcon = _iconDatabase.DefaultIcon;
                if (defaultIcon != null)
                {
                    _image.sprite = defaultIcon;
                    Debug.LogWarning($"[IconUI] Icon not found: {debugInfo}. Using default icon instead.");
                }
                else
                {
                    Debug.LogError($"[IconUI] Icon not found: {debugInfo}. No default icon available!");
                }
            }
        }
    }
}
