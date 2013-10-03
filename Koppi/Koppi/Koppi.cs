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
    IntMeter elamaLaskuri;
    IntMeter tasoLaskuri;
    int omenoitaIlmassa;
    void LuoNaytto(IntMeter laskuri, double x, double y)
    {
        Label naytto = new Label();
        naytto.X = x;
        naytto.Y = y;
        naytto.BindTo(laskuri);

        Add(naytto);
    }

    public override void Begin()
    {
        pisteLaskuri = new IntMeter(0);
        LuoNaytto(pisteLaskuri, Screen.Left + 50, Screen.Top - 50);
        elamaLaskuri = new IntMeter (5, 0, 5);
        LuoNaytto(elamaLaskuri, Screen.Right - 50, Screen.Top - 50);
        tasoLaskuri = new IntMeter (1, 1, 10);
        LuoNaytto(tasoLaskuri, Screen.Left + 50, Screen.Top - 100);

        PhysicsObject pohja =
            Level.CreateBottomBorder();
        AddCollisionHandler(pohja, PutosiMaahan);

        PudotaOmenoita(tasoLaskuri.Value);

        Gravity = new Vector(0.0, -100.0);

        IsMouseVisible = true;
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
   
   
    void PutosiMaahan(
        PhysicsObject maa,
        PhysicsObject omena)
{

    if (omena.Color == Color.Red)
    {
        omena.Color = Color.Black;
        elamaLaskuri.AddValue(-1);
        if (elamaLaskuri.Value == 0)
        {
            ClearAll();
            Begin();
        }
        omenoitaIlmassa = omenoitaIlmassa - 1;
       // TarkistaOmenoidenLukumaara();

    }
 }

    void OmenaaKlikattu(IPhysicsObject KlikattuOmena)
    {
        if (KlikattuOmena.Color == Color.Red)
        {
            KlikattuOmena.Destroy();
            pisteLaskuri.AddValue(100);

            omenoitaIlmassa -= 1;
            TarkistaOnkoKaikkiKiinni();
        }
}
     
    void TarkistaOnkoKaikkiKiinni()
    {
        if (omenoitaIlmassa == 0)
        {
            tasoLaskuri.AddValue(1);
            PudotaOmenoita(tasoLaskuri.Value);
        }
    }
  
    void PudotaOmenoita(int lukumaara)
    {
        for (int i=0; i<lukumaara; i++)
        {

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

        omenoitaIlmassa = lukumaara;
    } 
    }
}    