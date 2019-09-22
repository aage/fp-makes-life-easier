using System;

namespace Calender.Api.IoC
{
    public static class IocExt
    {
        public static Action<T2> Apply<T1, T2>
            (this Action<T1, T2> action, T1 t1)
            => t2 => action(t1, t2);

        public static Func<R> Apply<T, R>
            (this Func<T, R> f, T t) => () => f(t);
    }
}
