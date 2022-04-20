using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitOM.Net.Sdp.Tests.ConsoleApp
{
	public sealed class SdpAttributeParamater
	{
		public readonly static IReadOnlyCollection<char> SupportedSeparators = new HashSet<char>()
		{
			':','=',
		};

		public SdpAttributeParamater()
		{
		}
		public SdpAttributeParamater(char separator , string name , string value)
		{
			Separator = separator;
			Name = name ?? string.Empty;
			Value = value ?? string.Empty;
		}

		public char Separator { get; private set; }
		public string Name  { get; private set; }
		public string Value { get; set; }

		public void Validate()
		{
			if ( ! TryValidate() )
			{
				throw new Exception("Validation error");
			}
		}

		public bool TryValidate()
		{
			if (char.IsWhiteSpace(Separator) || char.IsLetterOrDigit(Separator))
			{
				return false;
			}

			if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace( Value ) )
			{
				return false;
			}

			return true; 
		}

		public override string ToString()
		{
			return $"{Name}{Separator}{Value}";
		}

		public static SdpAttributeParamater Parse(string value)
		{
			return TryParse(value, out SdpAttributeParamater p) ? p : throw new FormatException();
		}

		public static SdpAttributeParamater Parse(string value,char separator)
		{
			return TryParse(value, separator , out SdpAttributeParamater p) ? p : throw new FormatException();
		}

		public static bool TryParse( string value , out SdpAttributeParamater result )
		{
			result = SupportedSeparators.Select(x => TryParse(value, out SdpAttributeParamater p) ? p : null).FirstOrDefault();

			return result != null;
		}

		public static bool TryParse( string value , char separator , out SdpAttributeParamater result )
		{
			result = null;

			if ( string.IsNullOrWhiteSpace( value ) )
			{
				return false;
			}

			var tokens = value.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);

			if ( tokens == null || ! tokens.Any() )
			{
				return false;
			}

			result = new SdpAttributeParamater(separator, tokens.ElementAtOrDefault(0), tokens.ElementAtOrDefault(1));

			return true;
		}
	}

	public sealed class SdpAttributeParamaterCollection : ICollection, ICollection<SdpAttributeParamater>
	{
		//private readonly Dictionary<string, HashSet<SdpAttributeParamater>> _items = new Dictionary<string, HashSet<SdpAttributeParamater>>(StringComparer.InvariantCultureIgnoreCase);

		private readonly HashSet<SdpAttributeParamater> _collection = new HashSet<SdpAttributeParamater>();

		public SdpAttributeParamater this[int index]
		{
			get => GetAt(index);
		}

		public SdpAttributeParamater this[ string name ]
		{
			get => GetByName(name);
		}

		public SdpAttributeParamater this[string name,int index]
		{
			get => GetByName(name,index);
		}

		public int Count => _collection.Count;

		public object SyncRoot => this;

		public bool IsSynchronized => false;

		public bool IsReadOnly => false;

		public void Add(SdpAttributeParamater item)
		{
			if (item == null)
			{
				throw new ArgumentNullException();
			}

			if (ContainsName(item.Name))
			{
				throw new ArgumentException();
			}

			if (!_collection.Add(item))
			{
				throw new ArgumentException();
			}
		}

		public void AddRange(IEnumerable<SdpAttributeParamater> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException();
			}

			items.ToList().ForEach(Add);
		}

		public void Clear()
		{
			_collection.Clear();
		}

		public bool Contains(SdpAttributeParamater item)
		{
			return _collection.Contains(item);
		}

		public bool ContainsName(string name)
		{
			return ContainsName(name, true);
		}

		public bool ContainsName(string name, bool ignoreCase)
		{
			return _collection.Any(x => string.Compare(name ?? string.Empty, x.Name ?? string.Empty, ignoreCase) == 0);
		}

		public void CopyTo(Array array, int index)
		{
			_collection.CopyTo(array as SdpAttributeParamater[], index);
		}

		public void CopyTo(SdpAttributeParamater[] array, int arrayIndex)
		{
			_collection.CopyTo(array, arrayIndex);
		}

		public IEnumerator GetEnumerator()
		{
			return _collection.GetEnumerator();
		}

		public SdpAttributeParamater GetAt(int index)
		{
			return _collection.ElementAt(index) ?? throw new InvalidOperationException();
		}

		public SdpAttributeParamater GetByName(string name)
		{
			return _collection.First(x => x.Name == name ) ?? throw new InvalidOperationException();
		}

		public SdpAttributeParamater GetByName(string name,int index)
		{
			return _collection.Where(x => x.Name == name).ElementAt(index) ?? throw new InvalidOperationException();
		}

		public SdpAttributeParamater FindByName(string name)
		{
			return _collection.FirstOrDefault(x => x.Name == name);
		}

		public SdpAttributeParamater FindByName(string name, int index)
		{
			return _collection.Where(x => x.Name == name).ElementAtOrDefault(index);
		}

		public SdpAttributeParamater FindAt(int index)
		{
			return _collection.ElementAtOrDefault(index);
		}

		public bool Remove(SdpAttributeParamater item)
		{
			return _collection.Remove(item);
		}

		public int RemoveRange(IEnumerable<SdpAttributeParamater> items)
		{
			return items?.Where(x => x != null).Where(_collection.Remove).Count() ?? 0;
		}

		public int RemoveRange(Predicate<SdpAttributeParamater> predicate)
		{
			return _collection.Where(x => predicate(x)).ToList().Where(_collection.Remove).Count();
		}

		public IEnumerable<SdpAttributeParamater> FindAllByName(string name)
		{
			return _collection.Where(x => x.Name == name).ToList();
		}

		public IEnumerable<SdpAttributeParamater> FindAll(Predicate<SdpAttributeParamater> predicate)
		{
			return _collection.Where(x => predicate(x)).ToList();
		}

		IEnumerator<SdpAttributeParamater> IEnumerable<SdpAttributeParamater>.GetEnumerator()
		{
			return _collection.GetEnumerator();
		}

		public bool TryAdd(SdpAttributeParamater item)
		{
			if (item == null)
				return false;

			if (ContainsName(item.Name))
			{
				return false;
			}
			return _collection.Add(item);
		}

		public bool TryAddRange(IEnumerable<SdpAttributeParamater> items)
		{
			return items?.Where(TryAdd).Any() ?? false;
		}

		public bool TryAddRange(IEnumerable<SdpAttributeParamater> items, out int result)
		{
			result = items?.Where(TryAdd).Count() ?? 0;
			return result > 0;
		}

		public bool TryGetAt(int index, out SdpAttributeParamater result)
		{
			result = _collection.ElementAtOrDefault(index);

			return result != null;
		}

		public bool TryGetName(string name, out SdpAttributeParamater result)
		{
			result = _collection.Where(x => x.Name == name).FirstOrDefault();

			return result != null;
		}

		public bool TryGetName(string name , int index, out SdpAttributeParamater result)
		{
			result = _collection.Where(x => x.Name == name).ElementAtOrDefault(index);

			return result != null;
		}
	}

	public class SdpAttributeField : BaseField
	{
		public SdpAttributeField( string name )
		{
			Name = name ?? string.Empty;
		}

		public override string TypeName => "a";

		public string Name { get; private set; }
		
		public override bool TryValidate()
		{
			return !string.IsNullOrEmpty(Name);
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var sessionDescriptor = new SessionDescriptor();

			sessionDescriptor.Repeats.Remove(null);
			sessionDescriptor.SessionName.Value = "the session";
			sessionDescriptor.Repeats.Add(new RepeatField(new ValueTime(1, 2), new ValueTime(3, 4)));
			sessionDescriptor.Repeats.Add(new RepeatField());
			sessionDescriptor.Repeats.Add(new RepeatField());
			sessionDescriptor.Repeats.Add(new RepeatField());
			sessionDescriptor.Repeats.TryAdd(null);
			sessionDescriptor.Origin.Address = "192.168.1.23";
			sessionDescriptor.Origin.AddressType = AddressType.IPV4;
			sessionDescriptor.Origin.NetworkType = NetworkType.Internet;
			sessionDescriptor.Origin.UserName = "Kader";
			sessionDescriptor.Origin.Version = "V1";
			sessionDescriptor.Origin.SessionId = "1234";
			sessionDescriptor.Attributes.TryAdd(null);
			sessionDescriptor.Attributes.TryAdd(new AttributeField("key1", "val1"));
			sessionDescriptor.Attributes.TryAdd(new AttributeField("key1", "val1"));
			sessionDescriptor.Attributes.TryAdd(new AttributeField("key2", "val2"));
			


			sessionDescriptor.Phones.TryAdd(null);
			sessionDescriptor.Phones.TryAdd(new PhoneField("+33 1 12 34 56 78"));
			sessionDescriptor.Phones.TryAdd(new PhoneField("+33 1 12 34 56 79"));
			sessionDescriptor.Uri.Value = "rtsp://123.123.35.5:34";
			
			Console.WriteLine(sessionDescriptor.Attributes.FirstOrDefault( x => x.Name == " key1 "));

			var session = SessionDescriptor.Parse(sessionDescriptor.ToString());

			Console.WriteLine(session);
			Console.WriteLine();
			Console.WriteLine(sessionDescriptor);
		}
	}
}
