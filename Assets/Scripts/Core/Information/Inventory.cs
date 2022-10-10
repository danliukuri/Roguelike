using System;
using Roguelike.Core.Characteristics;

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
        
        #region Events
        public event Action<int> NumberOfKeysChanged;
        #endregion
        
        #region Fields
        int numberOfKeys;
        #endregion
        
        #region Methods
        public void Reset() => NumberOfKeys = default;
        #endregion
    }
}