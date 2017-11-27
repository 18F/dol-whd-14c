using System;

namespace DOL.WHD.Section14c.EmailApi.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Describes a type model.
    /// </summary>
    public abstract class ModelDescription
    {
        /// <summary>
        /// 
        /// </summary>
        public string Documentation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Type ModelType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }
}