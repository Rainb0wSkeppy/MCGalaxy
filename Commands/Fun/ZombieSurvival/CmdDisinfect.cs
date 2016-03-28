/*
    Copyright 2011 MCForge
        
    Dual-licensed under the Educational Community License, Version 2.0 and
    the GNU General Public License, Version 3 (the "Licenses"); you may
    not use this file except in compliance with the Licenses. You may
    obtain a copy of the Licenses at
    
    http://www.opensource.org/licenses/ecl2.php
    http://www.gnu.org/licenses/gpl-3.0.html
    
    Unless required by applicable law or agreed to in writing,
    software distributed under the Licenses are distributed on an "AS IS"
    BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
    or implied. See the Licenses for the specific language governing
    permissions and limitations under the Licenses.
 */
namespace MCGalaxy.Commands {
    
    public sealed class CmdDisInfect : Command {
        
        public override string name { get { return "disinfect"; } }
        public override string shortcut { get { return "di"; } }
        public override string type { get { return CommandTypes.Games; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
		public override bool Enabled { get { return Server.zombie.Running; } }
        public CmdDisInfect() { }
        
        public override void Use(Player p, string message) {
            Player who = message == "" ? p : PlayerInfo.FindOrShowMatches(p, message);
            if (who == null) return;
            
            if (!who.infected || !Server.zombie.RoundInProgress) {
                Player.SendMessage(p, "Cannot disinfect player");
            } else if (!who.referee) {
                Server.zombie.DisinfectPlayer(who);
                Player.GlobalMessage(who.color + who.DisplayName + " %Swas disinfected!");
            }
        }
        
        public override void Help(Player p) {
            Player.SendMessage(p, "/disinfect [name] - disinfects [name]");
        }
    }
}
