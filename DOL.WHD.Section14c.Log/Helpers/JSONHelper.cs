using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace DOL.WHD.Section14c.Log.Helpers
{
    public static class JSONHelper
    {
        #region Public extension methods.
        /// <summary>
        /// Extened method of object class
        /// Converts an object to a json string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJSON(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                return serializer.Serialize(obj);
            }
            catch(Exception e)
            {
                return string.Empty;
            }
        }
        #endregion
    }
}