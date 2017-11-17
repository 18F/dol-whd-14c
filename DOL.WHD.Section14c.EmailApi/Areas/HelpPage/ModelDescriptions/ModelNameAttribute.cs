using System;

namespace DOL.WHD.Section14c.EmailApi.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Use this attribute to change the name of the <see cref="ModelDescription"/> generated for a type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public sealed class ModelNameAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public ModelNameAttribute(string name)
        {
            Name = name;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }
    }
}