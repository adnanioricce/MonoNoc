using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using Lib;
namespace VectorsMath
{
    public static class UIHelper
    {
        public static Desktop BuildMenu(IList<ICustomGame> games,GameSwitcher gameSwitcher){            
            var grid = new Grid
            {
                RowSpacing = 8,
                ColumnSpacing = 8
            };
            
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));                        
            // ComboBox
            var combo = new VerticalMenu
            {
                GridColumn = 1,
                GridRow = 0
            };
            foreach(var game in games)
            {
                var gameName = game.GetType().Name.Replace("Game","");
                var item = new MenuItem(gameName,gameName,Color.Green);
                item.Selected += (e,source) => {
                    gameSwitcher.SetCurrentGame(game);
                };
                combo.Items.Add(item);                
            }
            grid.Widgets.Add(combo);
            var desktop = new Desktop
            {
                Root = grid
            };
            return desktop;
        }
    }
}