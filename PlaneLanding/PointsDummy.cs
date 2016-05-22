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
            _pointsDummyList = new List<ModelData>() {_modelData1, _modelData2, _modelData3, _modelData4, _modelData5};
        }

        public PointsDummy(ModelData modelData1, ModelData modelData2, ModelData modelData3, ModelData modelData4, ModelData modelData5)
        {
            _modelData1 = modelData1;
            _modelData2 = modelData2;
            _modelData3 = modelData3;
            _modelData4 = modelData4;
            _modelData5 = modelData5;
            _pointsDummyList = new List<ModelData>() { modelData1, modelData2, modelData3, modelData4, modelData5 };
        }

        public PointsDummy(ModelData data)
        {
            _modelData1 = (ModelData)data.Clone();
            _modelData2 = (ModelData)data.Clone();
            _modelData3 = (ModelData)data.Clone();
            _modelData4 = (ModelData)data.Clone();
            _modelData5 = (ModelData)data.Clone();
            _pointsDummyList = new List<ModelData>() { _modelData1, _modelData2, _modelData3, _modelData4, _modelData5 };
        }

       






        #region private_fileds
        private ModelData _modelData1;
        private ModelData _modelData2;
        private ModelData _modelData3;
        private ModelData _modelData4;
        private ModelData _modelData5;
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

        public List<ModelData> PointsDummyList
        {
            get { return _pointsDummyList; }
            set { _pointsDummyList = value; }
        }
    }
}
