using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PoshCode.Controls
{
    partial class ConsoleControl
    {
        private static void InitializeCommands()
        {
            CommandManager.RegisterClassCommandBinding(typeof(ConsoleControl),
                                                       new CommandBinding(ApplicationCommands.Cut, OnExecuteCut, OnCanExecuteCut));
            CommandManager.RegisterClassCommandBinding(typeof(ConsoleControl),
                                                       new CommandBinding(ApplicationCommands.Paste, OnExecutePaste, OnCanExecutePaste));
            CommandManager.RegisterClassCommandBinding(typeof(ConsoleControl),
                                                       new CommandBinding(ApplicationCommands.Stop, OnApplicationStop));

            //CommandManager.RegisterClassCommandBinding(typeof(ConsoleRichTextBox),
            //    new CommandBinding(ApplicationCommands.Copy,
            //    new ExecutedRoutedEventHandler(OnCopy),
            //    new CanExecuteRoutedEventHandler(OnCanExecuteCopy)));

        }

        #region  Methods (13)

        //  Private Methods (13)

        /// <summary>
        /// A handler for the Application.Stop event...
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private static void OnApplicationStop(object sender, ExecutedRoutedEventArgs e)
        {
           var control = (ConsoleControl)sender;
            //if (!control.IsRunning)
            //{
            //    // TODO: Remove failed command from History ... control.History.ResetCurrentCommand();
            //    control.CurrentCommand = string.Empty;
            //    e.Handled = true;
            //}
            ApplicationCommands.Stop.Execute(null, (IInputElement)control.Parent);
        }
      
      //private static void OnCanDecreaseZoom(object target, CanExecuteRoutedEventArgs args)
      //  {
      //      args.CanExecute = ((ConsoleControl)(target)).FontSize > 1;
      //  }

      
      //private static void OnCanIncreaseZoom(object target, CanExecuteRoutedEventArgs args)
      //  {
      //      args.CanExecute = true;
      //  }
      
       // TODO: REIMPLEMENT copy, and keyboard selection shortcuts
       // ..... Including making sure that CUT does COPY?
       // ..... And can we optionally change the "prompt" on copy?

       //private static void OnCopy(object sender, ExecutedRoutedEventArgs e)
      //  {
            
      //      ((FlowDocumentReader)sender).Copy();
      //      e.Handled = true;
      //  }
      
      //private static void OnCanExecuteCopy(object target, CanExecuteRoutedEventArgs args)
      //  {
      //      args.CanExecute = ((TextBoxBase)(target)).IsEnabled;
      //  }

      private static void OnCanExecuteCut(object target, CanExecuteRoutedEventArgs args)
        {
            var box = (ConsoleControl)(target);
            args.CanExecute = box.IsEnabled && box.Selection != null && !box.Selection.IsEmpty;
        }
      
      private static void OnCanExecutePaste(object target, CanExecuteRoutedEventArgs args)
        {
           args.CanExecute = true;
            //ConsoleControl box = (ConsoleControl)target;
            //args.CanExecute = box._commandBox.IsEnabled && !box._commandBox.IsReadOnly && Clipboard.ContainsText();
        }
      private static void OnExecuteCut(object sender, ExecutedRoutedEventArgs e)
        {
            // Clipboard.SetText(((ConsoleControl)sender).Selection.Text);
            // ((ConsoleControl)sender).Selection.Text = String.Empty;
            //e.Command.RoutedEvent = ApplicationCommands.Copy

            ApplicationCommands.Copy.Execute(e.Parameter, ((ConsoleControl)sender)._commandBox );
            e.Handled = true;
        }
      
      //private static void OnDecreaseZoom(object sender, ExecutedRoutedEventArgs e)
      //  {
      //      ConsoleControl box = ((ConsoleControl)(sender));

      //      if (box.FontSize > 1)
      //      {
      //          box.FontSize -= 1.0;
      //      }

      //      e.Handled = true;
      //  }
      
      //private static void OnIncreaseZoom(object sender, ExecutedRoutedEventArgs e)
      //  {
      //      ((ConsoleControl)(sender)).FontSize += 1.0;
      //  }
      
      private static void OnExecutePaste(object sender, ExecutedRoutedEventArgs e)
        {
            var box = ((ConsoleControl)sender)._commandContainer.Child as RichTextBox;
            if(box != null)
            {
               box.Paste();                  
            } else
            {
               (((ConsoleControl) sender)._commandContainer.Child as PasswordBox)?.Paste();
            }
         //if (Clipboard.ContainsText())
            //{
            //    if (box.CaretInCommand)
            //    {
            //        // get a pointer with forward gravity so it will stick to the end of the paste. I don't know why you have to use the Offset instead of insertion...
            //        TextPointer insert = box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);// GetInsertionPosition(LogicalDirection.Forward);
            //        // now insert the text and make sure the caret is where it belongs.
            //        insert.InsertTextInRun(Clipboard.GetText(TextDataFormat.UnicodeText));
            //        box.CaretPosition = insert;
            //    }
            //    else
            //    {
            //        box.Document.ContentEnd.InsertTextInRun(Clipboard.GetText(TextDataFormat.UnicodeText));
            //        box.CaretPosition = box.Document.ContentEnd;
            //    }
            //}

            ((ConsoleControl)sender)._commandContainer.BringIntoView();
            ((ConsoleControl) sender)._commandContainer.Child.Focus();
            e.Handled = true;
        }
      
      //private static void OnZoom(object sender, ExecutedRoutedEventArgs e)
      //  {
      //      ConsoleControl control = (ConsoleControl)sender;

      //      if (e.Parameter is double)
      //      {
      //          control.SetZoomFactor((double)(e.Parameter));
      //          return;
      //      }

      //      string parameter = e.Parameter as string;
      //      double zoom = 1.0;

      //      if (parameter != null && double.TryParse(parameter, out zoom))
      //      {
      //          control.SetZoomFactor(zoom);
      //          return;
      //      }
      //  }
      
      //private void SetZoomFactor(double zoom)
      //  {            
      //      FontSize = Math.Max(1.0, zoom * Document.FontSize);
      //  }
      
      #endregion 

    }
}