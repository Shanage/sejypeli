using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class pong : PhysicsGame
{
    Vector nopeusYlos = new Vector(0, 200);
    Vector nopeusAlas = new Vector(0, -200);

    PhysicsObject pallo;
    PhysicsObject maila1;
    PhysicsObject maila2;

    IntMeter pelaajan1Pisteet;
    IntMeter pelaajan2Pisteet;

    public override void Begin()
    {
        LuoKentta();
        AsetaOhjaimet();
        LisaaLaskurit();
        AloitaPeli();
    }
        // TODO: Kirjoita ohjelmakoodisi tähän
    

    void LuoKentta()
    {
        pallo = new PhysicsObject(50, 50);
        pallo.Shape = Shape.Circle;
        pallo.Hit(new Vector(100, 100));
        pallo.X = -200.0;
        pallo.Y = 0.0;
        pallo.Restitution = 1.0;
        Add(pallo);

        maila1 = LuoMaila(Level.Left + 20.0, 0.0);
        maila2 = LuoMaila(Level.Right - 20.0, 0.0);

        Level.CreateBorders(1.0, false);
        Level.BackgroundColor = Color.Black;


        Camera.ZoomToLevel();

        AddCollisionHandler (pallo, KasittelePallonTormays);
    }

    PhysicsObject LuoMaila(double x, double y)
    {
        PhysicsObject maila = PhysicsObject.CreateStaticObject(20.0, 100.0);
        maila.Shape = Shape.Rectangle;
        maila.X = x;
        maila.Y = y;
        maila.Restitution = 1.0;
        Add(maila);
        return maila;
    }

    void AloitaPeli()
    {
        Vector impulssi = new Vector(500.0, 0.0);
        pallo.Hit(impulssi);
    }
    void AsetaOhjaimet()
    { 
        Keyboard.Listen(Key.A, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa ylös", maila1, nopeusYlos);
        Keyboard.Listen(Key.A, ButtonState.Released, AsetaNopeus, null, maila1, Vector.Zero); 
        Keyboard.Listen(Key.Z, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa alas", maila1, nopeusAlas);
        Keyboard.Listen(Key.Z, ButtonState.Released, AsetaNopeus, null, maila1, Vector.Zero);
       
        Keyboard.Listen(Key.Up, ButtonState.Down, AsetaNopeus, "Pelaaja 2: Liikuta mailaa ylös",maila2, nopeusYlos);
        Keyboard.Listen(Key.Up, ButtonState.Released, AsetaNopeus, null, maila2, Vector.Zero);
        Keyboard.Listen(Key.Down, ButtonState.Down, AsetaNopeus, "pelaaja 2:Liikuta mailaa alas", maila2, nopeusAlas); 
        Keyboard.Listen(Key.Down, ButtonState.Released, AsetaNopeus,  null, maila2, Vector.Zero);

        Keyboard.Listen(Key.F1, ButtonState.Pressed,  ShowControlHelp, "Näytä ohjeet");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    void AsetaNopeus (PhysicsObject maila, Vector nopeus)
    {
        if ((nopeus.Y < 0) && (maila.Bottom < Level.Bottom)) 
        
        {
            maila.Velocity = Vector.Zero;
            return;
        }
        if ((nopeus.Y > 0) && (maila.Top > Level.Top))
        {
            maila.Velocity = Vector.Zero;
            return;
        }

        
    }
    void Lisaalaskurit()
    {
        pelaajan1Pisteet = LuoPisteLaskuri (Screen.Left + 100.0, Screen.Top - 100.0);
        pelaajan2Pisteet = LuoPisteLaskuri (Screen.Right - 100.0, Screen.Top -100.0);
    } 

    IntMeter LuoPisteLaskuri (double z, double y)
    { 
        IntMeter laskuri = new IntMeter (0);
        laskuri.MaxValue = 10;

        Label naytto = new Label();
        naytto.BindTo (laskuri);
        naytto.X = X = x;
        naytto.Y = y;
        naytto.TextColor = Color.White;
        naytto.BorderColor = Level.BackgroundColor;
        Add(naytto);

        return laskuri;
    }

    maila.Velocity = nopeus;
   }
} 