using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using fNbt;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using KarolK72.Tools.McNbtUtils.App.Models;

namespace KarolK72.Tools.McNbtUtils.App.ViewModels;

public class MainWindowViewModel : ViewModelBase, IActivatableViewModel
{
    private string _filePath = string.Empty;
    public string FilePath
    {
        get => _filePath;
        set => this.RaiseAndSetIfChanged(ref _filePath, value);
    }

    private NbtFile? _nbtFile = null;
    public NbtFile? NbtFile
    {
        get => _nbtFile;
        set => this.RaiseAndSetIfChanged(ref _nbtFile, value);
    }

    public ObservableCollection<NbtTreeNode> NbtTree { get; } = [];

    private NbtTreeNode? _selectedNbtTreeNode;
    public NbtTreeNode? SelectedNbtTreeNode
    {
        get => _selectedNbtTreeNode;
        set => this.RaiseAndSetIfChanged(ref _selectedNbtTreeNode, value);
    }

    private NbtCompound? _selectedItemForGiveCommand = null;

    public NbtCompound? SelectedItemForGiveCommand
    {
        get => _selectedItemForGiveCommand;
        set => this.RaiseAndSetIfChanged(ref _selectedItemForGiveCommand, value);
    }

    private string _generatedGiveCommand = string.Empty;

    public string GeneratedGiveCommand
    {
        get => _generatedGiveCommand;
        set => this.RaiseAndSetIfChanged(ref _generatedGiveCommand, value);
    }
    
    public ReactiveCommand<string, NbtFile> LoadFileCommand { get; }
    
    public ViewModelActivator Activator { get; } = new();
    
    public MainWindowViewModel()
    {
        LoadFileCommand = ReactiveCommand.Create<string, NbtFile>(filePath => new NbtFile(filePath),
            canExecute: this.WhenAnyValue(v => v.FilePath, fp => !string.IsNullOrWhiteSpace(fp) && File.Exists(fp)));

        
        this.WhenActivated(d =>
        {
            LoadFileCommand.Subscribe(nbtFile => { NbtFile = nbtFile; }).DisposeWith(d);
            this.WhenAnyValue(v => v.NbtFile).Where(n => n is not null).Subscribe(nbtFile =>
            {
                // generate tree
                NbtTree.Clear();
                SelectedNbtTreeNode = null;
                SelectedItemForGiveCommand = null;
                
                var rootTag = nbtFile.RootTag;
                foreach (var tag in rootTag)
                {
                    NbtTree.Add(new NbtTreeNode(tag));
                }
            });
            this.WhenAnyValue(v => v.SelectedNbtTreeNode).Where(n => n is not null).Subscribe(node =>
            {
                SelectedItemForGiveCommand = null;
                GeneratedGiveCommand = string.Empty;
                if (node!.Tag is not NbtCompound nbtCompound)
                {
                    return;
                }

                if (nbtCompound.FirstOrDefault(t => t is NbtString && t is { Name: "id", HasValue: true }) is null)
                {
                    return;
                }
                
                GeneratedGiveCommand = GiveCommandGenerator.GenerateCommand(nbtCompound, "@s");
                
            });
        });
    }
}