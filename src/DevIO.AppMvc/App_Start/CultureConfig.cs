﻿using System.Globalization;

namespace DevIO.AppMvc.App_Start
{
    public class CultureConfig
    {
        public static void RegisterCulture()
        {
            var culture = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
    }
}