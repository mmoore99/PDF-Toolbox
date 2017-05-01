﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Input;

namespace PDFToolbox.Behaviors
{
    public class DragBehavior : Behavior<FrameworkElement>
    {
        private bool _isMouseClicked = false;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonDown += new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonDown);
            AssociatedObject.MouseLeftButtonUp += new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonUp);
            AssociatedObject.MouseLeave += new MouseEventHandler(AssociatedObject_MouseLeave);
        }

        void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseClicked = true;
        }

        void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseClicked = false;
        }

        void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_isMouseClicked == false) return;

            IDragable dragObject = (IDragable)AssociatedObject.DataContext;

            if(dragObject!=null)
            {
                DataObject data = new DataObject();
                data.SetData(dragObject.DataType, AssociatedObject.DataContext);
                DragDrop.DoDragDrop(AssociatedObject, data, DragDropEffects.Move);
            }

            _isMouseClicked = false;
        }
    }
}
