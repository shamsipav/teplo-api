﻿namespace TeploAPI.Exceptions;

public class NoContentException : Exception
{
    public NoContentException(string message) : base(message)
    {
    }
}