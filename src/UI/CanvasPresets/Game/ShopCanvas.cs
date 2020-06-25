using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using MokeponGame.Gameplay;
using MokeponGame.Gameplay.WorldObjects;
using MokeponGame.UI.ImageEffects;

namespace MokeponGame.UI.CanvasPresets
{
    public class ShopCanvas : Canvas
    {
        Shop shop;
        Player player;
        int topShop; //index of first shop's item on screen
        int topPlayer; //index of first player's item on screen

        Dictionary<string, Item> items;

        public ShopCanvas(Player player, Shop shop)
        {
            this.player = player;
            this.shop = shop;

            if (shop.AvailableItems.Count == 0)
                throw new ApplicationException("Shop has no items to buy!");

            items = new Dictionary<string, Item>();

            foreach(var it in shop.AvailableItems)
            {
                items[it] = Managers.XmlManager.Instance.Load<Item>("Data/Items/" + it + ".xml");
            }

            Elements.Add(new Image("Black", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight), 0.5f));

            Image background = new Image("White", new Rectangle(100, 50, Globals.ScreenWidth - 200, Globals.ScreenHeight - 100));
            background.ImageColor = Color.Beige;
            Elements.Add(background);

            Image border = new Image("Black", new Rectangle(148, 148, Globals.ScreenWidth - 296, Globals.ScreenHeight - 226));
            Elements.Add(border);

            Image background2 = new Image("White", new Rectangle(150, 150, Globals.ScreenWidth - 300, Globals.ScreenHeight - 230), 0.6f);
            background2.ImageColor = Color.Chocolate;
            Elements.Add(background2);

            Image banner = new Image("Shop/Banner");
            banner.ImageColor = Color.Chocolate;
            Elements.Add(banner);
            Elements.Add(new Text("Shop", Color.White, Vector2.Zero, Vector2.One, "Expression-pro-32px"));
            
            Elements.Add(new Text("Buy", Color.White, Vector2.Zero, Vector2.One, "Expression-pro-32px"));
            Elements.Add(new Text("Sell", Color.White, Vector2.Zero, Vector2.One, "Expression-pro-32px"));

            Image buybg = new Image("White", new Rectangle(210, 200, 400, 380), 0.2f);
            buybg.ImageColor = Color.Black;
            Elements.Add(buybg);

            Image sellbg = new Image("White", new Rectangle(670, 200, 400, 380), 0.2f);
            sellbg.ImageColor = Color.Black;
            Elements.Add(sellbg);

            ButtonRows = 4;
            CurrentButton = 0;

            topShop = 0;
            topPlayer = 0;

            for(int i = 0; i < 4; ++i)
            {
                Buttons.Add(new Button(new Image("Black", new Rectangle(210, 200 + i * 95, 400, 95), 0.0f), 
                    new Text("", Color.White, new Vector2(215, 205 + i * 95), Vector2.One, "Expression-pro-24px")));
                AppearEffect e = new AppearEffect(0.3f, 3.0f);
                Buttons.Last().ButtonImage.AddEffect("AppearEffect", ref e);
                Elements.Add(new Text("", Color.White, new Vector2(215, 250 + i * 95), Vector2.Zero, "Expression-pro-18px"));
            }

            for(int i = 0; i < 4; ++i)
            {
                Buttons.Add(new Button(new Image("Black", new Rectangle(670, 200 + i * 95, 400, 95), 0.0f), 
                    new Text("", Color.White, new Vector2(675, 205 + i * 95), Vector2.One, "Expression-pro-24px")));
                AppearEffect e = new AppearEffect(0.3f, 3.0f);
                Buttons.Last().ButtonImage.AddEffect("AppearEffect", ref e);
                Elements.Add(new Text("", Color.White, new Vector2(675, 250 + i * 95), Vector2.Zero, "Expression-pro-18px"));
            }

            Buttons[0].ButtonImage.ActivateEffect("AppearEffect");
            Elements.Add(new Text("Reputation Points: " + player.Money, Color.White, new Vector2(670, 600), 
                Vector2.One, "Expression-pro-32px"));
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Elements[4].MoveMid(new Vector2(Globals.ScreenWidth / 2, 100));
            Elements[5].MoveMid(new Vector2(Globals.ScreenWidth / 2, 105));

