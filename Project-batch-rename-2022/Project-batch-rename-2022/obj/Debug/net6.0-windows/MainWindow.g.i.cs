﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "030F5D186FAB0FDD0ABB071E53A22FE908E3E96B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Fluent;
using Project_batch_rename_2022;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace Project_batch_rename_2022 {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : Fluent.RibbonWindow, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 24 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.ComboBox rulesComboBox;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.ComboBox typeComboBox;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.ComboBox conflictComboBox;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.RadioButton renameOriginal;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.RadioButton moveToNew;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.RadioButton copyToNew;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView chosenRulesListView;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ItemListView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Project-batch-rename-2022;V1.0.0.0;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\MainWindow.xaml"
            ((Project_batch_rename_2022.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 15 "..\..\..\MainWindow.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NewProjectBtnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 16 "..\..\..\MainWindow.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenProjectBtnClick);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 17 "..\..\..\MainWindow.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveProjectBtnClick);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 20 "..\..\..\MainWindow.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenPresetBtnClick);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 21 "..\..\..\MainWindow.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SavePresetBtnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.rulesComboBox = ((Fluent.ComboBox)(target));
            return;
            case 8:
            
            #line 32 "..\..\..\MainWindow.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.addRuleBtnClick);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 36 "..\..\..\MainWindow.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.resetRuleBtnClick);
            
            #line default
            #line hidden
            return;
            case 10:
            this.typeComboBox = ((Fluent.ComboBox)(target));
            
            #line 41 "..\..\..\MainWindow.xaml"
            this.typeComboBox.DropDownClosed += new System.EventHandler(this.typeComboBox_DropDownClosed);
            
            #line default
            #line hidden
            
            #line 41 "..\..\..\MainWindow.xaml"
            this.typeComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.typeComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 49 "..\..\..\MainWindow.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.addItems);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 50 "..\..\..\MainWindow.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.resetItems);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 56 "..\..\..\MainWindow.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.StartBtnClick);
            
            #line default
            #line hidden
            return;
            case 14:
            this.conflictComboBox = ((Fluent.ComboBox)(target));
            return;
            case 15:
            this.renameOriginal = ((Fluent.RadioButton)(target));
            return;
            case 16:
            this.moveToNew = ((Fluent.RadioButton)(target));
            return;
            case 17:
            this.copyToNew = ((Fluent.RadioButton)(target));
            return;
            case 18:
            
            #line 110 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.removeRuleBtnClick);
            
            #line default
            #line hidden
            return;
            case 19:
            this.chosenRulesListView = ((System.Windows.Controls.ListView)(target));
            
            #line 117 "..\..\..\MainWindow.xaml"
            this.chosenRulesListView.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.chosenRulesListView_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 120 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.EditRules);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 121 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteRules);
            
            #line default
            #line hidden
            return;
            case 23:
            this.ItemListView = ((System.Windows.Controls.ListView)(target));
            
            #line 141 "..\..\..\MainWindow.xaml"
            this.ItemListView.Drop += new System.Windows.DragEventHandler(this.ItemsDrop);
            
            #line default
            #line hidden
            
            #line 141 "..\..\..\MainWindow.xaml"
            this.ItemListView.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ItemListView_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 24:
            
            #line 144 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.removeFileFolder);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 22:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.MouseDoubleClickEvent;
            
            #line 132 "..\..\..\MainWindow.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.ChosenRule_DoubleClick);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

