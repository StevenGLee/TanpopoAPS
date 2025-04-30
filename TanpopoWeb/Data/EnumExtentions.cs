using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TanpopoWeb.Data
{
    public static class EnumExtensions
    {
        public static string GetDescription<TEnum>(this TEnum value)
        {
            var attribute = value.GetType()
                .GetField(value.ToString())
                ?.GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

            return attribute?.Name ?? value.ToString();
        }
    }
}
