using System;
using System.Collections.Generic;
using UnityEngine;

namespace SP.Item
{
    [Serializable]
    [CreateAssetMenu(fileName = "IconDatabaseSO", menuName = "SP/Item/Icon Database", order = 2)]
    public class IconDatabaseSO : ScriptableObject
    {
        private bool _isInitialized = false;
    
        [SerializeField] private Sprite _defaultIcon = null;
        [SerializeField] private List<UIIconData> _lstIconData = new List<UIIconData>();
        Dictionary<string, UIIconData> _dctIconDatas = new Dictionary<string, UIIconData>();
        Dictionary<int, UIIconData> _dctIconDatasByIndex = new Dictionary<int, UIIconData>();

        public Sprite DefaultIcon => _defaultIcon;
        public List<UIIconData> Database => _lstIconData;

        public Sprite GetIcon(string name)
        {
            InitIfNeeded();
            var data = GetIconData(name);
            return (data == null) ? null : data.Icon;
        }

        public Sprite GetIcon(int index)
        {
            InitIfNeeded();
            if (_dctIconDatasByIndex.TryGetValue(index, out var data))
            {
                return data.Icon;
            }
            return null;
        }

        public UIIconData GetIconData(string name)
        {
            InitIfNeeded();
            foreach (var item in _lstIconData)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }

        private void InitIfNeeded()
        {
            if (!_isInitialized)
            {
                foreach (var item in _lstIconData)
                {
                    _dctIconDatas[item.Name] = item;
                    _dctIconDatasByIndex[item.Index] = item;
                }
                
                _isInitialized = true;
            }
        }
    }
    
    [Serializable]
    public class UIIconData
    {
        public string Name;
        public int Index;
        public Sprite Icon;
    }
}