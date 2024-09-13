using System.Text;

namespace DesignPatterns.Creational.Factory;

public class T15_ObjectTrackingAndBulkReplacement
{
    public static async void Demo()
    {
        var trackingThemeFactory = new TrackingThemeFactory();
        foreach (var index in Enumerable.Range(0, 10))
        {
            var theme = trackingThemeFactory.CreateTheme(index % 2 == 0);
        }
        WriteLine(trackingThemeFactory.Info());

        var replaceableThemeFactory = new ReplaceableThemeFactory();
        var magicTheme = replaceableThemeFactory.CreateTheme(true);
        WriteLine(magicTheme.Value.BackgroundColor);
        replaceableThemeFactory.ReplaceThemes(false);
        WriteLine(magicTheme.Value.BackgroundColor);
    }

    public interface ITheme
    {
        string TextColor { get; }
        string BackgroundColor { get; }
    }

    public class LightTheme : ITheme
    {
        public string TextColor => "black";
        public string BackgroundColor => "white";
    }

    public class DarkTheme : ITheme
    {
        public string TextColor => "white";
        public string BackgroundColor => "dark_gray";
    }

    public class TrackingThemeFactory
    {
        private List<WeakReference<ITheme>> _weakReferences = new();

        public ITheme CreateTheme(bool dark)
        {
            ITheme theme = dark ? new DarkTheme() : new LightTheme();
            _weakReferences.Add(new WeakReference<ITheme>(theme));
            return theme;
        }

        public string Info()
        {
            var sb = new StringBuilder();
            foreach (var weakReference in _weakReferences)
            {
                if (weakReference.TryGetTarget(out var theme))
                {
                    var isDark = theme is DarkTheme;
                    sb.Append(isDark ? "Dark" : "Light")
                        .AppendLine(" Theme");
                }
            }

            return sb.ToString();
        }
    }

    public class ReplaceableThemeFactory
    {
        private List<WeakReference<Ref<ITheme>>> _themes = new();

        private Ref<ITheme> CreateThemeImpl(bool dark) => new Ref<ITheme>(dark ? new DarkTheme() : new LightTheme());

        public Ref<ITheme> CreateTheme(bool dark)
        {
            var theme = CreateThemeImpl(dark);
            _themes.Add(new(theme));
            return theme;
        }

        public void ReplaceThemes(bool dark)
        {
            foreach (var weakReference in _themes)
            {
                if (weakReference.TryGetTarget(out var themeRef))
                {
                    themeRef.Value = dark ? new DarkTheme() : new LightTheme();
                }
            }
        }
    }

    public class Ref<T>(T value)
        where T : class
    {
        public T Value = value;
    }
}