﻿namespace TestWork_VibeGames
{
    public class Player
    {
        private string nickname;
        private Coordinate coordinate;

        public Player(string nickname, Coordinate coordinate)
        {
            this.nickname = nickname;
            this.coordinate = coordinate;
        }

        public string Nickname { get => nickname; set => nickname = value; }
        public Coordinate Coordinate { get => coordinate; set => coordinate = value; }
    }
}
