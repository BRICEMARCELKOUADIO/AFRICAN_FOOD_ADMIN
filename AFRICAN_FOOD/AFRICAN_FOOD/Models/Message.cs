using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.Models
{
    public class Message
    {
        #region Fields

        #endregion //Fields

        public int MessageId { get; set; }
        public string UserId { get; set; }
        public string ReceverId { get; set; }
        public DateTime Date { get; set; }


        public IEnumerable<MessageDetail> Details { get; set; }
    }

    public class MessageDetail
    {
        public String MessageId { get; set; }
        public DateTime MessageDateTime { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public bool IsIncoming { get; set; }
    }
}
