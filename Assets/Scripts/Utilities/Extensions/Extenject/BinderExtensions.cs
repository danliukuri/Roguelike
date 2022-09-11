using System.Collections.Generic;
using Zenject;

namespace Roguelike.Utilities.Extensions.Extenject
{
    public static class BinderExtensions
    {
        #region FromBinder Methods
        public static ScopeConcreteIdArgConditionCopyNonLazyBinder FromMultipleUntyped(this FromBinder binder,
            ICollection<object> objects)
        {
            objects.CheckForNullException(nameof(objects)).CheckForNoElementsException(objects.Count);
            return binder.FromMethodMultipleUntyped(context => objects);
        }
        
        public static ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentOnParentContextGameObject
            (this FromBinder binder) => binder.FromComponentOn(InjectContextExtensions.ParentContextGameObject);
        #endregion
        
        #region ConditionCopyNonLazyBinder Methods
        public static CopyNonLazyBinder WhenParentContextIdEqual<T>(this ConditionCopyNonLazyBinder binder, T id) =>
            binder.When(context => context.IsParentContextIdEqual(id));
        #endregion
    }
}