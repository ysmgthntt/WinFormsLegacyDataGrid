using System;
using System.Resources;
using System.Runtime.Serialization;
using System.Windows.Forms;

internal static class SR
{
    private static ResourceManager? _resourceManager;

    internal static string GetResourceString(string value)
    {
        _resourceManager ??= new ResourceManager("System.SR", typeof(Control).Assembly);
        return _resourceManager.GetString(value) ?? value;
    }

    //
    internal static string CatAction => GetResourceString(nameof(CatAction));
    internal static string CatAppearance => GetResourceString(nameof(CatAppearance));
    internal static string CatBehavior => GetResourceString(nameof(CatBehavior));
    internal static string CatColors => GetResourceString(nameof(CatColors));
    internal static string CatData => GetResourceString(nameof(CatData));
    internal static string CatDisplay => GetResourceString(nameof(CatDisplay));
    internal static string CatLayout => GetResourceString(nameof(CatLayout));
    internal static string CatMouse => GetResourceString(nameof(CatMouse));
    internal static string CatPropertyChanged => GetResourceString(nameof(CatPropertyChanged));
    internal static string CatWindowStyle => GetResourceString(nameof(CatWindowStyle));
    //
    internal static string BadDataSourceForComplexBinding => nameof(BadDataSourceForComplexBinding);
    internal static string ControlBackColorDescr=>nameof(ControlBackColorDescr);
    internal static string ControlForeColorDescr => nameof(ControlForeColorDescr);

    internal static string DataGridAllowSortingDescr => nameof(DataGridAllowSortingDescr);
    internal static string DataGridAlternatingBackColorDescr => nameof(DataGridAlternatingBackColorDescr);
    internal static string DataGridBackButtonClickDescr => nameof(DataGridBackButtonClickDescr);
    internal static string DataGridBackgroundColorDescr => nameof(DataGridBackgroundColorDescr);
    internal static string DataGridBoolColumnAllowNullValue => nameof(DataGridBoolColumnAllowNullValue);
    internal static string DataGridBorderStyleDescr => nameof(DataGridBorderStyleDescr);
    internal static string DataGridBeginInit => nameof(DataGridBeginInit);

    internal static string DataGridCaptionBackButtonToolTip => nameof(DataGridCaptionBackButtonToolTip);
    internal static string DataGridCaptionBackColorDescr => nameof(DataGridCaptionBackColorDescr);
    internal static string DataGridCaptionFontDescr => nameof(DataGridCaptionFontDescr);
    internal static string DataGridCaptionForeColorDescr => nameof(DataGridCaptionForeColorDescr);
    internal static string DataGridCaptionTextDescr => nameof(DataGridCaptionTextDescr);
    internal static string DataGridCaptionVisibleDescr => nameof(DataGridCaptionVisibleDescr);
    internal static string DataGridCaptionDetailsButtonToolTip => nameof(DataGridCaptionDetailsButtonToolTip);

    internal static string DataGridColumnWidth => nameof(DataGridColumnWidth);
    internal static string DataGridColumnHeadersVisibleDescr => nameof(DataGridColumnHeadersVisibleDescr);
    internal static string DataGridColumnListManagerPosition => nameof(DataGridColumnListManagerPosition);
    internal static string DataGridColumnNoPropertyDescriptor => nameof(DataGridColumnNoPropertyDescriptor);
    internal static string DataGridColumnUnbound => nameof(DataGridColumnUnbound);
    internal static string DataGridCurrentCellDescr => nameof(DataGridCurrentCellDescr);
    internal static string DataGridColumnStyleDuplicateMappingName => nameof(DataGridColumnStyleDuplicateMappingName);
    internal static string DataGridColumnCollectionMissing => nameof(DataGridColumnCollectionMissing);

    internal static string DataGridDefaultColumnCollectionChanged => nameof(DataGridDefaultColumnCollectionChanged);
    internal static string DataGridDefaultTableSet => nameof(DataGridDefaultTableSet);
    internal static string DataGridDataSourceDescr => nameof(DataGridDataSourceDescr);
    internal static string DataGridDownButtonClickDescr => nameof(DataGridDownButtonClickDescr);
    internal static string DataGridDataMemberDescr => nameof(DataGridDataMemberDescr);

