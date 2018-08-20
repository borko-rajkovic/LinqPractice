<Query Kind="Program" />

class Program
{
	public class Cache
	{
		private int maxCacheSize;
		private Dictionary<int, LinkedListNode> map = new Dictionary<int, LinkedListNode>();
		private LinkedListNode listHead = null;
		public LinkedListNode listTail = null;


		public Cache(int maxSize)
		{
			maxCacheSize = maxSize;
		}

		/* Get value for key and mark as most recently used. */
		public string GetValue(int key)
		{
			LinkedListNode item = map.ContainsKey(key) ? map[key] : null;
			if (item == null)
			{
				return null;
			}

			/* Move to front of list to mark as most recently used. */
			if (item != listHead)
			{
				RemoveFromLinkedList(item);
				InsertAtFrontOfLinkedList(item);
			}
			return item.value;
		}

		/* Remove node from linked list. */
		private void RemoveFromLinkedList(LinkedListNode node)
		{
			if (node == null)
			{
				return;
			}
			if (node.prev != null)
			{
				node.prev.next = node.next;
			}
			if (node.next != null)
			{
				node.next.prev = node.prev;
			}
			if (node == listTail)
			{
				listTail = node.prev;
			}
			if (node == listHead)
			{
				listHead = node.next;
			}
		}

		/* Insert node at front of linked list. */
		private void InsertAtFrontOfLinkedList(LinkedListNode node)
		{
			if (listHead == null)
			{
				listHead = node;
				listTail = node;
			}
			else
			{
				listHead.prev = node;
				node.next = listHead;
				listHead = node;
			}
		}

		/* Remove key, value pair from cache, deleting from hash table
		 * and linked list. */
		public bool RemoveKey(int key)
		{
			LinkedListNode node = map.ContainsKey(key) ? map[key] : null;
			RemoveFromLinkedList(node);
			map.Remove(key);
			return true;
		}

		/* Put key, value pair in cache. Removes old value for key if
		 * necessary. Inserts pair into linked list and hash table.*/
		public void SetKeyValue(int key, string value)
		{
			/* Remove if already there. */
			RemoveKey(key);

			/* If full, remove least recently used item from cache. */
			if (map.Count >= maxCacheSize && listTail != null)
			{
				RemoveKey(listTail.key);
			}

			/* Insert new node. */
			LinkedListNode node = new LinkedListNode(key, value);
			InsertAtFrontOfLinkedList(node);
			map.Add(key, node);
		}

		public string GetCacheAsstring()
		{
			if (listHead == null) return "";
			return listHead.PrintForward();
		}

		public class LinkedListNode
		{
			public LinkedListNode next;
			public LinkedListNode prev;
			public int key;
			public string value;
			public LinkedListNode(int k, string v)
			{
				key = k;
				value = v;
			}

			public string PrintForward()
			{
				string data = "(" + key + "," + value + ")";
				if (next != null)
				{
					return data + "->" + next.PrintForward();
				}
				else
				{
					return data;
				}
			}
		}
	}

	public static void Main(string[] args)
	{
		int cache_size = 5;
		Cache cache = new Cache(cache_size);

		cache.SetKeyValue(1, "1");
		Console.WriteLine(cache.GetCacheAsstring());
		cache.SetKeyValue(2, "2");
		Console.WriteLine(cache.GetCacheAsstring());
		cache.SetKeyValue(3, "3");
		Console.WriteLine(cache.GetCacheAsstring());
		cache.GetValue(1);
		Console.WriteLine(cache.GetCacheAsstring());
		cache.SetKeyValue(4, "4");
		Console.WriteLine(cache.GetCacheAsstring());
		cache.GetValue(2);
		Console.WriteLine(cache.GetCacheAsstring());
		cache.SetKeyValue(5, "5");
		Console.WriteLine(cache.GetCacheAsstring());
		cache.GetValue(5);
		Console.WriteLine(cache.GetCacheAsstring());
		cache.SetKeyValue(6, "6");
		Console.WriteLine(cache.GetCacheAsstring());
		cache.GetValue(1);
		Console.WriteLine(cache.GetCacheAsstring());
		cache.SetKeyValue(5, "5a");
		Console.WriteLine(cache.GetCacheAsstring());
		cache.GetValue(3);
		Console.WriteLine(cache.GetCacheAsstring());
		// 6->5->2->4->1
	}
}
