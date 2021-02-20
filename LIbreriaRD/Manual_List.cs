using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace LIbreriaRD
{
    public class Manual_List<T> : IEnumerable<T>, IEnumerable

    {
        private Node<T> head;
        private Node<T> tail;
        private Node<T> current;
       
        public int Length { get; private set; }

        public Manual_List()
        {
            this.head = this.tail = null;
      
            this.Length = 0;
        }

        public T PeekFirst()
        {
            if (this.head == null)
            {
                throw new Exception("No items in the linked list to peek at");
            }

            return this.head.Data;


        }

        public T peekatposition(int  pos)
        {
            current = this.head;
            int cont = 0; 
            while (pos> cont)
            {
                current = this.current.Next;
                cont++;
            }
            return this.current.Data;
        }
  
        public void AddLast(T item)
        {
            if (head == null)
            {
           
                this.head = new Node<T>(item, null, null);
                this.tail = head;
                this.Length++;
            }
            else
            {
                Node<T> newLink = new Node<T>(item, null, this.tail);
                this.tail.Next = newLink;

                this.tail = newLink;
                this.Length++;
            }
        }

        public void RemoveLast()
        {
            if (head != null)
            {
                if (this.Length == 1)
                {
                    this.head = null;
                    this.tail = null;
                    this.Length--;
                }
                else
                {
                    this.tail.Previous.Next = null;
                    this.tail = this.tail.Previous;
                    this.Length--;
                }
            }
        }

      

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append("[").Append(this.Length).Append("] ");

            for (Node<T> link = this.head; link != null; link = link.Next)
            {
                result.Append(":").Append(link.Data.ToString()).Append(":");
            }

            return result.ToString();
        }

        private Manual_List<T>.LinkedListEnumerator GetEnumerator()
        {
            return new LinkedListEnumerator(this.head, this.tail, this.Length);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }

        public struct LinkedListEnumerator : IEnumerator<T>, IEnumerator
        {
            private Node<T> head;
            private Node<T> tail;
            private Node<T> currentLink;
            private int length;
            private bool startedFlag;

            public LinkedListEnumerator(Node<T> head, Node<T> tail, int length)
            {
                this.head = head;
                this.tail = tail;
                this.currentLink = null;
                this.length = length;
                this.startedFlag = false;
            }

            public T Current
            {
                get { return this.currentLink.Data; }
            }

            public void Dispose()
            {
                this.head = null;
                this.tail = null;
                this.currentLink = null;
            }

            object IEnumerator.Current
            {
                get { return this.currentLink.Data; }
            }

            public bool MoveNext()
            {
                if (this.startedFlag == false)
                {
                    this.currentLink = this.head;
                    this.startedFlag = true;
                }
                else
                {
                    this.currentLink = this.currentLink.Next;
                }

                return this.currentLink != null;
            }

            public void Reset()
            {
                this.currentLink = this.head;
            }




        }

    }
}
