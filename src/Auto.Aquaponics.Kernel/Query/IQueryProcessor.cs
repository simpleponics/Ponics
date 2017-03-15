﻿namespace Auto.Aquaponics.Kernel.Query
{
    public interface IQueryProcessor
    {
        TResult Process<TQuery, TResult>(TQuery query)
            where TQuery : Query<TResult>
            where TResult : QueryResult;
    }
}
