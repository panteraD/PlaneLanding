using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using GalaSoft.MvvmLight.CommandWpf;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SelectionMode = OxyPlot.SelectionMode;

namespace mainWindow
{
    class ViewModel : INotifyPropertyChanged
    {
        
        private DataGrid dataGridMass;
        private DataGrid dataGridMass2;
        private DataGrid dataGridSpeed;
        private DataGrid dataGridSpeed2;
        #region binding stuff
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
        
        //OxyRender smth
        private PlotModel _plotModel;

        //Binding prpops
        private ModelData _data;
        private PointsDummy _pointsDummyMass;
        private PointsDummy _pointsDummySpeed;
        private List<ModelData> _dataMassPointsList = new List<ModelData>();
        private List<ModelData> _dataSpeedPointsList = new List<ModelData>();
        private PlotController _customPlotController;

     




        #region binding properties

        public List<ModelData> DataMassPointsList
        {
            get { return _dataMassPointsList; }
            set { _dataMassPointsList = value; OnPropertyChanged("DataMassPointsList"); }
        }

        public List<ModelData> DataSpeedPointsList
        {
            get { return _dataSpeedPointsList; }
            set { _dataSpeedPointsList = value; OnPropertyChanged("DataSpeedPointsList"); }
        }


        public ModelData Data
        {
            get { return _data; }
            set { _data = value; OnPropertyChanged("Data"); }
        }



        public PlotModel PlotModel
        {
            get { return _plotModel; }
            set { _plotModel = value; OnPropertyChanged("PlotModel"); }
        }

        public PointsDummy PointsDummyMass
        {
            get { return _pointsDummyMass; }
            set { _pointsDummyMass = value; OnPropertyChanged("PointsDummyMass"); }
        }

        public PointsDummy PointsDummySpeed
        {
            get { return _pointsDummySpeed; }
            set { _pointsDummySpeed = value; OnPropertyChanged("PointsDummySpeed"); }
        }

        public PlotController CustomPlotController
        {
            get { return _customPlotController; }
            set { _customPlotController = value; OnPropertyChanged("CustomPlotController"); }
        }

        #endregion

        public ViewModel()
        {
            _data = new ModelData();
            //Shole point legend when hovering
            _customPlotController = new PlotController();
            _customPlotController.UnbindMouseDown(OxyMouseButton.Left);
            _customPlotController.BindMouseEnter(PlotCommands.HoverSnapTrack);
            _plotModel = new PlotModel();
            _pointsDummyMass = new PointsDummy();
            _pointsDummySpeed = new PointsDummy();
            //TESTING
            InitData(_data);
            //PointsDummySpeed.ModelData1.MaxNumber = 0.5;
            //PointsDummySpeed.ModelData2.MaxNumber = 0.6;
            //PointsDummySpeed.ModelData3.MaxNumber = 0.7;
            //PointsDummySpeed.ModelData4.MaxNumber = 0.8;
            //PointsDummySpeed.ModelData5.MaxNumber = 0.9;

        }

        public DataGrid DataGridMass
        {
            get { return dataGridMass; }
            set { dataGridMass = value; }
        }

        public DataGrid DataGridSpeed
        {
            get { return dataGridSpeed; }
            set { dataGridSpeed = value; }
        }


        public DataGrid DataGridMass2
        {
            get { return dataGridMass2; }
            set { dataGridMass2 = value; }
        }

        public DataGrid DataGridSpeed2
        {
            get { return dataGridSpeed2; }
            set { dataGridSpeed2 = value; }
        }


