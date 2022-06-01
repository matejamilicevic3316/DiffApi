using Application.Commands.Enums.Difference;

namespace Application.Helpers
{
    public class WordStoreAccessor
    {
        private static Dictionary<string, string> _wordStore = new Dictionary<string, string>();

        public void SetWord(int id ,HorziontalCheckSide horziontalCheckSide, string value)
        {
            var key = ToSerializedKey(id, horziontalCheckSide);

            var word = _wordStore.FirstOrDefault(x => x.Key == key).Value;

            if (word == null)
            {
                _wordStore.Add(key,value);

                return;
            }

            _wordStore[key] = value;
        }

        public string GetWord(int id, HorziontalCheckSide horziontalCheckSide) => _wordStore.FirstOrDefault(x => x.Key == ToSerializedKey(id, horziontalCheckSide)).Value;
        private string ToSerializedKey(int id, HorziontalCheckSide side) => id.ToString() + "_" + side.ToString();
    }
}