    internal static string DataGridEmptyColor => nameof(DataGridEmptyColor);
    internal static string DataGridExceptionInPaint => nameof(DataGridExceptionInPaint);
    internal static string DataGridErrorMessageBoxCaption => nameof(DataGridErrorMessageBoxCaption);

    internal static string DataGridFlatModeDescr => nameof(DataGridFlatModeDescr);
    internal static string DataGridFirstVisibleColumnDescr => nameof(DataGridFirstVisibleColumnDescr);

    internal static string DataGridGridLineColorDescr => nameof(DataGridGridLineColorDescr);
    internal static string DataGridGridTablesDescr => nameof(DataGridGridTablesDescr);
    internal static string DataGridGridLineStyleDescr => nameof(DataGridGridLineStyleDescr);

    internal static string DataGridHeaderBackColorDescr => nameof(DataGridHeaderBackColorDescr);
    internal static string DataGridHeaderFontDescr => nameof(DataGridHeaderFontDescr);
    internal static string DataGridHeaderForeColorDescr => nameof(DataGridHeaderForeColorDescr);
    internal static string DataGridHorizScrollBarDescr => nameof(DataGridHorizScrollBarDescr);

    internal static string DataGridLinkColorDescr => nameof(DataGridLinkColorDescr);
    internal static string DataGridListManagerDescr => nameof(DataGridListManagerDescr);
    internal static string DataGridLinkHoverColorDescr => nameof(DataGridLinkHoverColorDescr);

    internal static string DataGridNavigateEventDescr => nameof(DataGridNavigateEventDescr);
    internal static string DataGridNavigationModeDescr => nameof(DataGridNavigationModeDescr);
    internal static string DataGridNodeClickEventDescr => nameof(DataGridNodeClickEventDescr);
    internal static string DataGridNullText => nameof(DataGridNullText);

    internal static string DataGridOnDataSourceChangedDescr => nameof(DataGridOnDataSourceChangedDescr);
    internal static string DataGridOnParentRowsVisibleChangedDescr => nameof(DataGridOnParentRowsVisibleChangedDescr);
    internal static string DataGridOnCaptionVisibleChangedDescr => nameof(DataGridOnCaptionVisibleChangedDescr);
    internal static string DataGridOnFlatModeChangedDescr => nameof(DataGridOnFlatModeChangedDescr);
    internal static string DataGridOnParentRowsLabelStyleChangedDescr => nameof(DataGridOnParentRowsLabelStyleChangedDescr);
    internal static string DataGridOnReadOnlyChangedDescr => nameof(DataGridOnReadOnlyChangedDescr);
    internal static string DataGridOnBackgroundColorChangedDescr => nameof(DataGridOnBackgroundColorChangedDescr);
    internal static string DataGridOnBorderStyleChangedDescr => nameof(DataGridOnBorderStyleChangedDescr);
    internal static string DataGridOnCurrentCellChangedDescr => nameof(DataGridOnCurrentCellChangedDescr);
    internal static string DataGridOnNavigationModeChangedDescr => nameof(DataGridOnNavigationModeChangedDescr);

    internal static string DataGridPushedIncorrectValueIntoColumn => nameof(DataGridPushedIncorrectValueIntoColumn);
    internal static string DataGridParentRowsForeColorDescr => nameof(DataGridParentRowsForeColorDescr);
    internal static string DataGridPreferredColumnWidthDescr => nameof(DataGridPreferredColumnWidthDescr);
    internal static string DataGridPreferredRowHeightDescr => nameof(DataGridPreferredRowHeightDescr);
    internal static string DataGridParentRowsBackColorDescr => nameof(DataGridParentRowsBackColorDescr);
    internal static string DataGridParentRowsLabelStyleDescr => nameof(DataGridParentRowsLabelStyleDescr);
    internal static string DataGridParentRowsVisibleDescr => nameof(DataGridParentRowsVisibleDescr);

