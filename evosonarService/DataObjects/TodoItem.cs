using Microsoft.Azure.Mobile.Server;

namespace evosonarService.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}