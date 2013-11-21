using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class Niki : PhysicsGame
{
    const double nopeus = 200;
    const double hyppyNopeus = 650;
    const int RUUDUN_KOKO = 40;

    PlatformCharacter pelaaja1;
    PhysicsObject luola;

    Image pelaajanKuva = LoadImage("kavely1,2");
    Image marjanKuva = LoadImage("marja2");
    Image taustaKuva = LoadImage("pelitaso");
    Image tasoKuva = LoadImage("palikka2");
    Image luolaKuva = LoadImage("luola");


    public override void Begin()
    {
        Gravity = new Vector(0, -900);

        LuoKentta();
        LisaaNappaimet();

        Camera.Follow(pelaaja1);
        Camera.ZoomFactor = 3.0;
        Camera.StayInLevel = true;
    }

    void LuoKentta()
    {
        ColorTileMap ruudut = ColorTileMap.FromLevelAsset("kenttaNiki2");

        ruudut.SetTileMethod(Color.FromHexCode("2629FF"), LisaaPelaaja);
        ruudut.SetTileMethod(Color.FromHexCode("562F20"), LisaaTaso);
        ruudut.SetTileMethod(Color.FromHexCode("FF0004"), LisaaMarja);
        ruudut.SetTileMethod(Color.FromHexCode("00BE00"), LisaaLuola);


        ruudut.Execute();

        AddCollisionHandler(pelaaja1, "marja", PelaajaOsuu);
        AddCollisionHandler(pelaaja1, "luola", PelaajaOsuu2);
        Level.Background.Image = taustaKuva;
    }

    void LisaaTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Position = paikka;
        taso.Color = Color.Green;
        taso.Image = tasoKuva;
        Add(taso);
    }

    void LisaaLuola(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject luola = PhysicsObject.CreateStaticObject(60, 60);
        luola.Position = paikka;
        luola.Image = luolaKuva;
        luola.Tag = "luola";
        Add(luola);
    }

    void LisaaPelaaja(Vector paikka, double leveys, double korkeus)
    {
        pelaaja1 = new PlatformCharacter(40, 40);
        pelaaja1.Position = paikka;
        pelaaja1.Mass = 6.0;
        pelaaja1.Image = pelaajanKuva;
        Add(pelaaja1);
    }

    void LisaaMarja(Vector paikka, double leveys, double korkeus)
    {
        IPhysicsObject marja = new PhysicsObject(15, 15);
       //marja.IgnoresCollisionResponse = true;
        marja.Position = paikka;
        marja.Image = marjanKuva;
        marja.Tag = "marja";
        Add(marja, 1);

    }

    void LisaaNappaimet()
    {
        Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Näytä ohjeet");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        Keyboard.Listen(Key.Left, ButtonState.Down, Liikuta, "Liikkuu vasemmalle", pelaaja1, -nopeus);
        Keyboard.Listen(Key.Right, ButtonState.Down, Liikuta, "Liikkuu vasemmalle", pelaaja1, nopeus);
        Keyboard.Listen(Key.Up, ButtonState.Pressed, Hyppaa, "Pelaaja hyppää", pelaaja1, hyppyNopeus);

        ControllerOne.Listen(Button.Back, ButtonState.Pressed, Exit, "Poistu pelistä");

        ControllerOne.Listen(Button.DPadLeft, ButtonState.Down, Liikuta, "Pelaaja liikkuu vasemmalle", pelaaja1, -nopeus);
        ControllerOne.Listen(Button.DPadRight, ButtonState.Down, Liikuta, "Pelaaja liikkuu oikealle", pelaaja1, nopeus);
        ControllerOne.Listen(Button.A, ButtonState.Pressed, Hyppaa, "Pelaaja hyppää", pelaaja1, hyppyNopeus);

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
    }

    void Liikuta(PlatformCharacter hahmo, double nopeus)
    {
        hahmo.Walk(nopeus);
    }

    void Hyppaa(PlatformCharacter hahmo, double nopeus)
    {
        hahmo.Jump(nopeus);
    }

    void PelaajaOsuu(PhysicsObject pelaaja, PhysicsObject marja)
    {
        marja.Destroy();
    }

    void PelaajaOsuu2(PhysicsObject pelaaja, PhysicsObject luola)
    {
        ClearAll();
        Begin();
    }
    


}