        private void InitData(ModelData data)
        {
            data.Mass = 165d;
            data.LongitudinalWind = 0;
            data.CrossWind = 0;
            data.Hlanding = 15d;
            data.Temperature = 25; //TODO: is it really needed?
            data.CyMaxLanding = 2.35d;
            //data.CalcCyGliding(); //get CyGlding
            data.CxGliding = 0.18d;
         //   data.CalcKGliding(); //calc
            data.P0 = 1.1839;
            data.S = 350;
           // data.CalcVelocityGliding(); //calc
            data.Ac = 7; //NOT USED
            data.CyStall = 2.4;

            //PP2
           // data.CalcVelocityStall(); //calc
            // make a check
            data.CyLanding = 1.7;
            data.CxLanding = 0.22;
           // data.CalcKLanding(); //calc
            //data.CalcVelocityLanding(); //calc
            //data.CalcKMean();
          //  data.CalcLengthGliding();
            data.CyRun = 1.05;
            data.CxRun = 0.175;
            data.FRunFriction = 0.25;
          //  data.CalcLengthRun();
          //  data.CalcLengthFull();
            data.LengthGiven = 3500;
          //  data.CalcLengthNeeded();





        }

        //TODO: update
        private void AddDataPointForMassList()
        {
            //if (_data.P.Equals(0))
            //{
            //    return;
            //}
            //_dataMassPointsList.Add(_data.Clone());
            dataGridMass.ItemsSource = null;
            dataGridMass.ItemsSource = DataMassPointsList;
            dataGridMass2.ItemsSource = null;
            dataGridMass2.ItemsSource = DataMassPointsList;
        }
        //TODO:update
        private void AddDataPointForSpeedList()
        {
            //if (_data.P.Equals(0))
            //{
            //    return;
            //}
            //_dataSpeedPointsList.Add(_data.Clone());
            dataGridSpeed.ItemsSource = null;
            dataGridSpeed.ItemsSource = DataSpeedPointsList;
            dataGridSpeed2.ItemsSource = null;
            dataGridSpeed2.ItemsSource = DataSpeedPointsList;
        }

        //PLOTTING METHODS

        /// <summary>
        /// Converts ModelData points list to DataPoint list by given props
        /// </summary>
        /// <param name="propX"></param>
        /// <param name="propY"></param>
        /// <param name="dataPointsList"></param>
        /// <returns></returns>
        public List<DataPoint> GetXYPointsFromAll(String propX, String propY, List<ModelData> dataPointsList)
        {
            List<DataPoint> list = new List<DataPoint>();
            foreach (ModelData data in dataPointsList)
            {
                list.Add(new DataPoint((double)data.GetType().GetProperty(propX).GetValue(data, null), (double)data.GetType().GetProperty(propY).GetValue(data, null)));
            }

            return list;
        }

        public enum SortByWhat
        {
            Mass, Speed
        }

        private void ShowPMass()
        {
            UpdatePlot("Mass", "P", "m, кг", "P", "зависимость P от массы", this._dataMassPointsList, SortByWhat.Mass);
        }

        public void ShowQMass()
        {

            UpdatePlot("Mass", "Q", "m, кг", "Q", "зависимость Q от массы", this._dataMassPointsList, SortByWhat.Mass);

        }

        public void ShowPV()
        {
            //UpdatePlot("Velocity", "P", "V, м/с", "P", "зависимость P от скорости", false, this._dataSpeedPointsList, SortByWhat.Speed);
            UpdatePlot("MaxNumber", "P", "M", "P", "зависимость P от скорости", this._dataSpeedPointsList, SortByWhat.Speed);
        }

        public void ShowQV()
        {
            //UpdatePlot("Velocity", "Q", "V, м/с", "Q", "зависимость Q от скорости", false, this._dataSpeedPointsList, SortByWhat.Speed);
            UpdatePlot("MaxNumber", "Q", "М", "Q", "зависимость Q от скорости", this._dataSpeedPointsList, SortByWhat.Speed);
        }




