using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList=new List<Card>();

    void Awake(){
        //Tipo/color de la carta (Rojo,azul,amarillo,verde,morado)(0,1,2,3,4)
        //(ID de la carta, su nombre, el coste de monedas, el tipo,la descripcion si la tiene)
        cardList.Add(new Card(0," ",3,3,"-0-"));
        cardList.Add(new Card(1,"Cuartel",3,0,"-1-"));
        cardList.Add(new Card(2,"Palacio",5,2,"-2-"));
        cardList.Add(new Card(3,"Templo",1,1,"-3-"));
        cardList.Add(new Card(4,"Observatorio",3,4,"-4-"));
        cardList.Add(new Card(5,"Herreria",5,4,"-5-"));
        cardList.Add(new Card(6,"Monasterio",3,1,"-6-"));
        cardList.Add(new Card(7,"Fuente de los deseos",5,4,"Al final de la partida, ademas de los 5 puntos equivalentes a su coste, te da un punto por cada otro distrito violeta en mesa.-7-"));
        cardList.Add(new Card(8,"Iglesia",2,1,"-8-"));
        cardList.Add(new Card(9,"Atalaya",1,0,"-9-"));
        cardList.Add(new Card(10,"Mercado",2,3,"-10-"));
        cardList.Add(new Card(11,"Taberna",1,3,"-11-"));
        cardList.Add(new Card(12,"Prision",2,0,"-12-"));
        cardList.Add(new Card(13,"Señorio",3,2,"-13"));
        cardList.Add(new Card(14,"Castillo",4,2,"-14-"));
        cardList.Add(new Card(15,"Catedral",5,1,"-15-"));
        cardList.Add(new Card(16,"Ayuntamiento",5,3,"-16"));
        cardList.Add(new Card(17,"Ciudad Embrujada",2,4,"-17-"));
        cardList.Add(new Card(18,"Biblioteca",6,4,"Si escoges recibir dos cartas al principio de tu turno, te quedas con ambas.-18-"));
        cardList.Add(new Card(19,"Cementerio",4,4,"-19-"));
        cardList.Add(new Card(20,"Universidad",6,4,"Construir el distrito cuesta 6 pero vale 8 al final del juego.-20-"));
        cardList.Add(new Card(21,"Puerta del Dragon",6,4,"Construir el distrito cuesta 6 pero vale 8 al final del juego.-21-"));
        cardList.Add(new Card(22,"Cantera",5,4,"Puedes contruir distritos con el mismo nombre que otros que ya tengas en juego.-22-"));
        cardList.Add(new Card(23,"Sala de mapas",5,4,"Al final de la partida, añade un punto por cada distrito que tengas en mano.-23-"));
        cardList.Add(new Card(24,"Sala de baile",4,4,"Cada turno robas una moneda.-24-"));
        cardList.Add(new Card(25,"Escuela de magia",6,4,"-25-"));
        cardList.Add(new Card(26,"Tesoro imperial",4,4,"Al final de la partida, vale igual que la cantidad de monedas que poseas.-26-"));
        cardList.Add(new Card(27,"Torreon",3,4,"No se puede destruir.-27-"));
        cardList.Add(new Card(28,"Torreon",3,4,"No se puede destruir.-28-"));
        cardList.Add(new Card(29,"Faro",3,2,"-29-"));
        cardList.Add(new Card(30,"Parque",4,2,"-30-"));
        cardList.Add(new Card(31,"Gran Muralla",6,4,"-31-"));
        cardList.Add(new Card(32,"Laboratorio",5,4,"-32-"));
        cardList.Add(new Card(33,"Museo",4,4,"La ciudad esta completa con un tipo menos de distrito.-33-"));
        cardList.Add(new Card(34,"Polvorin",3,4,"-34-"));
        cardList.Add(new Card(35,"Hospicio",5,4,"Si al final de turno no tienes monedas, ganas 2.-35-"));
        cardList.Add(new Card(36,"Fabrica",6,4,"Cada vez que contruyes un distrito recibes una moneda.-36-"));
        cardList.Add(new Card(37,"Hospital",6,4,"Puedes robar cartas y monedas al comienzo de turno.-37-"));
        cardList.Add(new Card(38,"Sala del Trono",5,4,"-38-"));
        cardList.Add(new Card(39,"Fuente de los deseos",5,4,"Al final de la partida, ademas de los 5 puntos equivalentes a su coste, te da un punto por cada otro distrito violeta en mesa.-39-"));
        cardList.Add(new Card(40,"Monasterio",3,1,"-40-"));
        cardList.Add(new Card(41,"Templo",1,1,"-41-"));
        cardList.Add(new Card(42,"Templo",1,1,"-42-"));
        cardList.Add(new Card(43,"Palacio",5,2,"-43-"));
        cardList.Add(new Card(44,"Iglesia",2,1,"-44-"));
        cardList.Add(new Card(45,"Iglesia",2,1,"-45-"));
        cardList.Add(new Card(46,"Palacio",5,2,"-46-"));
        cardList.Add(new Card(47,"Monasterio",3,1,"-47-"));
        cardList.Add(new Card(48,"Catedral",5,1,"-48-"));
        cardList.Add(new Card(49,"Señorio",3,2,"-49-"));
        cardList.Add(new Card(50,"Señorio",3,2,"-50-"));
        cardList.Add(new Card(51,"Señorio",3,2,"-51-"));
        cardList.Add(new Card(52,"Castillo",4,2,"-52-"));
        cardList.Add(new Card(53,"Castillo",4,2,"-53-"));
        cardList.Add(new Card(54,"Cuartel",3,0,"-54-"));
        cardList.Add(new Card(55,"Cuartel",3,0,"-55-"));
        cardList.Add(new Card(56,"Prision",2,0,"-56-"));
        cardList.Add(new Card(57,"Prision",2,0,"-57-"));
        cardList.Add(new Card(58,"Atalaya",1,0,"-58-"));
        cardList.Add(new Card(59,"Atalaya",1,0,"-59-"));
        cardList.Add(new Card(60,"Señorio",3,2,"-60-"));
        cardList.Add(new Card(61,"Fortaleza",5,0,"-61-"));
        cardList.Add(new Card(62,"Fortaleza",5,0,"-62-"));
        cardList.Add(new Card(63,"Mercado",2,3,"-63-"));
        cardList.Add(new Card(64,"Mercado",2,3,"-64-"));
        cardList.Add(new Card(65,"Mercado",2,3,"-65-"));
        cardList.Add(new Card(66,"Taberna",1,3,"-66-"));
        cardList.Add(new Card(67,"Taberna",1,3,"-67-"));
        cardList.Add(new Card(68,"Taberna",1,3,"-68-"));
        cardList.Add(new Card(69,"Taberna",1,3,"-69-"));
        cardList.Add(new Card(70,"Puerto",4,3,"-70-"));
        cardList.Add(new Card(71,"Puerto",4,3,"-71-"));
        cardList.Add(new Card(72,"Puerto",4,3,"-72-"));
        cardList.Add(new Card(73,"Almacenes",3,3,"-73-"));
        cardList.Add(new Card(74,"Almacenes",3,3,"-74-"));
        cardList.Add(new Card(75,"Almacenes",3,3,"-75-"));
        cardList.Add(new Card(76,"Tienda",2,3,"-76-"));
        cardList.Add(new Card(77,"Tienda",2,3,"-77-"));
        cardList.Add(new Card(78,"Tienda",2,3,"-78-"));
        cardList.Add(new Card(79,"Ayuntamiento",5,3,"-79-"));
        cardList.Add(new Card(80,"Atalaya",1,0,"-80-"));


    }





}
