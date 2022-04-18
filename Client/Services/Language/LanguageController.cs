using System;
using System.Collections.Generic;
using Client.Models.Locales;
using static CitizenFX.Core.Native.API;

namespace Client.Services.Language
{
    public enum Locales
    {
        EN,
        FR,
        DE,
        IT,
        ES,
        BR,
        PL,
        RU,
        KR,
        TW,
        JP,
        MX,
        CN
    }
    public static class LanguageService
    {
        private static Dictionary<string, string> _currentLocale;
        private static Dictionary<string, string> _fallback;
        public static void LoadTranslation(int language)
        {
            var currentLang = (Locales) language;
            _fallback = De.Locale;
            switch (currentLang)
            {
                case Locales.EN:
                    _currentLocale = En.Locale;
                    break;
                case Locales.BR:
                    break;
                case Locales.CN:
                    break;
                case Locales.DE:
                    _currentLocale = De.Locale;
                    break;
                case Locales.ES:
                    break;
                case Locales.FR:
                    _currentLocale = FR.Locale;
                    break;
                case Locales.KR:
                    break;
                case Locales.JP:
                    break;
                case Locales.IT:
                    break;
                case Locales.PL:
                    break;
                case Locales.RU:
                    break;
                case Locales.TW:
                    break;
                case Locales.MX:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public static string Translate(string key)
        {
            if (_currentLocale == null)
            {
                var currentLocale = GetCurrentLanguage();
                LoadTranslation(currentLocale);
            }

            if (_currentLocale.ContainsKey(key)) return _currentLocale[key];
            return !_fallback.ContainsKey(key) ? key : _fallback[key];
        }
    }
}