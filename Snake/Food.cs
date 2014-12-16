using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Food
    {
        public coord position;
        private coord viewport;

        public Food(coord vp) {
            viewport = vp;
            setPosition();
        }

        private void setPosition()
        {
            Random rnd = new Random();
            position.x = rnd.Next(1, viewport.x - 1);
            position.y = rnd.Next(1, viewport.y - 1);
        }

        public void nom()
        {
            setPosition();   
        }
    }
    
    struct coord {
        public int x;
        public int y;
    }

}
