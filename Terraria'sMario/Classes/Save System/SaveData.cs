using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Terraria_sMario.Classes.Logic.Objects.Creatures;

namespace Terraria_sMario.Classes.Save_System
{
    class SaveData
    {
        public string DataName { get; private set; }

        public DifficultTypes Difficult { get; private set; }

        public List<Player> players { get; private set; }


        public SaveData(string dataName, DifficultTypes difficult, List<Player> players)
        {
            DataName = dataName;
            Difficult = difficult;
            this.players = players;
        }

        public void updatePlayers(List<Player> players) => this.players = players;


        public List<Image> getPlayerImages() => players.Select(x => x.drawingImage).ToList();

        public List<string> getPlayerNames() => players.Select(x => x.Name).ToList();

        public List<int> getPlayerLevels() => players.Select(x => x.level).ToList();

    }
}
