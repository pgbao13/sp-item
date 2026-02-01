using UnityEngine;
using TMPro;

namespace SP.Item
{
    public class UIItem : MonoBehaviour
    {
        [SerializeField] private UIIcon _icon = null;
        [SerializeField] private TextMeshProUGUI _txtQuantity = null;
        [SerializeField] private string _strQuatityPrefix = "x";
        [SerializeField] private string _strQuantitySuffix = "";
        [SerializeField] private int _showQuantityThreshold = 1;
        
        [Header("Setting")]
        [SerializeField] private bool _useQuantityThreshold = true;
        [SerializeField] private bool _quantityFormatMoney = false;

        public string Id { get; private set; }
        public double Quantity { get; private set; }
        public UIIcon Icon => _icon;
        public TextMeshProUGUI TxtQuantity => _txtQuantity;
        public string StrQuatityPrefix => _strQuatityPrefix;
        public string StrQuantitySuffix => _strQuantitySuffix;

        public void Setup(ItemModel info, bool forceUsePrefix = false, bool forceUseSuffix = false)
        {
            gameObject.SetActive(true);
            Id = info.Id;
            Quantity = info.Quantity;

            _icon.Setup(Id);
            SetupQuantity(forceUsePrefix, forceUseSuffix);
        }

        private void SetupQuantity(bool forceUsePrefix = false, bool forceUseSuffix = false)
        {
            if (_txtQuantity == null)
                return;
            
            string strQuantity = (_quantityFormatMoney) ? Quantity.ToString("###,###,##0") : Quantity.ToString();
            if (forceUsePrefix)
                strQuantity = _strQuatityPrefix + strQuantity;
            if (forceUseSuffix)
                strQuantity = strQuantity + _strQuantitySuffix;
            _txtQuantity.text = strQuantity;

            if (_useQuantityThreshold)
                _txtQuantity.gameObject.SetActive(Quantity >= _showQuantityThreshold);
            else
                _txtQuantity.gameObject.SetActive(true);
        }
    }
}