using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card
{
    public int id;//Identificador de la carta
    public string cardName;//Nombre de la carta
    public int cost;//Coste en monedas de la carta
    public int type;//Tipo/color de la carta (Rojo,azul,amarillo,verde,morado)(0,1,2,3,4)
    public string cardDescription;//Si la tiene, descripcion de lo que hace

    

    public Card(int Id,string CardName,int Cost,int Type,string CardDescription){
        this.id=Id;
        this.cardName=CardName;
        this.cost=Cost;
        this.type=Type;
        this.cardDescription=CardDescription;

    }
    public Card(){

    }






}