    internal static string DataGridRowRowHeight => nameof(DataGridRowRowHeight);
    internal static string DataGridReadOnlyDescr => nameof(DataGridReadOnlyDescr);
    internal static string DataGridRowHeadersVisibleDescr => nameof(DataGridRowHeadersVisibleDescr);
    internal static string DataGridRowHeaderWidthDescr => nameof(DataGridRowHeaderWidthDescr);

    internal static string DataGridSetListManager => nameof(DataGridSetListManager);
    internal static string DataGridSetSelectIndex => nameof(DataGridSetSelectIndex);
    internal static string DataGridSettingCurrentCellNotGood => nameof(DataGridSettingCurrentCellNotGood);
    internal static string DataGridSelectionBackColorDescr => nameof(DataGridSelectionBackColorDescr);
    internal static string DataGridScrollEventDescr => nameof(DataGridScrollEventDescr);
    internal static string DataGridSelectedIndexDescr => nameof(DataGridSelectedIndexDescr);
    internal static string DataGridSelectionForeColorDescr => nameof(DataGridSelectionForeColorDescr);

    internal static string DataGridTableStyleTransparentBackColorNotAllowed => nameof(DataGridTableStyleTransparentBackColorNotAllowed);
    internal static string DataGridTableStyleTransparentAlternatingBackColorNotAllowed => nameof(DataGridTableStyleTransparentAlternatingBackColorNotAllowed);
    internal static string DataGridTableStyleTransparentHeaderBackColorNotAllowed => nameof(DataGridTableStyleTransparentHeaderBackColorNotAllowed);
    internal static string DataGridTableStyleTransparentSelectionBackColorNotAllowed => nameof(DataGridTableStyleTransparentSelectionBackColorNotAllowed);
    internal static string DataGridTableCollectionMissingTable => nameof(DataGridTableCollectionMissingTable);
    internal static string DataGridTableStyleCollectionAddedParentedTableStyle => nameof(DataGridTableStyleCollectionAddedParentedTableStyle);
    internal static string DataGridTableStyleDuplicateMappingName => nameof(DataGridTableStyleDuplicateMappingName);
    internal static string DataGridToolTipEmptyIcon => nameof(DataGridToolTipEmptyIcon);

    internal static string DataGridTransparentAlternatingBackColorNotAllowed => nameof(DataGridTransparentAlternatingBackColorNotAllowed);
    internal static string DataGridTransparentHeaderBackColorNotAllowed => nameof(DataGridTransparentHeaderBackColorNotAllowed);
    internal static string DataGridTransparentParentRowsBackColorNotAllowed => nameof(DataGridTransparentParentRowsBackColorNotAllowed);
    internal static string DataGridTransparentSelectionBackColorNotAllowed => nameof(DataGridTransparentSelectionBackColorNotAllowed);
    internal static string DataGridTransparentBackColorNotAllowed => nameof(DataGridTransparentBackColorNotAllowed);
    internal static string DataGridTransparentCaptionBackColorNotAllowed => nameof(DataGridTransparentCaptionBackColorNotAllowed);

    internal static string DataGridUnbound => nameof(DataGridUnbound);

    internal static string DataGridVertScrollBarDescr => nameof(DataGridVertScrollBarDescr);
    internal static string DataGridVisibleColumnCountDescr => nameof(DataGridVisibleColumnCountDescr);
    internal static string DataGridVisibleRowCountDescr => nameof(DataGridVisibleRowCountDescr);






    internal static string FormatControlFormatDescr => nameof(FormatControlFormatDescr);

    internal static string AccDGCollapse => nameof(AccDGCollapse);
    internal static string AccDGExpand => nameof(AccDGExpand);
    internal static string AccDGNavigate => nameof(AccDGNavigate);
    internal static string AccDGEdit => nameof(AccDGEdit);
    internal static string AccDGNavigateBack => nameof(AccDGNavigateBack);
    internal static string AccDGNewRow => nameof(AccDGNewRow);
    internal static string AccDGParentRow => nameof(AccDGParentRow);
    internal static string AccDGParentRows => nameof(AccDGParentRows);

    internal static string DataGridRowRowNumber => nameof(DataGridRowRowNumber);
}