        #region fast points calc
        //retarded piece of code
        /*
        public void Calc5Mass()
        {
            //copy some data
            PointsDummyMass.ModelData1 = _data.CopyDataForMassCalc(PointsDummyMass.ModelData1);
            PointsDummyMass.ModelData2 = _data.CopyDataForMassCalc(PointsDummyMass.ModelData2);
            PointsDummyMass.ModelData3 = _data.CopyDataForMassCalc(PointsDummyMass.ModelData3);
            PointsDummyMass.ModelData4 = _data.CopyDataForMassCalc(PointsDummyMass.ModelData4);
            PointsDummyMass.ModelData5 = _data.CopyDataForMassCalc(PointsDummyMass.ModelData5);

            //do calculation
            PointsDummyMass.ModelData1.CalcMu();
            PointsDummyMass.ModelData2.CalcMu();
            PointsDummyMass.ModelData3.CalcMu();
            PointsDummyMass.ModelData4.CalcMu();
            PointsDummyMass.ModelData5.CalcMu();

            PointsDummyMass.ModelData1.CalcXi();
            PointsDummyMass.ModelData2.CalcXi();
            PointsDummyMass.ModelData3.CalcXi();
            PointsDummyMass.ModelData4.CalcXi();
            PointsDummyMass.ModelData5.CalcXi();
        }

        public void Calc5KDash()
        {
            PointsDummyMass.ModelData1.CalcK();
            PointsDummyMass.ModelData2.CalcK();
            PointsDummyMass.ModelData3.CalcK();
            PointsDummyMass.ModelData4.CalcK();
            PointsDummyMass.ModelData5.CalcK();

            PointsDummyMass.ModelData1.CalcB();
            PointsDummyMass.ModelData2.CalcB();
            PointsDummyMass.ModelData3.CalcB();
            PointsDummyMass.ModelData4.CalcB();
            PointsDummyMass.ModelData5.CalcB();

            PointsDummyMass.ModelData1.CalcLambdaZero();
            PointsDummyMass.ModelData2.CalcLambdaZero();
            PointsDummyMass.ModelData3.CalcLambdaZero();
            PointsDummyMass.ModelData4.CalcLambdaZero();
            PointsDummyMass.ModelData5.CalcLambdaZero();

            PointsDummyMass.ModelData1.CalcLambdas();
            PointsDummyMass.ModelData2.CalcLambdas();
            PointsDummyMass.ModelData3.CalcLambdas();
            PointsDummyMass.ModelData4.CalcLambdas();
            PointsDummyMass.ModelData5.CalcLambdas();

            PointsDummyMass.ModelData1.CalcPQ();
            PointsDummyMass.ModelData2.CalcPQ();
            PointsDummyMass.ModelData3.CalcPQ();
            PointsDummyMass.ModelData4.CalcPQ();
            PointsDummyMass.ModelData5.CalcPQ();

            DataMassPointsList.Clear();
            DataMassPointsList.Add(PointsDummyMass.ModelData1);
            DataMassPointsList.Add(PointsDummyMass.ModelData2);
            DataMassPointsList.Add(PointsDummyMass.ModelData3);
            DataMassPointsList.Add(PointsDummyMass.ModelData4);
            DataMassPointsList.Add(PointsDummyMass.ModelData5);

            dataGridMass.ItemsSource = null;
            dataGridMass.ItemsSource = DataMassPointsList;
            dataGridMass2.ItemsSource = null;
            dataGridMass2.ItemsSource = DataMassPointsList;
        }

        public void Calc5Speed()
        {
            PointsDummySpeed.ModelData1 = _data.CopyDataForSpeedCalc(PointsDummySpeed.ModelData1);
            PointsDummySpeed.ModelData2 = _data.CopyDataForSpeedCalc(PointsDummySpeed.ModelData2);
            PointsDummySpeed.ModelData3 = _data.CopyDataForSpeedCalc(PointsDummySpeed.ModelData3);
            PointsDummySpeed.ModelData4 = _data.CopyDataForSpeedCalc(PointsDummySpeed.ModelData4);
            PointsDummySpeed.ModelData5 = _data.CopyDataForSpeedCalc(PointsDummySpeed.ModelData5);


            PointsDummySpeed.ModelData1.CalcALL();
            PointsDummySpeed.ModelData2.CalcALL();
            PointsDummySpeed.ModelData3.CalcALL();
            PointsDummySpeed.ModelData4.CalcALL();
            PointsDummySpeed.ModelData5.CalcALL();

            DataSpeedPointsList.Clear();
            DataSpeedPointsList.Add(PointsDummySpeed.ModelData1);
            DataSpeedPointsList.Add(PointsDummySpeed.ModelData2);
            DataSpeedPointsList.Add(PointsDummySpeed.ModelData3);
            DataSpeedPointsList.Add(PointsDummySpeed.ModelData4);
            DataSpeedPointsList.Add(PointsDummySpeed.ModelData5);

            dataGridSpeed.ItemsSource = null;
            dataGridSpeed.ItemsSource = DataSpeedPointsList;
            dataGridSpeed2.ItemsSource = null;
            dataGridSpeed2.ItemsSource = DataSpeedPointsList;
        }
        */
        #endregion




