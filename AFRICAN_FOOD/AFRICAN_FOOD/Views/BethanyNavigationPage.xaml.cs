﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AFRICAN_FOOD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BethanyNavigationPage : NavigationPage
    {
        public BethanyNavigationPage()
        {
            InitializeComponent();
        }

        public BethanyNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}