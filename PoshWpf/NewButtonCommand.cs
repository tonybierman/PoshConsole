﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Windows;
using System.Windows.Controls;

namespace PoshWpf
{
    [Cmdlet(VerbsCommon.New, "Button", SupportsShouldProcess = false, ConfirmImpact = ConfirmImpact.None, DefaultParameterSetName = "DataTemplate")]
    public class NewButtonCommand : WpfNewControlCommandBase
    {
        [Parameter( Position = 1 )]
        public RoutedEventHandler Click { get; set; }

        protected override void ProcessRecord()
		  {
            _dispatcher.Invoke((Action)(() =>
            {
                if (Content != null)
                {
                    object output = Content.BaseObject;

                    control = new Button();

                    if (Click != null)
                    {
							  ((Button)control).Click += Click;
                    }

                    if (_element != null)
                    {
                        ErrorRecord err;
                        FrameworkElement el;
                        _template.TryLoadXaml(out el, out err);
                        el.DataContext = output;
								((Button)control).Content = el;
                    }
                    else
                    {
							  ((Button)control).Content = output;
                    }
                }
            }));
				base.ProcessRecord();
				WriteObject(control);
        }
    }
}