        #region Plotting methods



        /// <summary>
        /// Draws plot with given params
        /// </summary>
        /// <param name="xProp">x Property name in ModelData class</param>
        /// <param name="yProp">y Property name in ModelData class</param>
        /// <param name="xAxisTitle">x axis title</param>
        /// <param name="yAxisTitle">y axis title</param>
        /// <param name="legend">legend</param>
        /// <param name="dataPointsList">reference to List containing modelData points</param>
        /// <param name="sortByWhat">sort by SortByWhat enum</param>
        private void UpdatePlot(String xProp, String yProp, String xAxisTitle, String yAxisTitle, String legend, List<ModelData> dataPointsList, SortByWhat sortByWhat)
        {

            LoadData(xProp, yProp, legend, dataPointsList, sortByWhat);
            SetUpAxes(xAxisTitle, yAxisTitle);
        }

        private void SetUpAxes(String xAxisTitle, String yAxisTitle)
        {
            PlotModel.Axes.Clear();
            var yAxis = new OxyPlot.Axes.LinearAxis()
            {
                Position = AxisPosition.Left,
                Title = yAxisTitle,
                TitlePosition = 1,
                TitleFontSize = 20,
                StringFormat = "0.####"

            };

            var xAxis = new OxyPlot.Axes.LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Title = xAxisTitle,
                TitlePosition = 1,
                TitleFontSize = 20,
                StringFormat = "0.####"

            };

            PlotModel.Axes.Add(xAxis);
            PlotModel.Axes.Add(yAxis);

            //PlotModel.Axes.Add(new LinearAxis(AxisPosition.Left, "X") { Title = xAxisTitle });
            //PlotModel.Axes.Add(new LinearAxis(AxisPosition.Bottom, "Y") { Title = yAxisTitle });

            PlotModel.PlotMargins = new OxyThickness(40, 40, 40, 40);
        }


        public void LoadData(String param1, String param2, String legend, List<ModelData> dataPointsList, SortByWhat sortByWhat)
        {
            if (PlotModel != null)
            {
                PlotModel.Series.Clear();
            }
            else
            {
                return;
            }
            var lineSerie = new LineSeries
            {
                StrokeThickness = 2,
                MarkerSize = 3,
                //default if false
                CanTrackerInterpolatePoints = true,
                Title = legend,
                //default if false
                Smooth = true,

                MarkerType = MarkerType.Circle,
            };



            if (dataPointsList != null && dataPointsList.Count < 2)
            {
                MessageBox.Show("Количество добаленных точек меньше двух");
                return;
            }
           
            /*
            if (sortByWhat.Equals(SortByWhat.Mass))
            {
                //сортировка  точек в листе по массе
                dataPointsList.Sort(delegate (ModelData c1, ModelData c2) { return c1.Mass.CompareTo(c2.Mass); });
            }
            else if (sortByWhat.Equals(SortByWhat.Speed))
            {
                dataPointsList.Sort(delegate (ModelData c1, ModelData c2) { return c1.Velocity.CompareTo(c2.Velocity); });
            }
            */

            lineSerie.Points.AddRange(this.GetXYPointsFromAll(param1, param2, dataPointsList));


            PlotModel.Series.Add(lineSerie);
        }

