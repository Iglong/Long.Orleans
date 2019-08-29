using App.Services;
using GrainInterfaces;
using Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace App.ViewModels
{
    /// <summary>
    /// 登录模型
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        bool _remembner = false;
        public bool Remeber
        {
            get { return _remembner; }
            set { SetProperty(ref _remembner, value); }
        }
        string _userName = default;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private ICommand _cmdLogin;

        public ICommand Command
        {
            get {
                Action executeMethod = async () => {
                    var gather = this.GetServer<IGather>("dfg");
                    var responseGather = await gather.HydrologyData(new Model.RequestModel<object> { Origin = new PassportModel() });
                };
                Func<bool> canExecuteMethod = ()=> { return true; };
                _cmdLogin = new DelegateCommand(executeMethod, canExecuteMethod);
                return _cmdLogin; }
            set { }
        }

    }
}
