using AFRICAN_FOOD.Models;
using AFRICAN_FOOD.Templates;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AFRICAN_FOOD.Controls
{
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        #region Fields

        private readonly DataTemplate _incomingDataTemplate;
        private readonly DataTemplate _outgoingDataTemplate;

        #endregion //Fields

        #region Constructors

        public MessageDataTemplateSelector()
        {
            _incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            _outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }


        #endregion //Constructors


        #region Methods

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is MessageDetail detail))
                return null;
            return !detail.IsIncoming ? _incomingDataTemplate : _outgoingDataTemplate;
        }

        #endregion //Methods
    }
}
