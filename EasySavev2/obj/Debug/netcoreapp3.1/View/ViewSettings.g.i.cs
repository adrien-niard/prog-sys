﻿#pragma checksum "..\..\..\..\View\ViewSettings.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "94B450BEFA26D534DF390E894B1BD01B8C32FD3E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using EasySavev2;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace EasySavev2 {
    
    
    /// <summary>
    /// Settings
    /// </summary>
    public partial class Settings : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\..\View\ViewSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextEncrypt;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\View\ViewSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListEncrypt;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\View\ViewSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButAdd;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\View\ViewSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButDelete;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\View\ViewSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Save;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\View\ViewSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SizeKo;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/EasySavev2;V1.0.0.0;component/view/viewsettings.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\ViewSettings.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 21 "..\..\..\..\View\ViewSettings.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TextEncrypt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.ListEncrypt = ((System.Windows.Controls.ListBox)(target));
            
            #line 27 "..\..\..\..\View\ViewSettings.xaml"
            this.ListEncrypt.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListEncrypt_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ButAdd = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\..\View\ViewSettings.xaml"
            this.ButAdd.Click += new System.Windows.RoutedEventHandler(this.ButAdd_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ButDelete = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\..\View\ViewSettings.xaml"
            this.ButDelete.Click += new System.Windows.RoutedEventHandler(this.ButDelete_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Save = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\..\View\ViewSettings.xaml"
            this.Save.Click += new System.Windows.RoutedEventHandler(this.SaveSet_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 31 "..\..\..\..\View\ViewSettings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButBack_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.SizeKo = ((System.Windows.Controls.TextBox)(target));
            
            #line 33 "..\..\..\..\View\ViewSettings.xaml"
            this.SizeKo.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SizeKo_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

