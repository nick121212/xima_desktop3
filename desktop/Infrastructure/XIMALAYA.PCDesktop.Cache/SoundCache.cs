using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Tools;

namespace XIMALAYA.PCDesktop.Cache
{
    /// <summary>
    /// 声音缓存类
    /// </summary>
    public class SoundCache : Singleton<SoundCache>
    {
        private Dictionary<long, SoundData> Sounds { get; set; }

        private SoundCache()
        {
            this.Sounds = new Dictionary<long, SoundData>();
        }
        /// <summary>
        /// 索引器
        /// 获取缓存的声音数据
        /// 设置新的声音数据缓存
        /// </summary>
        /// <param name="SoundID"></param>
        /// <returns></returns>
        public SoundData this[long SoundID]
        {
            get
            {
                if (this.Sounds.ContainsKey(SoundID))
                {
                    return this.Sounds[SoundID];
                }
                return null;
            }
            set
            {
                if (this.Sounds.ContainsKey(SoundID))
                {
                    this.SetData(this.Sounds[SoundID], value);
                }
                else
                {
                    this.Sounds.Add(SoundID, value);
                }
            }
        }
        /// <summary>
        /// 获取缓存的声音数据
        /// 设置新的声音数据缓存
        /// </summary>
        /// <param name="soundData"></param>
        /// <returns></returns>
        public SoundData GetData(SoundData soundData)
        {
            if (this.Sounds.ContainsKey(soundData.TrackId))
            {
                this.SetData(this.Sounds[soundData.TrackId], soundData);
                return this.Sounds[soundData.TrackId];
            }

            this.Sounds.Add(soundData.TrackId, soundData);

            return soundData;
        }

        private void SetData(SoundData oldSound, SoundData newSound)
        {
            object newValue;

            foreach (PropertyInfo property in typeof(SoundData).GetProperties())
            {
                newValue = property.GetValue(newSound);

                if (newValue != null && newValue != this.DefaultForType(newValue.GetType()))
                {
                    property.SetValue(oldSound, newValue);
                }
            }
        }
        /// <summary>
        /// 设置缓存数据
        /// </summary>
        /// <param name="sounds"></param>
        public void SetData(SoundData[] sounds)
        {
            foreach (SoundData sound in sounds)
            {
                this[sound.TrackId] = sound;
            }
        }

        private object DefaultForType(Type targetType)
        {
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
        }
    }
}
