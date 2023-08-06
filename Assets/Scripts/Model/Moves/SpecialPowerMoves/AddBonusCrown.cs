using System.Threading.Tasks;

namespace InheritanceFiveCrowns {
    public class AddBonusCrown : Move {
        protected Player Player;

        public AddBonusCrown(Player player) {
            Player = player;
        }

        protected internal override Task<int> Enact() {
            Player.NumberOfCrowns++;
            return Task.FromResult(0);
        }
    }
}