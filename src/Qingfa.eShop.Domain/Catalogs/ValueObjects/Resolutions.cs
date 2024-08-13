using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Images.ValueObjects
{
    public class Resolution : ValueObject
    {
        public string Key { get; }
        public string Url { get; }

        private Resolution(string key, string url)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Key cannot be null or whitespace.", nameof(key));
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentException("URL cannot be null or whitespace.", nameof(url));

            Key = key;
            Url = url;
        }

        // Static method to create a new Resolution instance
        public static Resolution Create(string key, string url)
        {
            return new Resolution(key, url);
        }

        public Resolution WithUpdatedUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentException("URL cannot be null or whitespace.", nameof(url));
            return new Resolution(Key, url);
        }

        public static Dictionary<string, Resolution> CreateFromDictionary(Dictionary<string, string> dictionary)
        {
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

            var resolutions = new Dictionary<string, Resolution>();
            foreach (var kvp in dictionary)
            {
                resolutions[kvp.Key] = Create(kvp.Key, kvp.Value);
            }

            return resolutions;
        }

        public KeyValuePair<string, string> ToKeyValuePair()
        {
            return new KeyValuePair<string, string>(Key, Url);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Key;
            yield return Url;
        }
    }
}
