using LoonieBot.Win.Locator;
using LoonieTrader.Library.Caches;
using LoonieTrader.Library.Interfaces;
using Serilog;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace LoonieBot.Win
{
    public partial class FormChart : Form
    {
        public FormChart()
        {
            InitializeComponent();

            _logger = ServiceLocator.Container.GetInstance<ILogger>();

            _settingsService = ServiceLocator.Container.GetInstance<ISettingsService>();
            _pricesCache = ServiceLocator.Container.GetInstance<IPricesCache>();



            _pricesCache.NewPrice += _pricesCache_NewPrice;


            //------------------
            //   _timer = new System.Windows.Forms.Timer();
            //   _timer.Tick += Timer_Tick;
            //  _timer.Start();
        }

        private void FormChart_Load(object sender, EventArgs e)
        {
            // https://github.com/kirsan31/winforms-datavisualization

            //            double yValue = 1;
            //Random random = new Random();
            //for (int pointIndex = 0; pointIndex < 20; pointIndex++)
            //{
            //    double yValue =  (random.NextDouble() * 2.0 );
            //    chart1.Series["Series1"].Points.AddY(yValue);
            //}

            chart1.Series["Series1"].ChartType = SeriesChartType.FastLine;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 1.0491;
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 1.0482;

        }

        private void FormChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            _pricesCache.NewPrice -= _pricesCache_NewPrice;

            //   _timer.Stop();
        }

        private void _pricesCache_NewPrice(object? sender, PriceEventArgs e)
        {
            Debug.WriteLine(e.ToString());

            // double yValue = 50.0;
            // Random random = new Random();
            // for (int pointIndex = 0; pointIndex < 20; pointIndex++)
            // {
            // yValue = yValue + (random.NextDouble() * 10.0 - 5.0);


            //  CultureInfo culture = new CultureInfo("en-US");
            double yValue = double.Parse(e.ask, _culture);

            chart1.Series["Series1"].Points.AddY(yValue);

            var yAxis = chart1.ChartAreas["ChartArea1"].AxisY;

            if (_isFirst)
            {
                _isFirst = false;
                yAxis.Maximum = yValue + 0.0001;
                yAxis.Minimum = yValue - 0.0001;
            }

            yAxis.Maximum = Math.Max(yAxis.Maximum, yValue+0.0001);
            yAxis.Minimum = Math.Min(yAxis.Minimum, yValue-0.0001);

            //}

            //           chart1.Series["Series1"].ChartType = SeriesChartType.FastLine;
        }

        // private readonly System.Windows.Forms.Timer _timer;
        private readonly ILogger _logger;
        private bool _isFirst = true;
        private readonly ISettingsService _settingsService;
        private readonly IPricesCache _pricesCache;
        private readonly CultureInfo _culture = new CultureInfo("en-US");



        //private void Timer_Tick(object? sender, EventArgs e)
        //{
        //    double yValue = 50.0;
        //    Random random = new Random();
        //    for (int pointIndex = 0; pointIndex < 2; pointIndex++)
        //    {
        //        yValue = yValue + (random.NextDouble() * 10.0 - 5.0);
        //        chart1.Series["Series1"].Points.AddY(yValue);
        //    }

        //    chart1.Series["Series1"].ChartType = SeriesChartType.FastLine;
        //}




    }


}
