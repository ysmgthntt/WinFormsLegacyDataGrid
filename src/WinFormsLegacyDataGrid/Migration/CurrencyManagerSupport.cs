using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WinFormsLegacyDataGrid.Migration
{
    internal static class CurrencyManagerSupport
    {
        internal static object? Get(this CurrencyManager currencyManager, int index)
        {
            IList list = currencyManager.List;
            if (index < 0 || index >= list.Count)
                throw new IndexOutOfRangeException();
            return list[index];
        }

        [UnsafeAccessor(UnsafeAccessorKind.Method, Name = nameof(GetListName))]
        private static extern string GetListNameInternal(CurrencyManager currencyManager);

        internal static string GetListName(this CurrencyManager currencyManager)
            => GetListNameInternal(currencyManager);

        internal static bool GetAllowAdd(this CurrencyManager currencyManager)
            => currencyManager.List switch
            {
                null => false,
                IBindingList bindingList => bindingList.AllowNew,
                var list => !list.IsReadOnly && !list.IsFixedSize
            };

        internal static bool GetAllowEdit(this CurrencyManager currencyManager)
            => currencyManager.List switch
            {
                null => false,
                IBindingList bindingList => bindingList.AllowEdit,
                var list => !list.IsReadOnly
            };

        internal static bool GetAllowRemove(this CurrencyManager currencyManager)
            => currencyManager.List switch
            {
                null => false,
                IBindingList bindingList => bindingList.AllowRemove,
                var list => !list.IsReadOnly && !list.IsFixedSize
            };

        internal static ListSortDirection GetSortDirection(this CurrencyManager currencyManager)
            => currencyManager.List switch
            {
                IBindingList { SupportsSorting: true } bindingList => bindingList.SortDirection,
                _ => ListSortDirection.Ascending
            };

        internal static PropertyDescriptor? GetSortProperty(this CurrencyManager currencyManager)
            => currencyManager.List switch
            {
                IBindingList { SupportsSorting: true } bindingList => bindingList.SortProperty,
                _ => null
            };

        internal static void SetSort(this CurrencyManager currencyManager, PropertyDescriptor property, ListSortDirection sortDirection)
        {
            if (currencyManager.List is IBindingList { SupportsSorting: true } bindingList)
                bindingList.ApplySort(property, sortDirection);
        }
    }
}
