using BM.Library.DataAccess;
using BM.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BM.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for DefaultsView.xaml
    /// </summary>
    public partial class DefaultsView : UserControl
    {
        private DefaultsData _defaultsData = new DefaultsData();

        public DefaultsView()
        {
            InitializeComponent();

            //LoadDefaultsFromDB();
        }

        //private void LoadDefaultsFromDB()
        //{
        //    var model = _defaultsData.GetDefaultsData();

        //    if (model != null)
        //    {
        //        HourlyRateTextbox.Text = model.HourlyRate.ToString();
        //        PreBillCheckbox.IsChecked = (model.HourlyRate > 0);
        //        HasCutOffCheckbox.IsChecked = (model.HasCutOff > 0);
        //        CutOffTextbox.Text = model.CutOff.ToString();
        //        MinimumHoursTextbox.Text = model.MinimumHours.ToString();
        //        BillingIncrementTextbox.Text = model.BillingIncrement.ToString();
        //        RoundUpAfterXMinuteTextbox.Text = model.RoundUpAfterXMinutes.ToString();
        //    }
        //    else
        //    {
        //        HourlyRateTextbox.Text = "0";
        //        PreBillCheckbox.IsChecked = true;
        //        HasCutOffCheckbox.IsChecked = false;
        //        CutOffTextbox.Text = "0";
        //        MinimumHoursTextbox.Text = "0.25";
        //        BillingIncrementTextbox.Text = "0.25";
        //        RoundUpAfterXMinuteTextbox.Text = "0";
        //    }
        //}

        //private (bool isValid, DefaultsModel model) ValidateForm()
        //{
        //    var isValid = true;
        //    var model = new DefaultsModel();

        //    try
        //    {
        //        model.PreBill = (bool)PreBillCheckbox.IsChecked ? 1 : 0;
        //        model.HasCutOff = (bool)HasCutOffCheckbox.IsChecked ? 1 : 0;
        //        model.HourlyRate = double.Parse(HourlyRateTextbox.Text);
        //        model.CutOff = int.Parse(CutOffTextbox.Text);
        //        model.MinimumHours = double.Parse(MinimumHoursTextbox.Text);
        //        model.BillingIncrement = double.Parse(BillingIncrementTextbox.Text);
        //        model.RoundUpAfterXMinutes = int.Parse(RoundUpAfterXMinuteTextbox.Text);
        //    }
        //    catch
        //    {
        //        isValid = false;
        //    }

        //    return (isValid, model);
        //}
        //private void UpdateDefaultsTable(DefaultsModel model)
        //{
        //    _defaultsData.DeleteDefaultsData();
        //    _defaultsData.InsertDefaultsData(model);
        //}

        //private void UpdateForm_Click(object sender, RoutedEventArgs e)
        //{
        //    var form = ValidateForm();

        //    if (form.isValid)
        //    {
        //        UpdateDefaultsTable(form.model);
        //        MessageBox.Show("The form has been updated successfuly");
        //    }
        //    else
        //    {
        //        MessageBox.Show("The form is not valid. Please check your entries and try again.");
        //        return;
        //    }
        //}
    }
}
