using DryIoc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.Test;

public class TypeRegistrationTest
{
    [Fact]
    public void RegisterDefault()
    {
        var types = new Type[] { };
        var container = new Container();
        var typeRegister = new TypeRegistrator(container, types);

        //typeRegister.Include<>();

        //container.Resolve();
    }
}

