using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Responses
{
    public class GuessResponse
    {
        public GuessingResult GuessingResult { get; set; }
    }

    public enum GuessingResult
    {
        Less,
        More,
        Guessed
    }
}
