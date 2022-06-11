namespace PvsStudioLicense.Application.Enums;

using System.ComponentModel;
using HandyControl.Controls;
using HandyControl.Tools.Converter;
using Assets;

/// <summary>
/// Type license.
/// </summary>
[TypeConverter(typeof(EnumDescriptionTypeConverter))]
public enum TypeLicense
{
    /// <summary>
    /// Individual.
    /// </summary>
    [LocalizedDescription("Test1", typeof(EnumResources))]
    Individual,

    /// <summary>
    /// OpenSource.
    /// </summary>
    [Description("This is horrible")]
    OpenSource,

    /// <summary>
    /// Student.
    /// </summary>
    [Description("This is horrible1")]
    Student
}