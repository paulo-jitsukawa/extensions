using System.Diagnostics;

namespace Jitsukawa.Extensions.Shell
{
    /// <summary>
    /// Interage com o terminal do sistema.
    /// </summary>
    public static class Shell
    {
        /// <summary>
        /// Executa um comando e captura sua saída.
        /// </summary>
        /// <param name="command">Comando que deve ser executado.</param>
        /// <param name="directory">Diretório em que deve executar.</param>
        /// <param name="parameters">Parâmetros do comando.</param>
        /// <returns>Código de retorno do comando e sua saída para o sistema.</returns>
        public static (int Code, string StdOut) Run(this string command, string directory, string parameters)
        {
            int retorno = -1;
            string saida;

            var argumentos = new ProcessStartInfo(command, parameters);
            argumentos.CreateNoWindow = true;
            argumentos.UseShellExecute = false;
            argumentos.RedirectStandardOutput = true;

            if (!string.IsNullOrEmpty(directory))
                argumentos.WorkingDirectory = directory;

            using (Process processo = new Process())
            {
                processo.StartInfo = argumentos;
                processo.Start();
                saida = processo.StandardOutput.ReadToEnd();
                processo.WaitForExit();
                retorno = processo.ExitCode;
            }

            return (retorno, saida);
        }
    }
}
