using System;

namespace TabMenu2
{
    internal class MainViewModel : BaseViewModel
    {
        private string myViete = "This is Viete";
        private string mysinCorrectTill = "0";
        private string myBackground = "#fff";
        private uint mysinPrecision = 1000;
        private uint mysinMinPrec = 5;
        private uint mysinMaxPrec = 10000;
        private bool mysinProgress = false;

        private string mysqrtCorrectTill = "0";
        private uint mysqrtPrecision = 1000;
        private uint mysqrtMinPrec = 5;
        private uint mysqrtMaxPrec = 10000;
        private bool mysqrtProgress = false;

        private string mysechCorrectTill = "0";
        private uint mysechPrecision = 1000;
        private uint mysechMinPrec = 5;
        private uint mysechMaxPrec = 10000;
        private bool mysechProgress = false;

        static MainViewModel _details;


        public string Viete {
            get { return myViete; }
            set {
                myViete = value;
                OnPropertyChanged("Viete");
            }
        }

        public string SinCorrectTill {
            get { return mysinCorrectTill; }
            set {
                mysinCorrectTill = value;
                OnPropertyChanged("SinCorrectTill");
            }
        }

        public string SqrtCorrectTill {
            get { return mysqrtCorrectTill; }
            set {
                mysqrtCorrectTill = value;
                OnPropertyChanged("SqrtCorrectTill");
            }
        }

        public string SechCorrectTill {
            get { return mysechCorrectTill; }
            set {
                mysechCorrectTill = value;
                OnPropertyChanged("SechCorrectTill");
            }
        }

        public string Background {
            get { return myBackground; }
            set {
                myBackground = value;
                OnPropertyChanged("Background");
            }
        }

        public bool isSinProgress {
            get { return mysinProgress; }
            set {
                mysinProgress = value;
                OnPropertyChanged("isSinProgress");
            }
        }

        public bool isSqrtProgress {
            get { return mysqrtProgress; }
            set {
                mysqrtProgress = value;
                OnPropertyChanged("isSqrtProgress");
            }
        }

        public bool isSechProgress {
            get { return mysechProgress; }
            set {
                mysechProgress = value;
                OnPropertyChanged("isSechProgress");
            }
        }

        public uint SinPrecision {
            get { return mysinPrecision; }
            set {
                mysinPrecision = value;
                OnPropertyChanged("SinPrecision");
            }
        }

        public uint SqrtPrecision {
            get { return mysqrtPrecision; }
            set {
                mysqrtPrecision = value;
                OnPropertyChanged("SqrtPrecision");
            }
        }

        public uint SechPrecision {
            get { return mysechPrecision; }
            set {
                mysechPrecision = value;
                OnPropertyChanged("SechPrecision");
            }
        }

        public uint SinMaxPrec {
            get { return mysinMaxPrec; }
            set {
                mysinMaxPrec = value;
                OnPropertyChanged("SinMaxPrec");
            }
        }

        public uint SqrtMaxPrec {
            get { return mysqrtMaxPrec; }
            set {
                mysqrtMaxPrec = value;
                OnPropertyChanged("SqrtMaxPrec");
            }
        }

        public uint SechMaxPrec {
            get { return mysechMaxPrec; }
            set {
                mysechMaxPrec = value;
                OnPropertyChanged("MaxPrec");
            }
        }

        public uint SinMinPrec {
            get { return mysinMinPrec; }
            set {
                mysinMinPrec = value;
                OnPropertyChanged("SinMinPrec");
            }
        }

        public uint SqrtMinPrec {
            get { return mysqrtMinPrec; }
            set {
                mysqrtMinPrec = value;
                OnPropertyChanged("SqrtMinPrec");
            }
        }

        public uint SechMinPrec {
            get { return mysechMinPrec; }
            set {
                mysechMinPrec = value;
                OnPropertyChanged("SechMinPrec");
            }
        }

        public static MainViewModel GetDetails()
        {
            if (_details == null)
                _details = new MainViewModel();

            return _details;
        }
        
    }
}