using Layui.Core.Base;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiFundamentalElement.ViewModels
{
    public class ProgressBarViewModel : LayuiViewModelBase
    {

        public ProgressBarViewModel(IRegionManager regionManager, IDialogService dialogServic) : base(regionManager, dialogServic)
        {

        }

        private double _ProVaule = 20;
        public double ProVaule
        {
            get { return _ProVaule; }
            set { SetProperty(ref _ProVaule, value); }
        }

        private bool _IsEnabled = true;
        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set { SetProperty(ref _IsEnabled, value); }
        }



        public DelegateCommand Set50Command => new DelegateCommand(Set50);

        public DelegateCommand LoadingCommand => new DelegateCommand(Loading);



        private void Set50()
        {
            ProVaule = 50;
        }

        private void Loading()
        {
            ProVaule = 0;
            IsEnabled = false;

            Random random = new Random();

            Task.Run(async () =>
            {
                while (ProVaule < 100)
                {
                    ProVaule += random.Next(0, 10);

                    await Task.Delay(500);
                }

                IsEnabled = true;
            });
        }

    }
}
