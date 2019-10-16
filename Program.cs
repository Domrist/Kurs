using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.Xml;

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
        public int life;
        public Hero()
        {
        	life = 3;
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
            }
        }
    }
    public class Wall
    {
	   public string [] massOfWal;
       public RectangleShape [] sqarPosition;
       public int address;
       public Wall(int x)
       {
           address = 0;
           sqarPosition = new RectangleShape[150];
	       massOfWal = new string []{
                                 "00000000100000000",
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
        }
        public void update(RenderWindow win)
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
        public static bool stay;
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

            int bufferX = b.trackX.Count - (b.trackX.Count - step);
            int bufferY =b.trackY.Count - (b.trackY.Count - step);
            if(bufferX < b.trackX.Count && bufferY < b.trackY.Count)
            {
                float xB = b.trackX[bufferX];
                float yB = b.trackY[bufferY];
                goTo(xB, yB);
                if (goTo(xB, yB))
                {
                    step++;
                }
            }
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
            for(int i = 0;i < car.Count;i++)
            {
                w.Draw(car[i]);
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
        	pointsStr = new Text(points.ToString(),f);
        	pointsStr.Position = new Vector2f(0,180);
    		pointsStr.FillColor = Color.Red;
            draw(win);
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
    	public void update(RenderWindow win)
    	{
    		win.Draw(t);
    	}
    }
    public class AnswersAndQuestions //////////////////////////ПОКАЗАТЬ АКСЁНОВОЙ И СПРРОСИТЬ ПРО ИЗМЕНЕНИЕ АТРИБУТОВ XML
    {
    	public	Font f;
    	public	Text question;
    	public	Text answer1;
    	public	Text answer2;
    	public	Text answer3;
    	public XmlDocument doc;
    	public XmlElement root;
    	public Text rightQuestion;
    	public string finalAnswer;
    	public Text [] mas;
        public Random rnd;
        public string questionID;
        public bool lockQuestion;
        public int randId;
    	public AnswersAndQuestions()
    	{
    		finalAnswer = new string("");
    		f = new Font("font.ttf");
    		question = new Text("",f); 
    		answer1 = new Text("",f); 
    		answer2 = new Text("",f);
    		answer3 = new Text("",f);
            question.FillColor = Color.Green;
    		rightQuestion = new Text("",f);
            answer1.FillColor = Color.Red;
            answer2.FillColor = Color.Red;
            answer3.FillColor = Color.Red;
            rnd = new Random();
    		doc = new XmlDocument();
    		doc.Load("question.xml");
    		root = doc.DocumentElement;
    		mas = new Text[3]{answer1,answer2,answer3};
    		lockQuestion = false;
    	}
    	public void showQuestion(RenderWindow win)
    	{
    		win.Draw(question);
    		win.Draw(answer1);
    		win.Draw(answer2);
    		win.Draw(answer3);
    	}
    	public void setQuestion()
    	{
    		if(!lockQuestion)
    		{
            	randId = rnd.Next(1,5); //что то недао сделать с программой
            	lockQuestion = true;
            	questionID = randId.ToString();
    		}
    		else
    		{

    		}
    		foreach(XmlNode node in root.ChildNodes) //перебираем весь файл на отдельные ноды
    		{

                /*Цикл ниже - перебираем отдельный узел,на которые не ответили*/
                
                if(node.Attributes.GetNamedItem("isAnswered").Value == "no" && 
                   node.Attributes.GetNamedItem("id").Value == randId.ToString())
                {
	                foreach(XmlNode i in node)
	                {
	                	if(i.Name == "text")
	                    {
	                    	question.DisplayedString = i.InnerText;
	                    }
	                    if(i.Name == "rightQuestion")
	                    {
	                    	finalAnswer = i.InnerText.ToString();
	                    }
	                    if(i.Name == "answer1")
	                    {
	                        answer1.DisplayedString = i.InnerText;
	                    }
	                    if(i.Name == "answer2")
	                    {
	                        answer2.DisplayedString = i.InnerText;
	                    }
	                    if(i.Name == "answer3")
	                    {
	                        answer3.DisplayedString = i.InnerText;
	                    }

	                }
	                question.Position = new Vector2f(255 - question.GetGlobalBounds().Width/2,0);
	                answer1.Position = new Vector2f(510/4 - answer1.GetGlobalBounds().Width/2,100);
	                answer2.Position = new Vector2f(510/4*2 - answer2.GetGlobalBounds().Width/2,100);
	                answer3.Position = new Vector2f(510/4*3 - answer3.GetGlobalBounds().Width/2,100);
                    break;
	            }
                
    		} 
    	}
        
    	public bool checkQuestion(RenderWindow win,Hero h,Wolf w,Eagle e)
    	{
    		foreach(Text button in mas)
    		{
	    		if(Mouse.GetPosition(win).X >= button.Position.X &&
	    		   Mouse.GetPosition(win).X <= button.GetGlobalBounds().Width + button.Position.X &&
	    		   Mouse.GetPosition(win).Y <= button.Position.Y + button.GetGlobalBounds().Height + 20 &&
	    		   Mouse.GetPosition(win).Y >= button.Position.Y )
	    		{
	    			button.FillColor = Color.Green;
	    			if(Mouse.IsButtonPressed(Mouse.Button.Left))	
	    			{
		    			if(button.DisplayedString == finalAnswer)
	    				{
                            w.sprt.Position = new Vector2f(0,0);
                            e.sprt.Position = new Vector2f(e.currentX,e.currentY);
                            Beast.stay = false;
                            lockQuestion = true;
	    					return true;
	    				}
	    				else if(button.DisplayedString != finalAnswer) 
	    				{
	    					h.life -= 1;
                            w.sprt.Position = new Vector2f(0,0);
                            e.sprt.Position = new Vector2f(300,300);  //////////////////////////ПОФИКСИТЬ ПОЛОЖЕНИЕ ОРЛА ПОСЛЕ СБРОСА ВОПРОСА
                            lockQuestion = true;
                            Beast.stay = false;
	    					return false;
	    				}
	    			}
	    			else
	    			{
	    			}
	    		}		
	    		else
	    		{
	    			button.FillColor = Color.Red;
	    		}
	    	}
	    	return false;
    	}
    }
    public class Creatures
    {
    	public Creatures()
    	{
    	}
    	public bool check(Hero h,Beast b,Beast bb) //проверка,поймали ли зайчика
    	{
    		bool q = false;
    		
			if(b.sprt.GetGlobalBounds().Intersects(h.sprt.GetGlobalBounds()) || bb.sprt.GetGlobalBounds().Intersects(h.sprt.GetGlobalBounds()))
			{
				q =  true;
			}
			else
			{
				q = false;
			}
    		
    		return q;
    	}
	    public void drawAll(RenderWindow win,Hero h,Eagle e,Wolf w)
	    {
	    	win.Draw(h.sprt);	
	    	win.Draw(w.sprt);	
	    	win.Draw(e.sprt);	
	    }	
	    public void updateAll(RenderWindow win,Hero h,Eagle e,Wolf w,Wall wal,Carrot car,Time t,Clock c,Points p,
	    					  AnswersAndQuestions quest)
	    {
	    	if(check(h,w,e))
	    	{
	    		Beast.stay = true;
	    		quest.setQuestion();
	    	}
	    	if(!check(h,w,e))
	    	{
	    		Beast.stay = false;
	    	}
	    	if(Beast.stay == false) //если зайца не поймали,то тогда рисуем всё
	    	{
		    	wal.update(win);
		    	p.update(win);
			    car.update(win,h);
		    	h.update(win,wal);
		    	e.update(win,h,t,c);
		    	w.update(h,win);
		    	drawAll(win,h,e,w);
	    	}
	    	else //если зайца поймали то тогда ничего не рисуем и останавливаем всех персонажей 
	    	{
	    		h.sprt.Position = new Vector2f(h.sprt.Position.X,h.sprt.Position.Y);
	    		e.sprt.Position = new Vector2f(e.sprt.Position.X,e.sprt.Position.Y);
	    		w.sprt.Position = new Vector2f(w.sprt.Position.X,w.sprt.Position.Y);
	    		quest.showQuestion(win);
	    		quest.checkQuestion(win,h,w,e);
	    	}
		    
	    }
    }
    public class Over
    {
    	public Font f;
    	public Text gameOverText;
		public Text gameOverGoodText;
		public Over()
		{
    		f = new Font("font.ttf");
    		gameOverText = new Text("Game Over :..(",f);
    		gameOverGoodText = new Text("You Win :)",f);
    		gameOverGoodText.FillColor = Color.Red;
    		gameOverText.FillColor = Color.Red;
    		gameOverText.Position = new Vector2f(100,300);
			gameOverGoodText.Position = new Vector2f(100,300);
		}
    	public void GameOver(ref RenderWindow win,ref Time t)
    	{
    		if((int)t.AsSeconds() > 5)
    		{
    			win.Close();
    		}
    		else
    		{
    			win.Draw(gameOverText);
    		}
    	}
    	public void GameGood(ref RenderWindow win,ref Time t)
    	{
    		if((int)t.AsSeconds() > 5)
    		{
    			win.Close();
    		}
    		else
    		{
    			win.Draw(gameOverGoodText);
    		}
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
            window.SetMouseCursorVisible(true);
            Points pp = new Points();
            Creatures creatures = new Creatures();
            Beast.stay = false;
            AnswersAndQuestions quest = new AnswersAndQuestions();
            bool deadAll = false;
            Over over = new Over();
            while(window.IsOpen)
            {
            	Console.WriteLine(h.life);
                t = c.ElapsedTime;
                window.DispatchEvents(); 
                window.Clear();
                if(car.points < 190 && deadAll == false)
                {
	                creatures.updateAll(window,h,bird,wolf,w,car,t,c,pp,quest);
                }
                if(car.points >= 190)
                {
                	if(!deadAll)
                	{
                		c.Restart();
                		deadAll = true;
                	}
                	over.GameGood(ref window,ref t);
                	//ДОБАВИТЬ ХОРОШУЮ КОНЦОВУ
                }
                if(h.life == 0)
                {
                	if(!deadAll)
                	{
                		deadAll = true;
                		c.Restart();
                	}
                	over.GameOver(ref window,ref t);
                	//ДОБАВИТЬ ПЛОХУЮ КОНЦОВКУ
                }
                window.Display();
            }
        }
    }
}