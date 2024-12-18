using PAWPMD.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Strategy;

/// <summary>
/// Defines the contract for widget strategy implementations.
/// Strategies implementing this interface are responsible for handling widget-related operations, such as saving a widget.
/// </summary>
public interface IWidgetStrategy
{
    /// <summary>
    /// Saves a widget asynchronously.
    /// </summary>
    /// <param name="widgetDTO">The data transfer object containing widget information.</param>
    /// <param name="userId">The optional ID of the user to whom the widget belongs.</param>
    /// <param name="widgetId">The optional ID of the widget being edited.</param>
    /// <returns>A <see cref="WidgetResponseDTO"/> containing the widget's response data.</returns>
    Task<WidgetResponseDTO> SaveWidgetAsync(WidgetRequestDTO widgetDTO, int? userId, int? widgetId);
}