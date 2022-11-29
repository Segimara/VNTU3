using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassPDFparser
{
    public record ValidateObj
    {
        public string LessonName { get; set; }
        public bool isCorrect{ get; set; }
    }
}
