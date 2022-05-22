using EjemploFlujoAsync;
using System.Diagnostics;

//Iniciamos un contador de tiempo SINCRONA           
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

Console.WriteLine("\nBienvenido a la calculadora de Hipotecas Síncrona");

var aniosVidaLaboral = CalculadoraHipotecaSync.obtenerAniosVidaLaboral();
Console.WriteLine($"\nAños de vida laboral Obtenidos: {aniosVidaLaboral}");

var esTipoContratoIndefinido = CalculadoraHipotecaSync.EsTipoContratoIndefinido();
Console.WriteLine($"\nTipo de contrato indefinido: {esTipoContratoIndefinido}");

var sueldoNeto = CalculadoraHipotecaSync.ObtenerSueldoNeto();
Console.WriteLine($"\nSueldo neto Obtenido: {sueldoNeto}");

var gastosMensuales = CalculadoraHipotecaSync.ObtenerGastosMensuales();
Console.WriteLine($"\nGastos Mensuales Obtenidos: {gastosMensuales}");

var hipotecaConcedida = CalculadoraHipotecaSync.AnalizarInformacionParaConcederHipoteca(
    aniosVidaLaboral, esTipoContratoIndefinido,sueldoNeto,gastosMensuales, cantidadSolicitada:50000, aniosPagar:30);

var resultado = hipotecaConcedida ? "APROBADA" : "DENAGADA";

Console.WriteLine($"\nAnalisis Finalizado. Su solicitud de hipoteca ha sido: {resultado}");

stopwatch.Stop();

Console.WriteLine($"\nLa operacion Sincrona ha durado : {stopwatch.Elapsed}");

//Iniciamos un contador de tiempo ASINCRONA  
stopwatch.Restart();
Console.WriteLine("\n**************************************************");
Console.WriteLine("\nBienvenido a la calculadora de Hipotecas Asíncrona");
Console.WriteLine("\n**************************************************");

Task<int> aniosVidaLaboralTask = CalculadoraHipotecaAsync.obtenerAniosVidaLaboral();
Task<bool> esTipoContratoIndefinidoTask = CalculadoraHipotecaAsync.EsTipoContratoIndefinido();
Task<int> sueldoNetoTask = CalculadoraHipotecaAsync.ObtenerSueldoNeto();
Task<int> gastosMensualesTask = CalculadoraHipotecaAsync.ObtenerGastosMensuales();

var analisisHipotetaTasks = new List<Task>
{
    aniosVidaLaboralTask,
    esTipoContratoIndefinidoTask,
    sueldoNetoTask,
    gastosMensualesTask
};

while (analisisHipotetaTasks.Any())
{
    Task tareaFinalizada = await Task.WhenAny(analisisHipotetaTasks);

    if (tareaFinalizada== aniosVidaLaboralTask)
    {
        Console.WriteLine($"\nAños de vida laboral Obtenidos: {aniosVidaLaboralTask.Result}");

    }
    else if (tareaFinalizada == esTipoContratoIndefinidoTask)
    {
        Console.WriteLine($"\nTipo de contrato indefinido: {esTipoContratoIndefinidoTask.Result}");


    }
    else if (tareaFinalizada== sueldoNetoTask)
    {
        Console.WriteLine($"\nSueldo neto Obtenido: {sueldoNetoTask.Result}");


    }
    else if (tareaFinalizada== gastosMensualesTask)
    {
        Console.WriteLine($"\nGastos Mensuales Obtenidos: {gastosMensualesTask.Result}");

    }
    analisisHipotetaTasks.Remove(tareaFinalizada); // eliminamos las tareas compleatadas para salir del while

    var hipotecaAsyncConcedida = CalculadoraHipotecaAsync.AnalizarInformacionParaConcederHipoteca(
    aniosVidaLaboralTask.Result, esTipoContratoIndefinidoTask.Result, sueldoNetoTask.Result, gastosMensualesTask.Result, cantidadSolicitada: 50000, aniosPagar: 30);

    var resultadoAsync = hipotecaAsyncConcedida ? "APROBADA" : "DENAGADA";

    Console.WriteLine($"\nAnalisis Finalizado. Su solicitud de hipoteca ha sido: {resultadoAsync}");
    
    stopwatch.Stop();

    Console.WriteLine($"\nLa operacion Asincrona ha durado : {stopwatch.Elapsed}");

    Console.Read();
}
