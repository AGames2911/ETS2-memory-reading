using Ets2SdkClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E36dashboard
{
    public partial class Form1 : Form
    {
        //use ets2sdkclient
        public Ets2SdkTelemetry Telemetry;

        bool isConnected = false;

        string ets2arduino;

        public Form1()
        {
            InitializeComponent();
            //call sdk
            Telemetry = new Ets2SdkTelemetry();
            //show list of valid com ports
            foreach (string s in SerialPort.GetPortNames())
            {
                cmbsp.Items.Add(s);
            }

        }

        //ets2 dll waardes
        private void Telemetry_Data(Ets2Telemetry data, bool updated)
        {
            try
            {
                //
                //ets2 dll
                //

                if (this.InvokeRequired)
                {
                    this.Invoke(new TelemetryData(Telemetry_Data), new object[2] { data, updated });
                    return;
                }

                #region waardes ets2

                //
                //values to string
                //

                //float physics values
                string ets2speedKmh = Convert.ToString(Math.Round(Convert.ToDecimal(data.Physics.SpeedKmh)));
                string ets2speedMph = Convert.ToString(Math.Round(Convert.ToDecimal(data.Physics.SpeedMph)));
                //float drivetrain values
                string ets2rpm = data.Drivetrain.EngineRpm.ToString();
                string ets2maxrpm = data.Drivetrain.EngineRpmMax.ToString();
                string ets2odometer = data.Drivetrain.TruckOdometer.ToString();
                string ets2fuel = data.Drivetrain.Fuel.ToString();
                string ets2gear = data.Drivetrain.Gear.ToString();
                string ets2watertemp = data.Drivetrain.WaterTemperature.ToString();
                string ets2oiltemp = data.Drivetrain.OilTemperature.ToString();
                string ets2airpres = data.Drivetrain.AirPressure.ToString();
                string ets2oilpres = data.Drivetrain.OilPressure.ToString();
                string ets2braketemp = data.Drivetrain.BrakeTemperature.ToString();
                //drivetrain bool values
                string ets2engineEnabled = data.Drivetrain.EngineEnabled.ToString();
                string ets2electricEnabled = data.Drivetrain.ElectricEnabled.ToString();
                string ets2FuelWarningLight = data.Drivetrain.FuelWarningLight.ToString();
                string ets2CruiseControl = data.Drivetrain.CruiseControl.ToString();
                string ets2ParkingBrake = data.Drivetrain.ParkingBrake.ToString();
                string ets2MotorBrake = data.Drivetrain.MotorBrake.ToString();
                //float job values
                string ets2mass = Convert.ToString(Math.Round(Convert.ToDecimal(data.Job.Mass)));
                string ets2TrailerID = data.Job.TrailerId.ToString();
                string ets2TrailerName = data.Job.TrailerName.ToString();
                string ets2Cargo = data.Job.Cargo.ToString();
                string ets2Deadline = data.Job.Deadline.ToString();
                string ets2CitySource = data.Job.CitySource.ToString();
                string ets2CityDestination = data.Job.CityDestination.ToString();
                string ets2CompanySource = data.Job.CompanySource.ToString();
                string ets2CompanyDestination = data.Job.CompanyDestination.ToString();
                string ets2NavigationDistanceLeft = data.Job.NavigationDistanceLeft.ToString();
                string ets2SpeedLimit = data.Job.SpeedLimit.ToString();
                string ets2TrailerAttached = data.Job.TrailerAttached.ToString();
                //Lights bool values
                string ets2BlinkerLeftActive = data.Lights.BlinkerLeftActive.ToString();
                string ets2BlinkerRightActive = data.Lights.BlinkerRightActive.ToString();
                string ets2BlinkerLeftOn = data.Lights.BlinkerLeftOn.ToString();
                string ets2BlinkerRightOn = data.Lights.BlinkerRightOn.ToString();
                string ets2ParkingLights = data.Lights.ParkingLights.ToString();
                string ets2LowBeams = data.Lights.LowBeams.ToString();
                string ets2HighBeams = data.Lights.HighBeams.ToString();
                string ets2FrontAux = data.Lights.FrontAux.ToString();
                string ets2RoofAux = data.Lights.RoofAux.ToString();
                string ets2BrakeLight = data.Lights.BrakeLight.ToString();
                string ets2ReverseLight = data.Lights.ReverseLight.ToString();
                string ets2LightsDashboard = data.Lights.LightsDashboard.ToString();
                string ets2Beacon = data.Lights.Beacon.ToString();
                //damage values
                string ets2EngineDamage = data.Damage.WearEnigne.ToString();
                string ets2TransmissionDamage = data.Damage.WearTransmission.ToString();
                string ets2CabinDamage = data.Damage.WearCabin.ToString();
                string ets2ChassisDamage = data.Damage.WearChassis.ToString();
                string ets2WheelsDamage = data.Damage.WearWheels.ToString();
                string ets2TrailerDamage = data.Damage.WearTrailer.ToString();

                //
                //output string values to textbox
                //

                //values to outputtextbox
                txtoutputspeedKmh.Text = ets2speedKmh + "/Kmh";
                txtoutputspeedMph.Text = ets2speedMph + "/Mph";
                txtoutputrpm.Text = ets2rpm + "/min";
                txtoutputfuel.Text = ets2fuel + "L";
                txtoutputwatertemp.Text = ets2watertemp + "°C";
                txtoutputgear.Text = ets2gear;

                //physics float values
                txtspeedKmh.Text = ets2speedKmh;
                txtspeedMph.Text = ets2speedMph;
                //drivetrain float values
                txtrpm.Text = ets2rpm;
                txtmaxrpm.Text = ets2maxrpm;
                txtodometer.Text = ets2odometer;
                txtfuel.Text = ets2fuel;
                txtgear.Text = ets2gear;
                txtwatertemp.Text = ets2watertemp;
                txtodometer.Text = ets2odometer;
                txtmaxrpm.Text = ets2maxrpm;
                txtoiltemp.Text = ets2oiltemp;
                txtairpres.Text = ets2airpres;
                txtbraketemp.Text = ets2braketemp;
                //drivetrain bool values
                txtelectricEnabled.Text = ets2engineEnabled;
                txtengineEnabled.Text = ets2engineEnabled;
                txtFuelWarningLight.Text = ets2FuelWarningLight;
                txtCruiseControl.Text = ets2CruiseControl;
                txtParkingBrake.Text = ets2ParkingBrake;
                txtMotorBrake.Text = ets2MotorBrake;
                //job float values
                txtmass.Text = ets2mass;
                txtTrailerID.Text = ets2TrailerID;
                txtTrailerName.Text = ets2TrailerName;
                txtCargo.Text = ets2Cargo;
                txtDeadline.Text = ets2Deadline;
                txtCitySource.Text = ets2CitySource;
                txtCityDestination.Text = ets2CityDestination;
                txtCompanySource.Text = ets2CompanySource;
                txtCompanyDestination.Text = ets2CompanyDestination;
                txtNavigationDistanceLeft.Text = ets2NavigationDistanceLeft;
                txtSpeedLimit.Text = ets2SpeedLimit;
                txtTrailerAttached.Text = ets2TrailerAttached;
                //lights bool values
                txtLeftBlinkerActive.Text = ets2BlinkerLeftActive;
                txtRightBlinkerActive.Text = ets2BlinkerRightActive;
                txtLeftBlinkerOn.Text = ets2BlinkerLeftOn;
                txtRightBlinkerOn.Text = ets2BlinkerRightOn;
                txtParkingLights.Text = ets2ParkingLights;
                txtLowBeams.Text = ets2LowBeams;
                txtHighBeams.Text = ets2HighBeams;
                txtFrontAux.Text = ets2FrontAux;
                txtRoofAux.Text = ets2RoofAux;
                txtBrakeLight.Text = ets2BrakeLight;
                txtReverseLight.Text = ets2ReverseLight;
                txtLightsDashboard.Text = ets2LightsDashboard;
                txtBeacon.Text = ets2Beacon;
                //damage float values
                txtEngineDamage.Text = ets2EngineDamage;
                txtTransmissionDamage.Text = ets2TransmissionDamage;
                txtCabinDamage.Text = ets2CabinDamage;
                txtChassisDamage.Text = ets2ChassisDamage;
                txtWheelsDamage.Text = ets2WheelsDamage;
                txtTrailerDamage.Text = ets2TrailerDamage;

                //
                //output string for arduino
                //
                //ets2rpm = "10";

                ets2arduino = ets2speedKmh + "*" + ets2rpm + "*" + ets2fuel + "*" + ets2watertemp + "*" + ets2gear + "*" + ets2engineEnabled + "*" + ets2electricEnabled + "*" + ets2TrailerAttached + "*" + ets2FuelWarningLight + "*";
                //ets2arduino = ets2speedKmh;
                txtets2arduino.Text = ets2arduino;

                #endregion
            }
            catch
            {
            }

        }

        private void Cmbgame_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbgame.SelectedIndex)
            {
                case 0:
                    //ets2
                    //telemetry data = telemetry_data + telemetry_data (loop)
                    Telemetry.Data += Telemetry_Data;
                    break;
                case 1:
                    //ATS
                    break;
                case 2:
                    //Beamng
                    break;

            }
        }

        private void BtnOpen_Click_1(object sender, EventArgs e)
        {
            //testen
            if (isConnected == false)
            {
                connectToArduino();
            }
            else
            {
                disconnectFromArduino();
            }
        }


        //serial connect void
        private void connectToArduino()
        {
            isConnected = true;
            serialPort.PortName = cmbsp.Text;
            serialPort.Open();
            btnOpen.Text = "Disconnect";
        }

        //serial disconnect void
        private void disconnectFromArduino()
        {
            isConnected = false;
            serialPort.Close();
            btnOpen.Text = "Connect";
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            switch (cmbgame.SelectedIndex)
            {
                case 0:
                    //ets2
                    serialPort.WriteLine(ets2arduino);
                    break;
                case 1:
                    //ATS
                    break;
                case 2:
                    //Beamng
                    break;
            }
            
        }
    }
}
