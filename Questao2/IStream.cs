using System;

namespace ValidarStream
{
    interface IStream
    {
        char getNext();
        Boolean hasNext();
    }
}
