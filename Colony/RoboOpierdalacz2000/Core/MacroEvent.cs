using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboOpierdalacz2000.Core
{
    /// <summary>
    /// All possible events that macro can record
    /// </summary>
    [Serializable]
    public enum MacroEventType
    {
        MouseMove,
        MouseDown,
        MouseUp,
        MouseWheel,
        KeyDown,
        KeyUp
    }

    /// <summary>
    /// Set of macro movies
    /// </summary>
    [Serializable]
    public class MacroPack: IListSource
    {
        public bool ContainsListCollection
        {
            get
            {
                return true;
            }
        }

        public List<MacroMovie> Movies { get; set; } = new List<MacroMovie>();

        public IList GetList()
        {
            return Movies;
        }
    }
    /// <summary>
    /// Full length record (list) of macro events
    /// </summary>
    [Serializable]
    public class MacroMovie
    {
        public string Name { get; set; }
        public List<MacroEvent> Records { get; set; } = new List<MacroEvent>();

        public override string ToString()
        {
            return Name;
        }

        public MacroMovie()
        {

        }
        public MacroMovie(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Series of events that can be recorded any played back
    /// </summary>
    [Serializable]
    public class MacroEvent
    {

        public MacroEventType MacroEventType;
        public EventArgs EventArgs;
        public long TimeSinceLastEvent;

        public MacroEvent(MacroEventType macroEventType, EventArgs eventArgs, long timeSinceLastEvent)
        {

            MacroEventType = macroEventType;
            EventArgs = eventArgs;
            TimeSinceLastEvent = timeSinceLastEvent;

        }

    }
}
