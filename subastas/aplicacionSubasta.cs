using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.Extensions;
using AventStack.ExtentReports.Reporter;
using subastas.utilities;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports.Model;
using OpenQA.Selenium.Interactions;
using subastas.crearSubastas;

namespace subastas
{
    public class aplicacionSubasta
    {
        ExtentReports extent = new ExtentReports();
        ExtentTest test;
        public IWebDriver driver;
        //Start Chrome browser.
        [SetUp]
        public void ConfigurationBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--incognito");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
        }


        //Settings Report file.
        [OneTimeSetUp]
        public void Setup()
        {
            var htmlReporter = new ExtentHtmlReporter(@"C:\Users\MissaelGuerrero\source\repos\subastas\subastas\index.html");
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Name project:", "Pruebas Subastas");
            extent.AddSystemInfo("Host Name:", "https://subastasml-qa.azurewebsites.net/");
            extent.AddSystemInfo("Environment:", "QA");
            extent.AddSystemInfo("Username:", "Missael Guerrero");
        }


        [Test]
        public void iniciarNavegador()
        {
            var directoryScreenshots = @"C:\Users\MissaelGuerrero\source\repos\subastas\subastas\ScreenShotsIniciarNavegador\";
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            driver.Url = "https://subastasml-qa.azurewebsites.net/";
            Thread.Sleep(3000);
            test.Pass("Navegador abierto."); 
            var tituloPagina = "Inicio - Bids Exchang";
            if(tituloPagina==driver.Title)
            {
                Screenshot screenshot1 = (driver as ITakesScreenshot).GetScreenshot();
                screenshot1.SaveAsFile(directoryScreenshots + @"1_HOME_NO_SESSION.png", ScreenshotImageFormat.Png);
                test.Pass("El título si corresponde al indicado en la página.");
                driver.Close();
            }
            else
            {
                Screenshot screenshot2 = (driver as ITakesScreenshot).GetScreenshot();
                screenshot2.SaveAsFile(directoryScreenshots + @"1_TITLE_BIDSEXCHANGE_FAILED.png", ScreenshotImageFormat.Png);
                test.Fail("El título de la página no está correcto");
                driver.Close();
            }
            extent.Flush();
        }

        [Test]
        public void iniciarSesionAdmin()
        {
            var directoryScreenshots = @"C:\Users\MissaelGuerrero\source\repos\subastas\subastas\ScreenShotsIniciarSesionAdmin\";
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            Thread.Sleep(3000);
            test.Pass("Navegador abierto.");
            string usuario = "admin@subastas.com";
            string password = "Pato1234!";
            var btnIniciarSesion = driver.FindElement(By.XPath("//li//a[@class='nav-link text-dark']"));
            btnIniciarSesion.Click();
            Thread.Sleep(3000);
            test.Pass("Inicio de sesión.");
            Screenshot screenshot1 = (driver as ITakesScreenshot).GetScreenshot();
            screenshot1.SaveAsFile(directoryScreenshots+@"1_LOGIN.png", ScreenshotImageFormat.Png);
            var user = driver.FindElement(By.Id("Input_Email"));
            var pass = driver.FindElement(By.Id("Input_Password"));
            user.SendKeys(usuario);
            pass.SendKeys(password);
            test.Pass("Usuario y contraseña ingresados.");
            Screenshot screenshot2 = (driver as ITakesScreenshot).GetScreenshot();
            screenshot2.SaveAsFile(directoryScreenshots + @"2_CREDENTIALS.png", ScreenshotImageFormat.Png);
            Thread.Sleep(3000);
            driver.ExecuteJavaScript("document.getElementById('login-submit').click();");
            Thread.Sleep(3000);
            test.Pass("Iniciar sesión con credenciales agregadas.");
            Screenshot screenshot3 = (driver as ITakesScreenshot).GetScreenshot();
            screenshot3.SaveAsFile(directoryScreenshots + @"3_HOME.png", ScreenshotImageFormat.Png);
            test.Pass("Pantalla Home o Inicio de BidsExchange en sesión con usuario de pruebas administrador.");
            extent.Flush();
        }

        [Test]
        public void crearSubasta()
        {
            var directoryScreenshots = @"C:\Users\MissaelGuerrero\source\repos\subastas\subastas\ScreenShotsCrearSubastas\";
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            driver.Url = "https://subastasml-qa.azurewebsites.net/";
            Thread.Sleep(3000);
            test.Pass("Navegador abierto."); 
            string usuario = "admin@subastas.com";
            string password = "Pato1234!";
            var btnIniciarSesion = driver.FindElement(By.XPath("//li//a[@class='nav-link text-dark']"));
            if (btnIniciarSesion.Displayed)
            {
                btnIniciarSesion.Click();
                Thread.Sleep(3000);
                test.Pass("Pantalla de Inicio de sesión.");
                Screenshot screenshot1 = (driver as ITakesScreenshot).GetScreenshot();
                screenshot1.SaveAsFile(directoryScreenshots + @"1_LOGIN.png", ScreenshotImageFormat.Png);
                var user = driver.FindElement(By.Id("Input_Email"));
                var pass = driver.FindElement(By.Id("Input_Password"));
                if (user.Displayed && pass.Displayed)
                {
                    user.SendKeys(usuario);
                    pass.SendKeys(password);
                    test.Pass("Usuario y contraseña fueron ingresados correctamente.");
                    Screenshot screenshot2 = (driver as ITakesScreenshot).GetScreenshot();
                    screenshot2.SaveAsFile(directoryScreenshots + @"2_CREDENTIALS.png", ScreenshotImageFormat.Png);
                    Thread.Sleep(3000);
                    driver.ExecuteJavaScript("document.getElementById('login-submit').click();");
                    Thread.Sleep(3000);
                    test.Pass("Inicio BidsExchange en sesión con credenciales Admin.");
                    Screenshot screenshot3 = (driver as ITakesScreenshot).GetScreenshot();
                    screenshot3.SaveAsFile(directoryScreenshots + @"3_HOME.png", ScreenshotImageFormat.Png);
                    test.Pass("Pantalla de Inicio de BidsExchange en sesión con usuario de pruebas administrador.");
                    //Crear Subasta
                    crearNuevaSubasta nueva = new crearNuevaSubasta();
                    nueva.crearNuevasSubasta(driver, test, extent);
                    driver.Close();
                    test.Pass("Navegador cerrado con éxito.");
                    extent.Flush();

                }
                else
                {
                    test.Fail("No se encontraron los elementos de usuario y contraseña");
                    driver.Close();
                    test.Pass("Navegador cerrado con éxito.");
                    extent.Flush();
                }               
            }
            else
            {
                test.Fail("No fue posible iniciar sesión en la página");
                driver.Close();
                test.Pass("Navegador cerrado con éxito.");
                extent.Flush();
            }
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == TestStatus.Failed)
            {
                test.Fail("Prueba fallada");
            }
            else if (status == TestStatus.Passed)
            {
            }
        }
    }
}
