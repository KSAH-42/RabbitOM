//                  W A R N I N G

// The ExtensionList class used an hashset, it may cause issues because in some
// cases duplicated elements are allowed, for the SdpSequence class
// duplicated value are granted, please check in other places
// if the ExtensionList class must forbid duplicated value
// otherwise change replace the internal hashset member
// by a string list



using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitOM.Net.Sdp.Tests.ConsoleApp
{
	public sealed class SdpSequence
	{
		public readonly static IReadOnlyCollection<char> SupportedSeparators = new HashSet<char>()
		{
			'/' , '\\'
		};

		private readonly ExtensionList _values = new ExtensionList();

		public ExtensionList Values
		{
			get => _values;
		}

		public void Validate()
		{
			if ( ! TryValidate() )
			{
				throw new Exception("Validation failed");
			}
		}

		public bool TryValidate()
		{
			return _values.Any();
		}

		public override string ToString()
		{
			return string.Join( "/" , _values);
		}

		public static SdpSequence Parse( string value )
		{
			return TryParse(value, out SdpSequence result) ? result : throw new InvalidOperationException();
		}

		public static bool TryParse( string value, out SdpSequence result )
		{
			result = null;

			if ( string.IsNullOrWhiteSpace( value ) )
			{
				return false;
			}

			if (!SupportedSeparators.Where(x => value.Contains(x)).Any())
			{
				return false;
			}			

			var tokens = value.Split( SupportedSeparators.ToArray() , StringSplitOptions.RemoveEmptyEntries );

			if (tokens == null || !tokens.Any() )
			{
				return false;
			}

			result = new SdpSequence();
			result.Values.AddRange(tokens);

			return true;
		}
	}

	public sealed class SdpSequenceCollection : ICollection, ICollection<SdpSequence>, IEnumerable, IEnumerable<SdpSequence>
	{
		private readonly HashSet<SdpSequence> _collection = new HashSet<SdpSequence>();

		public SdpSequenceCollection()
		{
		}

		public SdpSequenceCollection( IEnumerable<SdpSequence> items )
		{
			AddRange(items);
		}

		public SdpSequence this[ int index ]
		{
			get => GetAt(index);
		}

		public int Count => _collection.Count;

		public object SyncRoot => this;

		public bool IsSynchronized => false;

		public bool IsReadOnly => false;

		public void Add(SdpSequence item)
		{
			if ( item == null )
			{
				throw new ArgumentNullException(nameof(item));
			}

			if ( !_collection.Add( item ) )
			{
				throw new ArgumentException(nameof(item));
			}
		}

		public void AddRange( IEnumerable<SdpSequence> items )
		{
			if ( items == null )
			{
				throw new ArgumentNullException(nameof(items));
			}

			items.ToList().ForEach(Add);
		}

		public void Clear()
		{
			_collection.Clear();
		}

		public bool Contains(SdpSequence item)
		{
			return _collection.Contains(item);
		}

		public void CopyTo(Array array, int index)
		{
			_collection.CopyTo(array as SdpSequence[], index);
		}

		public void CopyTo(SdpSequence[] array, int arrayIndex)
		{
			_collection.CopyTo(array, arrayIndex);
		}

		public IEnumerator GetEnumerator()
		{
			return _collection.GetEnumerator();
		}

		public SdpSequence GetAt( int index )
		{
			return _collection.ElementAt(index) ?? throw new InvalidOperationException();
		}

		public SdpSequence FindAt(int index)
		{
			return _collection.ElementAtOrDefault(index);
		}

		public IEnumerable<SdpSequence> FindAll( Predicate<SdpSequence> predicate )
		{
			return _collection.Where(x => predicate(x)).ToList();
		}

		public bool Remove(SdpSequence item)
		{
			return _collection.Remove(item);
		}

		public int RemoveRange( IEnumerable<SdpSequence> items )
		{
			if ( items == null )
			{
				return 0;
			}

			return items.Where(x => _collection.Remove(x)).Count();
		}

		IEnumerator<SdpSequence> IEnumerable<SdpSequence>.GetEnumerator()
		{
			return _collection.GetEnumerator();
		}

		public bool TryAdd( SdpSequence item )
		{
			if ( item == null )
			{
				return false;
			}

			return _collection.Add(item);
		}

		public bool TryAddRange(IEnumerable<SdpSequence> items )
		{
			return items?.Where(TryAdd).Any() ?? false;
		}

		public bool TryAddRange( IEnumerable<SdpSequence> items , out int result )
		{
			result = items?.Where(TryAdd).Count() ?? 0;
			return result > 0;
		}

		public bool TryGetAt( int index , out SdpSequence sequence )
		{
			sequence = _collection.ElementAtOrDefault(index);
			return sequence != null;
		}
	}

	public sealed class SdpAttributeParameter
	{
		public readonly static IReadOnlyCollection<char> SupportedSeparators = new HashSet<char>()
		{
			':','=',
		};

		public SdpAttributeParameter()
		{
		}
		public SdpAttributeParameter(char separator , string name , string value)
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

		public static SdpAttributeParameter Parse(string value)
		{
			return TryParse(value, out SdpAttributeParameter p) ? p : throw new FormatException();
		}

		public static SdpAttributeParameter Parse(string value,char separator)
		{
			return TryParse(value, separator , out SdpAttributeParameter p) ? p : throw new FormatException();
		}

		public static bool TryParse( string value , out SdpAttributeParameter result )
		{
			result = SupportedSeparators.Select(x => TryParse(value, x ,out SdpAttributeParameter p) ? p : null).Where( x => x != null ).FirstOrDefault();

			return result != null;
		}

		public static bool TryParse( string value , char separator , out SdpAttributeParameter result )
		{
			result = null;

			if ( string.IsNullOrWhiteSpace( value ) || ! value.Contains( separator ) )
			{
				return false;
			}

			var tokens = value.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);

			if ( tokens == null || ! tokens.Any() )
			{
				return false;
			}

			result = new SdpAttributeParameter(separator, tokens.ElementAtOrDefault(0), tokens.ElementAtOrDefault(1));

			return true;
		}
	}

	public sealed class SdpAttributeParamaterCollection : ICollection, ICollection<SdpAttributeParameter>
	{
		//private readonly Dictionary<string, HashSet<SdpAttributeParamater>> _items = new Dictionary<string, HashSet<SdpAttributeParamater>>(StringComparer.InvariantCultureIgnoreCase);

		private readonly HashSet<SdpAttributeParameter> _collection = new HashSet<SdpAttributeParameter>();

		public SdpAttributeParameter this[int index]
		{
			get => GetAt(index);
		}

		public SdpAttributeParameter this[ string name ]
		{
			get => GetByName(name);
		}

		public SdpAttributeParameter this[string name,int index]
		{
			get => GetByName(name,index);
		}

		public int Count => _collection.Count;

		public object SyncRoot => this;

		public bool IsSynchronized => false;

		public bool IsReadOnly => false;

		public void Add(SdpAttributeParameter item)
		{
			if (item == null)
			{
				throw new ArgumentNullException();
			}

			if (!_collection.Add(item))
			{
				throw new ArgumentException();
			}
		}

		public void AddRange(IEnumerable<SdpAttributeParameter> items)
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

		public bool Contains(SdpAttributeParameter item)
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
			_collection.CopyTo(array as SdpAttributeParameter[], index);
		}

		public void CopyTo(SdpAttributeParameter[] array, int arrayIndex)
		{
			_collection.CopyTo(array, arrayIndex);
		}

		public IEnumerator GetEnumerator()
		{
			return _collection.GetEnumerator();
		}

		public SdpAttributeParameter GetAt(int index)
		{
			return _collection.ElementAt(index) ?? throw new InvalidOperationException();
		}

		public SdpAttributeParameter GetByName(string name)
		{
			return _collection.First(x => x.Name == name ) ?? throw new InvalidOperationException();
		}

		public SdpAttributeParameter GetByName(string name,int index)
		{
			return _collection.Where(x => x.Name == name).ElementAt(index) ?? throw new InvalidOperationException();
		}

		public SdpAttributeParameter FindByName(string name)
		{
			return _collection.FirstOrDefault(x => x.Name == name);
		}

		public SdpAttributeParameter FindByName(string name, int index)
		{
			return _collection.Where(x => x.Name == name).ElementAtOrDefault(index);
		}

		public SdpAttributeParameter FindAt(int index)
		{
			return _collection.ElementAtOrDefault(index);
		}

		public string FindValueByName( string name )
		{
			return _collection.FirstOrDefault(x => x.Name == name)?.Value ?? string.Empty;
		}

		public bool Remove(SdpAttributeParameter item)
		{
			return _collection.Remove(item);
		}

		public int RemoveRange(IEnumerable<SdpAttributeParameter> items)
		{
			return items?.Where(x => x != null).Where(_collection.Remove).Count() ?? 0;
		}

		public int RemoveRange(Predicate<SdpAttributeParameter> predicate)
		{
			return _collection.Where(x => predicate(x)).ToList().Where(_collection.Remove).Count();
		}

		public IEnumerable<SdpAttributeParameter> FindAllByName(string name)
		{
			return _collection.Where(x => x.Name == name).ToList();
		}

		public IEnumerable<SdpAttributeParameter> FindAll(Predicate<SdpAttributeParameter> predicate)
		{
			return _collection.Where(x => predicate(x)).ToList();
		}

		IEnumerator<SdpAttributeParameter> IEnumerable<SdpAttributeParameter>.GetEnumerator()
		{
			return _collection.GetEnumerator();
		}

		public bool TryAdd(SdpAttributeParameter item)
		{
			if (item == null)
				return false;

			return _collection.Add(item);
		}

		public bool TryAddRange(IEnumerable<SdpAttributeParameter> items)
		{
			return items?.Where(TryAdd).Any() ?? false;
		}

		public bool TryAddRange(IEnumerable<SdpAttributeParameter> items, out int result)
		{
			result = items?.Where(TryAdd).Count() ?? 0;
			return result > 0;
		}

		public bool TryGetAt(int index, out SdpAttributeParameter result)
		{
			result = _collection.ElementAtOrDefault(index);

			return result != null;
		}

		public bool TryGetName(string name, out SdpAttributeParameter result)
		{
			result = _collection.Where(x => x.Name == name).FirstOrDefault();

			return result != null;
		}

		public bool TryGetName(string name , int index, out SdpAttributeParameter result)
		{
			result = _collection.Where(x => x.Name == name).ElementAtOrDefault(index);

			return result != null;
		}
	}

	public class SdpAttributeField : BaseField
	{
		private string _value = string.Empty;

		private readonly SdpAttributeParamaterCollection _parameters = new SdpAttributeParamaterCollection();

		private readonly SdpSequenceCollection _sequences = new SdpSequenceCollection();

		public override string TypeName => "a";

		public string Value
		{
			get => _value;
			set => _value = SessionDescriptorDataConverter.Trim(value);
		}

		public SdpAttributeParamaterCollection Parameters { get => _parameters; }

		public SdpSequenceCollection Sequences { get => _sequences; }

		public override bool TryValidate()
		{
			return ! string.IsNullOrWhiteSpace( _value )  
				|| _parameters.Any()
				|| _sequences.Any();
		}

		public override string ToString()
		{
			var result = string.Join( " " , new string[] { Value }.Concat( _parameters.Select( x => x.ToString() ) ) ) + " " + string.Join( " " , _sequences.Select( x => x ) );

			return result;
		}

		public static SdpAttributeField Parse(string value)
		{
			return TryParse(value, out SdpAttributeField result) ? result : throw new FormatException();
		}

		public static bool TryParse(string value, out SdpAttributeField result )
		{
			result = null;

			if ( string.IsNullOrWhiteSpace( value ) )
			{
				return false;
			}

			var tokens = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			if ( tokens == null || ! tokens.Any() )
			{
				return false;
			}

			result = new SdpAttributeField();

			foreach ( var token in tokens )
			{
				if ( SdpAttributeParameter.TryParse(token, out SdpAttributeParameter parameter ) && result.Parameters.TryAdd( parameter ) )
				{
					continue;
				}

				if ( SdpSequence.TryParse( token , out SdpSequence sequence ) && result.Sequences.TryAdd( sequence ) )
				{
					continue;
				}

				result.Value = string.Concat(result.Value, string.Join(" ", token));
			}

			return true;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var attribute = SdpAttributeField.Parse("input/ZEZ/eZ test parameter1:value1 parameter2=value2 parameter2=value3 a/b/c//d");

			Console.WriteLine( attribute );
			Console.WriteLine(attribute.Value);
			attribute.Parameters.ToList().ForEach(Console.WriteLine);
			attribute.Sequences.ToList().ForEach(Console.WriteLine);

			Console.WriteLine( attribute.Value );

			Console.WriteLine(attribute.Parameters["parameter1"].Value);
			Console.WriteLine(attribute.Parameters["parameter2"].Value);
			Console.WriteLine(attribute.Parameters["parameter2",1].Value);
			Console.WriteLine(attribute.Parameters.FindValueByName("parameter2"));
			Console.WriteLine(attribute.Parameters.FindValueByName("parameter3"));
			Console.WriteLine(attribute.Sequences.Count);
			Console.WriteLine(attribute.Sequences[0].Values.Count);
			Console.WriteLine(attribute.Sequences[0].Values[0]);
			Console.WriteLine(attribute.Sequences[0].Values[1]);
			Console.WriteLine(attribute.Sequences[0].Values[2]);

			var sessionDescriptor = new SessionDescriptor();

			sessionDescriptor.Version.Value = 1;
			sessionDescriptor.SessionName.Value = "My session name";
			sessionDescriptor.Repeats.Add(new RepeatField(new ValueTime(1, 2), new ValueTime(3, 4)));
			sessionDescriptor.Repeats.Add(new RepeatField(new ValueTime(10, 20), new ValueTime(30, 40)));
			sessionDescriptor.Origin.Address = "192.168.1.23";
			sessionDescriptor.Origin.AddressType = AddressType.IPV4;
			sessionDescriptor.Origin.NetworkType = NetworkType.Internet;
			sessionDescriptor.Origin.UserName = "Kader";
			sessionDescriptor.Origin.Version = "V1";
			sessionDescriptor.Origin.SessionId = "123456789";
			sessionDescriptor.Attributes.Add(new AttributeField("myAttribute1", "myValue1"));
			sessionDescriptor.Attributes.Add(new AttributeField("myAttribute2", "myValue2"));
			sessionDescriptor.Attributes.Add(new AttributeField("myAttribute2", "myValue3"));

			sessionDescriptor.Phones.Add(new PhoneField("+33 1 12 34 56 78"));
			sessionDescriptor.Phones.Add(new PhoneField("+33 1 12 34 56 79"));
			sessionDescriptor.Uri.Value = "rtsp://192.168.0.11:554";

			for ( int i = 1; i <= 10; ++ i )
			{
				var mediaDescription = new MediaDescriptionField();

				mediaDescription.Payload = 1 + i;
				mediaDescription.Port = 10 + i;
				mediaDescription.Profile = ProfileType.AVP;
				mediaDescription.Protocol = ProtocolType.RTP;
				mediaDescription.Type = MediaType.Video;

				mediaDescription.Encryption.Key = "myKey"+i.ToString();
				mediaDescription.Encryption.Method = "myMethod"+i.ToString();
				mediaDescription.Connection.Address = "192.168.0."+i.ToString();
				mediaDescription.Connection.AddressType = AddressType.IPV4;
				mediaDescription.Connection.NetworkType = NetworkType.Internet;
				mediaDescription.Bandwiths.Add(new BandwithField("modifier", i));
				mediaDescription.Bandwiths.Add(new BandwithField("modifier"+i.ToString(), i+i));
				mediaDescription.Attributes.Add(new AttributeField("myAttribute1", "myValue1"));
				mediaDescription.Attributes.Add(new AttributeField("myAttribute2", "myValue2"));
				mediaDescription.Attributes.Add(new AttributeField("myAttribute3", "myValue3"));

				sessionDescriptor.MediaDescriptions.Add(mediaDescription);
			}

			
			Console.WriteLine(sessionDescriptor.ToString());

			if ( SessionDescriptor.TryParse( sessionDescriptor.ToString() , out SessionDescriptor descriptor ) )
			{
				Console.WriteLine("Ok");
			}

			
		}
	}
}
