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
    [LocalizedDescription("Individual", typeof(EnumResources))]
    Individual,

    /// <summary>
    /// OpenSource.
    /// </summary>
    [LocalizedDescription("OpenSource", typeof(EnumResources))]
    OpenSource,

    /// <summary>
    /// Student.
    /// </summary>
    [LocalizedDescription("Student", typeof(EnumResources))]
    Student
}