﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotWars.Core;

namespace BotWars.GameEngine
{
    public class Game
    {
        public GameState GameState;
        public List<BotPlayer> Players;
        int _energySpawnFrequency;
        //this is creating a schism between where the player should be updated. need gamestate with more dynamic players
        public Game(GameState gameState, IEnumerable<BotPlayer> players){
            GameState = gameState;
            Players = players.ToList();
            _energySpawnFrequency = 3;
        }

        public void UpdateGameState(IEnumerable<PlayerMoves> playersMoves)
        {
            _ClearDeadBotlets();
            _ApplyMoves(playersMoves);
            _FightBattles();
            _RazeBases();
            _SpawnBots();
            _CollectEnergy();
            _PlaceEnergy();
            _UpdateTurns();
            _CheckForWinner();
        }

        private void _UpdateTurns()
        {
            GameState.turnsElapsed++;
        }
        private void _ClearDeadBotlets()
        {
            Players.ToList().ForEach(p => GameState.grid = GameState.grid.Replace(p.deadBotletId, '.'));
        }
        private void _ApplyMoves(IEnumerable<PlayerMoves> playersMoves)
        {
            _SanitizeMoves(playersMoves);
            _removeBotletsFromOldLocations(playersMoves);
            _placeBotletsOnNewLocations(playersMoves);
        }
        private void _RazeBases()
        {

        }
        private void _CollectEnergy()
        {           
            List<int> energyLocations = _GetEnergyLocations();
            foreach (int energyLocation in GameState.grid)
            {
                //collect energy
            }
        }

        private List<int> _GetEnergyLocations()
        {
            List<int> energyLocations = new List<int>();
            for(int i = 0; i< GameState.grid.Length; i++){
                if(GameState.grid[i] == '*') energyLocations.Add(i);
            }
            return energyLocations;
        }
        private void _FightBattles()
        {

        }
        private void _SpawnBots()
        {
            Players.ToList().ForEach(p => _SpawnBot(p));
        }
        private void _SpawnBot(BotPlayer player){
            if (player.spawnDisabled == false && player.energy > 0 && GameState.grid[player.spawn] == '.')
            {
                StringBuilder grid = new StringBuilder(GameState.grid);
                grid[player.spawn] = player.botletId;
                GameState.grid = grid.ToString();
                player.energy--;
                _GetPlayer(player).energy = player.energy;
            }
        }
        private Player _GetPlayer(BotPlayer player)
        {
            Dictionary<string, Player> players = new Dictionary<string, Player>() 
                { {"p1", GameState.p1}, {"p2", GameState.p2} };
            return players[player.playerName];
        }
        private void _PlaceEnergy()
        {
            List<Tuple<int,int>> symetricEmptySpaces =  _GetSymetricEmptySpaces();
            if (GameState.turnsElapsed % _energySpawnFrequency == 0 && symetricEmptySpaces.Count > 0)
            {
                Tuple<int, int> emptySpaces = _GetRandomPairOfEmptySpaces(symetricEmptySpaces);
                _PlaceEnergy(emptySpaces);
            }
        }

        private void _PlaceEnergy(Tuple<int, int> emptySpaces)
        {
            StringBuilder grid = new StringBuilder(GameState.grid);
            grid[emptySpaces.Item1] = '*';
            grid[emptySpaces.Item2] = '*';
            GameState.grid = grid.ToString();
        }

        private Tuple<int, int> _GetRandomPairOfEmptySpaces(List<Tuple<int, int>> symetricEmptySpaces)
        {
            Random random = new Random();
            return symetricEmptySpaces[random.Next(symetricEmptySpaces.Count)];
        }

        private List<Tuple<int, int>> _GetSymetricEmptySpaces()
        {
            List<Tuple<int, int>> emptySpaces = new List<Tuple<int, int>>();
            for (int i = 0; i < GameState.grid.Length; i++)
            {
                int first = i;
                int second = GameState.grid.Length - 1 - i;
                if (GameState.grid[first] == '.' && GameState.grid[second] == '.')
                {
                    emptySpaces.Add(new Tuple<int,int>(first,second));
                }
            }
            return emptySpaces;
        }
        private void _IncrementTurnsElapsed()
        {

        }
        private void _CheckForWinner()
        {
            //AreMovesWellFormed
        }
        private void _SanitizeMoves(IEnumerable<PlayerMoves> playersMoves)
        {
            foreach(PlayerMoves playerMoves in playersMoves){
                if (playerMoves.Moves == null)
                {
                    playerMoves.Moves = new List<BotletMove>();
                }
                else
                {
                    playerMoves.Moves = _GetValidMovesOnly(playerMoves.Moves);
                }
            }
        }
        private IEnumerable<BotletMove> _GetValidMovesOnly(IEnumerable<BotletMove> moves)
        {
            return moves;
        }

        private bool _AreMovesWellFormed(IEnumerable<BotletMove> moves)
        {
            return true;
        }
        private void _removeBotletsFromOldLocations(IEnumerable<PlayerMoves> playersMoves)
        {
            IEnumerable<BotletMove> moves = playersMoves.Aggregate(new List<BotletMove>(), (sum, pm) => sum.Concat(pm.Moves).ToList());
            char[] grid = GameState.grid.ToCharArray();
            foreach(BotletMove move in moves)
            {
                grid.SetValue('.', move.from);
            }
            GameState.grid = new string(grid);
        }
        private void _placeBotletsOnNewLocations(IEnumerable<PlayerMoves> playersMoves)
        {
            char[] grid = GameState.grid.ToCharArray();
            foreach (PlayerMoves playerMoves in playersMoves)
            {
                BotPlayer botPlayer = Players.Find(p => p.playerName == playerMoves.PlayerName);
                foreach (BotletMove move in playerMoves.Moves)
                {
                    switch (grid[move.to])
                    {
                        //this rule seems weird, it implies that you can collect/spawn on same turn.
                        case  '*':
                            if (botPlayer.playerName == "p1")
                                GameState.p1.energy++;
                            else
                                GameState.p2.energy++;
                            break;
                        case '.':
                            grid[move.to] = botPlayer.botletId;
                            break;
                        default:
                            grid[move.to] = botPlayer.deadBotletId;
                            break;
                    }
                }
            }
            GameState.grid = new string(grid);
        }
        private char[] _GetGridAfterMove(char[] grid, string player){
            return grid;
        }
    }
}
