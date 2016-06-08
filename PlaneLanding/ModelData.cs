using System;
using System.ComponentModel;
using System.Collections.Generic;
using OxyPlot;

namespace mainWindow
{
    /// <summary>
    /// Данный класс хранит все необходыме данные для вычислений
    /// </summary>
    public class ModelData : INotifyPropertyChanged, ICloneable
    {
        public const Double MAXCONVERT = 340.3f;
        public const Double GFORCE = 9.81;
        public const Double NU = 0.4;
        public const double CYMAXCONVERT = 0.6;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        #region Constructors

        public ModelData()
        {

        }



        #endregion

        #region private_fileds
        /// <summary>
        /// Высота условного препятствия
        /// </summary>
        private double _hlanding;

        /// <summary>
        /// Масса самолета
        /// </summary>
        private double _mass;

        /// <summary>
        /// Коэффицент трения при пробеге
        /// </summary>
        private double _fRunFriction;

        /// <summary>
        /// Продолная составляющая ветра
        /// </summary>
        private double _longitudinalWind;

        /// <summary>
        /// Поперечный ветер
        /// </summary>
        private double _crossWind;

        /// <summary>
        /// Температура
        /// </summary>
        private double _temperature;

        private double _CyMaxLanding;
        /// <summary>
        /// Коэфф проъемной силы при планировании
        /// _cyMaxLanding*0.6
        /// </summary>
        private double _CyGliding;

        /// <summary>
        /// Коэффициент лобового сопротивления при планировании
        /// </summary>
        private double _CxGliding;

        /// <summary>
        /// Аэродинамическое качество ВС при планировании
        /// </summary>
        private double _KGliding;

        private double _GGliding;

        /// <summary>
        /// Плотность воздуха, кг/м3
        /// </summary>
        private double _p_0;

        /// <summary>
        /// Площадь крыла, м2
        /// </summary>
        private double _S;

        /// <summary>
        /// Скорость предпосадочного планирования 
        /// </summary>
        private double _VelocityGliding;

        /// <summary>
        /// Угол атаки сваливания
        /// </summary>
        private double _ac;

        /// <summary>
        /// Коэффициент подъемной силы сваливания
        /// </summary>
        private double _CyStall;

        /// <summary>
        /// Скорость сваливания, м/с
        /// </summary>
        private double _VelocityStall;

        /// <summary>
        /// Коэффициент подъемной силы при посадочном угле атаки(7-10º)
        /// </summary>
        private double _CyLanding;

        /// <summary>
        /// Коэффициент лобового сопротивления при посадке
        /// </summary>
        private double _CxLanding;

        /// <summary>
        /// Аэродинамическое качество ВС при посадке
        /// </summary>
        private double _KLanding;


        /// <summary>
        /// Посадочная скорость ВС, м/с
        /// </summary>
        private double _VelocityLadning;

        /// <summary>
        /// Среднее аэродинамическое качество ВС на участке планирования - парашютирования
        /// </summary>
        private double _KMean;

        /// <summary>
        /// Дистанция воздушного посадочного участка, м
        /// </summary>
        private double _LengthGliding;

        /// <summary>
        /// Коэффициент подъемной силы при пробеге 
        /// </summary>
        private double _CyRun;

        /// <summary>
        /// Коэффициент лобового сопротивления при пробеге
        /// </summary>
        private double _CxRun;

        /// <summary>
        /// Длина пробега, м
        /// </summary>
        private double _LengthRun;

        /// <summary>
        /// Посадочная дистанция, м
        /// </summary>
        private double _lengthFullDistance;

        /// <summary>
        /// Располагаемая посадочная дистанция, м
        /// </summary>
        private double _LengthGiven;

        /// <summary>
        /// Потребная посадочная дистанция, м
        /// </summary>
        private double _LengthNeeded;

        #endregion


        #region Properties

        public double Mass
        {
            get { return _mass; }
            set
            {
                _mass = value; OnPropertyChanged("Mass");
                CalcG();
            }
        }

        public double Hlanding
        {
            get { return _hlanding; }
            set { _hlanding = value; OnPropertyChanged("Hlanding"); }
        }

        public double FRunFriction
        {
            get { return _fRunFriction; }
            set { _fRunFriction = value; OnPropertyChanged("FRunFRiction"); }
        }

        public double LongitudinalWind
        {
            get { return _longitudinalWind; }
            set { _longitudinalWind = value; OnPropertyChanged("LongitudinalWind"); }
        }

        public double CrossWind
        {
            get { return _crossWind; }
            set { _crossWind = value; OnPropertyChanged("CrossWind"); }
        }

        public double Temperature
        {
            get { return _temperature; }
            set { _temperature = value; OnPropertyChanged("Temperature"); }
        }

        public double CyMaxLanding
        {
            get { return _CyMaxLanding; }
            set { _CyMaxLanding = value; OnPropertyChanged("CyMaxLanding"); }
        }

        public double CyGliding
        {
            get { return _CyGliding; }
            set { _CyGliding = value; OnPropertyChanged("CyGliding"); }
        }

        public double CxGliding
        {
            get { return _CxGliding; }
            set { _CxGliding = value; OnPropertyChanged("CxGliding"); }
        }

        public double KGliding
        {
            get { return _KGliding; }
            set { _KGliding = value; OnPropertyChanged("KGliding"); }
        }

