using System;

namespace Jitsukawa.Extensions.Log
{
    public static class Log
    {
        /// <summary>
        /// Concatena as mensagens da pilha de exceções para fins de depuração.
        /// </summary>
        public static string StackMessages(this Exception e) =>
            string.Concat(
                e.GetType(),
                ": ",
                e.Message,
                string.IsNullOrEmpty(e.StackTrace) ?
                    string.Empty :
                    string.Concat(
                        Environment.NewLine,
                        e.StackTrace),
                e.InnerException == null ?
                    string.Empty :
                    string.Concat(
                        Environment.NewLine,
                        Environment.NewLine,
                        StackMessages(e.InnerException)));
    }
}
