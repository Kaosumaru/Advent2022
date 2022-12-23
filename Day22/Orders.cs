using System.Text;

namespace Day22
{
    public class Orders
    {
        public struct Order
        {
            public int Rotate;
            public int Forward;
        }

        public Orders()
        {

        }

        public void Parse(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            Parse(stream);
        }

        public void Parse(MemoryStream r)
        {
            while (true)
            {
                int b = r.ReadByte();
                if (b == -1)
                    break;

                if (b == 'L')
                    AddRotateOrder(-1);
                else if (b == 'R')
                    AddRotateOrder(1);
                else if (b >= '0' && b <= '9')
                {
                    r.Seek(-1, SeekOrigin.Current);
                    AddForwardOrder(ParseNumber(r));
                }
            }
        }

        void AddRotateOrder(int rotate)
        {
            OrderList.Add(new Order { Rotate = rotate });
        }

        void AddForwardOrder(int forward)
        {
            OrderList.Add(new Order { Forward = forward });
        }

        private int ParseNumber(MemoryStream r)
        {
            long start = r.Position;
            while (true)
            {
                int b = r.ReadByte();
                if (b == -1)
                    break;
                if (b < '0' || b > '9')
                {
                    r.Seek(-1, SeekOrigin.Current);
                    break;
                }
            }

            int length = (int)(r.Position - start);
            r.Seek(start, SeekOrigin.Begin);


            byte[] buffer = new byte[length];
            Span<byte> bytes = buffer;

            r.Read(bytes);

            var str = Encoding.ASCII.GetString(buffer);
            int i = int.Parse(str);
            return i;
        }

        public List<Order> OrderList = new List<Order>();
    }

}
