using UnityEngine;
using Zenject;

namespace Roguelike.Utilities.Extensions.Extenject
{
    public static class InjectContextExtensions
    {
        #region Methods
        public static bool IsParentContextIdEqual<T>(this InjectContext context, T id)
        {
            id.CheckForNullException(nameof(id));
            InjectContext parentContext = context.ParentContext.CheckForNullException(nameof(context.ParentContext));
            return parentContext.IsContextIdEqualCheckedId(id);
        }
        public static bool IsContextIdEqual<T>(this InjectContext context, T id) =>
            context.IsContextIdEqualCheckedId(id.CheckForNullException(nameof(id)));
        static bool IsContextIdEqualCheckedId<T>(this InjectContext context, T id) => Equals(context.Identifier, id);
        
        public static GameObject ParentContextGameObject(this InjectContext context) =>
            ((Component)context.ParentContext.ObjectInstance).gameObject;
        #endregion
    }
}