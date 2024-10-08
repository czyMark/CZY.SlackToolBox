﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using CZY.SlackToolBox.LuckyControl.ColorPicker;

namespace CZY.SlackToolBox.LuckyControl.ColorPickerControls.Dialogs
{
    /// <summary>
    /// Interaction logic for ColorPickerFullDialog.xaml
    /// </summary>
    public partial class ColorPickerFullDialog : Window  , IColorDialog
    {
        public ColorPickerFullDialog()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
             
        }

         [Category("ColorPicker")]
        public Color SelectedColor
        {
            get { return colorPickerFull.SelectedColor; }
            set { colorPickerFull.SelectedColor = value; }
        }

         [Category("ColorPicker")]
        public Color InitialColor
        {
            get { return colorPickerFull.InitialColor; }
            set
            {
                colorPickerFull.InitialColor = value;
                colorPickerFull.SelectedColor = value;
            }
        }

         [Category("ColorPicker")]
        public ColorSelector.ESelectionRingMode SelectionRingMode
        {
            get { return colorPickerFull.SelectionRingMode; }
            set { colorPickerFull.SelectionRingMode = value; }
        }
    }
}
