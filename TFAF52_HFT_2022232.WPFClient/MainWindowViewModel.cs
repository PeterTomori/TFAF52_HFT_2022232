﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
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
        RestService rs;
        public RestCollection<Company> Companies { get; set; }

        public RestCollection<Planet> Planets { get; set; }

        public RestCollection<Ship> Ships { get; set; }

        public List<Company> ShipManufacturerList { get; set; }

        public List<Planet> OwnedbyCompanyList { get; set; }

        public List<Ship> ShipOfFactionsList { get; set; }

        public List<Company> OwnerOfPlanetList { get; set; }

        public List<FactionCounted> FactionCounteds { get; set; }

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
                        CompanyId = value.CompanyId,
                        Faction = value.Faction
                    };
                    OnPropertyChanged();
                    (DeleteCompanyCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private Planet selectedPlanet;

        public Planet SelectedPlanet
        {
            get { return selectedPlanet; }
            set
            {
                if (value != null)
                {
                    selectedPlanet = new Planet()
                    {
                        PlanetName = value.PlanetName,
                        PlanetId = value.PlanetId
                    };
                    OnPropertyChanged();
                    (DeleteCompanyCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private Ship selectedShip;

        public Ship SelectedShip
        {
            get { return selectedShip; }
            set
            {
                if (value != null)
                {
                    selectedShip = new Ship()
                    {
                        ShipName = value.ShipName,
                        ShipId = value.ShipId,
                        ShipType = value.ShipType
                    };
                    OnPropertyChanged();
                    (DeleteCompanyCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public string ShipManufacturer { get; set; }
        public string Ownedbycompany { get; set; }
        public string ShipOfFactions { get; set; }
        public string OwnerOfPlanet { get; set; }

        #region Commands

        public ICommand CreateCompanyCommand { get; set; }

        public ICommand DeleteCompanyCommand { get; set; }

        public ICommand UpdateCompanyCommand { get; set; }

        public ICommand CreateShipCommand { get; set; }

        public ICommand DeleteShipCommand { get; set; }

        public ICommand UpdateShipCommand { get; set; }

        public ICommand CreatePlanetCommand { get; set; }

        public ICommand DeletePlanetCommand { get; set; }

        public ICommand UpdatePlanetCommand { get; set; }

        public ICommand ShipManufacturersCommand { get; set; }

        public ICommand OwnedbycompanyCommand { get; set; }

        public ICommand ShipOfFactionsCommand { get; set; }

        public ICommand OwnerOfPlanetCommand { get; set; }

        #endregion

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
                Planets = new RestCollection<Planet>("http://localhost:27110/", "planet", "hub");
                Ships = new RestCollection<Ship>("http://localhost:27110/", "ship", "hub");

                rs = new RestService("http://localhost:27110/");

                // Company Commands

                CreateCompanyCommand = new RelayCommand(() =>
                {
                    Companies.Add(new Company()
                    {
                        CompanyName = SelectedCompany.CompanyName,
                        Faction = SelectedCompany.Faction
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

                // Planet Commands

                CreatePlanetCommand = new RelayCommand(() =>
                {
                    Planets.Add(new Planet()
                    {
                        PlanetName = SelectedPlanet.PlanetName
                    });
                });

                UpdatePlanetCommand = new RelayCommand(() =>
                {
                    Planets.Update(SelectedPlanet);
                });

                DeletePlanetCommand = new RelayCommand(() =>
                {
                    Planets.Delete(SelectedPlanet.PlanetId);
                },
                () =>
                {
                    return SelectedPlanet != null;
                });
                SelectedPlanet = new Planet();

                // Ship Commands

                CreateShipCommand = new RelayCommand(() =>
                {
                    Ships.Add(new Ship()
                    {
                        ShipName = SelectedShip.ShipName,
                        ShipType = SelectedShip.ShipType
                    });
                });

                UpdateShipCommand = new RelayCommand(() =>
                {
                    Ships.Update(SelectedShip);
                });

                DeleteShipCommand = new RelayCommand(() =>
                {
                    Ships.Delete(SelectedShip.ShipId);
                },
                () =>
                {
                    return SelectedShip != null;
                });
                SelectedShip = new Ship();

                ShipManufacturersCommand = new RelayCommand(() =>
                {
                    //Returns the Company who builds the given Ship
                    ShipManufacturerList = rs.Get<Company>("stat/shipManufacturers/" + ShipManufacturer);
                });

                OwnedbycompanyCommand = new RelayCommand(() =>
                {
                    //Returns given Company's Planet(s)
                    OwnedbyCompanyList = rs.Get<Planet>("stat/ownedByCompany/" + Ownedbycompany);
                });

                ShipOfFactionsCommand = new RelayCommand(() =>
                {
                    //Returns a Faction's Ships
                    ShipOfFactionsList = rs.Get<Ship>("stat/shipOfFactions/" + ShipOfFactions);
                });

                OwnerOfPlanetCommand = new RelayCommand(() =>
                {
                    //Returns which Company owns the given Planet
                    OwnerOfPlanetList = rs.Get<Company>("stat/ownerOfPlanet" + OwnerOfPlanet);
                });
                
                ////Returns how many ship each faction has
                FactionCounteds = rs.Get<FactionCounted>("stat/shipFactions");

                //List<Company> Q1 = rs.Get<Company>("stat/shipManufacturers/" + ShipManufacturer);
                ;
            }
        }
    }
}
