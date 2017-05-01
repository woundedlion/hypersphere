using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace Generator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public UILogger UILogger { get; } = new UILogger();
		private bool autoScroll = true;
		private System.Threading.Timer timer;

		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;
			App.Log += UILogger.Log;
			App.Log(LogLevel.INFO, "Initialized");
			timer = new Timer(x => App.Log(LogLevel.DEBUG, "xxxxx"), null, 0, 10);
		}

		private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			if (e.ExtentHeightChange == 0)
			{
				autoScroll = (e.Source as ScrollViewer).VerticalOffset == (e.Source as ScrollViewer).ScrollableHeight;
			}

			if (autoScroll && e.ExtentHeightChange != 0)
			{
				(e.Source as ScrollViewer).ScrollToVerticalOffset((e.Source as ScrollViewer).ExtentHeight);
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			timer.Change(Timeout.Infinite, Timeout.Infinite);
		}
	}

	public class UILogger
	{
		private ObservableTailDropQueue<LogEntry> entries = new ObservableTailDropQueue<LogEntry>(1000);
		public ObservableTailDropQueue<LogEntry> Entries { get { return entries; } }

		public void Log(LogLevel level, string message)
		{
			LogEntry e = new LogEntry();
			e.level = level;
			e.message = message;
			e.time = DateTime.Now;
			// Post the entry to the UI thread
			Application.Current.Dispatcher.BeginInvoke((Action)(() => this.entries.Enqueue(e)));
		}
	}

	public class MinConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return Math.Min((double)values[0], (double)values[1]);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
