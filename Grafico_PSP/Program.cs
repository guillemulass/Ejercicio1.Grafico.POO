using System;
using System.Collections.Generic;
using Grafico_PSP;

Main();

void Main()
{
    Console.WriteLine("Crear Punto:\nIntroduce las coordenadas en formato: (x,y)\n");
    var coords = Console.ReadLine()!.Split(',');
    Punto punto = new Punto(Int32.Parse(coords[0]), Int32.Parse(coords[1]));
    
    Console.WriteLine("Crear Circulo:\nIntroduce las coordenadas en formato: (x,y,radio)\n");
    coords = Console.ReadLine()!.Split(',');
    Circulo circulo = new Circulo(Int32.Parse(coords[0]), Int32.Parse(coords[1]), Int32.Parse(coords[2]));
    
    Console.WriteLine("Crear Rectangulo:\nIntroduce las coordenadas en formato: (x,y,ancho,alto)\n");
    coords = Console.ReadLine()!.Split(',');
    Rectangulo rectangulo = new Rectangulo(Int32.Parse(coords[0]), Int32.Parse(coords[1]), Int32.Parse(coords[2]), Int32.Parse(coords[3]));

    GraficoCompuesto graficoCompuesto = new GraficoCompuesto();
    
    graficoCompuesto.AñadirGrafico(punto);
    graficoCompuesto.AñadirGrafico(circulo);
    graficoCompuesto.AñadirGrafico(rectangulo);

    graficoCompuesto.Dibujar();
    
    Console.WriteLine("\n1. Mover el grafico\n2. Salir");
    var select = Console.ReadLine();

    while (select != "2")
    {
        if (select == "1") 
        {
            Console.WriteLine("Mover:\nIntroduce las coordenadas en formato: (x,y)\n");
            coords = Console.ReadLine()!.Split(',');
            if (graficoCompuesto.Mover(Int32.Parse(coords[0]), Int32.Parse(coords[1])))
            {
                Console.WriteLine(graficoCompuesto.Dibujar());
            }
            else
            {
                Console.WriteLine("Las coordenadas especificadas se salen de los limites");
            }
        }
        Console.WriteLine("\n1. Mover el grafico\n2. Salir");
        select = Console.ReadLine();
    }
    

}

namespace Grafico_PSP
{
    public interface IGrafico
        {
            bool Mover(int x, int y);
            string Dibujar();
        }

        public class Punto : IGrafico
        {
            protected int X { get; set; }
            protected int Y { get; set; }
            
            public Punto(int x, int y)
            {
                X = x;
                Y = y;
            }

            public bool Mover(int x, int y)
            {
                if (X is > 800 or < 0 || Y is > 600 or < 0)
                {
                    return false;
                }

                X += x;
                Y += y;
                
                return true;
            }

            public string Dibujar()
            {
                return "Punto: x = " + X + "y = " + Y;
            }
    
        }

        internal class Circulo : Punto
        {
            private int Radio { get; set; }
    
            public bool Mover(int x, int y)
            {
                if (X+Radio > 800 || X-Radio < 0 || Y+Radio > 600 || Y-Radio <0)
                {
                    return false;
                }
                X += x;
                Y += y;
                return true;
            }

            public Circulo(int x, int y, int radio) : base(x, y)
            {
                Radio = radio;
            }
    
            public string Dibujar()
            {
                return "Circulo: x = " + X + "y = " + Y + "radio = " + Radio;
            }
    
        }

        internal class Rectangulo : Punto
        {
            private int Ancho { get; set; }
            private int Alto { get; set; }

            public bool Mover(int x, int y)
            {
                if (x+Ancho > 800 || x+Ancho < 0 || y+Alto > 600 || y-Alto <0)
                {
                    return false;
                }

                X = x;
                Y = y;
                return true;
            }

            public Rectangulo(int x, int y, int ancho, int alto): base(x, y)
            {
                Ancho = ancho;
                Alto = alto;
            }
    
            public string Dibujar()
            {
                return "Circulo: x = " + X + "y = " + Y + "ancho = " + Ancho + "alto = " + Alto ;
            }
        }

        public class GraficoCompuesto : IGrafico
        {
            private readonly List<IGrafico> ListaGraficos = new List<IGrafico>();

            public void AñadirGrafico(IGrafico grafico)
            {
                ListaGraficos.Add(grafico);
            }

            public bool Mover(int x, int y)
            {
                foreach (var graf in ListaGraficos)
                {
                    if (graf.Mover(x, y))
                    {
                        return false;
                    }
                }

                return true;

            }

            public string Dibujar()
            {
                var respuesta = "";
                respuesta += "Grafico compuesto por:\n";
                foreach (var grafico in ListaGraficos)
                {
                    respuesta += grafico.Dibujar();
                }
                return $"{respuesta}\n";
            }

        }
}