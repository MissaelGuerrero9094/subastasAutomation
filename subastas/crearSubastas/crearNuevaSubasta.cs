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

namespace subastas.crearSubastas
{
    public class crearNuevaSubasta
    {

        public void crearNuevasSubasta(IWebDriver driver, ExtentTest test, ExtentReports extent)
        {
            var directoryScreenshots = @"C:\Users\MissaelGuerrero\source\repos\subastas\subastas\ScreenShotsCrearSubastas\";
            //Crear subasta
            Actions builder = new Actions(driver);
            var btnAdmin = builder.MoveToElement(driver.FindElement(By.XPath(@"//a[.='Admin']")));
            if (btnAdmin != null)
            {
                btnAdmin.Perform();
                Thread.Sleep(3000);
                Screenshot screenshot4 = (driver as ITakesScreenshot).GetScreenshot();
                screenshot4.SaveAsFile(directoryScreenshots + @"CrearSubasta_ToggleCrearSubasta.png", ScreenshotImageFormat.Png);
                test.Pass("Toggle de opciones para crear subasta se muestra correctamente.");
            }
            else
            {
                test.Fail("No se encontró el elemento de Botón Administrador para ir a opción de crear subasta");
                driver.Close();
                test.Pass("Navegador cerrado con éxito.");
                extent.Flush();
            }
            var btnAdminCrearSubasta = driver.FindElement(By.XPath(@"//a[.='Crear Subastas']"));
            if (btnAdminCrearSubasta.Displayed)
            {
                test.Pass("Botón para crear subasta se muestra correctamente.");
                btnAdminCrearSubasta.Click();
                Thread.Sleep(3000);
                //Llenar formulario de creación de subastas.
                var tituloSubasta = driver.FindElement(By.Id("Subasta_Title"));
                if(tituloSubasta.Displayed)
                {
                    Base tituloNuevoSubasta = new Base();
                    tituloSubasta.SendKeys(tituloNuevoSubasta.crearTituloSubasta());
                    Thread.Sleep(1000);
                    test.Pass("El elemento Título Subasta se muestra correctamente.");
                }
                else
                {
                    test.Fail("El elemento Título Subasta no se muestra en pantalla.");
                }
                var descSubasta = driver.FindElement(By.Id("Subasta_Description"));
                if (descSubasta.Displayed)
                {
                    test.Pass("El elemento Descripción de Subasta se muestra correctamente.");
                    descSubasta.SendKeys("Lorem ipsum dolor sit amet, consectetur adipiscing elit. In non vehicula ante. Proin nec metus pharetra, semper purus id, congue mauris. Cras malesuada ut augue et sollicitudin. Lorem ipsum dolor sit amet, consectetur adipiscing elit.");
                    driver.ExecuteJavaScript("document.body.style.zoom = '70%'");
                    Thread.Sleep(3000);
                }
                else
                {
                    test.Fail("El elemento Descripción Subasta no se muestra en pantalla.");
                }
                var fechaInicioSubasta = driver.FindElement(By.Id("Subasta_StartDate"));
                if(fechaInicioSubasta.Displayed)
                {
                    test.Pass("El elemento fecha de inicio de subasta se muestra correctamente ");
                    fechaInicioSubasta.Click();
                    Thread.Sleep(5000);
                    Thread.Sleep(2000);
                    
                }
                else
                {
                    test.Fail("No se encontró el elemento de fecha de inicio de subasta");
                }


            }
            else
            {
                test.Fail("No se encontró el elemento de Botón Crear subasta");
                driver.Close();
                test.Pass("Navegador cerrado con éxito.");
                extent.Flush();
            }
        }
       
    }
}
