using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace mainWindow
{
    class PointsDummy : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public PointsDummy()
        {
            _modelData1 = new ModelData();
            _modelData2 = new ModelData();
            _modelData3 = new ModelData();
            _modelData4 = new ModelData();
            _modelData5 = new ModelData();
            _modelData6 = new ModelData();
            _modelData7 = new ModelData();
            _pointsDummyList = new List<ModelData>() {_modelData1, _modelData2, _modelData3, _modelData4, _modelData5, _modelData6 , _modelData7 };
        }

        public PointsDummy(ModelData modelData1, ModelData modelData2, ModelData modelData3, ModelData modelData4, ModelData modelData5, ModelData modelData6,  ModelData modelData7)
        {
            _modelData1 = modelData1;
            _modelData2 = modelData2;
            _modelData3 = modelData3;
            _modelData4 = modelData4;
            _modelData5 = modelData5;
            _modelData7 = modelData6;
            _modelData6 = modelData7;
            _pointsDummyList = new List<ModelData>() { modelData1, modelData2, modelData3, modelData4, modelData5 };
        }

        public PointsDummy(ModelData data)
        {
            _modelData1 = (ModelData)data.Clone();
            _modelData2 = (ModelData)data.Clone();
            _modelData3 = (ModelData)data.Clone();
            _modelData4 = (ModelData)data.Clone();
            _modelData5 = (ModelData)data.Clone();
            _modelData6 = (ModelData)data.Clone();
            _modelData7 = (ModelData)data.Clone();

            _pointsDummyList = new List<ModelData>() { _modelData1, _modelData2, _modelData3, _modelData4, _modelData5, _modelData6 , _modelData7 };
        }

       






        #region private_fileds
        private ModelData _modelData1;
        private ModelData _modelData2;
        private ModelData _modelData3;
        private ModelData _modelData4;
        private ModelData _modelData5;
        private ModelData _modelData6;
        private ModelData _modelData7;
        private List<ModelData> _pointsDummyList; 

        #endregion

       

        public ModelData ModelData1
        {
            get { return _modelData1; }
            set { _modelData1 = value; OnPropertyChanged("ModelData1"); }
        }

        public ModelData ModelData2
        {
            get { return _modelData2; }
            set { _modelData2 = value; OnPropertyChanged("ModelData2"); }
        }

        public ModelData ModelData3
        {
            get { return _modelData3; }
            set { _modelData3 = value; OnPropertyChanged("ModelData3"); }
        }

        public ModelData ModelData4
        {
            get { return _modelData4; }
            set { _modelData4 = value; OnPropertyChanged("ModelData4"); }
        }

        public ModelData ModelData5
        {
            get { return _modelData5; }
            set { _modelData5 = value; OnPropertyChanged("ModelData5"); }
        }

        public ModelData ModelData6
        {
            get { return _modelData6; }
            set { _modelData6= value; OnPropertyChanged("ModelData6"); }
        }

        public ModelData ModelData7
        {
            get { return _modelData7; }
            set { _modelData7 = value; OnPropertyChanged("ModelData7"); }
        }

        public List<ModelData> PointsDummyList
        {
            get { return _pointsDummyList; }
            set { _pointsDummyList = value; }
        }
    }
}
