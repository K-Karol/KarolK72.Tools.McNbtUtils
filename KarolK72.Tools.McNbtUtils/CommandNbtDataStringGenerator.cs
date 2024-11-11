using System.Text;
using fNbt;

namespace KarolK72.Tools.McNbtUtils
{
    public static class CommandNbtDataStringGenerator
    {
        public static string Generate(NbtTag tag)
        {
            var builder = new StringBuilder();
            GenerateInternal(tag, builder, isFirstItem: true);
            return builder.Remove(builder.Length -2, 2).ToString();
        }

        private static void GenerateInternal(NbtTag tag, StringBuilder builder, bool isListItem = false, bool isFirstItem = false)
        {
            if((tag is NbtCompound && isListItem) || isFirstItem)
                builder.Append('{');

            if (!string.IsNullOrWhiteSpace(tag.Name) && !isFirstItem)
            {
                builder.Append(tag.Name.FormatName());
                builder.Append(": ");
            }
            
            if(tag is NbtCompound && !isListItem && !isFirstItem)
                builder.Append('{');

            if (tag is NbtList nbtList)
            {
                builder.Append('[');
                foreach (var subTag in nbtList)
                {
                    GenerateInternal(subTag, builder, true);
                }
                builder.Remove(builder.Length - 2, 2); // remove comma and space
                builder.Append(']');
            }
            else if (tag is NbtCompound nbtCompound)
            {
                foreach (var subTag in nbtCompound)
                {
                    GenerateInternal(subTag, builder);
                }
                builder.Remove(builder.Length - 2, 2); // remove comma and space
            }
            if (tag is NbtString)
            {
                builder.Append($"'{tag.StringValue}'");
            }
            else if (tag is NbtInt)
            {
                builder.Append(tag.IntValue);
            
            }
            else if (tag is NbtFloat)
            {
                builder.Append(tag.FloatValue);
                builder.Append("f");
            }
            else if (tag is NbtDouble)
            {
                builder.Append(tag.DoubleValue);
                builder.Append("d");
            }
            else if (tag is NbtShort)
            {
                builder.Append(tag.ShortValue);
                builder.Append("s");
            }
            else if (tag is NbtByte)
            {
                builder.Append(tag.ByteValue);
            }
            else if (tag is NbtLong)
            {
                builder.Append(tag.LongValue);
                builder.Append('l');
            }
            else if (tag is NbtByteArray)
            {
                // untested
                builder.Append('[').Append("B;");
                foreach (var b in tag.ByteArrayValue)
                {
                    builder.Append(b.ToString()).Append("b,");
                }
                builder.Remove(builder.Length - 1, 1).Append(']');
            }
            else if (tag is NbtIntArray)
            {
                // untested
                builder.Append('[').Append("I;");
                foreach (var i in tag.IntArrayValue)
                {
                    builder.Append(i.ToString()).Append(',');
                }
                builder.Remove(builder.Length - 1, 1).Append(']');
            }
            
            if (tag is NbtCompound || isFirstItem)
                builder.Append('}');
        
            builder.Append(", ");
            
        }
    }
}