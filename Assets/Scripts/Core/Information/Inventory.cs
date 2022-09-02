using System;

namespace Roguelike.Core.Information
{
    public class Inventory : IResettable
    {
        #region Properties
        public int NumberOfKeys
        {
            get => numberOfKeys;
            set
            {
                numberOfKeys = value;
                NumberOfKeysChanged?.Invoke(value);
            }
        }
        #endregion
        
        #region Fields
        public event Action<int> NumberOfKeysChanged;
        int numberOfKeys;
        #endregion
        
        #region Methods
        public void Reset() => NumberOfKeys = default;
        #endregion
    }
}