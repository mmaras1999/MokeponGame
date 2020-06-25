using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MokeponGame.UI
{
    public class DialogueBox : UIElement
    {
        public string DisplayText;
        public Image Background;
        public Text TextArea;
        public Queue<string> WrappedText;
        public bool Displaying;
        public bool Initialized;
        public bool Ended;

        string toDisplay;
        double delay = 0.1;
        double wait = 0;
        int textid;

        bool initializing;
        bool closing;
        bool autoclose;
        double timePassed = 0;
        double animationTime = 0.5;

        public DialogueBox(string text, bool background = true, bool autoclose = true)
        {
            DisplayText = text;
            if(background)
                Background = new Image("Black", new Rectangle(0, Globals.ScreenHeight, Globals.ScreenWidth, Globals.ScreenHeight / 4), 0.4f);
            TextArea = new Text("", "Expression-pro-32px");
            TextArea.Color = Color.White;
            this.autoclose = autoclose;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            if(Background != null)
                Background.LoadContent();
            TextArea.LoadContent();
            TextArea.Move(new Vector2(20, Globals.ScreenHeight  * 3 / 4 + 10));
            TextArea.Sfont.LineSpacing = 50;

            WrappedText = WrapText(DisplayText);
            Displaying = false;
            toDisplay = "";
            initializing = true;
            closing = false;
            Ended = false;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Background.UnloadContent();
            TextArea.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(Displaying)
            {
                if (textid == toDisplay.Length)
                {
                    Displaying = false;
                    return;
                }
                else
                {
                    if (wait > 0)
                        wait -= gameTime.ElapsedGameTime.TotalSeconds;

                    if (wait <= 0)
                    {
                        TextArea.TextValue += toDisplay[textid];
                        textid++;
                        wait = delay;
                    }
                }
            }
            else if(initializing)
            {
                if(timePassed >= animationTime)
                {
                    timePassed = 0;
                    initializing = false;
                    Initialized = true;
                    DisplayNext();
                    return;
                }

                timePassed += gameTime.ElapsedGameTime.TotalSeconds;

                if (Background != null)
                {
                    Vector2 mv = new Vector2(0, -Globals.ScreenHeight / 4);
                    Background.MoveVector(mv * (float)(gameTime.ElapsedGameTime.TotalSeconds / animationTime));
                    Background.Position.Y = Math.Max(Background.Position.Y, Globals.ScreenHeight * 3 / 4);
                }
            }
            else if(closing)
            {
                if(timePassed >= animationTime)
                {
                    timePassed = 0;
                    closing = false;
                    Ended = true;
                    return;
                }

                timePassed += gameTime.ElapsedGameTime.TotalSeconds;

                if (Background != null)
                {
                    Vector2 mv = new Vector2(0, Globals.ScreenHeight / 4);
                    Background.MoveVector(mv * (float)(gameTime.ElapsedGameTime.TotalSeconds / animationTime));
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if(Background != null)
                Background.Draw(spriteBatch);
            TextArea.Draw(spriteBatch);
        }

        public void DisplayNext()
        {
            if (!Initialized)
            {
                Initialized = true;
                initializing = true;
                return;
            }

            if (Displaying)
            {
                TextArea.TextValue = toDisplay;
                Displaying = false;
                return;
            }

            TextArea.TextValue = "";

            if(WrappedText.Count == 0)
            {
                TextArea.TextValue = "";
                if(autoclose)
                    closing = true;
                return;
            }

            toDisplay = WrappedText.Dequeue();

            if (toDisplay == "$!")
                toDisplay = WrappedText.Dequeue();

            while (WrappedText.Count > 0 && 
                TextArea.Sfont.MeasureString(toDisplay + "\n" + WrappedText.Peek()).Y <= Background.Height - 20)
            {
                if (WrappedText.Peek() != "$!")
                {
                    toDisplay = toDisplay + "\n" + WrappedText.Dequeue();
                }
                else
                {
                    WrappedText.Dequeue();
                    break;
                }
            }

            Displaying = true;
            textid = 0;
        }    

        Queue<string> WrapText(string text)
        {
            Queue<string> segments = new Queue<string>();

            string[] words = text.Split(' ');
            StringBuilder builder = new StringBuilder();
            float width = Background.Width - 40;
            float spaceWidth = TextArea.Sfont.MeasureString(" ").X;
            float currentWidth = 0;

            foreach(var word in words)
            {
                float size = TextArea.Sfont.MeasureString(word).X;

                if (currentWidth + size < width)
                {
                    builder.Append(word + " ");
                    currentWidth += size + spaceWidth;
                }
                else
                {
                    segments.Enqueue(builder.ToString());
                    builder.Clear();
                    builder.Append(word + " ");
                    currentWidth = size + spaceWidth;
                }
            }

            if (builder.ToString().Length > 0)
                segments.Enqueue(builder.ToString());

            return segments;
        }

        public void AddText(string text)
        {
            Queue<string> temp = WrapText(text);
            WrappedText.Enqueue("$!");
            
            while(temp.Count > 0)
            {
                WrappedText.Enqueue(temp.Dequeue());
            }
        }

        public void Close()
        {
            TextArea.TextValue = "";
            closing = true;
        }
    }
}
