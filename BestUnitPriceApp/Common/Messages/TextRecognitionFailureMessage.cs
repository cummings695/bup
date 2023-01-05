using CommunityToolkit.Mvvm.Messaging.Messages;
using LanguageExt.Common;

namespace BestUnitPriceApp.Common.Messages;

internal class TextRecognitionFailureMessage : ValueChangedMessage<Result<TextRecognitionFailure>>
{
    public TextRecognitionFailureMessage(Result<TextRecognitionFailure> value) : base(value)
    {
    }
}