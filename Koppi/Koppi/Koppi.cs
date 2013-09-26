using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class Koppi : PhysicsGame
{
    IntMeter pisteLaskuri;
    IntMeter elamat = new IntMeter(3, 0, 5);
    int level = 1;
    int omenoitaIlmassa = 1;

    void LuoElamalaskuri()
    {
        Label elamaNaytto = new Label();
        elamaNaytto.BindTo(elamat);
        elamaNaytto.X = Screen.Right - 50.50;
        elamaNaytto.Y = Screen.Top - 50.0;
        Add(elamaNaytto);
    }
    void LuopisteLaskuri()
    {
        pisteLaskuri = new IntMeter (0);
        Label pisteNaytto = new Label();
        pisteNaytto.BindTo(pisteLaskuri);
        Add(pisteNaytto);

    } 
    
    public override void Begin()
    {
        LuopisteLaskuri();
        LuoElamalaskuri();

        UusiOmena(level);
        omenoitaIlmassa = level;

        PhysicsObject pohja =
            Level.CreateBottomBorder();
        AddCollisionHandler(pohja, PutosiMaahan);
    
        PhysicsObject omena = new PhysicsObject(50, 50);
        omena.Shape = Shape.Circle;
        omena.Color = Color.Red;
        omena.Y = Screen.Top;
        GameObject lehti = new GameObject(20, 20);
        lehti.Shape = Shape.Heart;
        lehti.Color = Color.Green;
        Add(omena);
        lehti.Y = 30;
        omena.Add(lehti);
        Mouse.ListenOn(omena, MouseButton.Left,
            ButtonState.Pressed, OmenaaKlikattu,
            "omenaa klikattu", omena);

        omena.Hit(RandomGen.NextVector(50, 100));

        Gravity = new Vector(0.0, -100.0);

        IsMouseVisible = true;
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    void UusiOmena(int level);
        for (int i= 
    
    void PutosiMaahan(
        PhysicsObject maa,
        PhysicsObject omena)
    {
        

        if (omena.Color != Color.Black)
        {
        elamat.AddValue(-1);
        omena.Color = Color.Black();
        
        omena.Color = Color.Black;
        omenoitaIlmassa = omenoitaIlmassa
        }
    }
    
    void OmenaaKlikattu(IPhysicsObject KlikattuOmena)
    {
        KlikattuOmena.Destroy();
        pisteLaskuri.AddValue(100);
        omenoitaIlmassa();
    }

  

} 