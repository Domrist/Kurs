	using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
//количество морковок = 173
//коршун,лиса,медведь
namespace Kurs
{
    public class Hero
    {
        public Sprite sprt;
        public Image img;
        public Texture txt;
        public List<float> trackX;
        public List<float> trackY;
        public int direction;
        public Hero()
        {
            img = new Image("rabbit.png");
            txt = new Texture(img);

            sprt = new Sprite(txt);
            sprt.Scale = new Vector2f((float)0.03, (float)0.03);
            sprt.Position = new Vector2f(250, 250);
            direction = 1000;
            trackX = new List<float>();
            trackY = new List<float>();
            trackX.Add(100);
            trackX.Add(101);
            trackX.Add(102);
            trackY.Add(100);
            trackY.Add(101);
            trackY.Add(102);

        }
        
        public void update(RenderWindow w,Wall wal)
        {

            trackX.Add(sprt.Position.X);
            trackY.Add(sprt.Position.Y);

            if(trackX.Count != 0 && trackY.Count != 0)
            {
                if(trackX.Count > 2 && trackY.Count > 2)
                {
                    if((int)trackX[trackX.Count - 1] == (int)trackX[trackX.Count - 2] && (int)trackY[trackY.Count - 1] == (int)trackY[trackY.Count - 2])
                    {
                        trackX.RemoveAt(trackX.Count-1);
                        trackY.RemoveAt(trackY.Count-1);
                    }
                    else
                    {

                    }
                }
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                direction = 0;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                direction = 180;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                direction = -90;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                direction = 90;
            }

            if (direction == 0)
            { 
                sprt.Position = new Vector2f(sprt.Position.X,sprt.Position.Y -1);
            }
            if(direction == 180)
            {
                sprt.Position = new Vector2f(sprt.Position.X, sprt.Position.Y + 1);
            }
            if(direction == -90)
            {
                sprt.Position = new Vector2f(sprt.Position.X - 1, sprt.Position.Y);
            }
            if(direction == 90)
            {
                sprt.Position = new Vector2f(sprt.Position.X + 1, sprt.Position.Y);
            }
            
            foreach (RectangleShape i in wal.sqarPosition)
            {
                if(sprt.GetGlobalBounds().Intersects(i.GetGlobalBounds()))
                {
                    if(direction == 0)
                    {
                        sprt.Position = new Vector2f(sprt.Position.X, sprt.Position.Y + 1);
                    }
                    if (direction == 180)
                    {
                        sprt.Position = new Vector2f(sprt.Position.X, sprt.Position.Y - 1);
                    }
                    if (direction == 90)
                    {
                        sprt.Position = new Vector2f(sprt.Position.X - 1, sprt.Position.Y );
                    }
                    if (direction == -90)
                    {
                        sprt.Position = new Vector2f(sprt.Position.X + 1, sprt.Position.Y );
                    }
                }
                w.Draw(sprt);
            }
        }
    }
    public class Wall
    {
	   public string [] massOfWal;
       public RectangleShape [] sqarPosition;
       public int address;
       public Wall(int x) // С КАРТОЙ ПИЗДЕЦ,СРОЧНО ПЕРЕДЕЛАЙ ЭТОТ ТАСК //ХУЙНЯ СО 150-ЫМ ЭЛЕМЕНТОВ
       {
           address = 0;
           sqarPosition = new RectangleShape[150];
	       massOfWal = new string []{
                                 "00000000100000000", //1
        						 "01101110101110110",
        						 "00000000000000000", 
        						 "01101011111010110",
        						 "00001000100010000",
        						 "11101110101110111",
        						 "11101000000010111",
        						 "11101011011010111",
        						 "11100010001000111", 
        						 "11100010001000111", 
        						 "11101011111010111", 
        						 "11101000000010111", 
        						 "11101011111010111", 
        						 "00000000100000000", 
        						 "01101110101110110", 
        						 "00100000000010000",
        						 "10101011111010101", 
        						 "00001000100000100", 
        						 "01111110101111110",
                                 "00000000000000000"};
            for(int i = 0;i < 150;i++)
            {
                sqarPosition[i] = new RectangleShape(new Vector2f(30,30));
                sqarPosition[i].FillColor = Color.Green;
            }

            for(int i = 0;i < massOfWal.Length;i++)
            {
                for(int j = 0;j < massOfWal[i].ToCharArray().Length;j++)
                {
                    if(massOfWal[i].ToCharArray()[j] == '1')
                    {     
                        sqarPosition[address].Position = new Vector2f(j* 30,(float)i * 30);
                        address++;
                    }
                }
            }
            Console.WriteLine(address);   
        }
        public void draw(RenderWindow win)
        {
            
            for(int i = 0;i < 150;i++)
            {
                win.Draw(sqarPosition[i]);
            }
        }
    }
    public class Beast
    {
        public Sprite sprt;
        public Image img;
        public Texture txt;
        public virtual bool goTo(float x, float y)
        {
            float currentX = sprt.Position.X;
            float currentY = sprt.Position.Y;
            if ((int)sprt.Position.X < (int)x && (int)sprt.Position.Y < (int)y)
            {
                sprt.Position = new Vector2f(currentX + (float)0.5, currentY + (float)0.5);
            }
            else if ((int)sprt.Position.X < (int)x && (int)sprt.Position.Y > (int)y)
            {
                sprt.Position = new Vector2f(currentX + (float)0.5, currentY - (float)0.5);
            }
            else if ((int)sprt.Position.X > (int)x && (int)sprt.Position.Y < (int)y)
            {
                sprt.Position = new Vector2f(currentX - (float)0.5, currentY + (float)0.5);
            }
            else if ((int)sprt.Position.X > (int)x && (int)sprt.Position.Y > (int)y)
            {
                sprt.Position = new Vector2f(currentX - (float)0.5, currentY - (float)0.5);
            }
            else if ((int)sprt.Position.Y > (int)y)
            {
                sprt.Position = new Vector2f(currentX, currentY - (float)0.5);
            }
            else if ((int)sprt.Position.Y < (int)y)
            {
                sprt.Position = new Vector2f(currentX, currentY + (float)0.5);
            }
            else if ((int)sprt.Position.X < (int)x)
            {
                sprt.Position = new Vector2f(currentX + (float)0.5, currentY);
            }
            else if ((int)sprt.Position.X > (int)x)
            {
                sprt.Position = new Vector2f(currentX - (float)0.5, currentY);
            }
            if ((int)sprt.Position.X == (int)x && (int)sprt.Position.Y == (int)y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class Eagle : Beast
    {
        public bool legLock,goted;
        public float catchX, catchY, currentX, currentY;
        public float a;
        public Eagle()
        {
            legLock = false;
            goted = false;
            a = 0;
            catchX = 0;
            catchY = 0;
            currentX = 0;
            currentY = 0;
            img = new Image("eagle.png");
            txt = new Texture(img);
            sprt = new Sprite(txt);
            sprt.Scale = new Vector2f((float)0.03, (float)0.03);
            sprt.Position = new Vector2f(1, 1);
        }
        public override bool goTo(float x, float y)
        {
            if ((int)sprt.Position.X > (int)x)
            {
                sprt.Position = new Vector2f(sprt.Position.X - 1, sprt.Position.Y);
            }
            if ((int)sprt.Position.Y > (int)y)
            {
                sprt.Position = new Vector2f(sprt.Position.X, sprt.Position.Y - 1);
            }
            if ((int)sprt.Position.X < (int)x)
            {
                sprt.Position = new Vector2f(sprt.Position.X + 1, sprt.Position.Y);
            }
            if ((int)sprt.Position.Y < (int)y)
            {
                sprt.Position = new Vector2f(sprt.Position.X, sprt.Position.Y + 1);
            }
            if ((int)sprt.Position.X == (int)x && (int)sprt.Position.Y == (int)y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void update(RenderWindow win, Hero b, Time t, Clock c)
        {
            t = c.ElapsedTime;
            if ((int)t.AsSeconds() < 5)
            {
                float x = 250 + 110 * (float)Math.Cos(a);
                float y = 250 + 110 * (float)Math.Sin(a);
                a += (float)0.01;
                sprt.Position = new Vector2f(x, y);
            }
            else
            {
                if(legLock == false)
                {
                    currentX = sprt.Position.X;
                    currentY = sprt.Position.Y;
                    catchX = b.sprt.Position.X;
                    catchY = b.sprt.Position.Y;
                    legLock = true;
                }
                if(legLock == true)
                {
                    if(goted == false)
                    {
                        goTo(catchX,catchY);
                        if(goTo(catchX,catchY))
                        {
                            goted = true;
                        }
                    }
                    if(goted == true)
                    {
                        goTo(currentX,currentY);
                        if(goTo(currentX,currentY))
                        {
                            c.Restart();
                            legLock = false;
                            goted = false;
                        }    
                    }
                }

            }
            
                        
            win.Draw(sprt);
        }
        
    }
    public class Wolf : Beast
    {
        public int step;
        public Wolf()
        {
            step = 0;
            img = new Image("wolf.png");
            txt = new Texture(img, new IntRect(0, 0, 1000, 1000));
            sprt = new Sprite(txt);
            sprt.Scale = new Vector2f((float)0.03, (float)0.03);
            sprt.Position = new Vector2f(30, 30);

        }
        public void update(Hero b,RenderWindow win)
        {

            int bufferX = b.trackX.Count - (b.trackX.Count - step); // step;
            int bufferY =b.trackY.Count - (b.trackY.Count - step); //step; -- НУ ХУЙ ЗНАЕТ,НАДО ПЕРЕПРОБОВАТЬ НА ВИНДЕ ТАКЖЕ
            if(bufferX < b.trackX.Count && bufferY < b.trackY.Count)
            {
                float xB = b.trackX[bufferX];
                float yB = b.trackY[bufferY];
                goTo(xB, yB);
                if (goTo(xB, yB))
                {
                    step++;
                }
                /*
                if(sprt.GetGlobalBounds().Intersects(b.sprt.GetGlobalBounds()))
                {
                    win.Close(); /////////////////////////////////////////fix
                }
                */
            }
            win.Draw(sprt);
        }
    }

    public class Carrot
    {

        public Sprite sprt;
        public Image img;
        public Texture txt;
        public Font f;
        public int adrs;
        public List<Sprite> car;
        public Text pointsStr;
    	public int points;
        public Carrot(Wall w) 
        {
			points = 0;
        	f = new Font("font.ttf");
        	pointsStr = new Text(points.ToString(),f);
            adrs = 0;
            Image img = new Image("carrot.png");
            Texture txt = new Texture(img);
            Sprite sprt = new Sprite(txt);
            car = new List<Sprite>(190);
            
            for(int i = 0;i < w.massOfWal.Length;i++)
            {
                for(int j = 0;j < w.massOfWal[i].ToCharArray().Length;j++)
                {
                    if(w.massOfWal[i].ToCharArray()[j] == '0')
                    {
                        car.Add(new Sprite(txt));
                        car[adrs].Scale = new Vector2f((float)0.025,(float)0.025); 
                        adrs++;
                    }
                }
            }
            adrs = 0;
            for(int i = 0;i < w.massOfWal.Length;i++)
            {
                for(int j = 0;j < w.massOfWal[i].ToCharArray().Length;j++)
                {
                    if(w.massOfWal[i].ToCharArray()[j] == '0')
                    {
                        car[adrs].Position = new Vector2f((j * 30) + 10,(i * 30) + 10); 
                        adrs++;
                    }
                }
            }
            adrs = 0;
        }
        
        public void draw(RenderWindow w)
        {
            //173
            for(int i = 0;i < car.Count;i++)
            {
                w.Draw(car[i]);
                //Console.WriteLine(car[i].Position.X);
            }
        }
        public void update(RenderWindow win,Hero h)
        {
            for(int i = 0;i < car.Count;i++)
            {
                if(car[i].GetGlobalBounds().Intersects(h.sprt.GetGlobalBounds()))
                {
                	points++;
                    car.Remove(car[i]);
                }
            }
            draw(win);
        }
        public void updatePoints(RenderWindow win)
        {
        	pointsStr = new Text(points.ToString(),f);
        	pointsStr.Position = new Vector2f(0,180);
    		pointsStr.FillColor = Color.Red;

        	win.Draw(pointsStr);
        }
    }

    public class Points
    {
    	public Text t;
    	public Font f;
	
    	public Points()
    	{
    		f = new Font("font.ttf");
    		t = new Text("Points : ",f);
    		t.Position = new Vector2f(0,150);
    		t.FillColor = Color.Red;
    		t.CharacterSize = 18;
    	}
    }

class Program
{
        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow( new VideoMode(510,600),"ddd");
            Wall w = new Wall(39);
            Hero h = new Hero();
            Wolf wolf = new Wolf();
            Eagle bird = new Eagle();
            Time t = new Time();
            Clock c = new Clock();
            Carrot car = new Carrot(w);
            window.SetVerticalSyncEnabled(true);
            window.SetMouseCursorVisible(false);
            for(int i = 0;i < w.sqarPosition.Length;i++)
            {
                Console.WriteLine(i + "\t" + w.sqarPosition[i].Position.X + "\t" + w.sqarPosition[i].Position.Y);
            }
            Points pp = new Points();
            while(window.IsOpen)
            {
                t = c.ElapsedTime;
                window.DispatchEvents(); 
                window.Clear();
                window.Draw(w.sqarPosition[149]);
                car.update(window,h);
                w.draw(window);
                h.update(window,w);
                bird.update(window,h,t,c);
                wolf.update(h,window);
                window.Draw(pp.t); //рисуем текст
                car.updatePoints(window);
                window.Display();
            }
        }
    }
}
