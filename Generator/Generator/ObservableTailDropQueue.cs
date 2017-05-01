using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
	public class ObservableTailDropQueue<T> : INotifyCollectionChanged, IEnumerable<T>
	{
		public event NotifyCollectionChangedEventHandler CollectionChanged;
		private Queue<T> queue;
		private Int32 capacity;

		public ObservableTailDropQueue(Int32 capacity)
		{
			this.capacity = capacity;
			queue = new Queue<T>(capacity);
		}


		public void Enqueue(T item)
		{
			if (queue.Count == capacity)
			{
				Dequeue();
			}

			queue.Enqueue(item);
			CollectionChanged?.Invoke(this,
				new NotifyCollectionChangedEventArgs(
					NotifyCollectionChangedAction.Add, item));
		}

		public T Dequeue()
		{
			var item = queue.Dequeue();
			 CollectionChanged?.Invoke(this,
				new NotifyCollectionChangedEventArgs(
					NotifyCollectionChangedAction.Remove, item, 0));
			return item;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return queue.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
