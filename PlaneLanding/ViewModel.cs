using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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

        private DataGrid dataGridTemp;
        private DataGrid dataGridTemp2;
        private DataGrid dataGridFriction;
        private DataGrid dataGridFriction2;
        private DataGrid dataGridWind;
        private DataGrid dataGridWind2;
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
        private PointsDummy _pointsDummyTemp;
        private PointsDummy _pointsDummyFriction;
        private PointsDummy _pointsDummyWind;
        private List<ModelData> _dataTempPointsList = new List<ModelData>();
        private List<ModelData> _dataFrictionPointsList = new List<ModelData>();
        private List<ModelData> _dataWindPointsList = new List<ModelData>();
        private PlotController _customPlotController;






        #region binding properties

        public List<ModelData> DataTempPointsList
        {
            get { return _dataTempPointsList; }
            set { _dataTempPointsList = value; OnPropertyChanged("DataTempPointsList"); }
        }

        public List<ModelData> DataFrictionPointsList
        {
            get { return _dataFrictionPointsList; }
            set { _dataFrictionPointsList = value; OnPropertyChanged("DataFrictionPointsList"); }
        }

        public List<ModelData> DataWindPointsList
        {
            get { return _dataWindPointsList; }
            set { _dataWindPointsList = value; OnPropertyChanged("DataWindPointsList"); }
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

        public PointsDummy PointsDummyTemp
        {
            get { return _pointsDummyTemp; }
            set { _pointsDummyTemp = value; OnPropertyChanged("PointsDummyTemp"); }
        }

        public PointsDummy PointsDummyFriction
        {
            get { return _pointsDummyFriction; }
            set { _pointsDummyFriction = value; OnPropertyChanged("PointsDummyFriction"); }
        }

        public PointsDummy PointsDummyWind
        {
            get { return _pointsDummyWind; }
            set { _pointsDummyWind = value; OnPropertyChanged("PointsDummyWind"); }
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
            _pointsDummyTemp = new PointsDummy();
            _pointsDummyFriction = new PointsDummy();
            _pointsDummyWind = new PointsDummy();
            //TESTING
            InitData(_data);
        }

        public DataGrid DataGridTemp
        {
            get { return dataGridTemp; }
            set { dataGridTemp = value; }
        }

        public DataGrid DataGridFriction
        {
            get { return dataGridFriction; }
            set { dataGridFriction = value; }
        }

        public DataGrid DataGridWind
        {
            get { return dataGridWind; }
            set { dataGridWind = value; }
        }


        public DataGrid DataGridTemp2
        {
            get { return dataGridTemp2; }
            set { dataGridTemp2 = value; }
        }

        public DataGrid DataGridFriction2
        {
            get { return dataGridFriction2; }
            set { dataGridFriction2 = value; }
        }

        public DataGrid DataGridWind2
        {
            get { return dataGridWind2; }
            set { dataGridWind2 = value; }
        }


        private void InitData(ModelData data)
        {
            data.Mass = 165d;
            data.LongitudinalWind = 0;
            data.CrossWind = 10;
            data.Hlanding = 15d;
            data.Temperature = 25;
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

            PointsDummyFriction.ModelData1.FRunFriction = 0.25;
            PointsDummyFriction.ModelData2.FRunFriction = 0.1;
            PointsDummyFriction.ModelData3.FRunFriction = 0.07;
            PointsDummyFriction.ModelData4.FRunFriction = 0.045;
            PointsDummyFriction.ModelData5.FRunFriction = 0.03;

            PointsDummyTemp.ModelData1.P0 = 1.1455;
            PointsDummyTemp.ModelData2.P0 = 1.1839;
            PointsDummyTemp.ModelData3.P0 = 1.225;
            PointsDummyTemp.ModelData4.P0 = 1.269;
            PointsDummyTemp.ModelData5.P0 = 1.3163;
            PointsDummyTemp.ModelData6.P0 = 1.3673;
            PointsDummyTemp.ModelData7.P0 = 1.4224;

            PointsDummyTemp.ModelData1.Temperature = 35;
            PointsDummyTemp.ModelData2.Temperature = 25;
            PointsDummyTemp.ModelData3.Temperature = 15;
            PointsDummyTemp.ModelData4.Temperature = 5;
            PointsDummyTemp.ModelData5.Temperature = -5;
            PointsDummyTemp.ModelData6.Temperature = -15;
            PointsDummyTemp.ModelData7.Temperature = -25;

            PointsDummyWind.ModelData1.LongitudinalWind = -5d;
            PointsDummyWind.ModelData2.LongitudinalWind = 0;
            PointsDummyWind.ModelData3.LongitudinalWind = 5d;
            PointsDummyWind.ModelData4.LongitudinalWind = 10d;
            PointsDummyWind.ModelData5.LongitudinalWind = 15d;





        }

        //TODO: update
        private void AddDataPointForTempList()
        {
            //if (_data.P.Equals(0))
            //{
            //    return;
            //}
            _dataTempPointsList.Add((ModelData)_data.Clone());
            UpdateTempDataGrid();

        }
        //TODO:update
        private void AddDataPointForFrictionList()
        {
            //if (_data.P.Equals(0))
            //{
            //    return;
            //}
            _dataFrictionPointsList.Add((ModelData)_data.Clone());
            UpdateFrictionDataGrid();

        }

        private void AddDataPointForWindList()
        {
            //if (_data.P.Equals(0))
            //{
            //    return;
            //}
            _dataWindPointsList.Add((ModelData)_data.Clone());
            UpdateWindDataGrid();
        }

        private void UpdateTempDataGrid()
        {
            dataGridTemp.ItemsSource = null;
            dataGridTemp.ItemsSource = DataTempPointsList;
            dataGridTemp2.ItemsSource = null;
            dataGridTemp2.ItemsSource = DataTempPointsList;
        }

        private void UpdateFrictionDataGrid()
        {
            dataGridFriction.ItemsSource = null;
            dataGridFriction.ItemsSource = DataFrictionPointsList;
            dataGridFriction2.ItemsSource = null;
            dataGridFriction2.ItemsSource = DataFrictionPointsList;
        }

        private void UpdateWindDataGrid()
        {
            dataGridWind.ItemsSource = null;
            dataGridWind.ItemsSource = DataWindPointsList;
            dataGridWind2.ItemsSource = null;
            dataGridWind2.ItemsSource = DataWindPointsList;
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
            PO, Friction, Wind
        }


        public void ShowLTPlot()
        {
            UpdatePlot("Temperature", "LengthFullDistance", "T, град", "Lпос, м", "зависимость L от температуры", this.DataTempPointsList, SortByWhat.PO);
        }

        public void ShowLFPlot()
        {
            UpdatePlot("FRunFriction", "LengthFullDistance", "F_тр", "Lпос, м", "зависимость L от коэффицента трения", this.DataFrictionPointsList, SortByWhat.Friction);
        }

        public void ShowLWPlot()
        {
            UpdatePlot("LongitudinalWind", "LengthFullDistance", "W, м/с", "Lпос, м", "зависимость L от продольного ветра", this.DataWindPointsList, SortByWhat.Wind);
        }




        #region fast points calc


        public void Calc7Temp()
        {
            List<Double> pressureCopy = new List<double>()
            { PointsDummyTemp.ModelData1.P0,
                PointsDummyTemp.ModelData2.P0,
                PointsDummyTemp.ModelData3.P0,
                PointsDummyTemp.ModelData4.P0,
                PointsDummyTemp.ModelData5.P0,
                PointsDummyTemp.ModelData6.P0,
                PointsDummyTemp.ModelData7.P0,
           };


            List<Double> tempCopy = new List<double>()
            {
                PointsDummyTemp.ModelData1.Temperature,
                PointsDummyTemp.ModelData2.Temperature,
                PointsDummyTemp.ModelData3.Temperature,
                PointsDummyTemp.ModelData4.Temperature,
                PointsDummyTemp.ModelData5.Temperature,
                PointsDummyTemp.ModelData6.Temperature,
                PointsDummyTemp.ModelData7.Temperature,
            };

            DataTempPointsList.Clear();

            PointsDummyTemp = new PointsDummy(_data);
            for (int i = 0; i < PointsDummyTemp.PointsDummyList.Count; i++)
            {
                PointsDummyTemp.PointsDummyList[i].P0 = pressureCopy[i];
                PointsDummyTemp.PointsDummyList[i].Temperature = tempCopy[i];
                PointsDummyTemp.PointsDummyList[i].CalcAll();
                DataTempPointsList.Add(PointsDummyTemp.PointsDummyList[i]);
            }
            UpdateTempDataGrid();
        }

        public void Calc5Friction()
        {
            List<Double> tempValues = new List<double>()
            { PointsDummyFriction.ModelData1.FRunFriction,
                PointsDummyFriction.ModelData2.FRunFriction,
                PointsDummyFriction.ModelData3.FRunFriction,
                PointsDummyFriction.ModelData4.FRunFriction,
                //PointsDummyFriction.ModelData5.FRunFriction,
           };

            DataFrictionPointsList.Clear();

            PointsDummyFriction = new PointsDummy(_data);
            for (int i = 0; i < tempValues.Count; i++)
            {
                PointsDummyFriction.PointsDummyList[i].FRunFriction = tempValues[i];
                PointsDummyFriction.PointsDummyList[i].CalcAll();
                DataFrictionPointsList.Add(PointsDummyFriction.PointsDummyList[i]);
            }
            UpdateFrictionDataGrid();
        }

        public void Calc5Wind()
        {
            List<Double> tempValues = new List<double>() {
             PointsDummyWind.ModelData1.LongitudinalWind,
             PointsDummyWind.ModelData2.LongitudinalWind,
             PointsDummyWind.ModelData3.LongitudinalWind,
             PointsDummyWind.ModelData4.LongitudinalWind,
             PointsDummyWind.ModelData5.LongitudinalWind
           };

            DataWindPointsList.Clear();

            PointsDummyWind = new PointsDummy(_data);
            for (int i = 0; i < tempValues.Count; i++)
            {
                PointsDummyWind.PointsDummyList[i].LongitudinalWind = tempValues[i];
                PointsDummyWind.PointsDummyList[i].CalcAll();
                DataWindPointsList.Add(PointsDummyWind.PointsDummyList[i]);
            }
            UpdateWindDataGrid();
        }




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
                AxisTitleDistance = 20,
                
                Title = yAxisTitle,
                TitlePosition = 0.87,
                TitleFontSize = 45,
                FontSize = 25,
                StringFormat = "0.####"

            };

            var xAxis = new OxyPlot.Axes.LinearAxis()
            {
                Position = AxisPosition.Bottom,
                AxisTitleDistance = 20,
                Title = xAxisTitle,
                TitlePosition = 0.95,
                TitleFontSize = 45,
                FontSize = 20,
                StringFormat = "0.####"

            };

            PlotModel.Axes.Add(xAxis);
            PlotModel.Axes.Add(yAxis);


            PlotModel.PlotMargins = new OxyThickness(90, 40, 40,80);
            PlotModel.LegendFontSize = 30;
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
                StrokeThickness = 5,
                MarkerSize = 6,
                //default if false
                CanTrackerInterpolatePoints = true,
                Title = legend,
                //default if false
                //Smooth = true,

                MarkerType = MarkerType.Circle,
            };



            if (dataPointsList != null && dataPointsList.Count < 2)
            {
                MessageBox.Show("Количество добаленных точек меньше двух");
                return;
            }


            if (sortByWhat.Equals(SortByWhat.PO))
            {
                
                dataPointsList.Sort(delegate (ModelData c1, ModelData c2) { return c1.Temperature.CompareTo(c2.Temperature); });
            }
            else if (sortByWhat.Equals(SortByWhat.Friction))
            {
                dataPointsList.Sort(delegate (ModelData c1, ModelData c2) { return c1.FRunFriction.CompareTo(c2.FRunFriction); });
            }
            else if (sortByWhat.Equals(SortByWhat.Wind))
            {
                dataPointsList.Sort(delegate (ModelData c1, ModelData c2) { return c1.LongitudinalWind.CompareTo(c2.LongitudinalWind); });
            }


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





        private ICommand _addDataPointTemp;
        private ICommand _addDataPointFriction;
        private ICommand _addDataPointWind;
        private ICommand _showLT;
        private ICommand _showLF;
        private ICommand _showLW;
        private ICommand _put5Temp;
        private ICommand _put5Friction;
        private ICommand _put5Wind;
        //private ICommand _put5Speed;

        public ICommand CalcCyGliding => _calcCyGliding ?? (_calcCyGliding = new RelayCommand(delegate { _data.CalcCyGliding(); }));
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
            delegate
            {
                _data.CalcLengthFull();
                _data.CalcLengthNeeded();
            }));




        public ICommand ShowLT => _showLT ?? (_showLT = new RelayCommand(ShowLTPlot));
        public ICommand ShowLF => _showLF ?? (_showLF = new RelayCommand(ShowLFPlot));
        public ICommand ShowLW => _showLW ?? (_showLW = new RelayCommand(ShowLWPlot));


        public ICommand AddDataPonitTemp => _addDataPointTemp ?? (_addDataPointTemp = new RelayCommand(AddDataPointForTempList));
        public ICommand AddDataPonitFriction => _addDataPointFriction ?? (_addDataPointFriction = new RelayCommand(AddDataPointForFrictionList));
        public ICommand AddDataPonitWind => _addDataPointWind ?? (_addDataPointWind = new RelayCommand(AddDataPointForWindList));


        public ICommand Put5Temp => _put5Temp ?? (_put5Temp = new RelayCommand(Calc7Temp));
        public ICommand Put5Friction => _put5Friction ?? (_put5Friction = new RelayCommand(Calc5Friction));
        public ICommand Put5Wind => _put5Wind ?? (_put5Wind = new RelayCommand(Calc5Wind));



        #endregion
    }
}
