using System;
using System.Reflection;
using System.Text;
using GalaSoft.MvvmLight;
using LoonieTrader.Library.Constants;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class AboutWindowViewModel : ViewModelBase
    {
        public AboutWindowViewModel()
        {
        }

        public string AboutText
        {
            get
            {
                var resp = new StringBuilder();
                resp.Append(AppProperties.ApplicationName);
                resp.Append(" v");
                resp.AppendLine(Assembly.GetExecutingAssembly().GetName().Version.ToString());
                resp.AppendLine();

                resp.AppendLine("The following frameworks has been used:");
                resp.AppendLine("LiveCharts");
                resp.AppendLine("Extended WPF Toolkit");
                resp.AppendLine("YamlDotNet");
                resp.AppendLine("AutoMapper");
                resp.AppendLine("SeriLog");
                resp.AppendLine("Jil");
                resp.AppendLine("FileHelper");
                resp.AppendLine("MVVM Light");
                resp.AppendLine("StructureMap");
                resp.AppendLine("JsonPrettyPrinter");
                resp.AppendLine("Microsoft Frameworks");
                resp.AppendLine("");
                resp.AppendLine("Plus Visual Studio with ReSharper");

                return resp.ToString();

                //return "About it, and credits to used software,  and credits to used software, " +Environment.NewLine+" and credits to used software,  and credits to used software,  and credits to used software, ";
            }
        }
    }
}