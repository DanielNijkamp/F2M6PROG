using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace F2M6PROG
{
    public class SCP
    {
        [JsonProperty("Name")]
        public string Name;

        [JsonProperty("Description")]
        public string Description;

        [JsonProperty("SCP_Procedure")]
        public string SCP_procedure;

        [JsonProperty("AccesLevel")]
        public int AccessLevel;

        [JsonProperty("Objectclass")]
        public string Objectclass{get; private set;}

     
        

        public SCP(string name, int acceslevel,string inputclass ,string procedure, string description)
        {
            this.Name = name;
            this.AccessLevel = acceslevel;
            this.Description = description;
            this.SCP_procedure = procedure;
            this.Objectclass = inputclass;
        }


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
