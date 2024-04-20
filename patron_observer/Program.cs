using System;
using System.Collections.Generic;
using System.Threading;

public interface ITiendaObservable
{
    void AgregarObservador(IPedidoObserver observador);
    void QuitarObservador(IPedidoObserver observador);
    void NotificarObservadores();
}

public interface IPedidoObserver
{
    void Actualizar(int estadoPedido);
}

public class TiendaSandwiches : ITiendaObservable
{
    private List<IPedidoObserver> observadores = new List<IPedidoObserver>();
    private int estadoPedido = -1; // Estado inicial del pedido

    public void AgregarObservador(IPedidoObserver observador)
    {
        observadores.Add(observador);
    }

    public void QuitarObservador(IPedidoObserver observador)
    {
        observadores.Remove(observador);
    }

    public void NotificarObservadores()
    {
        foreach (var observador in observadores)
        {
            observador.Actualizar(estadoPedido);
        }
    }

    public void PedidoListo(int nuevoEstado)
    {
        estadoPedido = nuevoEstado;
        Console.WriteLine($"Tienda de sándwiches: Pedido listo. Estado: {estadoPedido}");
        NotificarObservadores();
    }
}

public class Cliente : IPedidoObserver
{
    private string nombre;
    private int estadoPedido;

    public Cliente(string nombre)
    {
        this.nombre = nombre;
    }

    public void Actualizar(int nuevoEstado)
    {
        estadoPedido = nuevoEstado;
        Console.WriteLine($"Cliente {nombre}: Se ha actualizado el estado del pedido. Nuevo estado: {estadoPedido}");
    }

    public void ActualizarEstado(int nuevoEstado)
    {
        estadoPedido = nuevoEstado;
        Console.WriteLine($"Cliente {nombre}: Estado del pedido actualizado. Nuevo estado: {estadoPedido}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var tienda = new TiendaSandwiches();
        var cliente1 = new Cliente("Cliente1");
        var cliente2 = new Cliente("Cliente2");

        tienda.AgregarObservador(cliente1);
        tienda.AgregarObservador(cliente2);

        Console.WriteLine("1 = ingreso nuevo pedido\n");
        Console.WriteLine("2 = pedido en proceso\n");
        Console.WriteLine("3 = pedido entregado\n");

        tienda.PedidoListo(2);

        cliente1.ActualizarEstado(3); // Simulación de actualización del estado del pedido para un cliente específico

        Console.ReadLine();
    }
}
