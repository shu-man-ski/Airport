﻿#pragma checksum "..\..\UsersWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6C5DD3781A80D9F536B20655C29AD6B8"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Airport;
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


namespace Airport {
    
    
    /// <summary>
    /// UsersWindow
    /// </summary>
    public partial class UsersWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\UsersWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userAddLogin;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\UsersWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userAddPassword;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\UsersWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userAddFullName;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\UsersWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button userAdd;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\UsersWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox userUpdateLogin;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\UsersWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userUpdatePassword;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\UsersWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userUpdateFullName;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\UsersWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button userUpdate;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\UsersWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox userDeleteByLogin;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\UsersWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid userGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/Диспетчер авиарейсов;component/userswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UsersWindow.xaml"
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
            this.userAddLogin = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.userAddPassword = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.userAddFullName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.userAdd = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\UsersWindow.xaml"
            this.userAdd.Click += new System.Windows.RoutedEventHandler(this.UserAdd_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.userUpdateLogin = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.userUpdatePassword = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.userUpdateFullName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.userUpdate = ((System.Windows.Controls.Button)(target));
            
            #line 88 "..\..\UsersWindow.xaml"
            this.userUpdate.Click += new System.Windows.RoutedEventHandler(this.UserUpdate_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.userDeleteByLogin = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            
            #line 101 "..\..\UsersWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.User_DeleteByLogin_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.userGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

