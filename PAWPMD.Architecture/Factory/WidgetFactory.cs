using PAWPMD.Models.Models;
using PAWPMD.Models;
using System;

namespace PAWPMD.Architecture.Factory
{
    /// <summary>
    /// Interface defining a factory for creating widgets.
    /// </summary>
    public interface IWidgetFactory
    {
        /// <summary>
        /// Creates an instance of a widget based on the specified type.
        /// </summary>
        /// <param name="widgetType">The type of widget to create (e.g., "TableWidget").</param>
        /// <returns>An instance of an object implementing <see cref="IWidget"/>.</returns>
        Widget Create(string widgetType);
    }

    /// <summary>
    /// Implementation of the Factory Method pattern for widget creation.
    /// </summary>
    public class WidgetFactory : Factory<Widget>, IWidgetFactory
    {
        /// <summary>
        /// Creates a widget based on the specified type.
        /// </summary>
        /// <param name="widgetType">The type of widget to create (e.g., "TableWidget", "ImageWidget").</param>
        /// <returns>An instance of the corresponding widget.</returns>
        /// <exception cref="ArgumentException">Thrown if the widget type is invalid.</exception>
        public override Widget Create(string widgetType) 
        {
            return widgetType switch
            {
                "TableWidget" => new WidgetTable(),
                "ImageWidget" => new WidgetImage(),
                "VideoWidget" => new WidgetVideo(),
                _ => throw new ArgumentException("Invalid widget type"),
            };
        }
    }
}
