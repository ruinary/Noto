﻿#pragma checksum "..\..\..\..\..\Views\UserControls\ChangeUC\ChangeTaskDescriptionUC.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B743A79A9401BF66CDA06BA1BF4F1AD576CE7EA42D7012206D345B5725DD6993"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using Noto.Views.UserControls.ChangeUC;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Noto.Views.UserControls.ChangeUC {
    
    
    /// <summary>
    /// ChangeTaskDescriptionUC
    /// </summary>
    public partial class ChangeTaskDescriptionUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangeTaskDescriptionUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid CurTaskDescriptionGrid;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangeTaskDescriptionUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock taskDescription;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangeTaskDescriptionUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid EditTaskDescription;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangeTaskDescriptionUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid EditTaskDescriptionGrid;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangeTaskDescriptionUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox editedTaskDescription;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Noto;component/views/usercontrols/changeuc/changetaskdescriptionuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangeTaskDescriptionUC.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.CurTaskDescriptionGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.taskDescription = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.EditTaskDescription = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            
            #line 23 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangeTaskDescriptionUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditTaskDescriptionButtonClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.EditTaskDescriptionGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.editedTaskDescription = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 41 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangeTaskDescriptionUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ConfTaskDescriptionButtonClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
