namespace PvsStudioLicense.Application.Enums
{
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using HandyControl.Tools.Converter;

    /// <summary>
    /// Language app.
    /// </summary>
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Language
    {
        /// <summary>
        /// Russian language.
        /// </summary>
        [Description("Русский")]
        [EnumMember(Value = "ru-RU")]
        Russian,

        /// <summary>
        /// English language.
        /// </summary>
        [Description("English")]
        [EnumMember(Value = "en-US")]
        English,
    }
}