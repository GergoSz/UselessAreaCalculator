using System;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TabMenu2
{

    public partial class MainWindow : Window
    {

        private readonly MainViewModel myDataContext;
        public Polyline sinCurve;
        private Canvas mySinCanvas;
        private Button mySinUpdateButton;
        private Slider mySinPrecSlider;

        public Polyline sqrtCurve;
        private Canvas mySqrtCanvas;
        private Button mySqrtUpdateButton;
        private Slider mySqrtPrecSlider;

        public Polyline sechCurve;
        private Canvas mySechCanvas;
        private Button mySechUpdateButton;
        private Slider mySechPrecSlider;

        enum EnumFunc { Sin, Sqrt, Sech };

        public MainWindow()
        {
            InitializeComponent();

            myDataContext = MainViewModel.GetDetails();
            DataContext = myDataContext;

            mySinCanvas = (Canvas)this.FindName("sincanvas");
            mySinUpdateButton = (Button)this.FindName("sinButton");
            mySinPrecSlider = (Slider)this.FindName("SinPrecSlider");

            mySqrtCanvas = (Canvas)this.FindName("sqrtcanvas");
            mySqrtUpdateButton = (Button)this.FindName("sqrtButton");
            mySqrtPrecSlider = (Slider)this.FindName("SqrtPrecSlider");

            mySechCanvas = (Canvas)this.FindName("sechcanvas");
            mySechUpdateButton = (Button)this.FindName("sechButton");
            mySechPrecSlider = (Slider)this.FindName("SechPrecSlider");

            sinCurve = new Polyline();
            sinCurve.Stroke = Brushes.Purple;
            sinCurve.StrokeThickness = 1;

            sqrtCurve = new Polyline();
            sqrtCurve.Stroke = Brushes.Purple;
            sqrtCurve.StrokeThickness = 1;

            sechCurve = new Polyline();
            sechCurve.Stroke = Brushes.Purple;
            sechCurve.StrokeThickness = 1;
            //myCanvas.Children.Add(sineCurve);

        }
        

        private async void CalcSinClicked(object sender, RoutedEventArgs e) {
            myDataContext.isSinProgress = true;
            myDataContext.Background = "#FFFFFF";

            mySinUpdateButton.IsEnabled = false;
            mySinPrecSlider.IsEnabled = false;

            mySinCanvas.Children.Clear();
            sinCurve.Points.Clear();

            await Task.Factory.StartNew(() => General(0, (3.14159265358 * 4), myDataContext.SinPrecision, EnumFunc.Sin));

            mySinUpdateButton.IsEnabled = true;
            mySinPrecSlider.IsEnabled = true;

            myDataContext.isSinProgress = false;

            /* ConcurrentBag<Tuple<int, int>> bag = new ConcurrentBag<Tuple<int, int>>{
                 Tuple.Create(2, 1),
                 Tuple.Create(3, 1),
                 Tuple.Create(5, 1),
                 Tuple.Create(7, 1)
             };

             var tasks = new[]{
                         new Task(() => Controller.FindHighestPowerUnder(bag, 10000)),
                         new Task(() => Controller.FindHighestPowerUnder(bag, 10000)),
             };

             Array.ForEach(tasks, t => t.Start());

             Task.WaitAll(tasks);*/



            /*await Task.Factory.StartNew(() =>
            myDataContext.Viete = Controller.CalculatePi(10000));
            string vietePi = Controller.GetNext(10).ToString();
            myDataContext.CorrectTill = Controller.TestPi( myDataContext.Viete).ToString();*/



        }

        private async void CalcSqrtClicked(object sender, RoutedEventArgs e)
        {
            myDataContext.isSqrtProgress = true;
            myDataContext.Background = "#FFFFFF";

            mySqrtUpdateButton.IsEnabled = false;
            mySqrtPrecSlider.IsEnabled = false;

            mySqrtCanvas.Children.Clear();
            sqrtCurve.Points.Clear();

            await Task.Factory.StartNew(() => General(0, (3.14159265358 * 4), myDataContext.SqrtPrecision, EnumFunc.Sqrt));

            mySqrtUpdateButton.IsEnabled = true;
            mySqrtPrecSlider.IsEnabled = true;

            myDataContext.isSqrtProgress = false;
        }

        private async void CalcSechClicked(object sender, RoutedEventArgs e)
        {
            myDataContext.isSechProgress = true;
            myDataContext.Background = "#FFFFFF";

            mySechUpdateButton.IsEnabled = false;
            mySechPrecSlider.IsEnabled = false;

            mySechCanvas.Children.Clear();
            sechCurve.Points.Clear();

            await Task.Factory.StartNew(() => General(-5, 5, myDataContext.SechPrecision, EnumFunc.Sech));

            mySechUpdateButton.IsEnabled = true;
            mySechPrecSlider.IsEnabled = true;

            myDataContext.isSechProgress = false;
        }


        public void General(double from, double to, uint divisions, Enum enumFunc){
            
            double step = Integral.GetStep(from, to, divisions);

            double[] FxArray = new double[divisions + 1];

            ConcurrentBag<Tuple<double, double>> bag = new ConcurrentBag<Tuple<double, double>>();

            int j = 0;
            double jcoord = 0;

            Func<double, double> func = Integral.Sinx;

            switch (enumFunc) {
                case EnumFunc.Sin:
                    func = Integral.Sinx;
                    break;
                case EnumFunc.Sqrt:
                    func = Integral.Sqrtx;
                    break;
                case EnumFunc.Sech:
                    func = Integral.Sechx;
                    break;
            }

            foreach (double i in Integral.GetFx(func, from, to, step))
            {
                this.Dispatcher.Invoke(() =>
                {
                    switch (enumFunc)
                    {
                        case EnumFunc.Sin:
                            mySinCanvas.Children.Clear();
                            sinCurve.Points.Add(new Point((jcoord * 40) + 50, (i * 40) - 150));
                            mySinCanvas.Children.Add(sinCurve);
                            break;
                        case EnumFunc.Sqrt:
                            mySqrtCanvas.Children.Clear();
                            sqrtCurve.Points.Add(new Point((jcoord * 40) + 50, (i * 40) - 150));
                            mySqrtCanvas.Children.Add(sqrtCurve);
                            break;
                        case EnumFunc.Sech:
                            mySechCanvas.Children.Clear();
                            sechCurve.Points.Add(new Point((jcoord * 40) + 50, (i * 40) - 150));
                            mySechCanvas.Children.Add(sechCurve);
                            break;
                    }
                    FxArray[j] = i;
                    j++;
                    jcoord += step;
                });

            }

            foreach (Tuple<double, double> i in Integral.GetFxPairs(FxArray))
            {

                bag.Add(i);

            }

            Task<double>[] tasks = new Task<double>[] {
                    new Task<double>( () => FindArea(bag, step)),
                    new Task<double>( () => FindArea(bag, step)),
                    new Task<double>( () => FindArea(bag, step)),
                };

            double sum = 0;

            Array.ForEach(tasks, t => t.Start());
            Array.ForEach(tasks, t => sum += t.Result);
            Task.WaitAll(tasks);

            switch (enumFunc)
            {
                case EnumFunc.Sin:
                    myDataContext.SinCorrectTill = sum.ToString();
                    break;
                case EnumFunc.Sqrt:
                    myDataContext.SqrtCorrectTill = sum.ToString();
                    break;
                case EnumFunc.Sech:
                    myDataContext.SechCorrectTill = sum.ToString();
                    break;
            }
        }

        public static double FindArea(ConcurrentBag<Tuple<double, double>> bag, double step)
        {

            Tuple<double, double> pair;

            double summ = 0;

            // while there are items to take, this will prefer local first, then steal if no local
            while (bag.TryTake(out pair))
            {
                //sineCurve.Points.Add(new Point( pair.Item1 ,  ));
                summ += Integral.GetArea(pair.Item1, pair.Item2, step);

            }

            Console.WriteLine("The area is: " + summ);
            return summ;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            /*int nbrOfTrials = 0, nbrOfSuccesses = 0;
            double probOfSuccesses = 0.00;
            double binomial = 0.00;
            
                binomial = BinomialProbability(nbrOfTrials,
                                    nbrOfSuccesses,
                                    probOfSuccesses);
         */
            
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        // Use the DataObject.Pasting Handler 
        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

    }
}
