using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using XIMALAYA.PCDesktop.Cache;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Core.Models.MutiData;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Core.Models.User;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Tools;

namespace XIMALAYA.PCDesktop.Modules.UserModule
{
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class MutiSoundViewModel : BaseViewModel
    {
        #region 属性

        /// <summary>
        /// 多声音数据
        /// </summary>
        public ObservableCollection<SoundData> Sounds { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        private MutiDataParam Params { get; set; }
        /// <summary>
        /// 专辑服务
        /// </summary>
        [Import]
        private ISoundService SoundService { get; set; }

        private string _Title;
        /// <summary>
        /// 佔位服务
        /// </summary>
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (value != _Title)
                {
                    _Title = value;
                    this.RaisePropertyChanged(() => this.Title);
                }
            }
        }


        #endregion

        #region 方法

        protected override void GetData(bool isClear)
        {
            if (this.SoundService == null)
            {
                throw new NullReferenceException();
            }

            this.Params.Page = this.CurrentPage;
            base.GetData(isClear);

            this.SoundService.GetMutiSounds(res =>
            {
                var result = res as MutiSoundResult;

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (result.Ret == 0)
                    {
                        this.IsWaiting = false;
                        this.Total = result.TotalCount;
                        this.Title = result.LongTitle;
                        this.Sounds.Clear();
                        foreach (var sound in result.Sounds)
                        {
                            this.Sounds.Add(sound);
                            SoundCache.Instance[sound.TrackId] = sound;
                        }
                    }
                    else
                    {
                        DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, GlobalDataSingleton.Instance.SystemTitle, result.Message);
                    }
                }), null);

            }, this.Params);
        }

        public void Initialize(int id)
        {
            this.Sounds = new ObservableCollection<SoundData>();
            this.Params = new MutiDataParam
            {
                ID = id,
                Type = 7,
                Page = 1,
                PerPage = 20
            };
            this.PageSize = (int)this.Params.PerPage;
            this.CurrentPage = 1;
            //1232
        }

        #endregion
    }
}