        #endregion

        #region click handlers

        
        private ICommand _calcCyGliding;
        private ICommand _calcKGliding;
        private ICommand _calcG;
        private ICommand _calcVelocityGliding;
        private ICommand _calcVelocityStall;
        private ICommand _calcKLanding;
        private ICommand _calcVelocityLanding;
        private ICommand _calcKMean;
        private ICommand _calcLengthGliding;
        private ICommand _calcLengthRun;
        private ICommand _calcLengthFull;
        private ICommand _calcLengthNeeded;





        private ICommand _updateGraph;
        private ICommand _initializePlot;
        private ICommand _addDataPointMass;
        private ICommand _addDataPointSpeed;
        private ICommand _showpv;
        private ICommand _showqv;
        private ICommand _showph;
        private ICommand _showqh;
        private ICommand _put5Mass;
        private ICommand _put5Kdash;
        private ICommand _put5Speed;

        public ICommand CalcCyGliding => _calcCyGliding ?? (_calcCyGliding = new RelayCommand(delegate {  _data.CalcCyGliding(); }));
        public ICommand CalcKGliding => _calcKGliding ?? (_calcKGliding = new RelayCommand(_data.CalcKGliding));
        public ICommand CalcG => _calcG ?? (_calcG = new RelayCommand(_data.CalcG));
        public ICommand CalcVelocityGliding => _calcVelocityGliding ?? (_calcVelocityGliding = new RelayCommand(_data.CalcVelocityGliding));
        public ICommand CalcVelocityStall => _calcVelocityStall ?? (_calcVelocityStall = new RelayCommand(_data.CalcVelocityStall));
        public ICommand CalcKLanding => _calcKLanding ?? (_calcKLanding = new RelayCommand(_data.CalcKLanding));
        public ICommand CalcVelocityLanding => _calcVelocityLanding ?? (_calcVelocityLanding = new RelayCommand(_data.CalcVelocityLanding));
        public ICommand CalcKMean => _calcKMean ?? (_calcKMean = new RelayCommand(_data.CalcKMean));
        public ICommand CalcLengthGliding => _calcLengthGliding ?? (_calcLengthGliding = new RelayCommand(_data.CalcLengthGliding));
        public ICommand CalcLengthRun => _calcLengthRun ?? (_calcLengthRun = new RelayCommand(_data.CalcLengthRun));
        public ICommand CalcLengthFull => _calcLengthFull ?? (_calcLengthFull = new RelayCommand(_data.CalcLengthFull));
        public ICommand CalcLengthNeeded => _calcLengthNeeded ?? (_calcLengthNeeded = new RelayCommand(
            delegate {_data.CalcLengthFull();
                         _data.CalcLengthNeeded();
            }));




        public ICommand ShowPMassPlot => _initializePlot ?? (_initializePlot = new RelayCommand(ShowPMass));
        public ICommand ShowQMassPlot => _updateGraph ?? (_updateGraph = new RelayCommand(ShowQMass));
        public ICommand ShowPVPlot => _showpv ?? (_showpv = new RelayCommand(ShowPV));
        public ICommand ShowQVPlot => _showqv ?? (_showqv = new RelayCommand(ShowQV));


        public ICommand AddDataPonitMass => _addDataPointMass ?? (_addDataPointMass = new RelayCommand(AddDataPointForMassList));
        public ICommand AddDataPonitSpeed => _addDataPointSpeed ?? (_addDataPointSpeed = new RelayCommand(AddDataPointForSpeedList));

        /*
        public ICommand Put5Mass => _put5Mass ?? (_put5Mass = new RelayCommand());
        public ICommand Put5Kdash => _put5Kdash ?? (_put5Kdash = new RelayCommand());
        public ICommand Put5Speed => _put5Speed ?? (_put5Speed = new RelayCommand());
        */
        

        #endregion
    }
}
