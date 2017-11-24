using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Netblaster.Hermes.DAL.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>System.String.</returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
        }
    }
}
