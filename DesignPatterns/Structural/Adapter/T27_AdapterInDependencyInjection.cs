using Autofac;
using Autofac.Features.Metadata;
using MoreLinq.Extensions;

namespace DesignPatterns.Structural.Adapter;

public class T27_AdapterInDependencyInjection
{
    public static void Demo()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<SaveCommand>().As<ICommand>()
            .WithMetadata("Name", "Save");
        builder.RegisterType<OpenCommand>().As<ICommand>()
            .WithMetadata("Name", "Open");
        // builder.RegisterType<Button>();
        builder.RegisterAdapter<Meta<ICommand>, Button>((cmd) => new Button(cmd.Value, (string)cmd.Metadata["Name"]));
        builder.RegisterType<Editor>();

        using (var container = builder.Build())
        {
            var editor = container.Resolve<Editor>();
            editor.RunAll();
        }
    }
}

public interface ICommand
{
    void Execute();
}

public class SaveCommand : ICommand
{
    public void Execute()
    {
        WriteLine("Saving a file...");
    }
}

public class OpenCommand : ICommand
{
    public void Execute()
    {
        WriteLine("Opening a file...");
    }
}

public class Button(ICommand command, string name)
{
    private readonly ICommand _command = command;
    private readonly string _name = name;

    public void Click()
    {
        WriteLine($"Executing {_name}");
        _command.Execute();
    }
}

public class Editor(IEnumerable<Button> buttons)
{
    private IEnumerable<Button> _buttons = buttons;

    public void RunAll() => _buttons.ForEach(button => button.Click());
}