﻿using System;
using System.Collections.Generic;
using System.Text;

namespace F2M6PROG
{
    public abstract class SCP
    {
        public int Name { get; private set; }
        public int AccessLevel { get; private set; }
        public ObjectClass currentclass{get; private set;}

     
        public enum ObjectClass
        {
            Safe,
            Euclid,
            Keter,
            Thaumiel,
            Apollyon,
        }
        

        protected SCP(int name, int acceslevel)
        {
            this.Name = name;
            this.AccessLevel = acceslevel;
        }
        public abstract void Show_Description();

        public abstract void Show_Special_Containment_Procedures();


        /*public enum GameState
        {
            IDLE,
            COMBAT,
            CUTSCENE,
            PLAYING
        }

        public GameState currentState = GameState.PLAYING;
        public void Run()
        {
            switch (currentState)
            {
                case GameState.IDLE:
                    if (true)
                    {
                        currentState = GameState.COMBAT;
                    }
                    break;
                case GameState.COMBAT:
                    break;
                case GameState.CUTSCENE:
                    break;
                case GameState.PLAYING:
                    break;
                default:
                    break;
            }
        }*/

        
    }
}
