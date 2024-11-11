using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using fNbt;

namespace KarolK72.Tools.McNbtUtils.App.Models;

public class NbtTreeNode
{
    public string Text { get;  }
    public ObservableCollection<NbtTreeNode> Children { get;  }
    public bool IsList { get;  }
    public NbtTag? Tag { get;  }

    public NbtTreeNode(NbtTag tag)
    {
        Tag = tag;
        if (!string.IsNullOrWhiteSpace(tag.Name))
        {
            Text = tag.Name + ": ";
        }

        switch (tag)
        {
            case NbtList nbtList:
                IsList = true;
                Text += $"{nbtList.Count} entries";
                Children = new ObservableCollection<NbtTreeNode>(nbtList.Select(t => new NbtTreeNode(t)).ToList());
                break;
            case NbtCompound nbtCompound:
                Text += $"{nbtCompound.Count} entries";
                Children = new ObservableCollection<NbtTreeNode>(nbtCompound.Select(t => new NbtTreeNode(t)).ToList());
                break;
            case NbtString:
                Text += tag.StringValue;
                break;
            case NbtInt:
                Text += tag.IntValue;
                break;
            case NbtFloat:
                Text += tag.FloatValue;
                break;
            case NbtDouble:
                Text += tag.DoubleValue;
                break;
            case NbtShort:
                Text += tag.ShortValue;
                break;
            case NbtByte:
                Text += tag.ByteValue;
                break;
            case NbtLong:
                Text += tag.LongValue;
                break;
            case NbtByteArray:
                IsList = true;
                Text += $"{tag.ByteArrayValue.Length} entries";
                Children = new ObservableCollection<NbtTreeNode>(
                    tag.ByteArrayValue.Select(b => new NbtTreeNode(b.ToString())));
                break;
            case NbtIntArray:
                IsList = true;
                Text += $"{tag.IntArrayValue.Length} entries";
                Children = new ObservableCollection<NbtTreeNode>(
                    tag.IntArrayValue.Select(b => new NbtTreeNode(b.ToString())));
                break;
        }
    }

    public NbtTreeNode(string titleOnly)
    {
        Text = titleOnly;
        Children = [];
    }
}