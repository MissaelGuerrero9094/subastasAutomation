using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.Extensions;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.DevTools.V107.Page;
using RazorEngine.Compilation.ImpromptuInterface;

namespace subastas.utilities
{
    public class Base
    {
        public string crearUsuarioEmailRandom()
        {
            int anio = DateTime.Now.Year;
            int dia = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int hora = DateTime.Now.Hour;
            int minuto = DateTime.Now.Minute;
            int sec = DateTime.Now.Second;
            int acao = DateTime.Now.Millisecond;

            string userRandom = "user"+dia+month+anio+"_"+hora+minuto+sec+acao+"@mailinator.com";

            return userRandom;
        }

        public string crearTituloSubasta()
        {
            int anio = DateTime.Now.Year;
            int dia = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int hora = DateTime.Now.Hour;
            int minuto = DateTime.Now.Minute;
            int sec = DateTime.Now.Second;
            int acao = DateTime.Now.Millisecond;

            string tituloSubastaRandom = "Subasta" + dia + month + anio + "_" + hora + minuto + sec + acao;

            return tituloSubastaRandom;
        }
    }
}