            Elements[6].MoveMid(new Vector2(Globals.ScreenWidth / 3 - 30, 175));
            Elements[7].MoveMid(new Vector2(2 * Globals.ScreenWidth / 3 + 30, 175));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //Update buttons
            for (int i = 0; i < 4; ++i)
            {
                if (topShop + i < shop.AvailableItems.Count)
                {
                    Buttons[i].ButtonText.TextValue = items[shop.AvailableItems[topShop + i]].Name;
                    (Elements[10 + i] as Text).TextValue = "Cost: " + items[shop.AvailableItems[topShop + i]].Cost;
                }
                else
                {
                    Buttons[i].ButtonText.TextValue = "";
                    (Elements[10 + i] as Text).TextValue = "";
                }
            }

            for (int i = 0; i < 4; ++i)
            {
                if(topPlayer + i < player.Items.Count)
                {
                    Buttons[4 + i].ButtonText.TextValue = player.Items[topPlayer + i].Name;

                    if(player.Items[topPlayer + i].Amount > 1)
                    {
                        Buttons[4 + i].ButtonText.TextValue += " (x" + player.Items[topPlayer + i].Amount + ")";
                    }

                    (Elements[10 + i + 4] as Text).TextValue = "Cost: " + player.Items[topPlayer + i].Cost;
                }
                else
                {
                    Buttons[4 + i].ButtonText.TextValue = "";
                    (Elements[10 + i + 4] as Text).TextValue = "";
                }
            }

            if (!((CurrentButton < 4 && shop.AvailableItems.Count > topShop + CurrentButton) ||
                (CurrentButton >= 4 && player.Items.Count > topPlayer + CurrentButton - 4)))
            {
                SwitchButtonSelection(CurrentButton, 0);
                return;
            }

            (Elements.Last() as Text).TextValue = "Reputation Points: " + player.Money;

            if(Managers.InputManager.Instance.KeysPressed(Keys.Escape))
            {
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.GameScreen).ChangeCanvas("null");
                return;
            }
        }

        public override void SwitchButtonSelection(int previous, int current)
        {
            if(previous != current)
            {
                if((current < 4 && shop.AvailableItems.Count > topShop + current) ||
                    (current >= 4 && player.Items.Count > topPlayer + current - 4))
                {
                    Buttons[previous].ButtonImage.DeactivateEffect("AppearEffect");
                    Buttons[current].ButtonImage.ActivateEffect("AppearEffect");
                }
                else
                {
                    previous = current;
                    return;
                }
            }
            else
            {
                if(current == 3 && Managers.InputManager.Instance.KeysPressed(Keys.Down))
                {
                    if(topShop + 4 < shop.AvailableItems.Count)
                    {
                        ++topShop;
                    }
                }
                else if(current == 0 && Managers.InputManager.Instance.KeysPressed(Keys.Up))
                {
                    if(topShop > 0)
                    {
                        --topShop;
                    }
                }
                else if(current == 4 && Managers.InputManager.Instance.KeysPressed(Keys.Up))
                {
                    if (topPlayer > 0)
                    {
                        --topPlayer;
                    }
                }
                else if(current == 7 && Managers.InputManager.Instance.KeysPressed(Keys.Down))
                {
                    if(topPlayer + 4 < player.Items.Count)
                    {
                        ++topPlayer;
                    }
                }
            }

            base.SwitchButtonSelection(previous, current);
        }

        public override void ButtonOnClick(int buttonid)
        {
            if(buttonid >= 4)
            {
                player.Money += player.Items[topPlayer + buttonid - 4].Cost;
                player.DeleteItem(player.Items[topPlayer + buttonid - 4]);
                return;
            }

            if(buttonid < 4)
            {
                if(player.Money >= items[shop.AvailableItems[topShop + buttonid]].Cost)
                {
                    player.Money -= items[shop.AvailableItems[topShop + buttonid]].Cost;
                    player.AddItem(items[shop.AvailableItems[topShop + buttonid]]);
                }
            }

            base.ButtonOnClick(buttonid);
        }
    }
}
