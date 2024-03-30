using System.ComponentModel;

namespace AGV.Enums
{
    public enum StatusRequest
    {
        [Description("Requested")]
        Requested = 1,
        [Description("Collection")]
        Collection = 2,
        [Description("Sent")]
        Sent = 3,
        [Description("Received")]
        Received = 4
    }
}
