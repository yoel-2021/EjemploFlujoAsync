using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploFlujoAsync
{
    public static class CalculadoraHipotecaSync
    {
        public static int obtenerAniosVidaLaboral()
        {
            Console.WriteLine("\nObteniendo años de vida laboral...");

            Task.Delay(5000).Wait(); // esperamos 5 segundos

            return new Random().Next(1, 35); // devolvemos un valor aleatorio entre 1 y 35
        }

        public static bool EsTipoContratoIndefinido()
        {
            Console.WriteLine("\nVerificando si el tipo de contrato es indefinido");
            Task.Delay(5000).Wait();
            return  (new Random().Next(1,10)) %2 == 0; // si el valor es par devolvemos true
        } 

        public static int ObtenerSueldoNeto()
        {
            Console.WriteLine("\nObteniendo sueldo neto...");

            Task.Delay(5000).Wait(); // esperamos 5 segundos

            return new Random().Next(800, 6000); // devolvemos un valor aleatorio entre 800 y 6000
        }

        public static int ObtenerGastosMensuales()
        {
            Console.WriteLine("\nObteniendo gastos mensuales...");

            Task.Delay(5000).Wait(); // esperamos 5 segundos

            return new Random().Next(200, 1000); // devolvemos un valor aleatorio entre 200 y 1000
        }


        public static bool AnalizarInformacionParaConcederHipoteca(
            int aniosVidaLaboral,
            bool tipoContratoEsIndefinido,
            int sueldoNeto,
            int gastosMensuales,
            int cantidadSolicitada,
            int aniosPagar)
        {
            Console.WriteLine("\nAnalizando informacion para conceder Hipoteca...");

            if (aniosVidaLaboral < 2)
            {
                return false;
            }

            //obtener la cuota mensual a pagar
            var cuota = (cantidadSolicitada / aniosPagar) / 12;

            if (cuota>=sueldoNeto || cuota > (sueldoNeto / 2))
            {
                return false;
            }

            //obtener porcentaje de gastos sobre el sueldo neto del usuario
            var porcentajeGastosSobreSueldo = ((gastosMensuales*100)/sueldoNeto);
            
            if (porcentajeGastosSobreSueldo > 30)
            {
                return false;
            }

            if((cuota+gastosMensuales)>= sueldoNeto)
            {
                return false;
            }

            if (!tipoContratoEsIndefinido)
            {
                if (((cuota+gastosMensuales)> (sueldoNeto / 3)))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            //si no entra en ninguna de las condiciones, si que la concedemos
            return true;
        }

    }
}
