using System;
using CommunityToolkit.Mvvm.Messaging.Messages;
using LanguageExt.Common;

namespace BestUnitPriceApp.Common.Messages;

internal class TextRecognitionCompleteMessage : ValueChangedMessage<Result<TextScanResult[]>>
{
    public TextRecognitionCompleteMessage(Result<TextScanResult[]> value) : base(value) { }
}