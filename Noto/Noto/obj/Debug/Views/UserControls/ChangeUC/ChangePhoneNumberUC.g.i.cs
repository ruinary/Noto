﻿#pragma checksum "..\..\..\..\..\Views\UserControls\ChangeUC\ChangePhoneNumberUC.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "839919352168A050A7EE6B4706EE87D43D3ED10B24F58812D1B5048A17DBB8BD"
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
    /// ChangePhoneNumberUC
    /// </summary>
    public partial class ChangePhoneNumberUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangePhoneNumberUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid CurPhone;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangePhoneNumberUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock userPhoneNumber;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangePhoneNumberUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid EditProfilePhoneNumber;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangePhoneNumberUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid EditPhone;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangePhoneNumberUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EditedPhone;
        
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
            System.Uri resourceLocater = new System.Uri("/Noto;component/views/usercontrols/changeuc/changephonenumberuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangePhoneNumberUC.xaml"
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
            this.CurPhone = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.userPhoneNumber = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.EditProfilePhoneNumber = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            
            #line 33 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangePhoneNumberUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_EditPhone);
            
            #line default
            #line hidden
            return;
            case 5:
            this.EditPhone = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.EditedPhone = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 50 "..\..\..\..\..\Views\UserControls\ChangeUC\ChangePhoneNumberUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_ConfPhone);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

