﻿#pragma checksum "..\..\..\..\WP_Editor\WP_Editor_DirectoryName_Input.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C9B7F2204ED6EB25904BD670BAC7766C2984C59C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ModbusWPFproject;
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
using WindowExtras.Wpf;
using WindowExtras.Wpf.Helpers;
using WindowExtras.Wpf.Menu;
using WindowExtras.Wpf.Shadows;


namespace ModbusWPFproject {
    
    
    /// <summary>
    /// WP_Editor_DirectoryName_Input
    /// </summary>
    public partial class WP_Editor_DirectoryName_Input : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\WP_Editor\WP_Editor_DirectoryName_Input.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid WP_Editor_Directoryname_Input_Grid;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\WP_Editor\WP_Editor_DirectoryName_Input.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WP_Editor_Directoryname_Input_Textbox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\WP_Editor\WP_Editor_DirectoryName_Input.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button WP_Editor_Directoryname_Input_Button_Go;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\WP_Editor\WP_Editor_DirectoryName_Input.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button WP_Editor_Directoryname_Input_Button_Close;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ModbusWPFproject;component/wp_editor/wp_editor_directoryname_input.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\WP_Editor\WP_Editor_DirectoryName_Input.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.WP_Editor_Directoryname_Input_Grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.WP_Editor_Directoryname_Input_Textbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\..\..\WP_Editor\WP_Editor_DirectoryName_Input.xaml"
            this.WP_Editor_Directoryname_Input_Textbox.PreviewTouchUp += new System.EventHandler<System.Windows.Input.TouchEventArgs>(this.WP_Editor_Directoryname_Input_Textbox_PreviewTouchUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.WP_Editor_Directoryname_Input_Button_Go = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\..\WP_Editor\WP_Editor_DirectoryName_Input.xaml"
            this.WP_Editor_Directoryname_Input_Button_Go.Click += new System.Windows.RoutedEventHandler(this.WP_Editor_Directoryname_Input_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.WP_Editor_Directoryname_Input_Button_Close = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\..\WP_Editor\WP_Editor_DirectoryName_Input.xaml"
            this.WP_Editor_Directoryname_Input_Button_Close.Click += new System.Windows.RoutedEventHandler(this.WP_Editor_Directoryname_Input_Button_Close_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
