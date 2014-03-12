using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Utilities
{
    public class AppSettings
    {
        IsolatedStorageSettings settings;

        const string HomeNewsFeedKeyName = "HomeNewsFeed";
        const string MostReadFeedKeyName = "MostReadFeed";
        const string MobilizerKeyName = "Mobilizer";

        const Feed HomeNewsFeedDefault = null;
        const string MostReadFeedDefault = "";
        const bool MobilizerDefault = false;

        public AppSettings()
        {
            settings = IsolatedStorageSettings.ApplicationSettings;
        }

        /// <summary>
        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddOrUpdateValue(string Key, Object value)
        {
            bool valueChanged = false;

            // If the key exists
            if (settings.Contains(Key))
            {
                // If the value has changed
                if (settings[Key] != value)
                {
                    // Store the new value
                    settings[Key] = value;
                    valueChanged = true;
                }
            }
            // Otherwise create the key.
            else
            {
                settings.Add(Key, value);
                valueChanged = true;
            }
            return valueChanged;
        }

        /// <summary>
        /// Get the current value of the setting, or if it is not found, set the 
        /// setting to the default setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValueOrDefault<T>(string Key, T defaultValue)
        {
            T value;

            // If the key exists, retrieve the value.
            if (settings.Contains(Key))
            {
                value = (T)settings[Key];
            }
            // Otherwise, use the default value.
            else
            {
                value = defaultValue;
            }
            return value;
        }

        /// <summary>
        /// Save the settings.
        /// </summary>
        public void Save()
        {
            settings.Save();
        }

        /// <summary>
        /// Property to get and set a Home News Feed Setting Key.
        /// </summary>
        public Feed HomeNewsFeedSetting
        {
            get
            {
                return GetValueOrDefault<Feed>(HomeNewsFeedKeyName, HomeNewsFeedDefault);
            }
            set
            {
                if (AddOrUpdateValue(HomeNewsFeedKeyName, value))
                {
                    Save();
                }
            }
        }

        /// <summary>
        /// Property to get and set a Most Read News Feed Setting Key.
        /// </summary>
        public string MostReadNewsFeedSetting
        {
            get
            {
                return GetValueOrDefault<string>(MostReadFeedKeyName, MostReadFeedDefault);
            }
            set
            {
                if (AddOrUpdateValue(MostReadFeedKeyName, value))
                {
                    Save();
                }
            }
        }

        /// <summary>
        /// Property to get and set a Most Read News Feed Setting Key.
        /// </summary>
        public bool MobilizerSetting
        {
            get
            {
                return GetValueOrDefault<bool>(MobilizerKeyName, MobilizerDefault);
            }
            set
            {
                if (AddOrUpdateValue(MobilizerKeyName, value))
                {
                    Save();
                }
            }
        }

    }
}