        public double GGliding
        {
            get { return _GGliding; }
            set { _GGliding = value; OnPropertyChanged("GGliding"); }
        }

        public double P0
        {
            get { return _p_0; }
            set { _p_0 = value; OnPropertyChanged("P0"); }
        }

        public double S
        {
            get { return _S; }
            set { _S = value; OnPropertyChanged("S"); }
        }

        public double VelocityGliding
        {
            get { return _VelocityGliding; }
            set { _VelocityGliding = value; OnPropertyChanged("VelocityGliding"); }
        }

        public double Ac
        {
            get { return _ac; }
            set { _ac = value; OnPropertyChanged("Ac"); }
        }

        public double CyStall
        {
            get { return _CyStall; }
            set { _CyStall = value; OnPropertyChanged("CyStall"); }
        }

        public double VelocityStall
        {
            get { return _VelocityStall; }
            set { _VelocityStall = value; OnPropertyChanged("VelocityStall"); }
        }

        public double CyLanding
        {
            get { return _CyLanding; }
            set { _CyLanding = value; OnPropertyChanged("CyLanding"); }
        }

        public double CxLanding
        {
            get { return _CxLanding; }
            set { _CxLanding = value; OnPropertyChanged("CxLanding"); }
        }

        public double KLanding
        {
            get { return _KLanding; }
            set { _KLanding = value; OnPropertyChanged("KLanding"); }
        }

        public double VelocityLadning
        {
            get { return _VelocityLadning; }
            set { _VelocityLadning = value; OnPropertyChanged("VelocityLadning"); }
        }

        public double KMean
        {
            get { return _KMean; }
            set { _KMean = value; OnPropertyChanged("KMean"); }
        }

        public double LengthGliding
        {
            get { return _LengthGliding; }
            set { _LengthGliding = value; OnPropertyChanged("LengthGliding"); }
        }

        public double CyRun
        {
            get { return _CyRun; }
            set { _CyRun = value; OnPropertyChanged("CyRun"); }
        }

        public double CxRun
        {
            get { return _CxRun; }
            set { _CxRun = value; OnPropertyChanged("CxRun"); }
        }

        public double LengthRun
        {
            get { return _LengthRun; }
            set { _LengthRun = value; OnPropertyChanged("LengthRun"); }
        }

        public double LengthFullDistance
        {
            get { return _lengthFullDistance; }
            set { _lengthFullDistance = value; OnPropertyChanged("LengthFullDistance"); }
        }

        public double LengthGiven
        {
            get { return _LengthGiven; }
            set { _LengthGiven = value; OnPropertyChanged("LengthGiven"); }
        }

        public double LengthNeeded
        {
            get { return _LengthNeeded; }
            set { _LengthNeeded = value; OnPropertyChanged("LengthNeeded"); }
        }

        #endregion

        #region  Calculations

        public void CalcCyGliding()
        {
            CyGliding = CyMaxLanding * CYMAXCONVERT;
        }

        public void CalcKGliding()
        {
            KGliding = CyGliding / CxGliding;
        }

        public void CalcG()
        {
            GGliding = Mass * GFORCE * 1000d;
        }

        public void CalcVelocityGliding()
        {
            VelocityGliding = Math.Sqrt(2d * GGliding / (P0 * S * CyGliding));
        }

        public void CalcVelocityStall()
        {
            VelocityStall = Math.Sqrt(2d * GGliding / (P0 * S * CyStall));

        }

        public void CalcKLanding()
        {
            KLanding = CyLanding / CxLanding;
        }

        public void CalcVelocityLanding()
        {
            VelocityLadning = Math.Sqrt(2d * GGliding / (P0 * S * CyLanding));
        }

        public void CalcKMean()
        {
            KMean = (KGliding + KLanding) * 0.5d;
        }

        public void CalcLengthGliding()
        {
            LengthGliding = KMean *
                            ((VelocityGliding * VelocityGliding - VelocityLadning * VelocityLadning) / (2d * GFORCE) + Hlanding);
        }

        public void CalcLengthRun()
        {
            LengthRun = (VelocityLadning + LongitudinalWind) * (VelocityLadning + LongitudinalWind) / 
                (GFORCE * (CxRun / CyLanding + FRunFriction * (2d - CyRun / CyLanding)));

        }

        public void CalcLengthFull()
        {
            LengthFullDistance = LengthGliding + LengthRun;
        }

        public void CalcLengthNeeded()
        {
            LengthNeeded = LengthFullDistance * 1.67d;
        }

        public bool IsLengthNeededLessThanGiven()
        {
            return LengthNeeded < LengthGiven ? true : false;
        }


        /// <summary>
        /// Проверка: Vпл/Vc > 1,3
        /// </summary>
        /// <returns></returns>
        public bool IsVelocityCheck()
        {
            return (VelocityGliding / VelocityStall > 1.3d) ? true : false;
        }

        public void CalcAll()
        {
            this.CalcCyGliding();
            this.CalcKGliding();
            this.CalcVelocityGliding();
            this.CalcVelocityStall();
            this.CalcKLanding();
            this.CalcVelocityLanding();
            this.CalcKMean();
            this.CalcLengthGliding();
            this.CalcLengthRun();
            this.CalcLengthFull();
            this.CalcLengthNeeded();
        }


        #endregion


        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
