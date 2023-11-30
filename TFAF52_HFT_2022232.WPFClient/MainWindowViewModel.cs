using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TFAF52_HFT_2022232.Models;

namespace TFAF52_HFT_2022232.WPFClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Company> Companies { get; set; }

        private Company selectedCompany;

        public Company SelectedCompany
        {
            get { return selectedCompany; }
            set 
            {
                if (value != null)
                {
                    selectedCompany = new Company()
                    {
                        CompanyName = value.CompanyName,
                        CompanyId = value.CompanyId
                    };
                    OnPropertyChanged();
                    (DeleteCompanyCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateCompanyCommand { get; set; }

        public ICommand DeleteCompanyCommand { get; set; }

        public ICommand UpdateCompanyCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Companies = new RestCollection<Company>("http://localhost:27110/", "company", "hub");
                CreateCompanyCommand = new RelayCommand(() =>
                {
                    Companies.Add(new Company()
                    {
                        CompanyName = SelectedCompany.CompanyName
                    });
                });

                UpdateCompanyCommand = new RelayCommand(() =>
                {
                    Companies.Update(SelectedCompany);
                });

                DeleteCompanyCommand = new RelayCommand(() =>
                {
                    Companies.Delete(SelectedCompany.CompanyId);
                },
                () =>
                {
                    return SelectedCompany != null;
                });
                SelectedCompany = new Company();
            }
        }
    }
}
