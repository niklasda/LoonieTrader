using System;
using System.Reflection;
using System.Text;
using GalaSoft.MvvmLight;
using JetBrains.Annotations;
using LoonieTrader.Library.Constants;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class AboutWindowViewModel : ViewModelBase
    {
        public string AboutText
        {
            get
            {
                var resp = new StringBuilder();
                resp.Append(AppProperties.ApplicationName);
                resp.Append(" v");
                resp.AppendLine(Assembly.GetExecutingAssembly().GetName().Version.ToString());
                resp.Append("CLR: ");
                resp.AppendLine(Environment.Version.ToString());
                resp.AppendLine();
                resp.Append(Environment.UserName);
                resp.Append("@");
                resp.AppendLine(Environment.UserDomainName);
                resp.AppendLine();

                resp.AppendLine("The following frameworks has been used:");
                resp.AppendLine("Extended WPF Toolkit");
                resp.AppendLine("YamlDotNet");
                resp.AppendLine("AutoMapper");
                resp.AppendLine("SeriLog");
                resp.AppendLine("Jil");
                resp.AppendLine("FileHelper");
                resp.AppendLine("MVVMLight");
                resp.AppendLine("StructureMap");
                resp.AppendLine("JsonPrettyPrinter");
                resp.AppendLine("Microsoft Frameworks");
                resp.AppendLine("");
                resp.AppendLine("Plus Visual Studio with ReSharper");

                return resp.ToString();
            }
        }
    }
}