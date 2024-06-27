using DevToys.Api;
using System.ComponentModel.Composition;
using static DevToys.Api.GUI;
using TerraSoft.RomanizeGreek;
using System.Text;
using System.Threading;


[Export(typeof(IGuiTool))]
[Name("RomanizeGreek")]                                                          // A unique, internal name of the tool.
[ToolDisplayInformation(
    IconFontName = "FluentSystemIcons",                                          // This font is available by default in DevToys
    IconGlyph = '\uE671',                                                        // An icon that represents a pizza
    GroupName = PredefinedCommonToolGroupNames.Text,                             // The group in which the tool will appear in the side bar.
    ResourceManagerAssemblyIdentifier = nameof(AssemblyIdentifier),              // The Resource Assembly Identifier to use
    ResourceManagerBaseName = "TerraSoft.RomanizeGreek.Resources",               // The full name (including namespace) of the resource file containing our localized texts
    ShortDisplayTitleResourceName = nameof(Resources.ShortDisplayTitle),         // The name of the resource to use for the short display title
    LongDisplayTitleResourceName = nameof(Resources.LongDisplayTitle),
    DescriptionResourceName = nameof(Resources.Description),
    AccessibleNameResourceName = nameof(Resources.AccessibleName))]
internal sealed class RomanizeGreekGui : IGuiTool
{
    private readonly IUIMultiLineTextInput _inputText = MultiLineTextInput();
    private readonly IUIMultiLineTextInput _outputText = MultiLineTextInput();
    private readonly IUISwitch _conversionModeSwitch = Switch("html-conversion-mode-switch");

    private enum GridRows
    {
        Input,
        Output
    }

    private enum GridColumns
    {
        Stretch
    }

    public UIToolView View
            => new(
                isScrollable: true,
                Grid()
                    .RowLargeSpacing()

                    .Rows(
                        (GridRows.Input, new UIGridLength(1, UIGridUnitType.Fraction)),
                        (GridRows.Output, new UIGridLength(1, UIGridUnitType.Fraction)))

                    .Columns(
                        (GridColumns.Stretch, new UIGridLength(1, UIGridUnitType.Fraction)))

                    .Cells(
                        Cell(
                            GridRows.Input,
                            GridColumns.Stretch,

                            _inputText
                                .Title(Resources.InputTitle)
                                .OnTextChanged(OnInputTextChanged)),

                        Cell(
                            GridRows.Output,
                            GridColumns.Stretch,

                            _outputText
                                .Title(Resources.OutputTitle)
                                .ReadOnly()
                                .Extendable())));

    public void OnDataReceived(string dataTypeName, object? parsedData)
    {
        throw new NotImplementedException();
    }

    private void OnInputTextChanged(string text)
    {
        _outputText.Text(Romanize.RomanizeText(text));
    }


    /*
    private void StartConvert(string text)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = new CancellationTokenSource();
        WorkTask = ConvertAsync(text, _settingsProvider.GetSetting(encoder), _cancellationTokenSource.Token);

    }

    private async Task ConvertAsync(string input, Base64Encoding encoderSetting, CancellationToken cancellationToken)
    {
        using (await _semaphore.WaitAsync(cancellationToken))
        {
            await TaskSchedulerAwaiter.SwitchOffMainThreadAsync(cancellationToken);

            string conversionResult;

            switch (_settingsProvider.GetSetting(conversionMode))
            {
                case EncodingConversion.Encode:
                    conversionResult
                        = Base64Helper.FromTextToBase64(
                            input,
                            encoderSetting,
                            _logger,
                            cancellationToken);
                    break;

                case EncodingConversion.Decode:
                    if (!string.IsNullOrEmpty(input) && !Base64Helper.IsBase64DataStrict(input))
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        _outputText.Text(Base64TextEncoderDecoder.InvalidBase64);
                        return;
                    }

                    conversionResult
                        = Base64Helper.FromBase64ToText(
                            input,
                           encoderSetting,
                            _logger,
                            cancellationToken);
                    break;

                default:
                    throw new NotSupportedException();
            }

            cancellationToken.ThrowIfCancellationRequested();
            _outputText.Text(conversionResult);
        }
    */
}