using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyServicesSample.Interfaces
{
    public interface ITextToSpeech
    {
        void Speak(string text);
    }

}