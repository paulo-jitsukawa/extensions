using System;
using System.Diagnostics;

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

        /// <summary>
        /// Escreve as mensagens da pilha de exceções no log de eventos do Windows.
        /// </summary>
        /// <param name="type">Tipo de erro.</param>
        /// <param name="maxSize">Limita o tamanho da mensagem a ser gravada. A soma dos tamanhos de todos os campos do log final NÃO pode superar 64kb (65536b)!</param>
        public static void WriteWindowsEventLog(this Exception e, string source, string log, EventLogEntryType type, int maxSize = 65000)
        {
            if (!string.IsNullOrEmpty(source))
            {
                if (!EventLog.SourceExists(source))
                    EventLog.CreateEventSource(new EventSourceCreationData(source, string.IsNullOrEmpty(log) ? source : log));

                var evt = new EventLog(log, ".", source);
                var msg = e.StackMessages();
                evt.WriteEntry(msg.Length > maxSize ? msg.Substring(0, maxSize) : msg, type);
            }
        }
    }
}
