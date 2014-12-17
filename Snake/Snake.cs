using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Snake
    {
        public bool isAlive;
        private int size;
        public int direction;
        private coord head;
        public readonly Queue<coord> the_queue;
        private coord viewport;

        public Snake(coord vp)
        {
            viewport = vp;

            //Insert Frankenstein Reference Here.
            isAlive = true;

            //Set start position to center
            head.x = viewport.x / 2;
            head.y = viewport.y / 2;

            //Generate random direction
            Random rnd = new Random();
            direction = rnd.Next(1, 5);

            //Size always starts at 3 (including head)
            size = 2;

            //Generate queue            
            the_queue = new Queue<coord>();
        }

        public Queue<coord> Move()
        {
            Queue<coord> deleted = new Queue<coord>();

            //If snake has become too long, make shorter
            while (the_queue.Count > size)
            {
                deleted.Enqueue(the_queue.Peek());
                the_queue.Dequeue();
            }

            //Find next position to move to
            coord next = nextPosition();

            //Check for signs of death.
            if (next.x <= 0 || next.x >= viewport.x - 1 || next.y <= 0 || next.y >= viewport.y - 1 || collidesWithSelf(next))
            {
                //Death is inevitable
                isAlive = false;
            }
            else
            {
                //Put the next position into the queue
                the_queue.Enqueue(next);
            }

            return deleted;
        }

        private bool collidesWithSelf(coord n)
        {
            foreach (coord qc in the_queue)
            {
                if (qc.x == n.x && qc.y == n.y)
                {
                    return true;
                }
            }

            return false;
        }


        public bool checkCollidesWithFood(List<Food> fd)
        {
            foreach (Food f in fd)
            {
                if (head.x == f.position.x && head.y == f.position.y)
                {
                    size++;
                    f.nom();
                    return true;
                }
            }
            return false;
        }


        private coord nextPosition()
        {
            coord c = new coord();

            switch (direction)
            {
                case 1:
                    //North/Up
                    c.x = head.x;
                    c.y = head.y--;
                    break;
                case 2:
                    //East/Right
                    c.x = head.x++;
                    c.y = head.y;
                    break;
                case 3:
                    //South/Down
                    c.x = head.x;
                    c.y = head.y++;
                    break;
                case 4:
                    //West/Left
                    c.x = head.x--;
                    c.y = head.y;
                    break;
                default:
                    throw new Exception("Invalid direction");
            }
            return c;
        }
    }
}
